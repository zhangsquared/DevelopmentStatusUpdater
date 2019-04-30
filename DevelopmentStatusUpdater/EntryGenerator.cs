using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public class EntryGenerator : IEntryGenerator, IRandomGenerator
	{
		private readonly string username;
		private readonly string[] applicationNames;

		private static readonly Random random = new Random();

		public EntryGenerator(string username, string[] appNames)
		{
			this.username = username;
			applicationNames = appNames;
		}

		public IEntry[] NewWorkEntries(string date, double hours)
		{
			int appNumber = RandomGenerateAppNumber(applicationNames.Length);
			IEntry[] entries = new IEntry[appNumber];

			List<string> usedNames = new List<string>();
			double remaingHours = hours;

			for(int i = 0; i < appNumber; i++)
			{
				string[] remainingNames = applicationNames.Except(usedNames).ToArray();
				string appName = appNumber == 1 ? applicationNames[0] : RandomGenerateApplicationName(remainingNames);
				usedNames.Add(appName);

				IEntryTemplate entryTemplate = RandomGenerateEntry(appName);
				entries[i] = new Entry(username, date, entryTemplate)
				{
					IsComplete = RandomGenerateComplete(entryTemplate.Type)
				};
				if (i == appNumber - 1) entries[i].Hours = remaingHours;
				else
				{
					entries[i].Hours = random.Next((int)(remaingHours/2), (int)remaingHours);
					remaingHours -= entries[i].Hours;
				}
			}
			return entries;
		}

		public IEntry NewHolidayEntry(string date, IEntryTemplate holidayEntry)
		{
			Entry entry = new Entry(username, date, holidayEntry)
			{
				IsComplete = RandomGenerateComplete(holidayEntry.Type)
			};
			return entry;
		}

		private IEntryTemplate RandomGenerateEntry(string appName)
		{
			double r = random.NextDouble();
			if (r < 0.7) return new DevelopmentEntryTemplate(appName);
			if (r < 0.9) return new BugFixEntryTemplate(appName);
			return new ClientSupportEntryTemplate(appName);
		}

		public int RandomGenerateAppNumber(int totalAppNumber)
		{
			double ratio = 0.85;
			double r = random.NextDouble();
			for (int i = 0; i < totalAppNumber - 1; i++)
			{
				if (r < ratio) return i + 1;
			}
			return totalAppNumber;
		}

		public string RandomGenerateApplicationName(string[] appNames)
		{
			double ratio = 0.85;
			double r = random.NextDouble();
			for(int i = 0; i < appNames.Length - 1; i++)
			{
				if (r < ratio) return appNames[i];
			}
			return appNames[appNames.Length - 1];
		}

		private bool RandomGenerateComplete(EntryTemplateType type)
		{
			double r = random.NextDouble();
			switch (type)
			{
				case EntryTemplateType.Bugfix:
					return r < 0.8;
				case EntryTemplateType.ClientSupport:
					return r < 0.95;
				case EntryTemplateType.Development:
					return r < 0.5;
				default:
					return true;
			}

		}
	}
}
