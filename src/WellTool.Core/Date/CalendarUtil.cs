using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 日历工具类
    /// </summary>
    public class CalendarUtil
    {
        /// <summary>
        /// 判断是否为上午
        /// </summary>
        /// <param name="calendar">日历对象</param>
        /// <returns>是否为上午</returns>
        public static bool IsAM(DateTime calendar)
        {
            return calendar.Hour < 12;
        }

        /// <summary>
        /// 判断是否为下午
        /// </summary>
        /// <param name="calendar">日历对象</param>
        /// <returns>是否为下午</returns>
        public static bool IsPM(DateTime calendar)
        {
            return calendar.Hour >= 12;
        }

        /// <summary>
        /// 判断两个日期是否为同一天
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>
        /// <returns>是否为同一天</returns>
        public static bool IsSameDay(DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day;
        }

        /// <summary>
        /// 判断两个日期是否为同一周
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>
        /// <param name="firstDayOfWeek">一周的第一天，默认Monday</param>
        /// <returns>是否为同一周</returns>
        public static bool IsSameWeek(DateTime date1, DateTime date2, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            var cal1 = GetWeekNumber(date1, firstDayOfWeek);
            var cal2 = GetWeekNumber(date2, firstDayOfWeek);
            return cal1 == cal2;
        }

        /// <summary>
        /// 获取日期在一年中的周数
        /// </summary>
        private static int GetWeekNumber(DateTime date, DayOfWeek firstDayOfWeek)
        {
            var culture = CultureInfo.CurrentCulture;
            return culture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, firstDayOfWeek);
        }

        /// <summary>
        /// 判断两个日期是否为同一月
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>
        /// <returns>是否为同一月</returns>
        public static bool IsSameMonth(DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month;
        }

        /// <summary>
        /// 判断两个日期是否为同一年
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>
        /// <returns>是否为同一年</returns>
        public static bool IsSameYear(DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year;
        }

        /// <summary>
        /// 判断是否为月末最后一天
        /// </summary>
        /// <param name="calendar">日期</param>
        /// <returns>是否为月末最后一天</returns>
        public static bool IsLastDayOfMonth(DateTime calendar)
        {
            var lastDay = DateTime.DaysInMonth(calendar.Year, calendar.Month);
            return calendar.Day == lastDay;
        }

        /// <summary>
        /// 获取一天的开始时间（00:00:00）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>一天的开始时间</returns>
        public static DateTime BeginOfDay(DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        /// 获取一天的结束时间（23:59:59.999）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>一天的结束时间</returns>
        public static DateTime EndOfDay(DateTime date)
        {
            return date.Date.AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// 获取周的开始日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="firstDayOfWeek">一周的第一天，默认Monday</param>
        /// <returns>周的开始日期</returns>
        public static DateTime BeginOfWeek(DateTime date, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            var diff = (7 + (date.DayOfWeek - firstDayOfWeek)) % 7;
            return date.AddDays(-diff).Date;
        }

        /// <summary>
        /// 获取周的结束日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="firstDayOfWeek">一周的第一天，默认Monday</param>
        /// <returns>周的结束日期</returns>
        public static DateTime EndOfWeek(DateTime date, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            return BeginOfWeek(date, firstDayOfWeek).AddDays(7).AddTicks(-1);
        }

        /// <summary>
        /// 获取月的开始日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>月的开始日期</returns>
        public static DateTime BeginOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 获取月的结束日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>月的结束日期</returns>
        public static DateTime EndOfMonth(DateTime date)
        {
            return BeginOfMonth(date).AddMonths(1).AddTicks(-1);
        }

        /// <summary>
        /// 获取季度的开始日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>季度的开始日期</returns>
        public static DateTime BeginOfQuarter(DateTime date)
        {
            var quarterMonth = (date.Month - 1) / 3 * 3 + 1;
            return new DateTime(date.Year, quarterMonth, 1);
        }

        /// <summary>
        /// 获取季度的结束日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>季度的结束日期</returns>
        public static DateTime EndOfQuarter(DateTime date)
        {
            return BeginOfQuarter(date).AddMonths(3).AddTicks(-1);
        }

        /// <summary>
        /// 获取年的开始日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>年的开始日期</returns>
        public static DateTime BeginOfYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        /// <summary>
        /// 获取年的结束日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>年的结束日期</returns>
        public static DateTime EndOfYear(DateTime date)
        {
            return new DateTime(date.Year, 12, 31, 23, 59, 59, 999);
        }

        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="birthday">出生日期</param>
        /// <param name="currentDate">当前日期</param>
        /// <returns>年龄</returns>
        public static int Age(DateTime birthday, DateTime currentDate)
        {
            var age = currentDate.Year - birthday.Year;
            if (currentDate.Month < birthday.Month || (currentDate.Month == birthday.Month && currentDate.Day < birthday.Day))
            {
                age--;
            }
            return age < 0 ? 0 : age;
        }

        /// <summary>
        /// 格式化为中文日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="isUpperCase">是否大写</param>
        /// <returns>中文日期字符串</returns>
        public static string FormatChineseDate(DateTime date, bool isUpperCase = false)
        {
            var yearStr = FormatChineseNumber(date.Year, isUpperCase);
            var monthStr = FormatChineseNumber(date.Month, isUpperCase);
            var dayStr = FormatChineseNumber(date.Day, isUpperCase);
            return $"{yearStr}年{monthStr}月{dayStr}日";
        }

        private static string FormatChineseNumber(int number, bool isUpperCase)
        {
            var chars = isUpperCase 
                ? new[] { '零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖' }
                : new[] { '零', '一', '二', '三', '四', '五', '六', '七', '八', '九' };
            
            if (number == 0) return "零";
            
            var result = "";
            var numStr = number.ToString();
            foreach (var c in numStr)
            {
                result += chars[c - '0'];
            }
            return result;
        }

        /// <summary>
        /// 获取小时的开始时间
        /// </summary>
        /// <param name="date">日期时间</param>
        /// <returns>小时的开始时间</returns>
        public static DateTime BeginOfHour(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0);
        }

        /// <summary>
        /// 获取小时的结束时间
        /// </summary>
        /// <param name="date">日期时间</param>
        /// <returns>小时的结束时间</returns>
        public static DateTime EndOfHour(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 59, 59, 999);
        }

        /// <summary>
        /// 获取年份和季度
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>年份和季度元组</returns>
        public static (int year, int quarter) YearAndQuarter(DateTime date)
        {
            var quarter = (date.Month - 1) / 3 + 1;
            return (date.Year, quarter);
        }
    }
}
