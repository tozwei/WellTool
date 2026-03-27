using System;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 星期枚举
    /// 与Calendar中的星期int值对应
    /// </summary>
    public enum Week
    {
        /// <summary>
        /// 周日
        /// </summary>
        Sunday = 1,
        /// <summary>
        /// 周一
        /// </summary>
        Monday = 2,
        /// <summary>
        /// 周二
        /// </summary>
        Tuesday = 3,
        /// <summary>
        /// 周三
        /// </summary>
        Wednesday = 4,
        /// <summary>
        /// 周四
        /// </summary>
        Thursday = 5,
        /// <summary>
        /// 周五
        /// </summary>
        Friday = 6,
        /// <summary>
        /// 周六
        /// </summary>
        Saturday = 7
    }

    public static class WeekExtensions
    {
        /// <summary>
        /// 获取ISO8601规范的int值，from 1 (Monday) to 7 (Sunday).
        /// </summary>
        /// <param name="week">星期枚举</param>
        /// <returns>ISO8601规范的int值</returns>
        public static int GetIso8601Value(this Week week)
        {
            int iso8601IntValue = (int)week - 1;
            if (iso8601IntValue == 0)
            {
                iso8601IntValue = 7;
            }
            return iso8601IntValue;
        }

        /// <summary>
        /// 转换为中文名
        /// </summary>
        /// <param name="week">星期枚举</param>
        /// <returns>星期的中文名</returns>
        public static string ToChinese(this Week week)
        {
            return week.ToChinese("星期");
        }

        /// <summary>
        /// 转换为中文名
        /// </summary>
        /// <param name="week">星期枚举</param>
        /// <param name="weekNamePre">表示星期的前缀，例如前缀为"星期"，则返回结果为"星期一"；前缀为"周"，结果为"周一"
        /// <returns>星期的中文名</returns>
        public static string ToChinese(this Week week, string weekNamePre)
        {
            switch (week)
            {
                case Week.Sunday:
                    return weekNamePre + "日";
                case Week.Monday:
                    return weekNamePre + "一";
                case Week.Tuesday:
                    return weekNamePre + "二";
                case Week.Wednesday:
                    return weekNamePre + "三";
                case Week.Thursday:
                    return weekNamePre + "四";
                case Week.Friday:
                    return weekNamePre + "五";
                case Week.Saturday:
                    return weekNamePre + "六";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 将星期int值转换为Week枚举对象
        /// </summary>
        /// <param name="calendarWeekIntValue">Calendar中关于Week的int值，1表示Sunday</param>
        /// <returns>Week枚举对象</returns>
        public static Week? Of(int calendarWeekIntValue)
        {
            if (calendarWeekIntValue >= 1 && calendarWeekIntValue <= 7)
            {
                return (Week)calendarWeekIntValue;
            }
            return null;
        }
    }
}