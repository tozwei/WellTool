using System;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 日期字段枚举
    /// </summary>
    public enum DateField
    {
        /// <summary>
        /// 年
        /// </summary>
        Year = 1,

        /// <summary>
        /// 月
        /// </summary>
        Month = 2,

        /// <summary>
        /// 日
        /// </summary>
        Day = 5,

        /// <summary>
        /// 小时
        /// </summary>
        Hour = 10,

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
        Millisecond = 14,

        /// <summary>
        /// 星期
        /// </summary>
        DayOfWeek = 7,

        /// <summary>
        /// 一年中的第几周
        /// </summary>
        WeekOfYear = 3,

        /// <summary>
        /// 一个月中的第几周
        /// </summary>
        WeekOfMonth = 4,

        /// <summary>
        /// 一个月中的第几天
        /// </summary>
        DayOfMonth = 5,

        /// <summary>
        /// 一年中的第几天
        /// </summary>
        DayOfYear = 6,

        /// <summary>
        /// 一个月中的第几个星期几
        /// </summary>
        DayOfWeekInMonth = 8,

        /// <summary>
        /// 上午/下午
        /// </summary>
        AmPm = 9,

        /// <summary>
        /// 一天中的第几小时（24小时制）
        /// </summary>
        HourOfDay = 11
    }

    /// <summary>
    /// DateField扩展方法
    /// </summary>
    public static class DateFieldExtensions
    {
        /// <summary>
        /// 获取字段对应的TimeSpan常量
        /// </summary>
        public static TimeSpan ToTimeSpan(this DateField field, int value = 1)
        {
            switch (field)
            {
                case DateField.Year:
                    return TimeSpan.FromDays(365 * value);
                case DateField.Month:
                    return TimeSpan.FromDays(30 * value);
                case DateField.Day:
                    return TimeSpan.FromDays(value);
                case DateField.Hour:
                    return TimeSpan.FromHours(value);
                case DateField.Minute:
                    return TimeSpan.FromMinutes(value);
                case DateField.Second:
                    return TimeSpan.FromSeconds(value);
                case DateField.Millisecond:
                    return TimeSpan.FromMilliseconds(value);
                default:
                    return TimeSpan.Zero;
            }
        }

        /// <summary>
        /// 获取日期时间单位名称
        /// </summary>
        public static string GetName(this DateField field)
        {
            switch (field)
            {
                case DateField.Year: return "年";
                case DateField.Month: return "月";
                case DateField.Day: return "日";
                case DateField.Hour: return "小时";
                case DateField.Minute: return "分钟";
                case DateField.Second: return "秒";
                case DateField.Millisecond: return "毫秒";
                case DateField.DayOfWeek: return "星期";
                default: return "";
            }
        }

        /// <summary>
        /// 获取DateTime.Kind对应的值
        /// </summary>
        public static int GetCalendarField(this DateField field)
        {
            switch (field)
            {
                case DateField.Year: return 1;
                case DateField.Month: return 2;
                case DateField.Day: return 5;
                case DateField.Hour: return 10;
                case DateField.Minute: return 12;
                case DateField.Second: return 13;
                case DateField.Millisecond: return 14;
                case DateField.DayOfWeek: return 7;
                default: return -1;
            }
        }
    }
}
