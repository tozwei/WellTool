namespace WellTool.Core.Lang;

/// <summary>
/// 匹配接口
/// </summary>
/// <typeparam name="T">匹配的对象类型</typeparam>
public interface IMatcher<in T>
{
	/// <summary>
	/// 给定对象是否匹配
	/// </summary>
	/// <param name="t">对象</param>
	/// <returns>是否匹配</returns>
	bool Match(T t);
}
