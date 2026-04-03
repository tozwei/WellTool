using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 列表分区或分段。通过传入分区长度，将指定列表分区为不同的块，每块区域的长度相同（最后一块可能小于长度）
    /// 分区是在原 List 的基础上进行的，返回的分区是不可变的抽象列表，原列表元素变更，分区中元素也会变更。
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class Partition<T> : IList<List<T>>
    {
        protected readonly IList<T> _list;
        protected readonly int _size;

        /// <summary>
        /// 列表分区
        /// </summary>
        /// <param name="list">被分区的列表，非空</param>
        /// <param name="size">每个分区的长度，必须大于0</param>
        public Partition(IList<T> list, int size)
        {
            _list = list ?? throw new ArgumentNullException(nameof(list));
            _size = Math.Min(list.Count, size);
        }

        /// <summary>
        /// 获取指定分区的元素
        /// </summary>
        public List<T> this[int index]
        {
            get
            {
                int start = index * _size;
                int end = Math.Min(start + _size, _list.Count);
                var result = new List<T>();
                for (int i = start; i < end; i++)
                {
                    result.Add(_list[i]);
                }
                return result;
            }
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// 获取分区个数
        /// </summary>
        public int Count
        {
            get
            {
                if (_size == 0)
                {
                    return 0;
                }
                int total = _list.Count;
                // 类似于判断余数，当总数非整份 size 时，多余的数 >=1，则相当于被除数多一个 size，做到 +1 目的
                return (total + _size - 1) / _size;
            }
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty => _list.Count == 0;

        /// <summary>
        /// 获取分区个数
        /// </summary>
        public int Size() => Count;

        /// <summary>
        /// 判断列表是否为空
        /// </summary>
        public bool IsListEmpty() => IsEmpty;

        /// <summary>
        /// 获取分区个数
        /// </summary>
        public int PartitionSize() => Count;

        public IEnumerator<List<T>> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(List<T> item) => throw new NotSupportedException();
        public void Clear() => throw new NotSupportedException();
        public bool Contains(List<T> item) => throw new NotSupportedException();
        public void CopyTo(List<T>[] array, int arrayIndex) => throw new NotSupportedException();
        public int IndexOf(List<T> item) => throw new NotSupportedException();
        public void Insert(int index, List<T> item) => throw new NotSupportedException();
        public bool Remove(List<T> item) => throw new NotSupportedException();
        public void RemoveAt(int index) => throw new NotSupportedException();
    }
}
