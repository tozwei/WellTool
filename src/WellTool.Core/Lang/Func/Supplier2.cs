namespace WellTool.Core.Lang.Func;

/// <summary>
/// 带两个参数的Supplier实现
/// </summary>
/// <typeparam name="T">目标类型</typeparam>
/// <typeparam name="P1">参数1类型</typeparam>
/// <typeparam name="P2">参数2类型</typeparam>
public class Supplier2<T, P1, P2> : ISupplier2<T, P1, P2>
{
	private readonly Func<P1, P2, T> _func;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="func">工厂函数</param>
	public Supplier2(Func<P1, P2, T> func)
	{
		_func = func ?? throw new ArgumentNullException(nameof(func));
	}

	/// <inheritdoc />
	public T Get(P1 p1, P2 p2) => _func(p1, p2);
}
