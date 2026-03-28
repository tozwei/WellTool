using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Map
{
    /// <summary>
    /// Map工具类
    /// </summary>
    public static class MapUtil
    {
        /// <summary>
        /// 创建一个空的字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <returns>空字典</returns>
        public static Dictionary<K, V> Empty<K, V>()
        {
            return new Dictionary<K, V>();
        }

        /// <summary>
        /// 创建一个包含指定键值对的字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="pairs">键值对数组</param>
        /// <returns>字典</returns>
        public static Dictionary<K, V> Of<K, V>(params KeyValuePair<K, V>[] pairs)
        {
            var map = new Dictionary<K, V>();
            foreach (var pair in pairs)
            {
                map[pair.Key] = pair.Value;
            }
            return map;
        }

        /// <summary>
        /// 创建一个包含指定键值对的字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="keys">键数组</param>
        /// <param name="values">值数组</param>
        /// <returns>字典</returns>
        public static Dictionary<K, V> Of<K, V>(K[] keys, V[] values)
        {
            var map = new Dictionary<K, V>();
            for (int i = 0; i < keys.Length && i < values.Length; i++)
            {
                map[keys[i]] = values[i];
            }
            return map;
        }

        /// <summary>
        /// 检查字典是否为空
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>是否为空</returns>
        public static bool IsEmpty<K, V>(Dictionary<K, V> map)
        {
            return map == null || map.Count == 0;
        }

        /// <summary>
        /// 检查字典是否不为空
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>是否不为空</returns>
        public static bool IsNotEmpty<K, V>(Dictionary<K, V> map)
        {
            return !IsEmpty(map);
        }

        /// <summary>
        /// 获取字典的大小
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>字典大小</returns>
        public static int Size<K, V>(Dictionary<K, V> map)
        {
            return map?.Count ?? 0;
        }

        /// <summary>
        /// 检查字典是否包含指定键
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="key">键</param>
        /// <returns>是否包含</returns>
        public static bool ContainsKey<K, V>(Dictionary<K, V> map, K key)
        {
            return map?.ContainsKey(key) ?? false;
        }

        /// <summary>
        /// 检查字典是否包含指定值
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="value">值</param>
        /// <returns>是否包含</returns>
        public static bool ContainsValue<K, V>(Dictionary<K, V> map, V value)
        {
            return map?.ContainsValue(value) ?? false;
        }

        /// <summary>
        /// 获取字典中的值，如果键不存在则返回默认值
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static V Get<K, V>(Dictionary<K, V> map, K key, V defaultValue = default)
        {
            if (map == null)
            {
                return defaultValue;
            }
            if (map.TryGetValue(key, out var value) && value != null)
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// 设置字典中的值
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Put<K, V>(Dictionary<K, V> map, K key, V value)
        {
            map[key] = value;
        }

        /// <summary>
        /// 移除字典中的键值对
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="key">键</param>
        /// <returns>是否移除成功</returns>
        public static bool Remove<K, V>(Dictionary<K, V> map, K key)
        {
            return map?.Remove(key) ?? false;
        }

        /// <summary>
        /// 清空字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        public static void Clear<K, V>(Dictionary<K, V> map)
        {
            map?.Clear();
        }

        /// <summary>
        /// 获取字典的所有键
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>键集合</returns>
        public static ICollection<K> Keys<K, V>(Dictionary<K, V> map)
        {
            return map != null ? map.Keys.ToList() : new List<K>();
        }

        /// <summary>
        /// 获取字典的所有值
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>值集合</returns>
        public static ICollection<V> Values<K, V>(Dictionary<K, V> map)
        {
            return map != null ? map.Values.ToList() : new List<V>();
        }

        /// <summary>
        /// 获取字典的所有键值对
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>键值对集合</returns>
        public static ICollection<KeyValuePair<K, V>> Entries<K, V>(Dictionary<K, V> map)
        {
            return map?.ToList() ?? new List<KeyValuePair<K, V>>();
        }

        /// <summary>
        /// 合并两个字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map1">第一个字典</param>
        /// <param name="map2">第二个字典</param>
        /// <returns>合并后的字典</returns>
        public static Dictionary<K, V> Merge<K, V>(Dictionary<K, V> map1, Dictionary<K, V> map2)
        {
            var result = new Dictionary<K, V>();
            if (map1 != null)
            {
                foreach (var pair in map1)
                {
                    result[pair.Key] = pair.Value;
                }
            }
            if (map2 != null)
            {
                foreach (var pair in map2)
                {
                    result[pair.Key] = pair.Value;
                }
            }
            return result;
        }

        /// <summary>
        /// 克隆字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>克隆后的字典</returns>
        public static Dictionary<K, V> Clone<K, V>(Dictionary<K, V> map)
        {
            if (map == null)
            {
                return null;
            }
            return new Dictionary<K, V>(map);
        }

        /// <summary>
        /// 将字典转换为字符串
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>字符串表示</returns>
        public static string ToString<K, V>(Dictionary<K, V> map)
        {
            if (IsEmpty(map))
            {
                return "{}";
            }
            var entries = map.Select(pair => $"{pair.Key}={pair.Value}");
            return "{" + string.Join(", ", entries) + "}";
        }
    }
}