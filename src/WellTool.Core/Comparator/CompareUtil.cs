using System;
using System.Collections.Generic;
using System.Globalization;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 比较工具类
    /// </summary>
    public static class CompareUtil
    {
        /// <summary>
        /// 比较两个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="a">第一个对象</param>
        /// <param name="b">第二个对象</param>
        /// <returns>比较结果：a &lt; b 返回负数，a == b 返回0，a &gt; b 返回正数</returns>
        public static int Compare<T>(T a, T b)
        {
            return Compare(a, b, false);
        }

        /// <summary>
        /// 比较两个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="a">第一个对象</param>
        /// <param name="b">第二个对象</param>
        /// <param name="nullGreater">null是否大于非null</param>
        /// <returns>比较结果：a &lt; b 返回负数，a == b 返回0，a &gt; b 返回正数</returns>
        public static int Compare<T>(T a, T b, bool nullGreater)
        {
            if (a == null && b == null)
            {
                return 0;
            }
            if (a == null)
            {
                return nullGreater ? 1 : -1;
            }
            if (b == null)
            {
                return nullGreater ? -1 : 1;
            }
            if (a is IComparable<T> comparable)
            {
                return comparable.CompareTo(b);
            }
            if (a is IComparable oldComparable)
            {
                return oldComparable.CompareTo(b);
            }
            return string.Compare(a.ToString(), b.ToString(), StringComparison.Ordinal);
        }

        /// <summary>
        /// 根据拼音比较字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="keyExtractor">键提取器</param>
        /// <param name="desc">是否降序</param>
        /// <returns>比较器</returns>
        public static Comparison<T> ComparingPinyin<T>(Func<T, string> keyExtractor, bool desc = false)
        {
            var comparer = StringComparer.Create(CultureInfo.CurrentCulture, true);
            return (a, b) => {
                var keyA = keyExtractor(a);
                var keyB = keyExtractor(b);
                var result = comparer.Compare(keyA, keyB);
                return desc ? -result : result;
            };
        }

        /// <summary>
        /// 比较两个对象是否相等
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="a">第一个对象</param>
        /// <param name="b">第二个对象</param>
        /// <returns>是否相等</returns>
        public static bool Equals<T>(T a, T b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null)
            {
                return false;
            }
            return a.Equals(b);
        }

        /// <summary>
        /// 获取对象的哈希码
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>哈希码</returns>
        public static int GetHashCode<T>(T obj)
        {
            return obj?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// 检查对象是否在指定范围内
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">要检查的对象</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>是否在范围内</returns>
        public static bool Between<T>(T value, T min, T max) where T : IComparable<T>
        {
            return Compare(value, min) >= 0 && Compare(value, max) <= 0;
        }

        /// <summary>
        /// 获取两个对象中的较小值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="a">第一个对象</param>
        /// <param name="b">第二个对象</param>
        /// <returns>较小值</returns>
        public static T Min<T>(T a, T b) where T : IComparable<T>
        {
            return Compare(a, b) <= 0 ? a : b;
        }

        /// <summary>
        /// 获取两个对象中的较大值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="a">第一个对象</param>
        /// <param name="b">第二个对象</param>
        /// <returns>较大值</returns>
        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return Compare(a, b) >= 0 ? a : b;
        }

        /// <summary>
        /// 限制对象在指定范围内
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">要限制的对象</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>限制后的值</returns>
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (Compare(value, min) < 0)
            {
                return min;
            }
            if (Compare(value, max) > 0)
            {
                return max;
            }
            return value;
        }
    }
}