using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 并发哈希集合
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class ConcurrentHashSet<T> : ICollection<T>, IReadOnlyCollection<T>
    {
        private readonly HashSet<T> _set;
        private readonly ReaderWriterLockSlim _lock;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConcurrentHashSet()
        {
            _set = new HashSet<T>();
            _lock = new ReaderWriterLockSlim();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comparer">比较器</param>
        public ConcurrentHashSet(IEqualityComparer<T> comparer)
        {
            _set = new HashSet<T>(comparer);
            _lock = new ReaderWriterLockSlim();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="collection">集合</param>
        public ConcurrentHashSet(IEnumerable<T> collection)
        {
            _set = new HashSet<T>(collection);
            _lock = new ReaderWriterLockSlim();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="comparer">比较器</param>
        public ConcurrentHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            _set = new HashSet<T>(collection, comparer);
            _lock = new ReaderWriterLockSlim();
        }

        /// <summary>
        /// 集合大小
        /// </summary>
        public int Count
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _set.Count;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>是否添加成功</returns>
        public bool Add(T item)
        {
            _lock.EnterWriteLock();
            try
            {
                return _set.Add(item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="item">元素</param>
        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        /// <summary>
        /// 清空集合
        /// </summary>
        public void Clear()
        {
            _lock.EnterWriteLock();
            try
            {
                _set.Clear();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 检查是否包含元素
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>是否包含</returns>
        public bool Contains(T item)
        {
            _lock.EnterReadLock();
            try
            {
                return _set.Contains(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 复制到数组
        /// </summary>
        /// <param name="array">目标数组</param>
        /// <param name="arrayIndex">开始索引</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _lock.EnterReadLock();
            try
            {
                _set.CopyTo(array, arrayIndex);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 移除元素
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>是否移除成功</returns>
        public bool Remove(T item)
        {
            _lock.EnterWriteLock();
            try
            {
                return _set.Remove(item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns>迭代器</returns>
        public IEnumerator<T> GetEnumerator()
        {
            _lock.EnterReadLock();
            try
            {
                return new List<T>(_set).GetEnumerator();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns>迭代器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <returns>数组</returns>
        public T[] ToArray()
        {
            _lock.EnterReadLock();
            try
            {
                var array = new T[_set.Count];
                _set.CopyTo(array);
                return array;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 转换为列表
        /// </summary>
        /// <returns>列表</returns>
        public List<T> ToList()
        {
            _lock.EnterReadLock();
            try
            {
                return new List<T>(_set);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}