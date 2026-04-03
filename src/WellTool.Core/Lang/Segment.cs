namespace WellTool.Core.Lang;

using System;

/// <summary>
/// 片段接口，表示一个区间或范围
/// </summary>
public interface Segment
{
    /// <summary>
    /// 获取起始位置
    /// </summary>
    object? StartIndex { get; }

    /// <summary>
    /// 获取结束位置
    /// </summary>
    object? EndIndex { get; }
}

/// <summary>
/// Int 类型的片段
/// </summary>
public class IntSegment : DefaultSegment<int>
{
    /// <summary>
    /// 构造
    /// </summary>
    public IntSegment(int startIndex, int endIndex) : base(startIndex, endIndex)
    {
    }
}

/// <summary>
/// Long 类型的片段
/// </summary>
public class LongSegment : DefaultSegment<long>
{
    /// <summary>
    /// 构造
    /// </summary>
    public LongSegment(long startIndex, long endIndex) : base(startIndex, endIndex)
    {
    }
}
