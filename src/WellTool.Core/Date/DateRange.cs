using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Date
{
    /// <summary>
    /// 日期范围
    /// </summary>
    public class DateRange : IEnumerable<DateTime>
    {
        private readonly DateTime _start;
        private readonly DateTime _end;
        private readonly DateField _unit;
        private readonly int _step;
        private readonly bool _includeStart;
        private readonly bool _includeEnd;

        /// <summary>
        /// 构造，包含开始和结束日期时间
        /// </summary>
        /// <param name="start">起始日期时间（包括）</param>
        /// <param name="end">结束日期时间（包括）</param>
        /// <param name="unit">步进单位</param>
        public DateRange(DateTime start, DateTime end, DateField unit)
            : this(start, end, unit, 1)
        {
        }

        /// <summary>
        /// 构造，包含开始和结束日期时间
        /// </summary>
        /// <param name="start">起始日期时间（包括）</param>
        /// <param name="end">结束日期时间（包括）</param>
        /// <param name="unit">步进单位</param>
        /// <param name="step">步进数</param>
        public DateRange(DateTime start, DateTime end, DateField unit, int step)
            : this(start, end, unit, step, true, true)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="start">起始日期时间</param>
        /// <param name="end">结束日期时间</param>
        /// <param name="unit">步进单位</param>
        /// <param name="step">步进数</param>
        /// <param name="includeStart">是否包含开始的时间</param>
        /// <param name="includeEnd">是否包含结束的时间</param>
        public DateRange(DateTime start, DateTime end, DateField unit, int step, bool includeStart, bool includeEnd)
        {
            _start = start;
            _end = end;
            _unit = unit;
            _step = step;
            _includeStart = includeStart;
            _includeEnd = includeEnd;
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        public IEnumerator<DateTime> GetEnumerator()
        {
            if (_step <= 0)
            {
                yield break;
            }

            if (_includeStart)
            {
                yield return _start;
            }

            var current = _start;
            var index = 1;
            while (true)
            {
                var next = current.Add((TimeSpan)_unit.ToTimeSpan() * _step);
                if (next > _end)
                {
                    break;
                }
                yield return next;
                current = next;
                index++;
            }

            if (_includeEnd && _step > 0)
            {
                var last = _start.Add((TimeSpan)_unit.ToTimeSpan() * _step * (GetCount() - 1));
                if (last < _end)
                {
                    yield return _end;
                }
            }
        }

        /// <summary>
        /// 获取范围内的日期数量
        /// </summary>
        public int Count()
        {
            if (_step <= 0) return 0;

            var totalSpan = _end - _start;
            var unitSpan = (TimeSpan)_unit.ToTimeSpan();
            var count = (int)(totalSpan / unitSpan / _step) + 1;

            if (!_includeStart) count--;
            if (!_includeEnd) count--;

            return count < 0 ? 0 : count;
        }

        private int GetCount()
        {
            if (_step <= 0) return 0;
            var totalSpan = _end - _start;
            var unitSpan = (TimeSpan)_unit.ToTimeSpan();
            return (int)(totalSpan / unitSpan / _step) + 1;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}