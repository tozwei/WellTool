namespace WellTool.Core.Lang;

/// <summary>
/// 替换器接口
/// </summary>
/// <typeparam name="T">替换对象类型</typeparam>
public interface IReplacer<T>
{
	/// <summary>
	/// 替换方法
	/// </summary>
	/// <param name="t">待替换对象</param>
	/// <returns>替换后的对象</returns>
	T Replace(T t);
}

/// <summary>
/// 替换器抽象类
/// </summary>
/// <typeparam name="T">替换对象类型</typeparam>
public abstract class Replacer<T> : IReplacer<T>
{
	/// <summary>
	/// 替换方法
	/// </summary>
	/// <param name="t">待替换对象</param>
	/// <returns>替换后的对象</returns>
	public abstract T Replace(T t);
}

/// <summary>
/// Lambda替换器
/// </summary>
/// <typeparam name="T">替换对象类型</typeparam>
public class LambdaReplacer<T> : Replacer<T>
{
	private readonly System.Func<T, T> _replacer;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="replacer">替换函数</param>
	public LambdaReplacer(System.Func<T, T> replacer)
	{
		_replacer = replacer;
	}

	/// <summary>
	/// 替换方法
	/// </summary>
	/// <param name="t">待替换对象</param>
	/// <returns>替换后的对象</returns>
	public override T Replace(T t)
	{
		return _replacer?.Invoke(t) ?? t;
	}
}
