namespace WellTool.Core.Lang.Func;

/// <summary>
/// 带参数的Supplier接口
/// </summary>
/// <typeparam name="T">目标类型</typeparam>
/// <typeparam name="P1">参数1类型</typeparam>
public interface ISupplier1<T, P1>
{
	/// <summary>
	/// 生成实例
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <returns>目标对象</returns>
	T Get(P1 p1);
}

/// <summary>
/// 带两个参数的Supplier接口
/// </summary>
/// <typeparam name="T">目标类型</typeparam>
/// <typeparam name="P1">参数1类型</typeparam>
/// <typeparam name="P2">参数2类型</typeparam>
public interface ISupplier2<T, P1, P2>
{
	/// <summary>
	/// 生成实例
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	/// <returns>目标对象</returns>
	T Get(P1 p1, P2 p2);
}

/// <summary>
/// 带三个参数的Supplier接口
/// </summary>
/// <typeparam name="T">目标类型</typeparam>
/// <typeparam name="P1">参数1类型</typeparam>
/// <typeparam name="P2">参数2类型</typeparam>
/// <typeparam name="P3">参数3类型</typeparam>
public interface ISupplier3<T, P1, P2, P3>
{
	/// <summary>
	/// 生成实例
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	/// <param name="p3">参数3</param>
	/// <returns>目标对象</returns>
	T Get(P1 p1, P2 p2, P3 p3);
}

/// <summary>
/// 带四个参数的Supplier接口
/// </summary>
/// <typeparam name="T">目标类型</typeparam>
/// <typeparam name="P1">参数1类型</typeparam>
/// <typeparam name="P2">参数2类型</typeparam>
/// <typeparam name="P3">参数3类型</typeparam>
/// <typeparam name="P4">参数4类型</typeparam>
public interface ISupplier4<T, P1, P2, P3, P4>
{
	/// <summary>
	/// 生成实例
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	/// <param name="p3">参数3</param>
	/// <param name="p4">参数4</param>
	/// <returns>目标对象</returns>
	T Get(P1 p1, P2 p2, P3 p3, P4 p4);
}

/// <summary>
/// 带五个参数的Supplier接口
/// </summary>
/// <typeparam name="T">目标类型</typeparam>
/// <typeparam name="P1">参数1类型</typeparam>
/// <typeparam name="P2">参数2类型</typeparam>
/// <typeparam name="P3">参数3类型</typeparam>
/// <typeparam name="P4">参数4类型</typeparam>
/// <typeparam name="P5">参数5类型</typeparam>
public interface ISupplier5<T, P1, P2, P3, P4, P5>
{
	/// <summary>
	/// 生成实例
	/// </summary>
	/// <param name="p1">参数1</param>
	/// <param name="p2">参数2</param>
	/// <param name="p3">参数3</param>
	/// <param name="p4">参数4</param>
	/// <param name="p5">参数5</param>
	/// <returns>目标对象</returns>
	T Get(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);
}
