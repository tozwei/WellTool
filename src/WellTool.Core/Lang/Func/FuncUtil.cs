namespace WellTool.Core.Lang.Func;

/// <summary>
/// 函数对象接口，用于包装一个函数为对象
/// </summary>
/// <typeparam name="P">参数类型</typeparam>
/// <typeparam name="R">返回值类型</typeparam>
public interface IFunc<P, R>
{
	/// <summary>
	/// 执行函数
	/// </summary>
	/// <param name="parameters">参数列表</param>
	/// <returns>函数执行结果</returns>
	R Call(params P[] parameters);

	/// <summary>
	/// 执行函数，异常包装为RuntimeException
	/// </summary>
	/// <param name="parameters">参数列表</param>
	/// <returns>函数执行结果</returns>
	R CallWithRuntimeException(params P[] parameters);
}

/// <summary>
/// 无参数的函数对象接口
/// </summary>
/// <typeparam name="R">返回值类型</typeparam>
public interface IFunc0<R>
{
	/// <summary>
	/// 执行函数
	/// </summary>
	/// <returns>函数执行结果</returns>
	R Call();

	/// <summary>
	/// 执行函数，异常包装为RuntimeException
	/// </summary>
	/// <returns>函数执行结果</returns>
	R CallWithRuntimeException();
}

/// <summary>
/// 只有一个参数的函数对象接口
/// </summary>
/// <typeparam name="P">参数类型</typeparam>
/// <typeparam name="R">返回值类型</typeparam>
public interface IFunc1<P, R>
{
	/// <summary>
	/// 执行函数
	/// </summary>
	/// <param name="parameter">参数</param>
	/// <returns>函数执行结果</returns>
	R Call(P parameter);

	/// <summary>
	/// 执行函数，异常包装为RuntimeException
	/// </summary>
	/// <param name="parameter">参数</param>
	/// <returns>函数执行结果</returns>
	R CallWithRuntimeException(P parameter);
}

/// <summary>
/// 两个参数的函数对象接口
/// </summary>
/// <typeparam name="P1">参数1类型</typeparam>
/// <typeparam name="P2">参数2类型</typeparam>
/// <typeparam name="R">返回值类型</typeparam>
public interface IFunc2<P1, P2, R>
{
	/// <summary>
	/// 执行函数
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	/// <returns>函数执行结果</returns>
	R Call(P1 p1, P2 p2);

	/// <summary>
	/// 执行函数，异常包装为RuntimeException
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	/// <returns>函数执行结果</returns>
	R CallWithRuntimeException(P1 p1, P2 p2);
}

/// <summary>
/// 三个参数的函数对象接口
/// </summary>
/// <typeparam name="P1">参数1类型</typeparam>
/// <typeparam name="P2">参数2类型</typeparam>
/// <typeparam name="P3">参数3类型</typeparam>
/// <typeparam name="R">返回值类型</typeparam>
public interface IFunc3<P1, P2, P3, R>
{
	/// <summary>
	/// 执行函数
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	/// <param name="p3">参数3</param>
	/// <returns>函数执行结果</returns>
	R Call(P1 p1, P2 p2, P3 p3);

	/// <summary>
	/// 执行函数，异常包装为RuntimeException
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	/// <param name="p3">参数3</param>
	/// <returns>函数执行结果</returns>
	R CallWithRuntimeException(P1 p1, P2 p2, P3 p3);
}
