using System;

namespace WellTool.Core.Lang;

/// <summary>
/// Predicate接口
/// </summary>
public interface IPredicate<T>
{
	bool Test(T t);
}

/// <summary>
/// Predicate实现
/// </summary>
public class Predicate<T> : IPredicate<T>
{
	private readonly Func<T, bool> _func;

	public Predicate(Func<T, bool> func)
	{
		_func = func;
	}

	public bool Test(T t) => _func(t);

	/// <summary>
	/// 与运算
	/// </summary>
	public Predicate<T> And(Predicate<T> other)
	{
		return new Predicate<T>(t => Test(t) && other.Test(t));
	}

	/// <summary>
	/// 或运算
	/// </summary>
	public Predicate<T> Or(Predicate<T> other)
	{
		return new Predicate<T>(t => Test(t) || other.Test(t));
	}

	/// <summary>
	/// 取反
	/// </summary>
	public Predicate<T> Negate()
	{
		return new Predicate<T>(t => !Test(t));
	}
}
