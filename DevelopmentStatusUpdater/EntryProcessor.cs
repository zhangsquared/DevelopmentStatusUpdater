using Nager.Date;
using System;
using System.Collections.Generic;
using System.Globalization;
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
			bool adamHoliday = entry.Application.Equals("admin", StringComparison.InvariantCultureIgnoreCase);
			if (adamHoliday) return true;

			DateTime date = DateTime.Parse(entry.Date, new CultureInfo("en-US"));
			return DateSystem.IsPublicHoliday(date, CountryCode.US) || DateSystem.IsWeekend(date, CountryCode.US);
		}

		public IEntryTemplate GenateHolidayEntry(IEntry entry)
		{
			HolidayEntryTemplate he = new HolidayEntryTemplate(entry.Description, entry.Hours);
			return he;
		}

	}
}
