using System;
using System.Globalization;

namespace WellTool.Core.Date
{
    public class DateUtil
    {
        public static DateTime Now()
        {
            return DateTime.Now;
        }

        public static DateTime Parse(string dateStr)
        {
            return DateTime.Parse(dateStr);
        }

        public static DateTime Parse(string dateStr, string format)
        {
            return DateTime.ParseExact(dateStr, format, CultureInfo.InvariantCulture);
        }

        public static bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }
    }
}