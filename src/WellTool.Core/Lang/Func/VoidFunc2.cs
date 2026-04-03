namespace WellTool.Core.Lang.Func;

/// <summary>
/// 无参数无返回值函数对象接口
/// </summary>
public interface IVoidFunc0
{
	/// <summary>
	/// 执行函数
	/// </summary>
	void Call();

	/// <summary>
	/// 执行函数，异常包装为RuntimeException
	/// </summary>
	void CallWithRuntimeException();
}

/// <summary>
/// 无参数无返回值函数对象实现
/// </summary>
public class VoidFunc0 : IVoidFunc0
{
	private readonly Action _action;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="action">执行动作</param>
	public VoidFunc0(Action action)
	{
		_action = action ?? throw new ArgumentNullException(nameof(action));
	}

	/// <inheritdoc />
	public void Call() => _action();

	/// <inheritdoc />
	public void CallWithRuntimeException()
	{
		try
		{
			_action();
		}
		catch (Exception e)
		{
			throw new Exception("Operation failed", e);
		}
	}
}

/// <summary>
/// 单参数无返回值函数对象接口
/// </summary>
/// <typeparam name="P">参数类型</typeparam>
public interface IVoidFunc1<P>
{
	/// <summary>
	/// 执行函数
	/// </summary>
	/// <param name="parameter">参数</param>
	void Call(P parameter);

	/// <summary>
	/// 执行函数，异常包装为RuntimeException
	/// </summary>
	/// <param name="parameter">参数</param>
	void CallWithRuntimeException(P parameter);
}

/// <summary>
/// 单参数无返回值函数对象实现
/// </summary>
/// <typeparam name="P">参数类型</typeparam>
public class VoidFunc1<P> : IVoidFunc1<P>
{
	private readonly Action<P> _action;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="action">执行动作</param>
	public VoidFunc1(Action<P> action)
	{
		_action = action ?? throw new ArgumentNullException(nameof(action));
	}

	/// <inheritdoc />
	public void Call(P parameter) => _action(parameter);

	/// <inheritdoc />
	public void CallWithRuntimeException(P parameter)
	{
		try
		{
			_action(parameter);
		}
		catch (Exception e)
		{
			throw new Exception("Operation failed", e);
		}
	}
}

/// <summary>
/// 双参数无返回值函数对象接口
/// </summary>
/// <typeparam name="P1">参数1类型</typeparam>
/// <typeparam name="P2">参数2类型</typeparam>
public interface IVoidFunc2<P1, P2>
{
	/// <summary>
	/// 执行函数
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	void Call(P1 p1, P2 p2);

	/// <summary>
	/// 执行函数，异常包装为RuntimeException
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	void CallWithRuntimeException(P1 p1, P2 p2);
}

/// <summary>
/// 双参数无返回值函数对象实现
/// </summary>
/// <typeparam name="P1">参数1类型</typeparam>
/// <typeparam name="P2">参数2类型</typeparam>
public class VoidFunc2<P1, P2> : IVoidFunc2<P1, P2>
{
	private readonly Action<P1, P2> _action;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="action">执行动作</param>
	public VoidFunc2(Action<P1, P2> action)
	{
		_action = action ?? throw new ArgumentNullException(nameof(action));
	}

	/// <inheritdoc />
	public void Call(P1 p1, P2 p2) => _action(p1, p2);

	/// <inheritdoc />
	public void CallWithRuntimeException(P1 p1, P2 p2)
	{
		try
		{
			_action(p1, p2);
		}
		catch (Exception e)
		{
			throw new Exception("Operation failed", e);
		}
	}
}
