using System;

namespace WellTool.Core.Lang;

/// <summary>
/// Function接口
/// </summary>
public interface IFunction<T, R>
{
	R Apply(T t);
}

/// <summary>
/// Function实现
/// </summary>
public class Function<T, R> : IFunction<T, R>
{
	private readonly Func<T, R> _func;

	public Function(Func<T, R> func)
	{
		_func = func;
	}

	public R Apply(T t) => _func(t);

	/// <summary>
	/// 组合函数
	/// </summary>
	public Function<T, V> AndThen<V>(Function<R, V> after)
	{
		return new Function<T, V>(t => after.Apply(Apply(t)));
	}

	/// <summary>
	/// 组合函数
	/// </summary>
	public Function<V, R> Compose<V>(Function<V, T> before)
	{
		return new Function<V, R>(v => Apply(before.Apply(v)));
	}
}
