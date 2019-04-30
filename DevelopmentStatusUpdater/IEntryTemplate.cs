using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public interface IEntryTemplate
	{
		string Application();
		string Description();
		bool? IsDevelopment();
		double Hours { get; set; }
		EntryTemplateType Type { get; }
	}

	public enum EntryTemplateType
	{
		Development,
		Bugfix,
		ClientSupport,
		Holiday
	}
}
