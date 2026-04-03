namespace WellTool.Core.Lang;

/// <summary>
/// 片段表示，用于表示文本、集合等数据结构的一个区间。
/// </summary>
/// <typeparam name="T">数字类型，用于表示位置index</typeparam>
public class DefaultSegment<T> : Segment<T> where T : struct
{
	private readonly T _startIndex;
	private readonly T _endIndex;

	public DefaultSegment(T startIndex, T endIndex)
	{
		_startIndex = startIndex;
		_endIndex = endIndex;
	}

	/// <summary>
	/// 获取起始位置
	/// </summary>
	public T StartIndex => _startIndex;

	/// <summary>
	/// 获取结束位置
	/// </summary>
	public T EndIndex => _endIndex;

	/// <summary>
	/// 片段长度
	/// </summary>
	public T Length => _endIndex;
}
