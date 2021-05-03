using System;
using System.IO;
using System.Linq;

namespace DevelopmentStatusUpdater
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = @"C:\Users\zzhang\Downloads\";
			string pathOrig = path + @"orig.txt";
			string pathNew = path + @"new.txt";

			string currentUser = "ZZ";
			string[] appNames = new[] { "VeriFi", "AiCR", "MIAC Security Tool" };
			double[] appRatios = new[] { 0.55, 0.44, 0.01};

			if (appNames.Length != appRatios.Length) Console.WriteLine("ERROR: app count and apperance ratio count doesn't match");
			if (Math.Abs(appRatios.Sum() - 1) > 0.00001) Console.WriteLine("ERROR: apperance ratio sum must = 1");

			LogUpdater logger = new LogUpdater(pathOrig, pathNew);
			IEntryProcessor processor = new EntryProcessor(currentUser);
			IEntryGenerator generator = new EntryGenerator(currentUser, appNames, appRatios);
			logger.Log(processor, generator);
		}
	}
}
