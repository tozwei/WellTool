using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Map
{
    /// <summary>
    /// 固定大小的 LinkedHashMap
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public class FixedLinkedHashMap<K, V> : IDictionary<K, V>, IEnumerable<KeyValuePair<K, V>>
    {
        private readonly int _capacity;
        private readonly LinkedList<KeyValuePair<K, V>> _linkedList;
        private readonly Dictionary<K, LinkedListNode<KeyValuePair<K, V>>> _dictionary;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">容量</param>
        public FixedLinkedHashMap(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Capacity must be greater than 0");
            }

            _capacity = capacity;
            _linkedList = new LinkedList<KeyValuePair<K, V>>();
            _dictionary = new Dictionary<K, LinkedListNode<KeyValuePair<K, V>>>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">容量</param>
        /// <param name="comparer">键比较器</param>
        public FixedLinkedHashMap(int capacity, IEqualityComparer<K> comparer)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Capacity must be greater than 0");
            }

            _capacity = capacity;
            _linkedList = new LinkedList<KeyValuePair<K, V>>();
            _dictionary = new Dictionary<K, LinkedListNode<KeyValuePair<K, V>>>(comparer);
        }

        /// <summary>
        /// 获取或设置值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public V this[K key]
        {
            get
            {
                if (_dictionary.TryGetValue(key, out var node))
                {
                    // 移动到链表头部
                    _linkedList.Remove(node);
                    _linkedList.AddFirst(node);
                    return node.Value.Value;
                }
                throw new KeyNotFoundException();
            }
            set
            {
                if (_dictionary.TryGetValue(key, out var node))
                {
                    // 更新值并移动到链表头部
                    node.Value = new KeyValuePair<K, V>(key, value);
                    _linkedList.Remove(node);
                    _linkedList.AddFirst(node);
                }
                else
                {
                    // 添加新元素
                    var newNode = new LinkedListNode<KeyValuePair<K, V>>(new KeyValuePair<K, V>(key, value));
                    _linkedList.AddFirst(newNode);
                    _dictionary[key] = newNode;

                    // 检查容量
                    if (_dictionary.Count > _capacity)
                    {
                        // 移除最旧的元素
                        var lastNode = _linkedList.Last;
                        _linkedList.RemoveLast();
                        _dictionary.Remove(lastNode.Value.Key);
                    }
                }
            }
        }

        /// <summary>
        /// 获取键集合
        /// </summary>
        public ICollection<K> Keys
        {
            get
            {
                var keys = new List<K>();
                foreach (var node in _linkedList)
                {
                    keys.Add(node.Key);
                }
                return keys;
            }
        }

        /// <summary>
        /// 获取值集合
        /// </summary>
        public ICollection<V> Values
        {
            get
            {
                var values = new List<V>();
                foreach (var node in _linkedList)
                {
                    values.Add(node.Value);
                }
                return values;
            }
        }

        /// <summary>
        /// 获取元素数量
        /// </summary>
        public int Count => _dictionary.Count;

        /// <summary>
        /// 是否为只读
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="item">键值对</param>
        public void Add(KeyValuePair<K, V> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Add(K key, V value)
        {
            if (_dictionary.ContainsKey(key))
            {
                throw new ArgumentException("An item with the same key has already been added");
            }

            var newNode = new LinkedListNode<KeyValuePair<K, V>>(new KeyValuePair<K, V>(key, value));
            _linkedList.AddFirst(newNode);
            _dictionary[key] = newNode;

            // 检查容量
            if (_dictionary.Count > _capacity)
            {
                // 移除最旧的元素
                var lastNode = _linkedList.Last;
                _linkedList.RemoveLast();
                _dictionary.Remove(lastNode.Value.Key);
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _linkedList.Clear();
            _dictionary.Clear();
        }

        /// <summary>
        /// 检查是否包含键值对
        /// </summary>
        /// <param name="item">键值对</param>
        /// <returns>是否包含</returns>
        public bool Contains(KeyValuePair<K, V> item)
        {
            return _dictionary.TryGetValue(item.Key, out var node) && 
                   EqualityComparer<V>.Default.Equals(node.Value.Value, item.Value);
        }

        /// <summary>
        /// 检查是否包含键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否包含</returns>
        public bool ContainsKey(K key)
        {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// 复制到数组
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="arrayIndex">数组索引</param>
        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            _linkedList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return _linkedList.GetEnumerator();
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 移除键值对
        /// </summary>
        /// <param name="item">键值对</param>
        /// <returns>是否移除成功</returns>
        public bool Remove(KeyValuePair<K, V> item)
        {
            if (Contains(item))
            {
                return Remove(item.Key);
            }
            return false;
        }

        /// <summary>
        /// 移除键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否移除成功</returns>
        public bool Remove(K key)
        {
            if (_dictionary.TryGetValue(key, out var node))
            {
                _linkedList.Remove(node);
                return _dictionary.Remove(key);
            }
            return false;
        }

        /// <summary>
        /// 尝试获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>是否获取成功</returns>
        public bool TryGetValue(K key, out V value)
        {
            if (_dictionary.TryGetValue(key, out var node))
            {
                // 移动到链表头部
                _linkedList.Remove(node);
                _linkedList.AddFirst(node);
                value = node.Value.Value;
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// 获取容量
        /// </summary>
        public int Capacity => _capacity;

        /// <summary>
        /// 获取最旧的键值对
        /// </summary>
        /// <returns>最旧的键值对</returns>
        public KeyValuePair<K, V>? GetOldest()
        {
            if (_linkedList.Count > 0)
            {
                return _linkedList.Last.Value;
            }
            return null;
        }

        /// <summary>
        /// 获取最新的键值对
        /// </summary>
        /// <returns>最新的键值对</returns>
        public KeyValuePair<K, V>? GetNewest()
        {
            if (_linkedList.Count > 0)
            {
                return _linkedList.First.Value;
            }
            return null;
        }
    }
}