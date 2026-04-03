using System;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 分段接口
    /// </summary>
    public interface ISegment<T>
    {
        /// <summary>
        /// 起始位置
        /// </summary>
        int Start { get; }

        /// <summary>
        /// 结束位置
        /// </summary>
        int End { get; }

        /// <summary>
        /// 值
        /// </summary>
        T Value { get; }
    }

    /// <summary>
    /// 分段
    /// </summary>
    public class Segment<T> : ISegment<T>
    {
        /// <summary>
        /// 起始位置
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// 结束位置
        /// </summary>
        public int End { get; }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Segment(int start, int end, T value)
        {
            if (start > end)
            {
                throw new ArgumentException("Start cannot be greater than End");
            }
            Start = start;
            End = end;
            Value = value;
        }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length => End - Start + 1;

        /// <summary>
        /// 检查位置是否在分段内
        /// </summary>
        public bool Contains(int index)
        {
            return index >= Start && index <= End;
        }

        /// <summary>
        /// 检查是否与另一个分段重叠
        /// </summary>
        public bool Overlaps(Segment<T> other)
        {
            return Start <= other.End && End >= other.Start;
        }
    }

    /// <summary>
    /// 默认分段
    /// </summary>
    public class DefaultSegment : ISegment<string>
    {
        public int Start { get; }
        public int End { get; }
        public string Value { get; }

        public DefaultSegment(int start, int end)
        {
            Start = start;
            End = end;
            Value = null;
        }

        public DefaultSegment(int start, int end, string value)
        {
            Start = start;
            End = end;
            Value = value;
        }

        public int Length => End - Start + 1;

        public bool Contains(int index)
        {
            return index >= Start && index <= End;
        }
    }
}
