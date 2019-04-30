using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public class EntryProcessor : IEntryProcessor
	{
		private readonly string username;

		public EntryProcessor(string username)
		{
			this.username = username;
		}

		public bool IsAdam(IEntry entry)
		{
			return entry.User.Equals("adam", StringComparison.InvariantCultureIgnoreCase);
		}

		public bool IsCurrentUser(IEntry entry)
			=> username.Equals(entry.User, StringComparison.InvariantCultureIgnoreCase);

		public bool IsHolidayEntry(IEntry entry)
		{
			return entry.Application.Equals("admin", StringComparison.InvariantCultureIgnoreCase);
		}

		public IEntryTemplate GenateHolidayEntry(IEntry entry)
		{
			HolidayEntryTemplate he = new HolidayEntryTemplate(entry.Description, entry.Hours);
			return he;
		}

	}
}
