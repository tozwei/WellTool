namespace WellTool.Core.Lang;

/// <summary>
/// 过滤器接口
/// </summary>
/// <typeparam name="T">被过滤的对象类型</typeparam>
public interface IFilter<in T>
{
	/// <summary>
	/// 是否接受对象
	/// </summary>
	/// <param name="t">检查的对象</param>
	/// <returns>是否接受对象</returns>
	bool Accept(T t);
}
