using System;

namespace WellDone.Core.Lang;

/// <summary>
/// Supplier接口
/// </summary>
public interface ISupplier<T>
{
	T Get();
}

/// <summary>
/// Supplier实现
/// </summary>
public class Supplier<T> : ISupplier<T>
{
	private readonly Func<T> _func;

	public Supplier(Func<T> func)
	{
		_func = func;
	}

	public T Get() => _func();
}
