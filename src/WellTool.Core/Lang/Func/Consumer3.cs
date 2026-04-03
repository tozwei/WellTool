namespace WellTool.Core.Lang.Func;

/// <summary>
/// 三参数消费者接口
/// </summary>
/// <typeparam name="T1">参数1类型</typeparam>
/// <typeparam name="T2">参数2类型</typeparam>
/// <typeparam name="T3">参数3类型</typeparam>
public interface IConsumer3<T1, T2, T3>
{
	/// <summary>
	/// 执行消费操作
	/// </summary>
	/// <param name="t1">参数1</param>
	/// <param name="t2">参数2</param>
	/// <param name="t3">参数3</param>
	void Accept(T1 t1, T2 t2, T3 t3);
}

/// <summary>
/// 三参数消费者实现
/// </summary>
/// <typeparam name="T1">参数1类型</typeparam>
/// <typeparam name="T2">参数2类型</typeparam>
/// <typeparam name="T3">参数3类型</typeparam>
public class Consumer3<T1, T2, T3> : IConsumer3<T1, T2, T3>
{
	private readonly Action<T1, T2, T3> _action;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="action">消费动作</param>
	public Consumer3(Action<T1, T2, T3> action)
	{
		_action = action ?? throw new ArgumentNullException(nameof(action));
	}

	/// <inheritdoc />
	public void Accept(T1 t1, T2 t2, T3 t3) => _action(t1, t2, t3);

	/// <summary>
	/// 转换为Action
	/// </summary>
	/// <param name="t1">参数1</param>
	/// <param name="t2">参数2</param>
	/// <param name="t3">参数3</param>
	public void Invoke(T1 t1, T2 t2, T3 t3) => _action(t1, t2, t3);
}
