using System;
using System.Collections.Generic;
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

        // 获取当前时间
        public static DateTime Date()
        {
            return DateTime.Now;
        }

        // 根据DateTime获取时间
        public static DateTime Date(DateTime date)
        {
            return date;
        }

        // 根据时间戳获取时间
        public static DateTime Date(long timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timestamp).ToLocalTime();
        }

        // 根据年、月、日获取时间
        public static DateTime Date(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }

        // 根据年、月、日、时、分获取时间
        public static DateTime Date(int year, int month, int day, int hour, int minute)
        {
            return new DateTime(year, month, day, hour, minute, 0);
        }

        // 根据年、月、日、时、分、秒获取时间
        public static DateTime Date(int year, int month, int day, int hour, int minute, int second)
        {
            return new DateTime(year, month, day, hour, minute, second);
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
            return date.DayOfWeek == System.DayOfWeek.Saturday || date.DayOfWeek == System.DayOfWeek.Sunday;
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

        // 比较两个日期（根据指定格式）
        public static int Compare(DateTime date1, DateTime date2, string format)
        {
            var str1 = Format(date1, format);
            var str2 = Format(date2, format);
            return string.Compare(str1, str2, StringComparison.Ordinal);
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

        // 获取当前时间
        public static DateTime Current()
        {
            return DateTime.Now;
        }

        // 获取年份
        public static int Year(DateTime date)
        {
            return date.Year;
        }

        // 获取年末
        public static DateTime EndOfYear(DateTime date)
        {
            return new DateTime(date.Year, 12, 31, 23, 59, 59);
        }

        // 获取季度末
        public static DateTime EndOfQuarter(DateTime date)
        {
            var quarter = (date.Month - 1) / 3 + 1;
            var month = quarter * 3;
            return new DateTime(date.Year, month, DateTime.DaysInMonth(date.Year, month), 23, 59, 59);
        }

        // 获取周开始
        public static DateTime BeginOfWeek(DateTime date)
        {
            var daysToSubtract = (int)date.DayOfWeek;
            if (daysToSubtract == 0) daysToSubtract = 7;
            return BeginOfDay(date.AddDays(-daysToSubtract + 1));
        }

        // 获取周结束
        public static DateTime EndOfWeek(DateTime date)
        {
            return EndOfDay(BeginOfWeek(date).AddDays(6));
        }

        // 获取星期几（1-7，1表示周日）
        public static int DayOfWeek(DateTime date)
        {
            return (int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        }

        // 格式化HTTP日期
        public static string FormatHttpDate(DateTime date)
        {
            return date.ToString("r", CultureInfo.InvariantCulture);
        }

        // 解析本地日期时间
        public static DateTime ParseLocalDateTime(string dateStr)
        {
            return DateTime.Parse(dateStr).ToLocalTime();
        }

        // 格式化本地日期时间
        public static string FormatLocalDateTime(DateTime date)
        {
            return date.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        // 计算周数差
        public static long BetweenWeek(DateTime beginDate, DateTime endDate)
        {
            return Between(beginDate, endDate, DateUnit.Week);
        }

        // 判断是否同一周
        public static bool IsSameWeek(DateTime date1, DateTime date2)
        {
            return BeginOfWeek(date1).Date == BeginOfWeek(date2).Date;
        }

        // 判断时间是否重叠
        public static bool IsOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start1 < end2 && start2 < end1;
        }

        // 判断时间是否在范围内
        public static bool IsIn(DateTime date, DateTime start, DateTime end)
        {
            return date >= start && date <= end;
        }

        // 通用偏移方法
        public static DateTime Offset(DateTime date, int offset, DateUnit unit)
        {
            switch (unit)
            {
                case DateUnit.Millisecond:
                    return date.AddMilliseconds(offset);
                case DateUnit.Second:
                    return date.AddSeconds(offset);
                case DateUnit.Minute:
                    return date.AddMinutes(offset);
                case DateUnit.Hour:
                    return date.AddHours(offset);
                case DateUnit.Day:
                    return date.AddDays(offset);
                case DateUnit.Week:
                    return date.AddDays(offset * 7);
                case DateUnit.Month:
                    return date.AddMonths(offset);
                case DateUnit.Year:
                    return date.AddYears(offset);
                default:
                    return date;
            }
        }

        // 格式化中文日期
        public static string FormatChineseDate(DateTime date)
        {
            return $"{date.Year}年{date.Month}月{date.Day}日";
        }

        // 格式化时间差
        public static string FormatBetween(DateTime beginDate, DateTime endDate)
        {
            var span = endDate - beginDate;
            var days = (int)span.TotalDays;
            var hours = span.Hours;
            var minutes = span.Minutes;
            var seconds = span.Seconds;

            var parts = new List<string>();
            if (days > 0) parts.Add($"{days}天");
            if (hours > 0) parts.Add($"{hours}小时");
            if (minutes > 0) parts.Add($"{minutes}分钟");
            if (seconds > 0 || parts.Count == 0) parts.Add($"{seconds}秒");

            return string.Join("", parts);
        }

        // 获取计时器
        public static StopWatch Timer()
        {
            return new StopWatch();
        }

        // 获取日历
        public static Calendar Calendar()
        {
            return new Calendar(TimeZoneInfo.Local, CultureInfo.CurrentCulture);
        }

        // 获取日历（指定日期）
        public static Calendar Calendar(DateTime date)
        {
            var calendar = new Calendar(TimeZoneInfo.Local, CultureInfo.CurrentCulture);
            calendar.Time = date;
            return calendar;
        }
    }
}