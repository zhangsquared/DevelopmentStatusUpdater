using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public interface IEntryProcessor
	{
		bool IsAdam(IEntry entry);

		bool IsCurrentUser(IEntry entry);

		bool IsHolidayEntry(IEntry entry);

		IEntryTemplate GenateHolidayEntry(IEntry entry);
	}
}
