namespace WellTool.Core.Lang.Intern;

/// <summary>
/// 驻留器接口
/// </summary>
/// <typeparam name="T">驻留类型</typeparam>
public interface IInterner<T>
{
	/// <summary>
	/// 驻留对象
	/// </summary>
	/// <param name="obj">对象</param>
	/// <returns>驻留后的对象</returns>
	T Intern(T obj);
}
