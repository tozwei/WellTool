// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 日期时间工具类
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
        /// 获取当前日期（不包含时间部分）
        /// </summary>
        /// <returns>当前日期</returns>
        public static DateTime Today()
        {
            return DateTime.Today;
        }

        /// <summary>
        /// 获取当前时间戳（毫秒）
        /// </summary>
        /// <returns>当前时间戳</returns>
        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        /// <summary>
        /// 将时间戳转换为日期时间
        /// </summary>
        /// <param name="timestamp">时间戳（毫秒）</param>
        /// <returns>日期时间</returns>
        public static DateTime FromTimestamp(long timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timestamp).ToLocalTime();
        }

        /// <summary>
        /// 将日期时间转换为时间戳
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>时间戳（毫秒）</returns>
        public static long ToTimestamp(DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        /// <summary>
        /// 格式化日期时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="format">格式</param>
        /// <returns>格式化后的字符串</returns>
        public static string Format(DateTime dateTime, string format)
        {
            return dateTime.ToString(format);
        }

        /// <summary>
        /// 解析字符串为日期时间
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="format">格式</param>
        /// <returns>日期时间</returns>
        public static DateTime Parse(string str, string format)
        {
            return DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 解析字符串为日期时间（尝试多种格式）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>日期时间</returns>
        public static DateTime Parse(string str)
        {
            return DateTime.Parse(str);
        }

        /// <summary>
        /// 获取指定日期的年
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年</returns>
        public static int Year(DateTime dateTime)
        {
            return dateTime.Year;
        }

        /// <summary>
        /// 获取指定日期的月
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>月</returns>
        public static int Month(DateTime dateTime)
        {
            return dateTime.Month;
        }

        /// <summary>
        /// 获取指定日期的日
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日</returns>
        public static int Day(DateTime dateTime)
        {
            return dateTime.Day;
        }

        /// <summary>
        /// 获取指定日期的时
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>时</returns>
        public static int Hour(DateTime dateTime)
        {
            return dateTime.Hour;
        }

        /// <summary>
        /// 获取指定日期的分
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>分</returns>
        public static int Minute(DateTime dateTime)
        {
            return dateTime.Minute;
        }

        /// <summary>
        /// 获取指定日期的秒
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>秒</returns>
        public static int Second(DateTime dateTime)
        {
            return dateTime.Second;
        }

        /// <summary>
        /// 获取指定日期的毫秒
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>毫秒</returns>
        public static int Millisecond(DateTime dateTime)
        {
            return dateTime.Millisecond;
        }

        /// <summary>
        /// 获取指定日期是星期几（1-7，1表示星期日）
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>星期几</returns>
        public static int DayOfWeek(DateTime dateTime)
        {
            int dayOfWeek = (int)dateTime.DayOfWeek;
            return dayOfWeek == 0 ? 7 : dayOfWeek;
        }

        /// <summary>
        /// 获取指定日期是当年的第几天
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>当年的第几天</returns>
        public static int DayOfYear(DateTime dateTime)
        {
            return dateTime.DayOfYear;
        }

        /// <summary>
        /// 计算两个日期之间的时间差
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>时间差</returns>
        public static TimeSpan Between(DateTime start, DateTime end)
        {
            return end - start;
        }

        /// <summary>
        /// 计算两个日期之间的天数差
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>天数差</returns>
        public static int BetweenDays(DateTime start, DateTime end)
        {
            return (int)(end.Date - start.Date).TotalDays;
        }

        /// <summary>
        /// 计算两个日期之间的小时差
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>小时差</returns>
        public static double BetweenHours(DateTime start, DateTime end)
        {
            return (end - start).TotalHours;
        }

        /// <summary>
        /// 计算两个日期之间的分钟差
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>分钟差</returns>
        public static double BetweenMinutes(DateTime start, DateTime end)
        {
            return (end - start).TotalMinutes;
        }

        /// <summary>
        /// 计算两个日期之间的秒数差
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>秒数差</returns>
        public static double BetweenSeconds(DateTime start, DateTime end)
        {
            return (end - start).TotalSeconds;
        }

        /// <summary>
        /// 计算两个日期之间的毫秒差
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>毫秒差</returns>
        public static double BetweenMillis(DateTime start, DateTime end)
        {
            return (end - start).TotalMilliseconds;
        }

        /// <summary>
        /// 向日期添加指定的年数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="years">年数</param>
        /// <returns>新的日期时间</returns>
        public static DateTime AddYears(DateTime dateTime, int years)
        {
            return dateTime.AddYears(years);
        }

        /// <summary>
        /// 向日期添加指定的月数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="months">月数</param>
        /// <returns>新的日期时间</returns>
        public static DateTime AddMonths(DateTime dateTime, int months)
        {
            return dateTime.AddMonths(months);
        }

        /// <summary>
        /// 向日期添加指定的天数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="days">天数</param>
        /// <returns>新的日期时间</returns>
        public static DateTime AddDays(DateTime dateTime, int days)
        {
            return dateTime.AddDays(days);
        }

        /// <summary>
        /// 向日期添加指定的小时数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="hours">小时数</param>
        /// <returns>新的日期时间</returns>
        public static DateTime AddHours(DateTime dateTime, int hours)
        {
            return dateTime.AddHours(hours);
        }

        /// <summary>
        /// 向日期添加指定的分钟数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="minutes">分钟数</param>
        /// <returns>新的日期时间</returns>
        public static DateTime AddMinutes(DateTime dateTime, int minutes)
        {
            return dateTime.AddMinutes(minutes);
        }

        /// <summary>
        /// 向日期添加指定的秒数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="seconds">秒数</param>
        /// <returns>新的日期时间</returns>
        public static DateTime AddSeconds(DateTime dateTime, int seconds)
        {
            return dateTime.AddSeconds(seconds);
        }

        /// <summary>
        /// 向日期添加指定的毫秒数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="milliseconds">毫秒数</param>
        /// <returns>新的日期时间</returns>
        public static DateTime AddMilliseconds(DateTime dateTime, int milliseconds)
        {
            return dateTime.AddMilliseconds(milliseconds);
        }

        /// <summary>
        /// 获取指定日期的开始时间（00:00:00）
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>当天的开始时间</returns>
        public static DateTime BeginOfDay(DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// 获取指定日期的结束时间（23:59:59.999）
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>当天的结束时间</returns>
        public static DateTime EndOfDay(DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// 获取指定日期所在月份的开始时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>当月的开始时间</returns>
        public static DateTime BeginOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// 获取指定日期所在月份的结束时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>当月的结束时间</returns>
        public static DateTime EndOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month), 23, 59, 59, 999);
        }

        /// <summary>
        /// 获取指定日期所在年份的开始时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>当年的开始时间</returns>
        public static DateTime BeginOfYear(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1);
        }

        /// <summary>
        /// 获取指定日期所在年份的结束时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>当年的结束时间</returns>
        public static DateTime EndOfYear(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 12, 31, 23, 59, 59, 999);
        }

        /// <summary>
        /// 获取指定日期所在星期的开始时间（默认星期一为一周的开始）
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>当周的开始时间</returns>
        public static DateTime BeginOfWeek(DateTime dateTime)
        {
            int dayOfWeek = (int)dateTime.DayOfWeek;
            if (dayOfWeek == 0) // 星期日
            {
                dayOfWeek = 7;
            }
            return dateTime.Date.AddDays(1 - dayOfWeek);
        }

        /// <summary>
        /// 获取指定日期所在星期的结束时间（默认星期日为一周的结束）
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>当周的结束时间</returns>
        public static DateTime EndOfWeek(DateTime dateTime)
        {
            int dayOfWeek = (int)dateTime.DayOfWeek;
            if (dayOfWeek == 0) // 星期日
            {
                dayOfWeek = 7;
            }
            return dateTime.Date.AddDays(7 - dayOfWeek).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
        }

        /// <summary>
        /// 判断指定日期是否是今天
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>是否是今天</returns>
        public static bool IsToday(DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today;
        }

        /// <summary>
        /// 判断指定日期是否是昨天
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>是否是昨天</returns>
        public static bool IsYesterday(DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today.AddDays(-1);
        }

        /// <summary>
        /// 判断指定日期是否是明天
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>是否是明天</returns>
        public static bool IsTomorrow(DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today.AddDays(1);
        }

        /// <summary>
        /// 判断指定日期是否在指定范围内
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>是否在范围内</returns>
        public static bool IsInRange(DateTime dateTime, DateTime start, DateTime end)
        {
            return dateTime >= start && dateTime <= end;
        }

        /// <summary>
        /// 获取两个日期之间的所有日期
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns>日期列表</returns>
        public static System.Collections.Generic.List<DateTime> DateRange(DateTime start, DateTime end)
        {
            var dates = new System.Collections.Generic.List<DateTime>();
            DateTime current = start.Date;
            while (current <= end.Date)
            {
                dates.Add(current);
                current = current.AddDays(1);
            }
            return dates;
        }

        /// <summary>
        /// 格式化时间间隔
        /// </summary>
        /// <param name="span">时间间隔</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatTimeSpan(TimeSpan span)
        {
            if (span.TotalDays >= 1)
            {
                return $"{span.Days}天{span.Hours}小时{span.Minutes}分钟{span.Seconds}秒";
            }
            else if (span.TotalHours >= 1)
            {
                return $"{span.Hours}小时{span.Minutes}分钟{span.Seconds}秒";
            }
            else if (span.TotalMinutes >= 1)
            {
                return $"{span.Minutes}分钟{span.Seconds}秒";
            }
            else
            {
                return $"{span.Seconds}秒{span.Milliseconds}毫秒";
            }
        }
    }
}
