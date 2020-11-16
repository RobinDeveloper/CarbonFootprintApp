using System;

namespace CarbonFootprint.utilities
{
    public class DateCollected
    {
        public DateTime LastCheckedDay;
        public DateTime LastCheckedWeek;
        public DateTime LastCheckedMonth;
        public DateTime LastCheckedYear;

        public DateCollected()
        {
            LastCheckedDay = DateTime.Today;
            LastCheckedMonth = DateTime.Today;
            LastCheckedWeek = DateTime.Today;
            LastCheckedYear = DateTime.Today;
        }
    }
}