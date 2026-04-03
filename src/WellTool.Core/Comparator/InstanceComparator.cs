using System;
using System.Collections.Generic;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 按照指定类型顺序排序，对象顺序取决于对象对应的类在数组中的位置。
    /// 如果对比的两个对象类型相同，返回0，默认如果对象类型不在列表中，则排序在前
    /// </summary>
    /// <typeparam name="T">用于比较的对象类型</typeparam>
    public class InstanceComparator<T> : IComparer<T>
    {
        private readonly bool _atEndIfMiss;
        private readonly Type[] _instanceOrder;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="instanceOrder">用于比较排序的对象类型数组，排序按照数组位置排序</param>
        public InstanceComparator(params Type[] instanceOrder)
            : this(false, instanceOrder)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="atEndIfMiss">如果不在列表中是否排在后边</param>
        /// <param name="instanceOrder">用于比较排序的对象类型数组</param>
        public InstanceComparator(bool atEndIfMiss, params Type[] instanceOrder)
        {
            if (instanceOrder == null)
            {
                throw new ArgumentNullException(nameof(instanceOrder), "'instanceOrder' array must not be null");
            }
            _atEndIfMiss = atEndIfMiss;
            _instanceOrder = instanceOrder;
        }

        /// <summary>
        /// 比较
        /// </summary>
        public int Compare(T x, T y)
        {
            int i1 = GetOrder(x);
            int i2 = GetOrder(y);
            return i1.CompareTo(i2);
        }

        /// <summary>
        /// 查找对象类型所在列表的位置
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>位置，未找到位置根据 atEndIfMiss 取不同值，false返回-1，否则返回列表长度</returns>
        private int GetOrder(T obj)
        {
            if (obj != null)
            {
                Type objType = obj.GetType();
                for (int i = 0; i < _instanceOrder.Length; i++)
                {
                    if (_instanceOrder[i].IsAssignableFrom(objType))
                    {
                        return i;
                    }
                }
            }
            return _atEndIfMiss ? _instanceOrder.Length : -1;
        }
    }
}
