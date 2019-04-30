using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public class Entry : IEntry
	{
		private const char SPLITER = '|';
		private const string MAINTAIN = "M";
		private const string DEVELOPMENT = "D";
		private const string NA = "N/A";
		private const char COMPLETE = 'Y';
		private const char NOT_COMPLETE = 'N';

		public string Date { get; private set; }
		public string User { get; private set; }
		public string Application { get; private set; }
		public string Description { get; private set; }
		public double Hours { get; set; }
		public bool? IsDevelopment { get; private set; }
		public bool IsComplete { get; set; }

		public Entry()
		{
		}

		public Entry(string username, string dateTime, IEntryTemplate template)
		{
			Date = dateTime;
			User = username;
			Application = template.Application();
			Description = template.Description();
			IsDevelopment = template.IsDevelopment();
			if (template.Type == EntryTemplateType.Holiday)
			{
				Hours = template.Hours;
				IsComplete = true;
			}
		}

		public bool FromLine(string line)
		{
			string[] parts = line.Split(SPLITER, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length < 5) return false;
			Date = parts[0];
			User = parts[1];
			Application = parts[2];
			Description = parts[3];
			bool format = double.TryParse(parts[4], out double hr);
			if (format) Hours = hr;
			return format;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(SPLITER);

			sb.Append(Date);
			sb.Append(SPLITER);

			sb.Append(User);
			sb.Append(SPLITER);

			sb.Append(Application);
			sb.Append(SPLITER);

			sb.Append(Description);
			sb.Append(SPLITER);

			sb.Append(Hours.ToString());
			sb.Append(SPLITER);

			sb.Append(IsDevelopment.HasValue ? (IsDevelopment.Value ? DEVELOPMENT : MAINTAIN) : NA);
			sb.Append(SPLITER);

			sb.Append(IsComplete ? COMPLETE : NOT_COMPLETE);
			sb.Append(SPLITER);

			return sb.ToString();
		}

	}
}
