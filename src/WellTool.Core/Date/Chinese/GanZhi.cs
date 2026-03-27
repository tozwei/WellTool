using System;

namespace WellTool.Core.Date.Chinese
{
    /// <summary>
    /// 天干地支类
    /// 天干地支，简称为干支
    /// </summary>
    public class GanZhi
    {
        /// <summary>
        /// 十天干：甲（jiǎ）、乙（yǐ）、丙（bǐng）、丁（dīng）、戊（wù）、己（jǐ）、庚（gēng）、辛（xīn）、壬（rén）、癸（guǐ）
        /// 十二地支：子（zǐ）、丑（chǒu）、寅（yín）、卯（mǎo）、辰（chén）、巳（sì）、午（wǔ）、未（wèi）、申（shēn）、酉（yǒu）、戌（xū）、亥（hài）
        /// 十二地支对应十二生肖:子-鼠，丑-牛，寅-虎，卯-兔，辰-龙，巳-蛇， 午-马，未-羊，申-猴，酉-鸡，戌-狗，亥-猪
        /// </summary>
        private static readonly string[] GAN = new string[] { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        private static readonly string[] ZHI = new string[] { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };

        /// <summary>
        /// 传入 月日的offset 传回干支, 0=甲子
        /// </summary>
        /// <param name="num">月日的offset</param>
        /// <returns>干支</returns>
        public static string Cyclicalm(int num)
        {
            return (GAN[num % 10] + ZHI[num % 12]);
        }

        /// <summary>
        /// 传入年传回干支
        /// </summary>
        /// <param name="year">农历年</param>
        /// <returns>干支</returns>
        public static string GetGanzhiOfYear(int year)
        {
            // 1864年（1900 - 36）是甲子年，用于计算基准的干支年
            return Cyclicalm(year - LunarInfo.BASE_YEAR + 36);
        }

        /// <summary>
        /// 获取干支月
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="month">公历月，从1开始</param>
        /// <param name="day">公历日</param>
        /// <returns>干支月</returns>
        public static string GetGanzhiOfMonth(int year, int month, int day)
        {
            //返回当月「节」为几日开始
            int firstNode = SolarTerms.GetTerm(year, (month * 2 - 1));
            // 依据12节气修正干支月
            int monthOffset = (year - LunarInfo.BASE_YEAR) * 12 + month + 11;
            if (day >= firstNode)
            {
                monthOffset++;
            }
            return Cyclicalm(monthOffset);
        }

        /// <summary>
        /// 获取干支日
        /// </summary>
        /// <param name="year">公历年</param>
        /// <param name="month">公历月，从1开始</param>
        /// <param name="day">公历日</param>
        /// <returns>干支</returns>
        public static string GetGanzhiOfDay(int year, int month, int day)
        {
            // 与1970-01-01相差天数，不包括当天
            var date = new DateTime(year, month, day);
            var epoch = new DateTime(1970, 1, 1);
            var days = (date - epoch).Days;
            //1899-12-21是农历1899年腊月甲子日  41：相差1900-01-31有41天
            return Cyclicalm((int)(days - LunarInfo.BASE_DAY + 41));
        }
    }
}