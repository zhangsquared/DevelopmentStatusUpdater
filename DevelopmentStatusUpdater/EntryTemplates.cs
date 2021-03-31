using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public class DevelopmentEntryTemplate : IEntryTemplate
	{
		private readonly string app;

		public DevelopmentEntryTemplate(string application)
		{
			app = application;
		}

		public string Application() => app;

		public string Description() => app + " feature development";

		public bool? IsDevelopment() => true;

		public double Hours { get; set; }

		public EntryTemplateType Type => EntryTemplateType.Development;
	}

	public class BugFixEntryTemplate : IEntryTemplate
	{
		private readonly string app;

		public BugFixEntryTemplate(string application)
		{
			app = application;
		}

		public string Application() => app;

		public string Description() => "bug fixes and maintenance";

		public bool? IsDevelopment() => true;

		public double Hours { get; set; }

		public EntryTemplateType Type => EntryTemplateType.Bugfix;
	}

	public class ClientSupportEntryTemplate : IEntryTemplate
	{
		private readonly string app;

		public ClientSupportEntryTemplate(string application)
		{
			app = application;
		}

		public string Application() => app;

		public string Description() => "client support; production support";

		public bool? IsDevelopment() => false;

		public double Hours { get; set; }

		public EntryTemplateType Type => EntryTemplateType.ClientSupport;
	}

	public class HolidayEntryTemplate : IEntryTemplate
	{
		private readonly string holiday;

		public HolidayEntryTemplate(string holidayDesc, double hours)
		{
			holiday = holidayDesc;
			Hours = hours;
		}

		public string Application() => "Admin";

		public string Description() => holiday;

		public bool? IsDevelopment() => null;

		public double Hours { get; set; }

		public EntryTemplateType Type => EntryTemplateType.Holiday;
	}

}
