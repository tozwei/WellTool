using System;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 环形索引工具类
    /// </summary>
    public static class RingIndexUtil
    {
        /// <summary>
        /// 获取环形数组中的下一个索引位置
        /// </summary>
        /// <param name="currentIndex">当前索引</param>
        /// <param name="total">总数</param>
        /// <returns>下一个索引</returns>
        public static int Next(int currentIndex, int total)
        {
            if (total <= 0)
            {
                throw new ArgumentException("Total must be greater than 0", nameof(total));
            }
            return (currentIndex + 1) % total;
        }

        /// <summary>
        /// 获取环形数组中的上一个索引位置
        /// </summary>
        /// <param name="currentIndex">当前索引</param>
        /// <param name="total">总数</param>
        /// <returns>上一个索引</returns>
        public static int Previous(int currentIndex, int total)
        {
            if (total <= 0)
            {
                throw new ArgumentException("Total must be greater than 0", nameof(total));
            }
            return (currentIndex - 1 + total) % total;
        }

        /// <summary>
        /// 获取环形数组中的指定偏移位置的索引
        /// </summary>
        /// <param name="currentIndex">当前索引</param>
        /// <param name="offset">偏移量，正数向后，负数向前</param>
        /// <param name="total">总数</param>
        /// <returns>计算后的索引</returns>
        public static int Offset(int currentIndex, int offset, int total)
        {
            if (total <= 0)
            {
                throw new ArgumentException("Total must be greater than 0", nameof(total));
            }
            int result = (currentIndex + offset) % total;
            if (result < 0)
            {
                result += total;
            }
            return result;
        }
    }
}
