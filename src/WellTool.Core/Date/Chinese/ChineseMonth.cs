using System;

namespace WellTool.Core.Date.Chinese
{
    /// <summary>
    /// 农历月份表示
    /// </summary>
    public class ChineseMonth
    {
        private static readonly string[] MONTH_NAME = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二" };
        private static readonly string[] MONTH_NAME_TRADITIONAL = { "正", "二", "三", "四", "五", "六", "七", "八", "九", "寒", "冬", "腊" };

        /// <summary>
        /// 当前农历月份是否为闰月
        /// </summary>
        /// <param name="year">农历年</param>
        /// <param name="month">农历月</param>
        /// <returns>是否为闰月</returns>
        public static bool IsLeapMonth(int year, int month)
        {
            return month == LunarInfo.LeapMonth(year);
        }

        /// <summary>
        /// 获得农历月称呼
        /// 当为传统表示时，表示为二月，腊月，或者润正月等
        /// 当为非传统表示时，二月，十二月，或者润一月等
        /// </summary>
        /// <param name="isLeapMonth">是否闰月</param>
        /// <param name="month">月份，从1开始，如果是闰月，应传入需要显示的月份</param>
        /// <param name="isTraditional">是否传统表示，例如一月传统表示为正月</param>
        /// <returns>返回农历月份称呼</returns>
        public static string GetChineseMonthName(bool isLeapMonth, int month, bool isTraditional)
        {
            return (isLeapMonth ? "闰" : "") + (isTraditional ? MONTH_NAME_TRADITIONAL : MONTH_NAME)[month - 1] + "月";
        }
    }
}