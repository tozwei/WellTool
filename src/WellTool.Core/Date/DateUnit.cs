using System;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 日期单位
    /// </summary>
    public enum DateUnit
    {
        /// <summary>
        /// 毫秒
        /// </summary>
        MILLISECOND,

        /// <summary>
        /// 秒
        /// </summary>
        SECOND,

        /// <summary>
        /// 分钟
        /// </summary>
        MINUTE,

        /// <summary>
        /// 小时
        /// </summary>
        HOUR,

        /// <summary>
        /// 天
        /// </summary>
        DAY,

        /// <summary>
        /// 周
        /// </summary>
        WEEK,

        /// <summary>
        /// 月
        /// </summary>
        MONTH,

        /// <summary>
        /// 年
        /// </summary>
        YEAR
    }

    /// <summary>
    /// 日期单位扩展方法
    /// </summary>
    public static class DateUnitExtensions
    {
        /// <summary>
        /// 获取单位对应的毫秒数
        /// </summary>
        /// <param name="unit">日期单位</param>
        /// <returns>毫秒数</returns>
        public static long GetMillis(this DateUnit unit)
        {
            switch (unit)
            {
                case DateUnit.MILLISECOND:
                    return 1;
                case DateUnit.SECOND:
                    return 1000;
                case DateUnit.MINUTE:
                    return 60 * 1000;
                case DateUnit.HOUR:
                    return 60 * 60 * 1000;
                case DateUnit.DAY:
                    return 24 * 60 * 60 * 1000;
                case DateUnit.WEEK:
                    return 7 * 24 * 60 * 60 * 1000;
                case DateUnit.MONTH:
                    return 30L * 24 * 60 * 60 * 1000; // 近似值
                case DateUnit.YEAR:
                    return 365L * 24 * 60 * 60 * 1000; // 近似值
                default:
                    throw new ArgumentException("Invalid date unit");
            }
        }
    }
}