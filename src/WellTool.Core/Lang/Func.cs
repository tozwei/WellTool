using System;
using System.Collections.Generic;

namespace WellDone.Core.Lang;

/// <summary>
/// Func相关接口
/// </summary>
public interface IFunc<T, R>
{
	R Invoke(T t);
}

/// <summary>
/// Func实现
/// </summary>
public class Func<T, R> : IFunc<T, R>
{
	private readonly Func<T, R> _func;

	public Func(Func<T, R> func)
	{
		_func = func;
	}

	public R Invoke(T t) => _func(t);

	public static implicit operator Func<T, R>(Func<T, R> func) => new Func<T, R>(func);
}

/// <summary>
/// 无参Func
/// </summary>
public interface IFunc<R>
{
	R Invoke();
}

/// <summary>
/// 无参Func实现
/// </summary>
public class Func<R> : IFunc<R>
{
	private readonly Func<R> _func;

	public Func(Func<R> func)
	{
		_func = func;
	}

	public R Invoke() => _func();

	public static implicit operator Func<R>(Func<R> func) => new Func<R>(func);
}
