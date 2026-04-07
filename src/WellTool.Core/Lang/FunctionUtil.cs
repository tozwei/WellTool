using System;

namespace WellTool.Core.Lang;

/// <summary>
/// Function接口
/// </summary>
public interface IFunctionUtil<T, R>
{
	R Apply(T t);
}

/// <summary>
/// Function实现
/// </summary>
public class FunctionUtil<T, R> : IFunctionUtil<T, R>
{
	private readonly System.Func<T, R> _func;

	public FunctionUtil(System.Func<T, R> func)
	{
		_func = func;
	}

	public R Apply(T t) => _func(t);

	/// <summary>
	/// 组合函数
	/// </summary>
	public FunctionUtil<T, V> AndThen<V>(FunctionUtil<R, V> after)
	{
		return new FunctionUtil<T, V>(t => after.Apply(Apply(t)));
	}

	/// <summary>
	/// 组合函数
	/// </summary>
	public FunctionUtil<V, R> Compose<V>(FunctionUtil<V, T> before)
	{
		return new FunctionUtil<V, R>(v => Apply(before.Apply(v)));
	}
}
