using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public class EntryGenerator : IEntryGenerator, IRandomGenerator
	{
		#region const
		// which app
		public const double APP_NUMBER_RATIO = 0.75;

		// EntryTemplateType
		public const double DEV_RATIO = 0.7;
		public const double BUG_RATIO = 0.2;
		// support ration = 1 - 0.7 - 0.2 = 0.1

		// IsComplete
		public const double DEV_COMPLETE_RATIO = 0.5;
		public const double BUG_COMPLETE_RATIO = 0.8;
		public const double SUPPORT_COMPLETE_RATIO = 0.95;
		#endregion

		private readonly string username;

		public readonly Dictionary<string, double> ApplicationMap; // key: app name; value: appearance ratio

		private static readonly Random random = new Random();

		public EntryGenerator(string username, string[] appNames, double[] appRatios)
		{
			this.username = username;
			ApplicationMap = new Dictionary<string, double>();
			for (int i = 0; i < appNames.Length; i++)
			{
				ApplicationMap[appNames[i]] = appRatios[i];
			}
		}

		public IEntry[] NewWorkEntries(string date, double hours)
		{
			int appNumber = RandomGenerateAppNumber(ApplicationMap.Keys.Count);
			IEntry[] entries = new IEntry[appNumber];

			List<string> usedNames = new List<string>();
			double remaingHours = hours;

			for (int i = 0; i < appNumber; i++)
			{
				string[] remainingNames = ApplicationMap.Keys.Except(usedNames).ToArray();
				string appName = RandomGenerateApplicationName(remainingNames, ApplicationMap);
				usedNames.Add(appName);

				IEntryTemplate entryTemplate = RandomGenerateEntry(appName);
				entries[i] = new Entry(username, date, entryTemplate)
				{
					IsComplete = RandomGenerateComplete(entryTemplate.Type)
				};
				if (i == appNumber - 1) entries[i].Hours = remaingHours;
				else
				{
					entries[i].Hours = random.Next((int)(remaingHours / 2), (int)Math.Ceiling(remaingHours));
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

		#region IRandomGenerator
		public IEntryTemplate RandomGenerateEntry(string appName)
		{
			double r = random.NextDouble();
			if (r < DEV_RATIO) return new DevelopmentEntryTemplate(appName);
			if (r < DEV_RATIO + BUG_RATIO) return new BugFixEntryTemplate(appName);
			return new ClientSupportEntryTemplate(appName);
		}

		public int RandomGenerateAppNumber(int totalAppNumber)
		{
			double d = 1 / APP_NUMBER_RATIO;
			double sum = 0;
			for (int i = 0; i < totalAppNumber; i++) sum += Math.Pow(d, i);
			double[] dist = new double[totalAppNumber];
			for (int i = 0; i < totalAppNumber; i++) dist[i] = Math.Pow(d, totalAppNumber - 1 - i) / sum;

			double r = random.NextDouble();
			double accumulated = 0;
			for (int i = 0; i < totalAppNumber; i++)
			{
				if (r < accumulated + dist[i]) return i + 1;
				else accumulated += dist[i];
			}
			return totalAppNumber;
		}

		public string RandomGenerateApplicationName(string[] appNames, IDictionary<string, double> appearanceRatio)
		{
			Dictionary<string, double> adjustedMap = new Dictionary<string, double>();
			foreach (string appName in appNames)
			{
				adjustedMap[appName] = appearanceRatio[appName];
			}
			double sum = adjustedMap.Values.Sum();
			foreach (string appName in appNames)
			{
				adjustedMap[appName] = adjustedMap[appName] / sum;
			}

			double r = random.NextDouble();
			double accumulated = 0;
			for (int i = 0; i < appNames.Length - 1; i++)
			{
				if (r < accumulated + adjustedMap[appNames[i]]) return appNames[i];
				else accumulated += adjustedMap[appNames[i]];
			}
			return appNames[appNames.Length - 1];
		}

		public bool RandomGenerateComplete(EntryTemplateType type)
		{
			double r = random.NextDouble();
			switch (type)
			{
				case EntryTemplateType.Bugfix:
					return r < BUG_COMPLETE_RATIO;
				case EntryTemplateType.ClientSupport:
					return r < SUPPORT_COMPLETE_RATIO;
				case EntryTemplateType.Development:
					return r < DEV_COMPLETE_RATIO;
				default:
					return true;
			}
		}
		#endregion
	}
}
