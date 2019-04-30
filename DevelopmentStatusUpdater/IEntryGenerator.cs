using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public interface IEntryGenerator
	{
		IEntry[] NewWorkEntries(string date, double hours);

		IEntry NewHolidayEntry(string date, IEntryTemplate holidayEntry);
	}
}
