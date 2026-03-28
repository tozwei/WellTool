using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 属性比较器，根据对象的属性进行排序
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class PropertyComparator<T> : IComparer<T>
    {
        private readonly string _propertyName;
        private readonly ListSortDirection _direction;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="direction">排序方向</param>
        public PropertyComparator(string propertyName, ListSortDirection direction = ListSortDirection.Ascending)
        {
            _propertyName = propertyName;
            _direction = direction;
        }

        /// <summary>
        /// 比较两个对象
        /// </summary>
        /// <param name="x">第一个对象</param>
        /// <param name="y">第二个对象</param>
        /// <returns>比较结果</returns>
        public int Compare(T x, T y)
        {
            if (x == null && y == null)
                return 0;
            if (x == null)
                return _direction == ListSortDirection.Ascending ? -1 : 1;
            if (y == null)
                return _direction == ListSortDirection.Ascending ? 1 : -1;

            var propertyInfo = typeof(T).GetProperty(_propertyName);
            if (propertyInfo == null)
                return 0;

            var xValue = propertyInfo.GetValue(x);
            var yValue = propertyInfo.GetValue(y);

            if (xValue == null && yValue == null)
                return 0;
            if (xValue == null)
                return _direction == ListSortDirection.Ascending ? -1 : 1;
            if (yValue == null)
                return _direction == ListSortDirection.Ascending ? 1 : -1;

            var comparer = Comparer<object>.Default;
            var result = comparer.Compare(xValue, yValue);

            return _direction == ListSortDirection.Ascending ? result : -result;
        }
    }
}