using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 唯一键集合，用于存储键值对并保证键的唯一性
    /// </summary>
    public class UniqueKeySet<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly Dictionary<TKey, TValue> _dict;
        private readonly HashSet<TKey> _keys;
        private readonly IEqualityComparer<TKey> _keyComparer;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UniqueKeySet() : this(EqualityComparer<TKey>.Default)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UniqueKeySet(IEqualityComparer<TKey> keyComparer)
        {
            _keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
            _dict = new Dictionary<TKey, TValue>(_keyComparer);
            _keys = new HashSet<TKey>(_keyComparer);
        }

        /// <summary>
        /// 元素数量
        /// </summary>
        public int Count => _dict.Count;

        /// <summary>
        /// 添加元素
        /// </summary>
        public bool Add(TKey key, TValue value)
        {
            if (_keys.Contains(key))
            {
                return false;
            }

            _dict.Add(key, value);
            _keys.Add(key);
            return true;
        }

        /// <summary>
        /// 添加或更新元素
        /// </summary>
        public void AddOrUpdate(TKey key, TValue value)
        {
            if (_keys.Contains(key))
            {
                _dict[key] = value;
            }
            else
            {
                _dict.Add(key, value);
                _keys.Add(key);
            }
        }

        /// <summary>
        /// 是否包含键
        /// </summary>
        public bool ContainsKey(TKey key)
        {
            return _keys.Contains(key);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dict.TryGetValue(key, out value);
        }

        /// <summary>
        /// 移除元素
        /// </summary>
        public bool Remove(TKey key)
        {
            if (_keys.Contains(key))
            {
                _keys.Remove(key);
                return _dict.Remove(key);
            }
            return false;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _dict.Clear();
            _keys.Clear();
        }

        /// <summary>
        /// 获取所有键
        /// </summary>
        public IEnumerable<TKey> Keys => _keys;

        /// <summary>
        /// 获取所有值
        /// </summary>
        public IEnumerable<TValue> Values => _dict.Values;

        /// <summary>
        /// 获取枚举器
        /// </summary>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 获取或添加值
        /// </summary>
        public TValue GetOrAdd(TKey key, TValue value)
        {
            if (_dict.TryGetValue(key, out var existing))
            {
                return existing;
            }

            _dict.Add(key, value);
            _keys.Add(key);
            return value;
        }

        /// <summary>
        /// 获取或添加值（使用工厂方法）
        /// </summary>
        public TValue GetOrAdd(TKey key, Func<TValue> factory)
        {
            if (_dict.TryGetValue(key, out var existing))
            {
                return existing;
            }

            var value = factory();
            _dict.Add(key, value);
            _keys.Add(key);
            return value;
        }
    }
}
