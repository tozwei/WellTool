using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 分区迭代器
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class PartitionIter<T> : IEnumerable<List<T>>, IDisposable
    {
        private readonly IList<T> _list;
        private readonly int _size;
        private bool _disposed = false;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="list">被分区的列表</param>
        /// <param name="size">每个分区的长度</param>
        public PartitionIter(IList<T> list, int size)
        {
            _list = list ?? throw new ArgumentNullException(nameof(list));
            _size = size > 0 ? size : 1;
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        public IEnumerator<List<T>> GetEnumerator()
        {
            for (int i = 0; i < _list.Count; i += _size)
            {
                int end = System.Math.Min(i + _size, _list.Count);
                var partition = new List<T>();
                for (int j = i; j < end; j++)
                {
                    partition.Add(_list[j]);
                }
                yield return partition;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}
