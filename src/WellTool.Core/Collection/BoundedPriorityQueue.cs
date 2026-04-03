using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 有界优先队列，超过容量后自动移除最低优先级的元素
    /// </summary>
    public class BoundedPriorityQueue<T> : IEnumerable<T>
    {
        private readonly List<T> _heap = new List<T>();
        private readonly int _maxSize;
        private readonly IComparer<T> _comparer;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxSize">最大容量</param>
        public BoundedPriorityQueue(int maxSize) : this(maxSize, Comparer<T>.Default)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxSize">最大容量</param>
        /// <param name="comparer">比较器</param>
        public BoundedPriorityQueue(int maxSize, IComparer<T> comparer)
        {
            if (maxSize <= 0)
            {
                throw new ArgumentException("Max size must be greater than 0", nameof(maxSize));
            }
            _maxSize = maxSize;
            _comparer = comparer ?? Comparer<T>.Default;
        }

        /// <summary>
        /// 元素数量
        /// </summary>
        public int Count => _heap.Count;

        /// <summary>
        /// 最大容量
        /// </summary>
        public int MaxSize => _maxSize;

        /// <summary>
        /// 是否已满
        /// </summary>
        public bool IsFull => _heap.Count >= _maxSize;

        /// <summary>
        /// 添加元素
        /// </summary>
        public void Enqueue(T item)
        {
            if (_heap.Count >= _maxSize)
            {
                // 如果队列已满，检查是否需要替换
                if (_comparer.Compare(item, _heap[0]) <= 0)
                {
                    // 新元素不大于最小元素，忽略
                    return;
                }
                // 移除最小元素
                RemoveRoot();
            }

            // 添加新元素
            _heap.Add(item);
            HeapifyUp(_heap.Count - 1);
        }

        /// <summary>
        /// 移除并返回队首元素
        /// </summary>
        public T Dequeue()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            T result = _heap[0];
            RemoveRoot();
            return result;
        }

        /// <summary>
        /// 获取但不移除队首元素
        /// </summary>
        public T Peek()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
            return _heap[0];
        }

        /// <summary>
        /// 安全获取队首元素
        /// </summary>
        public bool TryPeek(out T result)
        {
            if (_heap.Count == 0)
            {
                result = default;
                return false;
            }
            result = _heap[0];
            return true;
        }

        /// <summary>
        /// 安全移除队首元素
        /// </summary>
        public bool TryDequeue(out T result)
        {
            if (_heap.Count == 0)
            {
                result = default;
                return false;
            }
            result = Dequeue();
            return true;
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        public void Clear()
        {
            _heap.Clear();
        }

        /// <summary>
        /// 是否包含元素
        /// </summary>
        public bool Contains(T item)
        {
            return _heap.Contains(item);
        }

        private void RemoveRoot()
        {
            if (_heap.Count == 1)
            {
                _heap.RemoveAt(0);
                return;
            }

            // 将最后一个元素移到根位置
            _heap[0] = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);

            // 向下堆化
            HeapifyDown(0);
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (_comparer.Compare(_heap[index], _heap[parent]) < 0)
                {
                    Swap(index, parent);
                    index = parent;
                }
                else
                {
                    break;
                }
            }
        }

        private void HeapifyDown(int index)
        {
            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int smallest = index;

                if (left < _heap.Count && _comparer.Compare(_heap[left], _heap[smallest]) < 0)
                {
                    smallest = left;
                }

                if (right < _heap.Count && _comparer.Compare(_heap[right], _heap[smallest]) < 0)
                {
                    smallest = right;
                }

                if (smallest != index)
                {
                    Swap(index, smallest);
                    index = smallest;
                }
                else
                {
                    break;
                }
            }
        }

        private void Swap(int i, int j)
        {
            T temp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = temp;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _heap.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
