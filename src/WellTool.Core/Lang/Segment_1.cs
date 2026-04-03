namespace WellTool.Core.Lang;

/// <summary>
/// 片段表示，用于表示文本、集合等数据结构的一个区间。
/// </summary>
/// <typeparam name="T">数字类型，用于表示位置index</typeparam>
public interface ISegment<T>
{
	/// <summary>
	/// 获取起始位置
	/// </summary>
	T GetStartIndex();

	/// <summary>
	/// 获取结束位置
	/// </summary>
	T GetEndIndex();
}

/// <summary>
/// 片段实现
/// </summary>
/// <typeparam name="T">数字类型</typeparam>
public class Segment<T> : ISegment<T>
{
	private readonly T _startIndex;
	private readonly T _endIndex;

	public Segment(T startIndex, T endIndex)
	{
		_startIndex = startIndex;
		_endIndex = endIndex;
	}

	public T GetStartIndex() => _startIndex;

	public T GetEndIndex() => _endIndex;
}
