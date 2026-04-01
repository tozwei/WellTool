using System;
using System.Collections.Generic;

namespace WellTool.Core.Map
{
    /// <summary>
    /// Map 构建器
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public class MapBuilder<K, V>
    {
        private readonly Dictionary<K, V> _map;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MapBuilder()
        {
            _map = new Dictionary<K, V>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">初始容量</param>
        public MapBuilder(int capacity)
        {
            _map = new Dictionary<K, V>(capacity);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comparer">键比较器</param>
        public MapBuilder(IEqualityComparer<K> comparer)
        {
            _map = new Dictionary<K, V>(comparer);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictionary">字典</param>
        public MapBuilder(IDictionary<K, V> dictionary)
        {
            _map = new Dictionary<K, V>(dictionary);
        }

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>当前实例</returns>
        public MapBuilder<K, V> Put(K key, V value)
        {
            _map[key] = value;
            return this;
        }

        /// <summary>
        /// 批量添加键值对
        /// </summary>
        /// <param name="dictionary">字典</param>
        /// <returns>当前实例</returns>
        public MapBuilder<K, V> PutAll(IDictionary<K, V> dictionary)
        {
            foreach (var entry in dictionary)
            {
                _map[entry.Key] = entry.Value;
            }
            return this;
        }

        /// <summary>
        /// 批量添加键值对
        /// </summary>
        /// <param name="pairs">键值对数组</param>
        /// <returns>当前实例</returns>
        public MapBuilder<K, V> PutAll(params KeyValuePair<K, V>[] pairs)
        {
            foreach (var pair in pairs)
            {
                _map[pair.Key] = pair.Value;
            }
            return this;
        }

        /// <summary>
        /// 移除键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>当前实例</returns>
        public MapBuilder<K, V> Remove(K key)
        {
            _map.Remove(key);
            return this;
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <returns>当前实例</returns>
        public MapBuilder<K, V> Clear()
        {
            _map.Clear();
            return this;
        }

        /// <summary>
        /// 构建 Map
        /// </summary>
        /// <returns>Map</returns>
        public Dictionary<K, V> Build()
        {
            return _map;
        }

        /// <summary>
        /// 构建不可变 Map
        /// </summary>
        /// <returns>不可变 Map</returns>
        public IReadOnlyDictionary<K, V> BuildImmutable()
        {
            return new Dictionary<K, V>(_map);
        }

        /// <summary>
        /// 创建 MapBuilder 实例
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <returns>MapBuilder 实例</returns>
        public static MapBuilder<K, V> Create<K, V>()
        {
            return new MapBuilder<K, V>();
        }

        /// <summary>
        /// 创建 MapBuilder 实例
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="capacity">初始容量</param>
        /// <returns>MapBuilder 实例</returns>
        public static MapBuilder<K, V> Create<K, V>(int capacity)
        {
            return new MapBuilder<K, V>(capacity);
        }

        /// <summary>
        /// 创建 MapBuilder 实例
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="comparer">键比较器</param>
        /// <returns>MapBuilder 实例</returns>
        public static MapBuilder<K, V> Create<K, V>(IEqualityComparer<K> comparer)
        {
            return new MapBuilder<K, V>(comparer);
        }

        /// <summary>
        /// 创建 MapBuilder 实例
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <returns>MapBuilder 实例</returns>
        public static MapBuilder<K, V> Create<K, V>(IDictionary<K, V> dictionary)
        {
            return new MapBuilder<K, V>(dictionary);
        }
    }

    /// <summary>
    /// MapBuilder 扩展方法
    /// </summary>
    public static class MapBuilderExtensions
    {
        /// <summary>
        /// 转换为 MapBuilder
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <returns>MapBuilder</returns>
        public static MapBuilder<K, V> ToMapBuilder<K, V>(this IDictionary<K, V> dictionary)
        {
            return new MapBuilder<K, V>(dictionary);
        }
    }
}