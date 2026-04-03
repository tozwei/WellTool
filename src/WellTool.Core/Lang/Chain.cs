using System;

namespace WellTool.Core.Lang;

/// <summary>
/// 链式调用接口
/// </summary>
/// <typeparam name="T">链类型</typeparam>
public interface IChain<T>
{
	/// <summary>
	/// 获取当前链对象
	/// </summary>
	/// <returns>当前链对象</returns>
	T Get();
}

/// <summary>
/// 链式调用基类
/// </summary>
/// <typeparam name="T">链类型</typeparam>
public abstract class Chain<T> : IChain<T>
{
	/// <summary>
	/// 当前链对象
	/// </summary>
	protected T _current;

	/// <summary>
	/// 获取当前链对象
	/// </summary>
	public T Get()
	{
		return _current;
	}

	/// <summary>
	/// 链结束，返回当前对象
	/// </summary>
	/// <returns>当前对象</returns>
	public T End()
	{
		return _current;
	}
}
