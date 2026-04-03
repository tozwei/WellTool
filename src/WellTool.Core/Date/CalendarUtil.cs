using System;
using System.Globalization;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 日历工具类
    /// </summary>
    public static class CalendarUtil
    {
        /// <summary>
        /// 获取指定日期所在的周的第一天（周一）
        /// </summary>
        public static DateTime GetWeekFirstDay(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff);
        }

        /// <summary>
        /// 获取指定日期所在的周的最后一天（周日）
        /// </summary>
        public static DateTime GetWeekLastDay(DateTime date)
        {
            return GetWeekFirstDay(date).AddDays(6);
        }

        /// <summary>
        /// 获取指定日期所在的月的第一天
        /// </summary>
        public static DateTime GetMonthFirstDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 获取指定日期所在的月的最后一天
        /// </summary>
        public static DateTime GetMonthLastDay(DateTime date)
        {
            return GetMonthFirstDay(date).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 获取指定日期所在的年的第一天
        /// </summary>
        public static DateTime GetYearFirstDay(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        /// <summary>
        /// 获取指定日期所在的年的最后一天
        /// </summary>
        public static DateTime GetYearLastDay(DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }

        /// <summary>
        /// 获取两个日期之间的天数
        /// </summary>
        public static int GetDaysBetween(DateTime start, DateTime end)
        {
            return (int)(end - start).TotalDays;
        }

        /// <summary>
        /// 获取两个日期之间的工作日天数（不含周末）
        /// </summary>
        public static int GetWorkDays(DateTime start, DateTime end)
        {
            int days = 0;
            var current = start.Date;
            var endDate = end.Date;

            while (current <= endDate)
            {
                if (current.DayOfWeek != DayOfWeek.Saturday && current.DayOfWeek != DayOfWeek.Sunday)
                {
                    days++;
                }
                current = current.AddDays(1);
            }

            return days;
        }

        /// <summary>
        /// 判断是否为闰年
        /// </summary>
        public static bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        /// <summary>
        /// 获取某月的天数
        /// </summary>
        public static int GetDaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        /// <summary>
        /// 获取日期是当年的第几周
        /// </summary>
        public static int GetWeekOfYear(DateTime date)
        {
            var dfi = DateTimeFormatInfo.CurrentInfo;
            return dfi.Calendar.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        /// <summary>
        /// 获取季度
        /// </summary>
        public static int GetQuarter(DateTime date)
        {
            return (date.Month - 1) / 3 + 1;
        }

        /// <summary>
        /// 获取季度的第一天
        /// </summary>
        public static DateTime GetQuarterFirstDay(DateTime date)
        {
            int quarter = GetQuarter(date);
            return new DateTime(date.Year, (quarter - 1) * 3 + 1, 1);
        }

        /// <summary>
        /// 获取季度的最后一天
        /// </summary>
        public static DateTime GetQuarterLastDay(DateTime date)
        {
            return GetQuarterFirstDay(date).AddMonths(3).AddDays(-1);
        }

        /// <summary>
        /// 判断两个日期是否是同一天
        /// </summary>
        public static bool IsSameDay(DateTime a, DateTime b)
        {
            return a.Year == b.Year && a.Month == b.Month && a.Day == b.Day;
        }

        /// <summary>
        /// 判断两个日期是否是同一周
        /// </summary>
        public static bool IsSameWeek(DateTime a, DateTime b)
        {
            return IsSameDay(GetWeekFirstDay(a), GetWeekFirstDay(b));
        }

        /// <summary>
        /// 判断两个日期是否是同一月
        /// </summary>
        public static bool IsSameMonth(DateTime a, DateTime b)
        {
            return a.Year == b.Year && a.Month == b.Month;
        }

        /// <summary>
        /// 判断两个日期是否是同一年
        /// </summary>
        public static bool IsSameYear(DateTime a, DateTime b)
        {
            return a.Year == b.Year;
        }
    }
}
