using NAudio.FileFormats.Wav;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SPD.SX.SampleManager.Core
{
	public static class SampleConverter
	{
		public static void ConvertTo16Bit(FileInfo sourceFile, FileInfo destFile)
		{
			int outRate = 44100;
			using (var reader = new AudioFileReader(sourceFile.FullName))
			{
				var resampler = new WdlResamplingSampleProvider(reader, outRate);				
				WaveFileWriter.CreateWaveFile16(destFile.FullName, resampler);
			}
		}
	}
}
