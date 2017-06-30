using System;

namespace Economy.Utils.Helpers
{
    public class DateTimeHelper
    {
        public static DateTime GetLastDateInMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
        }

        public static DateTime GetFirstDateInMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
    }
}