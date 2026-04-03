using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 范围，用于表示一个起始值到结束值的范围
    /// </summary>
    public class Range<T> : IEnumerable<T> where T : IComparable<T>
    {
        /// <summary>
        /// 起始值
        /// </summary>
        public T Start { get; }

        /// <summary>
        /// 结束值
        /// </summary>
        public T End { get; }

        /// <summary>
        /// 步长
        /// </summary>
        public T Step { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Range(T start, T end, T step = default)
        {
            Start = start;
            End = end;
            Step = step;
        }

        /// <summary>
        /// 检查值是否在范围内
        /// </summary>
        public bool Contains(T value)
        {
            return value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            if (typeof(T) == typeof(int))
            {
                return (IEnumerator<T>)new IntRangeIterator(Convert.ToInt32(Start), Convert.ToInt32(End));
            }
            if (typeof(T) == typeof(long))
            {
                return (IEnumerator<T>)new LongRangeIterator(Convert.ToInt64(Start), Convert.ToInt64(End));
            }
            throw new NotSupportedException($"Range of {typeof(T).Name} is not supported");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class IntRangeIterator : IEnumerator<T>
        {
            private int _current;
            private readonly int _end;
            private readonly int _step;

            public IntRangeIterator(int start, int end, int step = 1)
            {
                _current = start - step;
                _end = end;
                _step = step;
            }

            public T Current => (T)(object)_current;
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _current += _step;
                return _current <= _end;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public void Dispose() { }
        }

        private class LongRangeIterator : IEnumerator<T>
        {
            private long _current;
            private readonly long _end;
            private readonly long _step;

            public LongRangeIterator(long start, long end, long step = 1)
            {
                _current = start - step;
                _end = end;
                _step = step;
            }

            public T Current => (T)(object)_current;
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _current += _step;
                return _current <= _end;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public void Dispose() { }
        }

        /// <summary>
        /// 创建整数范围
        /// </summary>
        public static Range<int> Of(int start, int end, int step = 1)
        {
            return new Range<int>(start, end, step);
        }

        /// <summary>
        /// 创建长整数范围
        /// </summary>
        public static Range<long> Of(long start, long end, long step = 1)
        {
            return new Range<long>(start, end, step);
        }
    }
}
