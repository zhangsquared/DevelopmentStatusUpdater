using DevelopmentStatusUpdater;
using Nager.Date;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;


namespace RandomGeneratorUnitTest
{
    public class NagerDateTest
    {
        [Fact]
        public void TestNagerDateLibrary()
        {
            var xmas = new DateTime(2020, 12, 25);
            var weekday = new DateTime(2020, 12, 11);
            var weekend = new DateTime(2020, 12, 12);

            Assert.True(DateSystem.IsPublicHoliday(xmas, CountryCode.US));
            Assert.False(DateSystem.IsPublicHoliday(weekday, CountryCode.US));
            Assert.True(DateSystem.IsWeekend(weekend, CountryCode.US));

            Assert.False(DateSystem.IsPublicHoliday(weekend, CountryCode.US)); // a public holiday is NOT a weekend
            Assert.False(DateSystem.IsWeekend(xmas, CountryCode.US)); // a weekend is NOT a public holiday
        }

        [Fact]
        public void DateStringFormat()
        {
            string str = "3/30/2021"; 
            CultureInfo culture = new CultureInfo("en-US");
            DateTime dateTime = DateTime.Parse(str, culture);
            Assert.True(dateTime.Month == 3);
            Assert.True(dateTime.Year == 2021);
            Assert.True(dateTime.Day == 30);
        }

    }
}
