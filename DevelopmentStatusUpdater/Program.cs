using System;
using System.IO;

namespace DevelopmentStatusUpdater
{
	class Program
	{
		static void Main(string[] args)
		{
			string pathOrig = @"C:\Users\zzhang\Downloads\Misc\orig.txt";
			string pathNew = @"C:\Users\zzhang\Downloads\Misc\new.txt";
			string currentUser = "ZZ";
			string[] appNames = new[] { "VeriFi", "MIAC Security Tool" };

			LogUpdater logger = new LogUpdater(pathOrig, pathNew);
			IEntryProcessor processor = new EntryProcessor(currentUser);
			IEntryGenerator generator = new EntryGenerator(currentUser, appNames);
			logger.Log(processor, generator);
		}
	}
}
