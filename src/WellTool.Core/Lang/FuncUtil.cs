using System;
using System.Collections.Generic;

namespace WellTool.Core.Lang;

/// <summary>
/// Func相关接口
/// </summary>
public interface IFuncUtil<T, R>
{
	R Invoke(T t);
}

/// <summary>
/// Func实现
/// </summary>
public class FuncUtil<T, R> : IFuncUtil<T, R>
{
	private readonly System.Func<T, R> _func;

	public FuncUtil(System.Func<T, R> func)
	{
		_func = func;
	}

	public R Invoke(T t) => _func(t);

	public static implicit operator FuncUtil<T, R>(System.Func<T, R> func) => new FuncUtil<T, R>(func);
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
	private readonly System.Func<R> _func;

	public Func(System.Func<R> func)
	{
		_func = func;
	}

	public R Invoke() => _func();

	public static implicit operator Func<R>(System.Func<R> func) => new Func<R>(func);
}
