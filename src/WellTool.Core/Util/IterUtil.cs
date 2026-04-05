using System;
using System.Collections.Generic;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 迭代工具类
    /// </summary>
    public static class IterUtil
    {
        /// <summary>
        /// 将两个列表转换为字典
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="keys">键列表</param>
        /// <param name="values">值列表</param>
        /// <param name="ignoreNullValue">是否忽略null值</param>
        /// <returns>字典</returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(IList<TKey> keys, IList<TValue> values, bool ignoreNullValue = false)
        {
            var dictionary = new Dictionary<TKey, TValue>();
            if (keys == null || values == null)
            {
                return dictionary;
            }

            int minCount = System.Math.Min(keys.Count, values.Count);
            for (int i = 0; i < minCount; i++)
            {
                var key = keys[i];
                var value = values[i];
                if (!ignoreNullValue || value != null)
                {
                    dictionary[key] = value;
                }
            }
            return dictionary;
        }
    }
}