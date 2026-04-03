using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WellTool.Core.Streams
{
    /// <summary>
    /// 可变的汇聚操作 Collector 相关工具封装
    /// </summary>
    public static class CollectorUtil
    {
        /// <summary>
        /// 提供任意对象的 Join 操作的汇聚器实现
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>结果字符串</returns>
        public static string Joining<T>(IEnumerable<T> source, string delimiter)
        {
            return Joining(source, delimiter, obj => obj?.ToString() ?? string.Empty);
        }

        /// <summary>
        /// 提供任意对象的 Join 操作的汇聚器实现
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="delimiter">分隔符</param>
        /// <param name="toStringFunc">自定义指定对象转换为字符串的方法</param>
        /// <returns>结果字符串</returns>
        public static string Joining<T>(IEnumerable<T> source, string delimiter, Func<T, string> toStringFunc)
        {
            var sb = new StringBuilder();
            bool first = true;
            foreach (var item in source)
            {
                if (!first && delimiter != null)
                {
                    sb.Append(delimiter);
                }
                sb.Append(toStringFunc(item));
                first = false;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 提供对 null 值友好的 Grouping 操作的汇聚器实现
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="K">分组键类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="classifier">分组依据</param>
        /// <returns>分组后的字典</returns>
        public static Dictionary<K, List<T>> GroupingBy<T, K>(IEnumerable<T> source, Func<T, K> classifier)
        {
            var result = new Dictionary<K, List<T>>();
            foreach (var item in source)
            {
                var key = item != null ? classifier(item) : default(K);
                if (!result.ContainsKey(key))
                {
                    result[key] = new List<T>();
                }
                result[key].Add(item);
            }
            return result;
        }

        /// <summary>
        /// 对 null 友好的 toMap 操作的汇聚器实现
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="K">map 中 key 的类型</typeparam>
        /// <typeparam name="V">map 中 value 的类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="keyMapper">指定 map 中的 key</param>
        /// <param name="valueMapper">指定 map 中的 value</param>
        /// <returns>结果字典</returns>
        public static Dictionary<K, V> ToMap<T, K, V>(IEnumerable<T> source, Func<T, K> keyMapper, Func<T, V> valueMapper)
        {
            return ToMap(source, keyMapper, valueMapper, (v1, v2) => v2);
        }

        /// <summary>
        /// 对 null 友好的 toMap 操作的汇聚器实现
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="K">map 中 key 的类型</typeparam>
        /// <typeparam name="V">map 中 value 的类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="keyMapper">指定 map 中的 key</param>
        /// <param name="valueMapper">指定 map 中的 value</param>
        /// <param name="mergeFunction">合并前对 value 进行的操作</param>
        /// <returns>结果字典</returns>
        public static Dictionary<K, V> ToMap<T, K, V>(
            IEnumerable<T> source,
            Func<T, K> keyMapper,
            Func<T, V> valueMapper,
            Func<V, V, V> mergeFunction)
        {
            var result = new Dictionary<K, V>();
            foreach (var item in source)
            {
                var key = item != null ? keyMapper(item) : default(K);
                var value = item != null ? valueMapper(item) : default(V);
                if (result.ContainsKey(key))
                {
                    result[key] = mergeFunction(result[key], value);
                }
                else
                {
                    result[key] = value;
                }
            }
            return result;
        }
    }
}
