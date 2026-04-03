namespace WellTool.Core.Lang.Copier;

/// <summary>
/// 拷贝器接口，用于对象之间的属性拷贝
/// </summary>
/// <typeparam name="T">源类型</typeparam>
/// <typeparam name="TResult">目标类型</typeparam>
public interface ICopier<in T, TResult>
{
	/// <summary>
	/// 拷贝对象
	/// </summary>
	/// <param name="src">源对象</param>
	/// <param name="dest">目标对象</param>
	/// <returns>目标对象</returns>
	TResult Copy(T src, TResult dest);

	/// <summary>
	/// 拷贝并返回新对象
	/// </summary>
	/// <param name="src">源对象</param>
	/// <returns>新对象</returns>
	TResult CopyNew(T src);
}

/// <summary>
/// 源到目标拷贝器接口
/// </summary>
/// <typeparam name="T">源类型</typeparam>
/// <typeparam name="TResult">目标类型</typeparam>
public interface ISrcToDestCopier<in T, TResult> : ICopier<T, TResult>
{
	/// <summary>
	/// 拷贝并覆盖目标
	/// </summary>
	/// <param name="src">源对象</param>
	/// <param name="dest">目标对象</param>
	/// <returns>目标对象</returns>
	new TResult Copy(T src, TResult dest);
}
