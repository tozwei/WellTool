using System;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 月份枚举
    /// 与Calendar中的月份int值对应
    /// </summary>
    public enum Month
    {
        /// <summary>
        /// 一月
        /// </summary>
        January = 0,
        /// <summary>
        /// 二月
        /// </summary>
        February = 1,
        /// <summary>
        /// 三月
        /// </summary>
        March = 2,
        /// <summary>
        /// 四月
        /// </summary>
        April = 3,
        /// <summary>
        /// 五月
        /// </summary>
        May = 4,
        /// <summary>
        /// 六月
        /// </summary>
        June = 5,
        /// <summary>
        /// 七月
        /// </summary>
        July = 6,
        /// <summary>
        /// 八月
        /// </summary>
        August = 7,
        /// <summary>
        /// 九月
        /// </summary>
        September = 8,
        /// <summary>
        /// 十月
        /// </summary>
        October = 9,
        /// <summary>
        /// 十一月
        /// </summary>
        November = 10,
        /// <summary>
        /// 十二月
        /// </summary>
        December = 11,
        /// <summary>
        /// 十三月，仅用于农历
        /// </summary>
        Undecember = 12
    }

    public static class MonthExtensions
    {
        /// <summary>
        /// 获取月份值，此值与System.DateTime对应
        /// 此值从1开始，即1表示一月
        /// </summary>
        /// <param name="month">月份枚举</param>
        /// <returns>月份值，从1开始计数</returns>
        public static int GetValueBaseOne(this Month month)
        {
            if (month == Month.Undecember)
            {
                throw new NotSupportedException("Unsupported Undecember Field");
            }
            return (int)month + 1;
        }

        /// <summary>
        /// 获取此月份最后一天的值
        /// </summary>
        /// <param name="month">月份枚举</param>
        /// <param name="isLeapYear">是否闰年</param>
        /// <returns>此月份最后一天的值</returns>
        public static int GetLastDay(this Month month, bool isLeapYear)
        {
            switch (month)
            {
                case Month.February:
                    return isLeapYear ? 29 : 28;
                case Month.April:
                case Month.June:
                case Month.September:
                case Month.November:
                    return 30;
                default:
                    return 31;
            }
        }

        /// <summary>
        /// 将月份int值转换为Month枚举对象
        /// </summary>
        /// <param name="calendarMonthIntValue">Calendar中关于Month的int值，从0开始</param>
        /// <returns>Month枚举对象</returns>
        public static Month? Of(int calendarMonthIntValue)
        {
            if (calendarMonthIntValue >= 0 && calendarMonthIntValue <= 12)
            {
                return (Month)calendarMonthIntValue;
            }
            return null;
        }

        /// <summary>
        /// 获得指定月的最后一天
        /// </summary>
        /// <param name="month">月份，从0开始</param>
        /// <param name="isLeapYear">是否为闰年，闰年只对二月有影响</param>
        /// <returns>最后一天，可能为28,29,30,31</returns>
        public static int GetLastDay(int month, bool isLeapYear)
        {
            var monthEnum = Of(month);
            if (monthEnum == null)
            {
                throw new ArgumentException("Invalid Month base 0: " + month);
            }
            return monthEnum.Value.GetLastDay(isLeapYear);
        }
    }
}