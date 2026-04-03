namespace WellTool.Core.Lang;

/// <summary>
/// 片段表示，用于表示文本、集合等数据结构的一个区间。
/// </summary>
/// <typeparam name="T">数字类型，用于表示位置index</typeparam>
public interface Segment<T> where T : struct
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
