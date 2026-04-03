using System;

namespace WellDone.Core.Lang;

/// <summary>
/// Action相关接口
/// </summary>
public interface IAction<T>
{
	void Invoke(T t);
}

/// <summary>
/// Action实现
/// </summary>
public class Action<T> : IAction<T>
{
	private readonly Action<T> _action;

	public Action(Action<T> action)
	{
		_action = action;
	}

	public void Invoke(T t) => _action(t);

	public static implicit operator Action<T>(Action<T> action) => new Action<T>(action);
}

/// <summary>
/// 无参Action
/// </summary>
public interface IAction
{
	void Invoke();
}

/// <summary>
/// 无参Action实现
/// </summary>
public class Action : IAction
{
	private readonly Action _action;

	public Action(Action action)
	{
		_action = action;
	}

	public void Invoke() => _action();

	public static implicit operator Action(Action action) => new Action(action);
}
