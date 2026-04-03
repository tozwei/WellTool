using System;

namespace WellTool.Core.Date
{
    /// <summary>
    /// LocalDateTime工具类
    /// </summary>
    public class LocalDateTimeUtil
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        public static DateTime Now()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 获取当前UTC时间
        /// </summary>
        public static DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        /// 从Date转换
        /// </summary>
        public static DateTime Of(DateTime date)
        {
            return date;
        }

        /// <summary>
        /// 从时间戳转换（毫秒）
        /// </summary>
        public static DateTime Of(long epochMilli)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(epochMilli).DateTime;
        }

        /// <summary>
        /// 从时间戳转换（毫秒），指定时区
        /// </summary>
        public static DateTime Of(long epochMilli, TimeZoneInfo timeZone)
        {
            var utc = DateTimeOffset.FromUnixTimeMilliseconds(epochMilli);
            return TimeZoneInfo.ConvertTimeFromUtc(utc.DateTime, timeZone);
        }

        /// <summary>
        /// 解析字符串为DateTime
        /// </summary>
        public static DateTime Parse(string dateStr)
        {
            return Parse(dateStr, DatePattern.NORM_DATETIME_PATTERN);
        }

        /// <summary>
        /// 使用指定格式解析字符串为DateTime
        /// </summary>
        public static DateTime Parse(string dateStr, string format)
        {
            if (DateTime.TryParseExact(dateStr, format, null, System.Globalization.DateTimeStyles.None, out var result))
            {
                return result;
            }
            // 尝试多种格式
            string[] formats = {
                DatePattern.NORM_DATETIME_PATTERN,
                DatePattern.NORM_DATE_PATTERN,
                "yyyyMMddHHmmss",
                "yyyyMMdd",
                "HH:mm:ss"
            };
            foreach (var fmt in formats)
            {
                if (DateTime.TryParseExact(dateStr, fmt, null, System.Globalization.DateTimeStyles.None, out result))
                {
                    return result;
                }
            }
            throw new DateException($"无法解析日期字符串: {dateStr}");
        }

        /// <summary>
        /// 格式化日期时间
        /// </summary>
        public static string Format(DateTime dateTime)
        {
            return dateTime.ToString(DatePattern.NORM_DATETIME_PATTERN);
        }

        /// <summary>
        /// 使用指定格式格式化日期时间
        /// </summary>
        public static string Format(DateTime dateTime, string pattern)
        {
            return dateTime.ToString(pattern);
        }

        /// <summary>
        /// 日期偏移
        /// </summary>
        public static DateTime Offset(DateTime dateTime, DateField dateField, int offset)
        {
            return dateTime.Add((TimeSpan)dateField.ToTimeSpan() * offset);
        }

        /// <summary>
        /// 计算两个日期的时间差（毫秒）
        /// </summary>
        public static long Between(DateTime start, DateTime end)
        {
            return (long)(end - start).TotalMilliseconds;
        }

        /// <summary>
        /// 计算两个日期的时间差
        /// </summary>
        public static long Between(DateTime start, DateTime end, DateUnit unit)
        {
            var diff = end - start;
            switch (unit)
            {
                case DateUnit.Millisecond:
                    return (long)diff.TotalMilliseconds;
                case DateUnit.Second:
                    return (long)diff.TotalSeconds;
                case DateUnit.Minute:
                    return (long)diff.TotalMinutes;
                case DateUnit.Hour:
                    return (long)diff.TotalHours;
                case DateUnit.Day:
                    return (long)diff.TotalDays;
                default:
                    return (long)diff.TotalMilliseconds;
            }
        }

        /// <summary>
        /// 判断是否为周末
        /// </summary>
        public static bool IsWeekend(DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// 判断两个日期是否为同一天
        /// </summary>
        public static bool IsSameDay(DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day;
        }

        /// <summary>
        /// 判断日期是否在指定时间范围内
        /// </summary>
        public static bool IsIn(DateTime dateTime, DateTime start, DateTime end)
        {
            return dateTime >= start && dateTime <= end;
        }

        /// <summary>
        /// 判断日期是否在指定时间范围内（不含结束）
        /// </summary>
        public static bool IsIn(DateTime dateTime, DateTime start, DateTime end, bool includeEnd)
        {
            if (includeEnd)
            {
                return dateTime >= start && dateTime <= end;
            }
            return dateTime >= start && dateTime < end;
        }

        /// <summary>
        /// 获取一天的开始时间（00:00:00）
        /// </summary>
        public static DateTime BeginOfDay(DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// 获取一天的结束时间（23:59:59.999）
        /// </summary>
        public static DateTime EndOfDay(DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// 将DateTime转换为时间戳（毫秒）
        /// </summary>
        public static long ToEpochMilli(DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 获取日期在年份中的周数
        /// </summary>
        public static int WeekOfYear(DateTime dateTime)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            return culture.Calendar.GetWeekOfYear(dateTime, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// 获取星期枚举值
        /// </summary>
        public static DayOfWeek DayOfWeek(DateTime dateTime)
        {
            return dateTime.DayOfWeek;
        }
    }
}