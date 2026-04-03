using System;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 平均分区，用于将集合均匀分割
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class AvgPartition<T> : Partition<T>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="list">被分区的列表</param>
        /// <param name="size">每个分区的长度</param>
        public AvgPartition(IList<T> list, int size)
            : base(list, size)
        {
        }

        /// <summary>
        /// 获取分区个数
        /// </summary>
        public new int Count
        {
            get
            {
                if (_size == 0)
                {
                    return 0;
                }
                int total = _list.Count;
                // 向上取整
                return (total + _size - 1) / _size;
            }
        }
    }

    /// <summary>
    /// 随机访问平均分区
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class RandomAccessAvgPartition<T> : AvgPartition<T>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="list">被分区的列表</param>
        /// <param name="size">每个分区的长度</param>
        public RandomAccessAvgPartition(IList<T> list, int size)
            : base(list, size)
        {
        }

        /// <summary>
        /// 获取分区个数
        /// </summary>
        public new int Count
        {
            get
            {
                if (_size == 0)
                {
                    return 0;
                }
                int total = _list.Count;
                return (total + _size - 1) / _size;
            }
        }
    }

    /// <summary>
    /// 随机访问分区
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class RandomAccessPartition<T> : Partition<T>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="list">被分区的列表</param>
        /// <param name="size">每个分区的长度</param>
        public RandomAccessPartition(IList<T> list, int size)
            : base(list, size)
        {
        }
    }
}
