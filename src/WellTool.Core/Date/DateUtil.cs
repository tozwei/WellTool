using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WellTool.Core.Date
{
    public class DateUtil
    {
        // 当前时间
        public static DateTime Now()
        {
            return DateTime.Now;
        }

        // 当前时间字符串，格式：yyyy-MM-dd HH:mm:ss
        public static string NowString()
        {
            return FormatDateTime(Now());
        }

        // 当前日期字符串，格式：yyyy-MM-dd
        public static string Today()
        {
            return FormatDate(Now());
        }

        // 解析日期字符串
        public static DateTime Parse(string dateStr)
        {
            return DateTime.Parse(dateStr);
        }

        // 解析指定格式的日期字符串
        public static DateTime Parse(string dateStr, string format)
        {
            return DateTime.ParseExact(dateStr, format, CultureInfo.InvariantCulture);
        }

        // 格式化日期为指定格式
        public static string Format(DateTime date, string format)
        {
            return date.ToString(format, CultureInfo.InvariantCulture);
        }

        // 格式化日期为标准日期时间格式：yyyy-MM-dd HH:mm:ss
        public static string FormatDateTime(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        // 格式化日期为标准日期格式：yyyy-MM-dd
        public static string FormatDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        // 格式化日期为标准时间格式：HH:mm:ss
        public static string FormatTime(DateTime date)
        {
            return date.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        }

        // 获取一天的开始时间
        public static DateTime BeginOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        // 获取一天的结束时间
        public static DateTime EndOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        // 偏移天
        public static DateTime OffsetDay(DateTime date, int offset)
        {
            return date.AddDays(offset);
        }

        // 偏移小时
        public static DateTime OffsetHour(DateTime date, int offset)
        {
            return date.AddHours(offset);
        }

        // 偏移月
        public static DateTime OffsetMonth(DateTime date, int offset)
        {
            return date.AddMonths(offset);
        }

        // 偏移年
        public static DateTime OffsetYear(DateTime date, int offset)
        {
            return date.AddYears(offset);
        }

        // 计算两个日期之间的时间差
        public static long Between(DateTime beginDate, DateTime endDate, DateUnit unit)
        {
            var span = endDate - beginDate;
            switch (unit)
            {
                case DateUnit.Millisecond:
                    return (long)span.TotalMilliseconds;
                case DateUnit.Second:
                    return (long)span.TotalSeconds;
                case DateUnit.Minute:
                    return (long)span.TotalMinutes;
                case DateUnit.Hour:
                    return (long)span.TotalHours;
                case DateUnit.Day:
                    return (long)span.TotalDays;
                case DateUnit.Week:
                    return (long)(span.TotalDays / 7);
                case DateUnit.Month:
                    return (endDate.Year - beginDate.Year) * 12 + (endDate.Month - beginDate.Month);
                case DateUnit.Year:
                    return endDate.Year - beginDate.Year;
                default:
                    return 0;
            }
        }

        // 计算两个日期之间的天数差
        public static long BetweenDay(DateTime beginDate, DateTime endDate, bool isReset)
        {
            if (isReset)
            {
                beginDate = BeginOfDay(beginDate);
                endDate = BeginOfDay(endDate);
            }
            return Between(beginDate, endDate, DateUnit.Day);
        }

        // 计算两个日期之间的月数差
        public static long BetweenMonth(DateTime beginDate, DateTime endDate, bool isReset)
        {
            var months = (endDate.Year - beginDate.Year) * 12 + (endDate.Month - beginDate.Month);
            if (!isReset && endDate.Day < beginDate.Day)
            {
                months--;
            }
            return months;
        }

        // 计算年龄
        public static int Age(DateTime birthday, DateTime dateToCompare)
        {
            var age = dateToCompare.Year - birthday.Year;
            if (dateToCompare.Month < birthday.Month || (dateToCompare.Month == birthday.Month && dateToCompare.Day < birthday.Day))
            {
                age--;
            }
            return age;
        }

        // 计算当前年龄
        public static int AgeOfNow(DateTime birthday)
        {
            return Age(birthday, Now());
        }

        // 计算当前年龄（从字符串解析生日）
        public static int AgeOfNow(string birthDay)
        {
            var birthday = Parse(birthDay);
            return AgeOfNow(birthday);
        }

        // 判断是否为闰年
        public static bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }

        // 获取指定月份的最后一天
        public static int GetLastDayOfMonth(DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }

        // 判断是否为月份的最后一天
        public static bool IsLastDayOfMonth(DateTime date)
        {
            return date.Day == GetLastDayOfMonth(date);
        }

        // 判断是否为周末
        public static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        // 获取一年中的第几天
        public static int DayOfYear(DateTime date)
        {
            return date.DayOfYear;
        }

        // 获取一年的总天数
        public static int LengthOfYear(int year)
        {
            return IsLeapYear(year) ? 366 : 365;
        }

        // 获取一年中的第几周
        public static int WeekOfYear(DateTime date)
        {
            var cal = CultureInfo.CurrentCulture.Calendar;
            var weekRule = CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule;
            var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            return cal.GetWeekOfYear(date, weekRule, firstDayOfWeek);
        }

        // 比较两个日期
        public static int Compare(DateTime date1, DateTime date2)
        {
            return date1.CompareTo(date2);
        }

        // 获取年份和季度
        public static string YearAndQuarter(DateTime date)
        {
            var quarter = (date.Month - 1) / 3 + 1;
            return $"{date.Year}{quarter}";
        }

        // 将时间字符串转换为秒数
        public static int TimeToSecond(string timeStr)
        {
            var parts = timeStr.Split(':');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid time format");
            }
            var hours = int.Parse(parts[0]);
            var minutes = int.Parse(parts[1]);
            var seconds = int.Parse(parts[2]);
            return hours * 3600 + minutes * 60 + seconds;
        }

        // 将秒数转换为时间字符串
        public static string SecondToTime(int seconds)
        {
            var hours = seconds / 3600;
            var minutes = (seconds % 3600) / 60;
            var secs = seconds % 60;
            return $"{hours:00}:{minutes:00}:{secs:00}";
        }

        // 解析日期字符串（只解析日期部分）
        public static DateTime ParseDate(string dateStr)
        {
            return Parse(dateStr, "yyyy-MM-dd");
        }

        // 解析时间字符串（只解析时间部分）
        public static DateTime ParseTime(string timeStr)
        {
            return Parse(timeStr, "HH:mm:ss");
        }
    }
}