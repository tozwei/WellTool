using System;

namespace WellDone.Core.Lang;

/// <summary>
/// Consumer接口
/// </summary>
public interface IConsumer<T>
{
	void Accept(T t);
}

/// <summary>
/// Consumer实现
/// </summary>
public class Consumer<T> : IConsumer<T>
{
	private readonly Action<T> _action;

	public Consumer(Action<T> action)
	{
		_action = action;
	}

	public void Accept(T t) => _action(t);
}
