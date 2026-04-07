using System;

namespace WellTool.Core.Lang;

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
public class ActionUtil<T> : IAction<T>
{
	private readonly System.Action<T> _action;

	public ActionUtil(System.Action<T> action)
	{
		_action = action;
	}

	public void Invoke(T t) => _action(t);

	public static implicit operator ActionUtil<T>(System.Action<T> action) => new ActionUtil<T>(action);
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
	private readonly System.Action _action;

	public Action(System.Action action)
	{
		_action = action;
	}

	public void Invoke() => _action();

	public static implicit operator Action(System.Action action) => new Action(action);
}
