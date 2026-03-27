using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 日期工具类
    /// </summary>
    public static class DateUtil
    {
        /// <summary>
        /// 获取当前日期时间
        /// </summary>
        /// <returns>当前日期时间</returns>
        public static DateTime Now()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 获取当前日期（不含时间）
        /// </summary>
        /// <returns>当前日期</returns>
        public static DateTimeExt Today()
        {
            return new DateTimeExt(System.DateTime.Today);
        }

        /// <summary>
        /// 获取当前时间戳（毫秒）
        /// </summary>
        /// <returns>当前时间戳</returns>
        public static long CurrentTimeMillis()
        {
            return (long)(System.DateTime.Now - new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        /// <summary>
        /// 解析日期字符串
        /// </summary>
        /// <param name="dateStr">日期字符串</param>
        /// <returns>日期时间</returns>
        public static DateTimeExt Parse(string dateStr)
        {
            return new DateTimeExt(System.DateTime.Parse(dateStr));
        }

        /// <summary>
        /// 解析日期字符串（指定格式）
        /// </summary>
        /// <param name="dateStr">日期字符串</param>
        /// <param name="format">格式</param>
        /// <returns>日期时间</returns>
        public static DateTimeExt Parse(string dateStr, string format)
        {
            return new DateTimeExt(System.DateTime.ParseExact(dateStr, format, null));
        }

        /// <summary>
        /// 格式化日期时间
        /// </summary>
        /// <param name="date">日期时间</param>
        /// <param name="format">格式</param>
        /// <returns>格式化后的字符串</returns>
        public static string Format(DateTimeExt date, string format)
        {
            return date.ToString(format);
        }

        /// <summary>
        /// 计算两个日期之间的天数差
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>天数差</returns>
        public static int DaysBetween(DateTimeExt start, DateTimeExt end)
        {
            return (int)((System.DateTime)end - (System.DateTime)start).TotalDays;
        }

        /// <summary>
        /// 计算两个日期之间的小时差
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>小时差</returns>
        public static double HoursBetween(DateTimeExt start, DateTimeExt end)
        {
            return ((System.DateTime)end - (System.DateTime)start).TotalHours;
        }

        /// <summary>
        /// 计算两个日期之间的分钟差
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>分钟差</returns>
        public static double MinutesBetween(DateTimeExt start, DateTimeExt end)
        {
            return ((System.DateTime)end - (System.DateTime)start).TotalMinutes;
        }

        /// <summary>
        /// 计算两个日期之间的秒差
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>秒差</returns>
        public static double SecondsBetween(DateTimeExt start, DateTimeExt end)
        {
            return ((System.DateTime)end - (System.DateTime)start).TotalSeconds;
        }

        /// <summary>
        /// 计算两个日期之间的毫秒差
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>毫秒差</returns>
        public static double MillisecondsBetween(DateTime start, DateTime end)
        {
            return ((System.DateTime)end - (System.DateTime)start).TotalMilliseconds;
        }

        /// <summary>
        /// 向日期添加年数
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="years">年数</param>
        /// <returns>新日期</returns>
        public static DateTime AddYears(DateTime date, int years)
        {
            return date.AddYears(years);
        }

        /// <summary>
        /// 向日期添加月数
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="months">月数</param>
        /// <returns>新日期</returns>
        public static DateTime AddMonths(DateTime date, int months)
        {
            return date.AddMonths(months);
        }

        /// <summary>
        /// 向日期添加天数
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="days">天数</param>
        /// <returns>新日期</returns>
        public static DateTime AddDays(DateTime date, int days)
        {
            return date.AddDays(days);
        }

        /// <summary>
        /// 向日期添加小时
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="hours">小时数</param>
        /// <returns>新日期</returns>
        public static DateTime AddHours(DateTime date, int hours)
        {
            return date.AddHours(hours);
        }

        /// <summary>
        /// 向日期添加分钟
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="minutes">分钟数</param>
        /// <returns>新日期</returns>
        public static DateTime AddMinutes(DateTime date, int minutes)
        {
            return date.AddMinutes(minutes);
        }

        /// <summary>
        /// 向日期添加秒数
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="seconds">秒数</param>
        /// <returns>新日期</returns>
        public static DateTime AddSeconds(DateTime date, int seconds)
        {
            return date.AddSeconds(seconds);
        }

        /// <summary>
        /// 向日期添加毫秒数
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="milliseconds">毫秒数</param>
        /// <returns>新日期</returns>
        public static DateTime AddMilliseconds(DateTime date, int milliseconds)
        {
            return date.AddMilliseconds(milliseconds);
        }

        /// <summary>
        /// 获取日期的年份
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>年份</returns>
        public static int Year(DateTime date)
        {
            return date.Year;
        }

        /// <summary>
        /// 获取日期的月份
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>月份（1-12）</returns>
        public static int Month(DateTime date)
        {
            return date.Month;
        }

        /// <summary>
        /// 获取日期的日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>日（1-31）</returns>
        public static int Day(DateTime date)
        {
            return date.Day;
        }

        /// <summary>
        /// 获取日期的小时
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>小时（0-23）</returns>
        public static int Hour(DateTime date)
        {
            return date.Hour;
        }

        /// <summary>
        /// 获取日期的分钟
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>分钟（0-59）</returns>
        public static int Minute(DateTime date)
        {
            return date.Minute;
        }

        /// <summary>
        /// 获取日期的秒
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>秒（0-59）</returns>
        public static int Second(DateTime date)
        {
            return date.Second;
        }

        /// <summary>
        /// 获取日期的毫秒
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>毫秒（0-999）</returns>
        public static int Millisecond(DateTime date)
        {
            return date.Millisecond;
        }

        /// <summary>
        /// 获取日期是星期几
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>星期几（0-6，0表示星期日）</returns>
        public static int DayOfWeek(DateTime date)
        {
            return (int)date.DayOfWeek;
        }

        /// <summary>
        /// 获取日期是当年的第几天
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>当年的第几天（1-366）</returns>
        public static int DayOfYear(DateTime date)
        {
            return date.DayOfYear;
        }

        /// <summary>
        /// 判断是否是闰年
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>是否是闰年</returns>
        public static bool IsLeapYear(int year)
        {
            return System.DateTime.IsLeapYear(year);
        }

        /// <summary>
        /// 获取月份的天数
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>月份的天数</returns>
        public static int DaysInMonth(int year, int month)
        {
            return System.DateTime.DaysInMonth(year, month);
        }

        /// <summary>
        /// 开始时间（设置为当天的00:00:00）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>当天的开始时间</returns>
        public static DateTimeExt BeginOfDay(DateTimeExt date)
        {
            return new DateTimeExt(((System.DateTime)date).Date);
        }

        /// <summary>
        /// 结束时间（设置为当天的23:59:59.999）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>当天的结束时间</returns>
        public static DateTimeExt EndOfDay(DateTimeExt date)
        {
            return new DateTimeExt(((System.DateTime)date).Date.AddDays(1).AddMilliseconds(-1));
        }

        /// <summary>
        /// 开始时间（设置为当月的第一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>当月的开始时间</returns>
        public static DateTimeExt BeginOfMonth(DateTimeExt date)
        {
            var sysDate = (System.DateTime)date;
            return new DateTimeExt(new System.DateTime(sysDate.Year, sysDate.Month, 1));
        }

        /// <summary>
        /// 结束时间（设置为当月的最后一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>当月的结束时间</returns>
        public static DateTimeExt EndOfMonth(DateTimeExt date)
        {
            var sysDate = (System.DateTime)date;
            return new DateTimeExt(new System.DateTime(sysDate.Year, sysDate.Month, System.DateTime.DaysInMonth(sysDate.Year, sysDate.Month)).AddDays(1).AddMilliseconds(-1));
        }

        /// <summary>
        /// 开始时间（设置为当年的第一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>当年的开始时间</returns>
        public static DateTimeExt BeginOfYear(DateTimeExt date)
        {
            var sysDate = (System.DateTime)date;
            return new DateTimeExt(new System.DateTime(sysDate.Year, 1, 1));
        }

        /// <summary>
        /// 结束时间（设置为当年的最后一天）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>当年的结束时间</returns>
        public static DateTimeExt EndOfYear(DateTimeExt date)
        {
            var sysDate = (System.DateTime)date;
            return new DateTimeExt(new System.DateTime(sysDate.Year, 12, 31).AddDays(1).AddMilliseconds(-1));
        }
    }
}