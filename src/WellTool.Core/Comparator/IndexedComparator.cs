using System;
using System.Collections.Generic;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 按照数组的顺序正序排列，数组的元素位置决定了对象的排序先后
    /// 默认的，如果参与排序的元素并不在数组中，则排序在前（可以通过 atEndIfMiss 设置)
    /// </summary>
    /// <typeparam name="T">被排序元素类型</typeparam>
    public class IndexedComparator<T> : IComparer<T>
    {
        private readonly bool _atEndIfMiss;
        private readonly Dictionary<T, int> _map;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="objs">参与排序的数组，数组的元素位置决定了对象的排序先后</param>
        public IndexedComparator(params T[] objs)
            : this(false, objs)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="atEndIfMiss">如果不在列表中是否排在后边</param>
        /// <param name="objs">参与排序的数组</param>
        public IndexedComparator(bool atEndIfMiss, params T[] objs)
        {
            if (objs == null)
            {
                throw new ArgumentNullException(nameof(objs), "'objs' array must not be null");
            }
            _atEndIfMiss = atEndIfMiss;
            _map = new Dictionary<T, int>(objs.Length);
            for (int i = 0; i < objs.Length; i++)
            {
                _map[objs[i]] = i;
            }
        }

        /// <summary>
        /// 比较
        /// </summary>
        public int Compare(T x, T y)
        {
            int index1 = GetOrder(x);
            int index2 = GetOrder(y);

            if (index1 == index2)
            {
                if (index1 < 0 || index1 == _map.Count)
                {
                    // 任意一个元素不在 map 中, 返回原顺序
                    return 1;
                }
                // 位置一样，认为是同一个元素
                return 0;
            }

            return index1.CompareTo(index2);
        }

        /// <summary>
        /// 查找对象类型所对应的顺序值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>位置，未找到位置根据 atEndIfMiss 取不同值，false返回-1，否则返回map长度</returns>
        private int GetOrder(T obj)
        {
            if (_map.TryGetValue(obj, out int order))
            {
                return order;
            }
            return _atEndIfMiss ? _map.Count : -1;
        }
    }
}
