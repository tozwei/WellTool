using System;
using System.Threading;

namespace WellTool.Core.Lang.Loader;

/// <summary>
/// 加载器接口
/// </summary>
/// <typeparam name="T">加载对象类型</typeparam>
public interface ILoader<T>
{
	/// <summary>
	/// 获取加载的对象
	/// </summary>
	/// <returns>加载的对象</returns>
	T Get();
}

/// <summary>
/// 惰性加载器基类
/// </summary>
/// <typeparam name="T">加载对象类型</typeparam>
public abstract class Loader<T> : ILoader<T>
{
	/// <summary>
	/// 是否已加载
	/// </summary>
	protected bool _loaded;

	/// <summary>
	/// 加载的对象
	/// </summary>
	protected T _value;

	/// <summary>
	/// 获取加载的对象
	/// </summary>
	/// <returns>加载的对象</returns>
	public T Get()
	{
		if (!_loaded)
		{
			lock (this)
			{
				if (!_loaded)
				{
					_value = Load();
					_loaded = true;
				}
			}
		}
		return _value;
	}

	/// <summary>
	/// 加载方法
	/// </summary>
	/// <returns>加载的对象</returns>
	protected abstract T Load();
}


