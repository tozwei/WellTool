using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 时区工具类
    /// </summary>
    public static class TimeZoneUtil
    {
        /// <summary>
        /// 获取所有可用时区
        /// </summary>
        public static TimeZoneInfo[] GetAllTimeZones()
        {
            return TimeZoneInfo.GetSystemTimeZones().ToArray();
        }

        /// <summary>
        /// 获取所有时区的ID
        /// </summary>
        public static string[] GetAllTimeZoneIds()
        {
            return TimeZoneInfo.GetSystemTimeZones().Select(tz => tz.Id).ToArray();
        }

        /// <summary>
        /// 通过ID获取时区
        /// </summary>
        public static TimeZoneInfo GetTimeZone(string timeZoneId)
        {
            return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }

        /// <summary>
        /// 获取本地时区
        /// </summary>
        public static TimeZoneInfo GetLocalTimeZone()
        {
            return TimeZoneInfo.Local;
        }

        /// <summary>
        /// 获取UTC时区
        /// </summary>
        public static TimeZoneInfo GetUtcTimeZone()
        {
            return TimeZoneInfo.Utc;
        }

        /// <summary>
        /// 将时间转换为UTC时间
        /// </summary>
        public static DateTime ToUtc(DateTime dateTime, TimeZoneInfo sourceZone = null)
        {
            if (sourceZone == null)
            {
                sourceZone = TimeZoneInfo.Local;
            }
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, sourceZone);
        }

        /// <summary>
        /// 将UTC时间转换为指定时区时间
        /// </summary>
        public static DateTime FromUtc(DateTime utcDateTime, TimeZoneInfo targetZone = null)
        {
            if (targetZone == null)
            {
                targetZone = TimeZoneInfo.Local;
            }
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, targetZone);
        }

        /// <summary>
        /// 获取当前时区的UTC偏移量
        /// </summary>
        public static TimeSpan GetUtcOffset(DateTime dateTime = default)
        {
            return TimeZoneInfo.Local.GetUtcOffset(dateTime);
        }

        /// <summary>
        /// 获取指定时区的UTC偏移量
        /// </summary>
        public static TimeSpan GetUtcOffset(TimeZoneInfo timeZone, DateTime dateTime = default)
        {
            return timeZone.GetUtcOffset(dateTime);
        }

        /// <summary>
        /// 获取时区名称
        /// </summary>
        public static string GetTimeZoneName(TimeZoneInfo timeZone)
        {
            return timeZone.DisplayName;
        }

        /// <summary>
        /// 获取时区标准名称
        /// </summary>
        public static string GetStandardName(TimeZoneInfo timeZone)
        {
            return timeZone.StandardName;
        }

        /// <summary>
        /// 获取时区缩写
        /// </summary>
        public static string GetAbbreviation(TimeZoneInfo timeZone, DateTime dateTime = default)
        {
            return timeZone.IsDaylightSavingTime(dateTime)
                ? timeZone.DaylightName
                : timeZone.StandardName;
        }

        /// <summary>
        /// 判断是否为夏令时
        /// </summary>
        public static bool IsDaylightSavingTime(TimeZoneInfo timeZone, DateTime dateTime = default)
        {
            return timeZone.IsDaylightSavingTime(dateTime);
        }

        /// <summary>
        /// 获取两个时区之间的时间差
        /// </summary>
        public static TimeSpan GetTimeDifference(TimeZoneInfo fromZone, TimeZoneInfo toZone, DateTime dateTime = default)
        {
            var fromOffset = fromZone.GetUtcOffset(dateTime);
            var toOffset = toZone.GetUtcOffset(dateTime);
            return toOffset - fromOffset;
        }

        /// <summary>
        /// 通过偏移量获取时区
        /// </summary>
        public static TimeZoneInfo GetTimeZoneByOffset(TimeSpan offset)
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            return timeZones.FirstOrDefault(tz => tz.BaseUtcOffset == offset);
        }

        /// <summary>
        /// 通过偏移量（小时）获取时区
        /// </summary>
        public static TimeZoneInfo GetTimeZoneByOffsetHours(int offsetHours)
        {
            return GetTimeZoneByOffset(TimeSpan.FromHours(offsetHours));
        }

        /// <summary>
        /// 获取支持的UTC偏移量列表
        /// </summary>
        public static List<TimeSpan> GetSupportedUtcOffsets()
        {
            var offsets = new HashSet<TimeSpan>();
            foreach (var tz in TimeZoneInfo.GetSystemTimeZones())
            {
                offsets.Add(tz.BaseUtcOffset);
            }
            return offsets.OrderBy(o => o).ToList();
        }
    }
}
