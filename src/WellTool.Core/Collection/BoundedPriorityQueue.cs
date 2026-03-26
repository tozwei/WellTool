using System;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 有界优先队列
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class BoundedPriorityQueue<T>
    {
#if NET6_0_OR_GREATER
        private readonly PriorityQueue<T, T> _queue;
#else
        // .NET Standard 2.1 实现
        private readonly List<T> _queue;
#endif
        private readonly int _capacity;
        private readonly IComparer<T> _comparer;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">队列容量</param>
        /// <param name="comparer">比较器</param>
        public BoundedPriorityQueue(int capacity, IComparer<T> comparer = null)
        {
            _capacity = capacity;
            _comparer = comparer ?? Comparer<T>.Default;
#if NET6_0_OR_GREATER
            _queue = new PriorityQueue<T, T>(_comparer);
#else
            _queue = new List<T>();
#endif
        }

        /// <summary>
        /// 队列大小
        /// </summary>
        public int Count
        {
            get
            {
#if NET6_0_OR_GREATER
                return _queue.Count;
#else
                return _queue.Count;
#endif
            }
        }

        /// <summary>
        /// 队列容量
        /// </summary>
        public int Capacity => _capacity;

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>是否成功入队</returns>
        public bool Enqueue(T item)
        {
#if NET6_0_OR_GREATER
            if (_queue.Count < _capacity)
            {
                _queue.Enqueue(item, item);
                return true;
            }

            if (_comparer.Compare(item, _queue.Peek()) > 0)
            {
                _queue.Dequeue();
                _queue.Enqueue(item, item);
                return true;
            }
#else
            if (_queue.Count < _capacity)
            {
                _queue.Add(item);
                _queue.Sort(_comparer);
                return true;
            }

            if (_comparer.Compare(item, _queue[0]) > 0)
            {
                _queue.RemoveAt(0);
                _queue.Add(item);
                _queue.Sort(_comparer);
                return true;
            }
#endif

            return false;
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <returns>出队元素</returns>
        public T Dequeue()
        {
#if NET6_0_OR_GREATER
            return _queue.Dequeue();
#else
            if (_queue.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
            var item = _queue[0];
            _queue.RemoveAt(0);
            return item;
#endif
        }

        /// <summary>
        /// 查看队首元素
        /// </summary>
        /// <returns>队首元素</returns>
        public T Peek()
        {
#if NET6_0_OR_GREATER
            return _queue.Peek();
#else
            if (_queue.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
            return _queue[0];
#endif
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        public void Clear()
        {
#if NET6_0_OR_GREATER
            _queue.Clear();
#else
            _queue.Clear();
#endif
        }

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <returns>数组</returns>
        public T[] ToArray()
        {
#if NET6_0_OR_GREATER
            var list = new List<T>();
            while (_queue.Count > 0)
            {
                list.Add(_queue.Dequeue());
            }
            var array = list.ToArray();
            // 重新入队
            foreach (var item in array)
            {
                _queue.Enqueue(item, item);
            }
            Array.Sort(array, _comparer);
            Array.Reverse(array);
            return array;
#else
            var array = _queue.ToArray();
            Array.Sort(array, _comparer);
            Array.Reverse(array);
            return array;
#endif
        }

        /// <summary>
        /// 转换为列表
        /// </summary>
        /// <returns>列表</returns>
        public List<T> ToList()
        {
            return new List<T>(ToArray());
        }
    }
}