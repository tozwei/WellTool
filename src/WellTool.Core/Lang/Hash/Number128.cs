using System;

namespace WellTool.Core.Lang.Hash
{
    /// <summary>
    /// 128位数字表示，分高位和低位
    /// </summary>
    public class Number128
    {
        /// <summary>
        /// 低位值
        /// </summary>
        public long LowValue { get; set; }

        /// <summary>
        /// 高位值
        /// </summary>
        public long HighValue { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="lowValue">低位</param>
        /// <param name="highValue">高位</param>
        public Number128(long lowValue, long highValue)
        {
            LowValue = lowValue;
            HighValue = highValue;
        }

        /// <summary>
        /// 获取高低位数组，long[0]：低位，long[1]：高位
        /// </summary>
        /// <returns>高低位数组，long[0]：低位，long[1]：高位</returns>
        public long[] GetLongArray()
        {
            return new long[] { LowValue, HighValue };
        }

        /// <summary>
        /// 获取int值
        /// </summary>
        /// <returns>int值</returns>
        public int IntValue()
        {
            return (int)LongValue();
        }

        /// <summary>
        /// 获取long值
        /// </summary>
        /// <returns>long值</returns>
        public long LongValue()
        {
            return LowValue;
        }

        /// <summary>
        /// 获取float值
        /// </summary>
        /// <returns>float值</returns>
        public float FloatValue()
        {
            return LongValue();
        }

        /// <summary>
        /// 获取double值
        /// </summary>
        /// <returns>double值</returns>
        public double DoubleValue()
        {
            return LongValue();
        }

        /// <summary>
        /// 比较两个Number128是否相等
        /// </summary>
        /// <param name="obj">要比较的对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var number128 = (Number128)obj;
            return LowValue == number128.LowValue && HighValue == number128.HighValue;
        }

        /// <summary>
        /// 获取哈希码
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(LowValue, HighValue);
        }
    }
}