namespace WellTool.Core.Lang;

///// <summary>
///// 过滤器接口
///// </summary>
///// <typeparam name="T">过滤对象类型</typeparam>
//public interface IFilter<T>
//{
//	/// <summary>
//	/// 过滤方法
//	/// </summary>
//	/// <param name="t">待过滤对象</param>
//	/// <returns>是否保留</returns>
//	bool Accept(T t);
//}

/// <summary>
/// 过滤器抽象类
/// </summary>
/// <typeparam name="T">过滤对象类型</typeparam>
public abstract class Filter<T> : IFilter<T>
{
	/// <summary>
	/// 过滤方法
	/// </summary>
	/// <param name="t">待过滤对象</param>
	/// <returns>是否保留</returns>
	public abstract bool Accept(T t);
}

/// <summary>
/// Lambda过滤器
/// </summary>
/// <typeparam name="T">过滤对象类型</typeparam>
public class LambdaFilter<T> : Filter<T>
{
	private readonly System.Func<T, bool> _filter;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="filter">过滤函数</param>
	public LambdaFilter(System.Func<T, bool> filter)
	{
		_filter = filter;
	}

	/// <summary>
	/// 过滤方法
	/// </summary>
	/// <param name="t">待过滤对象</param>
	/// <returns>是否保留</returns>
	public override bool Accept(T t)
	{
		return _filter?.Invoke(t) ?? true;
	}
}
