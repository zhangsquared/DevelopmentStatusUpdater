using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public class LogUpdater
	{
		private readonly string pathSource;
		private readonly string pathTarget;

		private const string dateMarker = "=====";
		private const string titleMarker = "|**";

		public LogUpdater(string pathSource, string pathTarget)
		{
			this.pathSource = pathSource;
			this.pathTarget = pathTarget;
		}

		public void Log(IEntryProcessor processor, IEntryGenerator generator)
		{
			StreamReader reader = new StreamReader(pathSource);
			StreamWriter writer = new StreamWriter(pathTarget);

			// init global variables
			bool hasUser = true;
			double totalWorkHours = 40;
			string line;
			string date = string.Empty;
			List<IEntryTemplate> holidayEntryTemplates = new List<IEntryTemplate>();
			bool isStart = true;

			// read and write
			while ((line = reader.ReadLine()) != null)
			{
				if (string.IsNullOrWhiteSpace(line)) continue;

				if (line.StartsWith(dateMarker))
				{
					if (!hasUser)
					{
						foreach(IEntryTemplate holidayEntryTemplate in holidayEntryTemplates)
						{
							IEntry newHolidayEntry = generator.NewHolidayEntry(date, holidayEntryTemplate);
							writer.WriteLine(newHolidayEntry.ToString());
							totalWorkHours -= holidayEntryTemplate.Hours;
						}
						foreach(IEntry workEntry in generator.NewWorkEntries(date, totalWorkHours))
						{
							writer.WriteLine(workEntry.ToString());
						}
					}
					if(!isStart) writer.WriteLine(string.Empty);
					isStart = false;

					// reset
					date = GetDateStr(line);
					hasUser = false;
					totalWorkHours = 40;
					holidayEntryTemplates.Clear();
				}
				else if(line.StartsWith(titleMarker))
				{
					// do nothing
				}
				else
				{
					IEntry entry = new Entry();
					bool format = entry.FromLine(line);
					if(!format)
					{
						writer.WriteLine(line);
						continue;
					}
					if(processor.IsAdam(entry) && processor.IsHolidayEntry(entry))
					{
						holidayEntryTemplates.Add(processor.GenateHolidayEntry(entry));
					}
					hasUser |= processor.IsCurrentUser(entry);
				}
				writer.WriteLine(line);
			}
		}

		private string GetDateStr(string line)
		{
			return line.Replace(dateMarker, "").Trim();
		}

	}
}
