using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SPD.SX.SampleManager.Core
{
	public class SampleMgr
	{
		/// <summary>
		/// Key (Category) and Value (Search filters separated by comma). 
		/// </summary>
		public Dictionary<string, string> CategoryFilters { get; set; } = new Dictionary<string, string>();

		public IEnumerable<FileInfo> Files { get; private set; }

		public SampleMgr(string folderPath, bool includeSubfolders = true)
		{
			Files = GetSampleFiles(folderPath, includeSubfolders).Select((filePath) => new FileInfo(filePath));
			CategoryFilters.Add("Snare", "Snare");
			CategoryFilters.Add("Tamb", "Tamb");
			CategoryFilters.Add("Snap", "Snap");
			CategoryFilters.Add("Kick", "Kick");
			CategoryFilters.Add("Tom", "Tom");
			CategoryFilters.Add("Cym", "Cym"); 
			CategoryFilters.Add("Clap", "Clap");
			CategoryFilters.Add("808FX", "808");
			CategoryFilters.Add("FX", "FX");
			CategoryFilters.Add("Perc", "Perc");
			CategoryFilters.Add("HH", "HiHat");


		}

		public static IEnumerable<string> GetSampleFiles(string path, bool includeSubfolders)
		{
			return Directory.GetFiles(path, "*.wav", includeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
		}	
		
		public void ExportAll(string outputPath, Action<string> progressCallback = null)
		{
			var categoryCount = new Dictionary<string, int>();
			foreach (var category in CategoryFilters.Keys)
				categoryCount.Add(category, 0);

			foreach (var file in Files) // TODO: Remove Take
			{
				var exportName = file.Name;
				var matches = GetCategoryMatches(file);
				if (matches.Count() == 1)
				{
					categoryCount[matches.First()]++;
					exportName = $"{matches.First()}{categoryCount[matches.First()]}.{file.Extension}";
				}

				progressCallback?.Invoke($"Converting {exportName}...");
				SampleConverter.ConvertTo16Bit(file, new System.IO.FileInfo($"{outputPath}\\{exportName}"));
				progressCallback?.Invoke("Done\n");
			}
			
		}

		public IEnumerable<string> GetCategoryMatches(FileInfo file) => 
			CategoryFilters.Keys.Where((category) => file.Name.ToUpper().Contains(category.ToUpper()));
		
	}
}
