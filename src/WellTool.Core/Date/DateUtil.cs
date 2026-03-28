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
            if (string.IsNullOrWhiteSpace(dateStr))
            {
                throw new ArgumentException("Date string cannot be empty");
            }

            // 尝试解析时间戳
            if (long.TryParse(dateStr, out var timestamp))
            {
                // 处理不同长度的时间戳
                if (timestamp.ToString().Length == 13)
                {
                    // 毫秒时间戳
                    return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timestamp).ToLocalTime();
                }
                else if (timestamp.ToString().Length == 10)
                {
                    // 秒时间戳
                    return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp).ToLocalTime();
                }
            }

            // 尝试解析不同格式的日期字符串
            var formats = new string[]
            {
                "yyyy-MM-dd HH:mm:ss",
                "yyyy/MM/dd HH:mm:ss",
                "yyyy-MM-dd",
                "yyyy/MM/dd",
                "HH:mm:ss",
                "yyyyMMddHHmmss",
                "yyyyMMdd",
                "MM-dd",
                "M-d",
                "yyyy-M-d",
                "yyyy/MM/dd HH:mm:ss.fff",
                "yyyy-MM-dd HH:mm:ss.fff",
                "yyyy-MM-ddTHH:mm:ss.fffZ",
                "yyyy-MM-ddTHH:mm:ss.fff+08:00",
                "yyyy-MM-dd HH:mm:ss",
                "yyyy-M-d H:m:s",
                "yyyy/MM/dd H:mm:ss"
            };

            // 处理带时区的日期字符串，忽略时区信息
            var cleanDateStr = dateStr;
            if (cleanDateStr.Contains('Z'))
            {
                // 移除Z标记
                cleanDateStr = cleanDateStr.Replace("Z", "");
                // 替换T为空格
                cleanDateStr = cleanDateStr.Replace('T', ' ');
                // 尝试解析清理后的日期字符串
                if (DateTime.TryParse(cleanDateStr, out var parsedDate))
                {
                    return parsedDate;
                }
            }
            else if (cleanDateStr.Contains('+') || (cleanDateStr.Contains('-') && cleanDateStr.LastIndexOf('-') > 10))
            {
                // 移除时区信息
                if (cleanDateStr.Contains('+'))
                {
                    cleanDateStr = cleanDateStr.Substring(0, cleanDateStr.IndexOf('+'));
                }
                else if (cleanDateStr.Contains('-') && cleanDateStr.LastIndexOf('-') > 10)
                {
                    cleanDateStr = cleanDateStr.Substring(0, cleanDateStr.LastIndexOf('-'));
                }
                
                // 替换T为空格
                cleanDateStr = cleanDateStr.Replace('T', ' ');
                
                // 尝试解析清理后的日期字符串
                if (DateTime.TryParse(cleanDateStr, out var parsedDate))
                {
                    return parsedDate;
                }
            }

            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            // 如果所有格式都失败，尝试默认解析
            try
            {
                return DateTime.Parse(dateStr);
            }
            catch
            {
                throw new ArgumentException("Invalid date format");
            }
        }

        // 解析日期字符串（可空版本）
        public static DateTime? ParseOrNull(string dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
            {
                return null;
            }

            try
            {
                return Parse(dateStr);
            }
            catch
            {
                return null;
            }
        }

        // 解析指定格式的日期字符串
        public static DateTime Parse(string dateStr, string format)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
            {
                throw new ArgumentException("Date string cannot be empty");
            }

            // 尝试解析带不同分隔符的日期
            if (format.Contains("-") && dateStr.Contains("/"))
            {
                format = format.Replace("-", "/");
            }
            else if (format.Contains("/") && dateStr.Contains("-"))
            {
                format = format.Replace("/", "-");
            }

            // 尝试解析单数字的月份和日期
            if (format.Contains("yyyy-MM-dd") || format.Contains("yyyy/MM/dd"))
            {
                var separator = format.Contains("-") ? "-" : "/";
                var parts = dateStr.Split(separator[0]);
                if (parts.Length >= 3)
                {
                    if (parts[1].Length == 1)
                    {
                        parts[1] = "0" + parts[1];
                    }
                    if (parts[2].Length == 1)
                    {
                        parts[2] = "0" + parts[2];
                    }
                    dateStr = string.Join(separator, parts);
                }
            }

            // 尝试解析带时分秒的格式
            if (format.Contains("HH:mm:ss") && dateStr.Contains(":"))
            {
                var timeParts = dateStr.Split(' ');
                if (timeParts.Length == 2)
                {
                    var time = timeParts[1].Split(':');
                    if (time.Length == 3)
                    {
                        if (time[0].Length == 1)
                        {
                            time[0] = "0" + time[0];
                        }
                        if (time[1].Length == 1)
                        {
                            time[1] = "0" + time[1];
                        }
                        if (time[2].Length == 1)
                        {
                            time[2] = "0" + time[2];
                        }
                        timeParts[1] = string.Join(":", time);
                        dateStr = string.Join(" ", timeParts);
                    }
                }
            }

            // 尝试使用不同的格式进行解析
            var possibleFormats = new List<string> { format };
            
            // 添加可能的变体格式
            if (format == "yyyy-MM-dd HH:mm:ss")
            {
                possibleFormats.Add("yyyy/MM/dd HH:mm:ss");
                possibleFormats.Add("yyyy-M-d H:m:s");
                possibleFormats.Add("yyyy/MM/dd H:mm:ss");
            }
            else if (format == "yyyy-MM-dd")
            {
                possibleFormats.Add("yyyy/MM/dd");
                possibleFormats.Add("yyyy-M-d");
            }

            foreach (var fmt in possibleFormats)
            {
                if (DateTime.TryParseExact(dateStr, fmt, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    return date;
                }
            }

            // 如果所有格式都失败，尝试默认解析
            try
            {
                return DateTime.Parse(dateStr);
            }
            catch
            {
                throw new ArgumentException("Invalid date format");
            }
        }

        // 解析指定格式的日期字符串（可空版本）
        public static DateTime? ParseOrNull(string dateStr, string format)
        {
            if (string.IsNullOrWhiteSpace(dateStr))
            {
                return null;
            }

            try
            {
                return Parse(dateStr, format);
            }
            catch
            {
                return null;
            }
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
            // 确保beginDate小于endDate
            if (beginDate > endDate)
            {
                var temp = beginDate;
                beginDate = endDate;
                endDate = temp;
            }

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
            var birthday = ParseOrNull(birthDay);
            if (birthday == null)
            {
                throw new ArgumentException("Invalid birthday format");
            }
            return AgeOfNow(birthday.Value);
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
            // 对于日期格式的比较，应该先解析为日期对象再比较
            if (format == "yyyy-MM-dd")
            {
                date1 = BeginOfDay(date1);
                date2 = BeginOfDay(date2);
            }
            else if (format == "yyyy-MM-dd HH:mm")
            {
                date1 = new DateTime(date1.Year, date1.Month, date1.Day, date1.Hour, date1.Minute, 0);
                date2 = new DateTime(date2.Year, date2.Month, date2.Day, date2.Hour, date2.Minute, 0);
            }
            else if (format == "yyyy-MM")
            {
                date1 = new DateTime(date1.Year, date1.Month, 1);
                date2 = new DateTime(date2.Year, date2.Month, 1);
            }

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

        // 解析日期字符串（只解析日期部分，可空版本）
        public static DateTime? ParseDateOrNull(string dateStr)
        {
            return ParseOrNull(dateStr, "yyyy-MM-dd");
        }

        // 解析时间字符串（只解析时间部分）
        public static DateTime ParseTime(string timeStr)
        {
            return Parse(timeStr, "HH:mm:ss");
        }

        // 解析时间字符串（只解析时间部分，可空版本）
        public static DateTime? ParseTimeOrNull(string timeStr)
        {
            return ParseOrNull(timeStr, "HH:mm:ss");
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