using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 线程安全的HashSet
    /// </summary>
    public class ConcurrentHashSet<T> : IReadOnlyCollection<T>, ICollection<T>
    {
        private readonly ConcurrentDictionary<T, bool> _dict;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConcurrentHashSet()
        {
            _dict = new ConcurrentDictionary<T, bool>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConcurrentHashSet(IEnumerable<T> collection)
        {
            _dict = new ConcurrentDictionary<T, bool>();
            foreach (var item in collection)
            {
                _dict.TryAdd(item, true);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConcurrentHashSet(IEqualityComparer<T> comparer)
        {
            _dict = new ConcurrentDictionary<T, bool>(comparer);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConcurrentHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            _dict = new ConcurrentDictionary<T, bool>(comparer);
            foreach (var item in collection)
            {
                _dict.TryAdd(item, true);
            }
        }

        /// <summary>
        /// 元素数量
        /// </summary>
        public int Count => _dict.Count;

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty => _dict.IsEmpty;

        /// <summary>
        /// 是否为只读
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// 添加元素
        /// </summary>
        public bool Add(T item)
        {
            return _dict.TryAdd(item, true);
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        /// <summary>
        /// 清空集合
        /// </summary>
        public void Clear()
        {
            _dict.Clear();
        }

        /// <summary>
        /// 是否包含元素
        /// </summary>
        public bool Contains(T item)
        {
            return _dict.ContainsKey(item);
        }

        /// <summary>
        /// 复制到数组
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("Array is too small");
            }

            foreach (var item in this)
            {
                array[arrayIndex++] = item;
            }
        }

        /// <summary>
        /// 移除元素
        /// </summary>
        public bool Remove(T item)
        {
            return _dict.TryRemove(item, out _);
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return _dict.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
