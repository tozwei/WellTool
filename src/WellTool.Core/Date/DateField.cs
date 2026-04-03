using System;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 日期各个部分的枚举
    /// 与Calendar相应值对应
    /// </summary>
    public enum DateField
    {
        /// <summary>
        /// 世纪
        /// </summary>
        Era = 0,
        /// <summary>
        /// 年
        /// </summary>
        Year = 1,
        /// <summary>
        /// 月
        /// </summary>
        Month = 2,
        /// <summary>
        /// 一年中第几周
        /// </summary>
        WeekOfYear = 3,
        /// <summary>
        /// 一月中第几周
        /// </summary>
        WeekOfMonth = 4,
        /// <summary>
        /// 一月中的第几天
        /// </summary>
        DayOfMonth = 5,
        /// <summary>
        /// 一年中的第几天
        /// </summary>
        DayOfYear = 6,
        /// <summary>
        /// 周几，1表示周日，2表示周一
        /// </summary>
        DayOfWeek = 7,
        /// <summary>
        /// 天所在的周是这个月的第几周
        /// </summary>
        DayOfWeekInMonth = 8,
        /// <summary>
        /// 上午或者下午
        /// </summary>
        AmPm = 9,
        /// <summary>
        /// 小时，用于12小时制
        /// </summary>
        Hour = 10,
        /// <summary>
        /// 小时，用于24小时制
        /// </summary>
        HourOfDay = 11,
        /// <summary>
        /// 分钟
        /// </summary>
        Minute = 12,
        /// <summary>
        /// 秒
        /// </summary>
        Second = 13,
        /// <summary>
        /// 毫秒
        /// </summary>
        Millisecond = 14
    }

    /// <summary>
    /// DateField 扩展方法
    /// </summary>
    public static class DateFieldExtensions
    {
        /// <summary>
        /// 转换为 TimeSpan
        /// </summary>
        public static TimeSpan ToTimeSpan(this DateField field)
        {
            switch (field)
            {
                case DateField.Year:
                    return TimeSpan.FromDays(365);
                case DateField.Month:
                    return TimeSpan.FromDays(30);
                case DateField.DayOfMonth:
                case DateField.DayOfYear:
                    return TimeSpan.FromDays(1);
                case DateField.Hour:
                case DateField.HourOfDay:
                    return TimeSpan.FromHours(1);
                case DateField.Minute:
                    return TimeSpan.FromMinutes(1);
                case DateField.Second:
                    return TimeSpan.FromSeconds(1);
                case DateField.Millisecond:
                    return TimeSpan.FromMilliseconds(1);
                default:
                    return TimeSpan.Zero;
            }
        }
    }
}