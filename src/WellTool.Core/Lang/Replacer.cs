namespace WellTool.Core.Lang;

/// <summary>
/// 替换器接口
/// 通过实现此接口完成指定类型对象的替换操作，替换后的目标类型依旧为指定类型
/// </summary>
/// <typeparam name="T">被替换操作的类型</typeparam>
public interface IReplacer<T>
{
	/// <summary>
	/// 替换指定类型为目标类型
	/// </summary>
	/// <param name="t">被替换的对象</param>
	/// <returns>替代后的对象</returns>
	T Replace(T t);
}
