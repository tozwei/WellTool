using System;
using System.Globalization;

namespace WellTool.Core.Date
{
    /// <summary>
    /// LocalDateTime工具类
    /// </summary>
    public static class LocalDateTimeUtil
    {
        /// <summary>
        /// 获取指定日期的开始时间（00:00:00）
        /// </summary>
        public static DateTime StartOfDay(DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        /// 获取指定日期的结束时间（23:59:59.999）
        /// </summary>
        public static DateTime EndOfDay(DateTime date)
        {
            return date.Date.AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// 判断是否是今天
        /// </summary>
        public static bool IsToday(DateTime date)
        {
            return date.Date == DateTime.Today;
        }

        /// <summary>
        /// 判断是否是昨天
        /// </summary>
        public static bool IsYesterday(DateTime date)
        {
            return date.Date == DateTime.Today.AddDays(-1);
        }

        /// <summary>
        /// 判断是否是明天
        /// </summary>
        public static bool IsTomorrow(DateTime date)
        {
            return date.Date == DateTime.Today.AddDays(1);
        }

        /// <summary>
        /// 判断是否在过去
        /// </summary>
        public static bool IsPast(DateTime date)
        {
            return date < DateTime.Now;
        }

        /// <summary>
        /// 判断是否在未来
        /// </summary>
        public static bool IsFuture(DateTime date)
        {
            return date > DateTime.Now;
        }

        /// <summary>
        /// 解析日期字符串
        /// </summary>
        public static DateTime? Parse(string dateStr)
        {
            if (DateTime.TryParse(dateStr, out DateTime result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 解析日期字符串（指定格式）
        /// </summary>
        public static DateTime? Parse(string dateStr, string format)
        {
            if (DateTime.TryParseExact(dateStr, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 安全解析日期字符串，为空或无效时返回默认值
        /// </summary>
        public static DateTime ParseSafe(string dateStr, DateTime defaultValue = default)
        {
            return Parse(dateStr) ?? defaultValue;
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        public static string Format(DateTime date, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return date.ToString(format);
        }

        /// <summary>
        /// 获取相对时间描述
        /// </summary>
        public static string GetRelativeTime(DateTime date)
        {
            var now = DateTime.Now;
            var diff = now - date;

            if (diff.TotalSeconds < 60)
            {
                return "刚刚";
            }
            if (diff.TotalMinutes < 60)
            {
                return $"{(int)diff.TotalMinutes}分钟前";
            }
            if (diff.TotalHours < 24)
            {
                return $"{(int)diff.TotalHours}小时前";
            }
            if (diff.TotalDays < 7)
            {
                return $"{(int)diff.TotalDays}天前";
            }
            if (diff.TotalDays < 30)
            {
                return $"{(int)(diff.TotalDays / 7)}周前";
            }
            if (diff.TotalDays < 365)
            {
                return $"{(int)(diff.TotalDays / 30)}月前";
            }
            return $"{(int)(diff.TotalDays / 365)}年前";
        }

        /// <summary>
        /// 获取中文星期
        /// </summary>
        public static string GetChineseDayOfWeek(DateTime date)
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return weekdays[(int)date.DayOfWeek];
        }

        /// <summary>
        /// 转换为UTC时间
        /// </summary>
        public static DateTime ToUniversalTime(DateTime date)
        {
            return date.ToUniversalTime();
        }

        /// <summary>
        /// 从UTC时间转换
        /// </summary>
        public static DateTime FromUniversalTime(DateTime date)
        {
            return DateTime.SpecifyKind(date, DateTimeKind.Utc).ToLocalTime();
        }

        /// <summary>
        /// 转换为指定时区的时间
        /// </summary>
        public static DateTime ConvertTime(DateTime date, TimeZoneInfo targetZone)
        {
            return TimeZoneInfo.ConvertTime(date, targetZone);
        }
    }
}
