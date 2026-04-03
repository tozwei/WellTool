using System;
using System.Collections.Generic;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 时区工具类
    /// </summary>
    public class TimeZoneUtil
    {
        /// <summary>
        /// GMT时区ID
        /// </summary>
        public const string GMT_ID = "GMT";

        /// <summary>
        /// GMT时区
        /// </summary>
        public static readonly TimeZoneInfo GMT = TimeZoneInfo.FindSystemTimeZoneById(GMT_ID);

        /// <summary>
        /// 根据时区ID获取对应的TimeZoneInfo
        /// </summary>
        /// <param name="id">时区ID</param>
        /// <returns>TimeZoneInfo</returns>
        public static TimeZoneInfo GetTimeZone(string id)
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(id);
            }
            catch
            {
                // 如果找不到，返回UTC时区
                return TimeZoneInfo.Utc;
            }
        }

        /// <summary>
        /// 返回非空时区，否则返回系统默认时区
        /// </summary>
        /// <param name="timeZone">时区</param>
        /// <returns>非空的时区</returns>
        public static TimeZoneInfo ToTimeZone(TimeZoneInfo timeZone)
        {
            return timeZone ?? TimeZoneInfo.Local;
        }
    }
}