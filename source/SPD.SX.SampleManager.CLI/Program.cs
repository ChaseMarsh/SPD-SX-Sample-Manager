using System;
using System.Linq;
using SPD.SX.SampleManager.Core;

namespace SPD.SX.SampleManager.CLI
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Beginning Search...");
			var sampleMgr = new SampleMgr(args[0]);
			Console.WriteLine(sampleMgr.Files.Count());

			sampleMgr.ExportAll(args[1], (msg)=>Console.Write(msg));
		}
	}
}
