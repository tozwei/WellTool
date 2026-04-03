namespace WellTool.Core.Lang.Func;

/// <summary>
/// 无返回值函数对象接口
/// </summary>
/// <typeparam name="P">参数类型</typeparam>
public interface IVoidFunc<P>
{
	/// <summary>
	/// 执行函数
	/// </summary>
	/// <param name="parameters">参数列表</param>
	void Call(params P[] parameters);

	/// <summary>
	/// 执行函数，异常包装为RuntimeException
	/// </summary>
	/// <param name="parameters">参数列表</param>
	void CallWithRuntimeException(params P[] parameters);
}

/// <summary>
/// 三参数无返回值函数对象接口
/// </summary>
/// <typeparam name="P1">参数1类型</typeparam>
/// <typeparam name="P2">参数2类型</typeparam>
/// <typeparam name="P3">参数3类型</typeparam>
public interface IVoidFunc3<P1, P2, P3>
{
	/// <summary>
	/// 执行函数
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	/// <param name="p3">参数3</param>
	void Call(P1 p1, P2 p2, P3 p3);

	/// <summary>
	/// 执行函数，异常包装为RuntimeException
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	/// <param name="p3">参数3</param>
	void CallWithRuntimeException(P1 p1, P2 p2, P3 p3);
}
