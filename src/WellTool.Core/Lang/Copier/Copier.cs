namespace WellTool.Core.Lang.Copier;

/// <summary>
/// 拷贝器接口
/// </summary>
/// <typeparam name="T">源类型</typeparam>
public interface ICopier<T>
{
	/// <summary>
	/// 执行拷贝
	/// </summary>
	/// <param name="src">源对象</param>
	/// <param name="dest">目标对象</param>
	/// <returns>目标对象</returns>
	T Copy(T src, T dest);
}

/// <summary>
/// 拷贝器抽象类
/// </summary>
/// <typeparam name="T">源类型</typeparam>
public abstract class Copier<T> : ICopier<T>
{
	/// <summary>
	/// 执行拷贝
	/// </summary>
	/// <param name="src">源对象</param>
	/// <param name="dest">目标对象</param>
	/// <returns>目标对象</returns>
	public abstract T Copy(T src, T dest);
}

/// <summary>
/// 源到目标拷贝器
/// </summary>
/// <typeparam name="S">源类型</typeparam>
/// <typeparam name="D">目标类型</typeparam>
public interface ISrcToDestCopier<S, D> : ICopier<S>
{
	/// <summary>
	/// 源对象
	/// </summary>
	S Src { get; set; }

	/// <summary>
	/// 目标对象
	/// </summary>
	D Dest { get; set; }

	/// <summary>
	/// 执行拷贝
	/// </summary>
	/// <returns>目标对象</returns>
	D Copy();
}
