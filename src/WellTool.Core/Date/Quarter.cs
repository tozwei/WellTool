using System;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 季度枚举
    /// </summary>
    public enum Quarter
    {
        /// <summary>
        /// 第一季度
        /// </summary>
        Q1 = 1,
        /// <summary>
        /// 第二季度
        /// </summary>
        Q2 = 2,
        /// <summary>
        /// 第三季度
        /// </summary>
        Q3 = 3,
        /// <summary>
        /// 第四季度
        /// </summary>
        Q4 = 4
    }

    public static class QuarterExtensions
    {
        /// <summary>
        /// 将季度int转换为Quarter枚举对象
        /// </summary>
        /// <param name="intValue">季度int表示</param>
        /// <returns>Quarter枚举对象</returns>
        public static Quarter? Of(int intValue)
        {
            switch (intValue)
            {
                case 1:
                    return Quarter.Q1;
                case 2:
                    return Quarter.Q2;
                case 3:
                    return Quarter.Q3;
                case 4:
                    return Quarter.Q4;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 根据给定的月份值返回对应的季度
        /// </summary>
        /// <param name="monthValue">月份值，取值范围为1到12</param>
        /// <returns>对应的季度</returns>
        public static Quarter FromMonth(int monthValue)
        {
            if (monthValue < 1 || monthValue > 12)
            {
                throw new ArgumentException("Invalid month value, must be between 1 and 12");
            }
            return Of((monthValue - 1) / 3 + 1).Value;
        }

        /// <summary>
        /// 该季度的第一个月
        /// </summary>
        /// <param name="quarter">季度</param>
        /// <returns>第一个月</returns>
        public static int FirstMonthValue(this Quarter quarter)
        {
            return ((int)quarter - 1) * 3 + 1;
        }

        /// <summary>
        /// 该季度的最后一个月
        /// </summary>
        /// <param name="quarter">季度</param>
        /// <returns>最后一个月</returns>
        public static int LastMonthValue(this Quarter quarter)
        {
            return (int)quarter * 3;
        }
    }
}