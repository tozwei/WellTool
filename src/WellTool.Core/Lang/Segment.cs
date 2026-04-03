namespace WellTool.Core.Lang;

/// <summary>
/// 片段表示，用于表示文本、集合等数据结构的一个区间
/// </summary>
/// <typeparam name="T">数字类型，用于表示位置index</typeparam>
public interface ISegment<T> where T : struct
{
	/// <summary>
	/// 获取起始位置
	/// </summary>
	T StartIndex { get; }

	/// <summary>
	/// 获取结束位置
	/// </summary>
	T EndIndex { get; }

	/// <summary>
	/// 片段长度
	/// </summary>
	T Length { get; }
}

/// <summary>
/// 片段默认实现
/// </summary>
/// <typeparam name="T">数字类型</typeparam>
public class DefaultSegment<T> : ISegment<T> where T : struct
{
	/// <summary>
	/// 起始位置
	/// </summary>
	public T StartIndex { get; }

	/// <summary>
	/// 结束位置
	/// </summary>
	public T EndIndex { get; }

	/// <summary>
	/// 片段长度
	/// </summary>
	public T Length { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="startIndex">起始位置</param>
	/// <param name="endIndex">结束位置</param>
	public DefaultSegment(T startIndex, T endIndex)
	{
		StartIndex = startIndex;
		EndIndex = endIndex;
	}
}
