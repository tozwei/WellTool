namespace WellTool.Core.Lang.Loader;

using System;
using System.Collections.Concurrent;

/// <summary>
/// 原子加载器
/// </summary>
public class AtomicLoader<T>
{
	private readonly Func<T> _loader;
	private T? _value;
	private bool _loaded;

	public AtomicLoader(Func<T> loader)
	{
		_loader = loader ?? throw new ArgumentNullException(nameof(loader));
	}

	/// <summary>
	/// 获取值
	/// </summary>
	public T Get()
	{
		if (_loaded) return _value!;
		lock (this)
		{
			if (_loaded) return _value!;
			_value = _loader();
			_loaded = true;
			return _value;
		}
	}

	/// <summary>
	/// 重置
	/// </summary>
	public void Reset()
	{
		lock (this)
		{
			_value = default;
			_loaded = false;
		}
	}
}

/// <summary>
/// 延迟加载器
/// </summary>
public class LazyLoader<T>
{
	private readonly Func<T> _loader;
	private T? _value;
	private bool _initialized;
	private readonly object _lock = new();

	public LazyLoader(Func<T> loader)
	{
		_loader = loader ?? throw new ArgumentNullException(nameof(loader));
	}

	/// <summary>
	/// 获取值
	/// </summary>
	public T Value
	{
		get
		{
			if (_initialized) return _value!;
			lock (_lock)
			{
				if (_initialized) return _value!;
				_value = _loader();
				_initialized = true;
				return _value;
			}
		}
	}

	/// <summary>
	/// 是否已初始化
	/// </summary>
	public bool IsInitialized => _initialized;
}

/// <summary>
/// 函数式延迟加载器
/// </summary>
public class LazyFunLoader<T>
{
	private readonly Func<T>? _loader;
	private readonly T? _value;
	private readonly bool _initialized;

	public LazyFunLoader(Func<T> loader)
	{
		_loader = loader;
	}

	public LazyFunLoader(T value)
	{
		_value = value;
		_initialized = true;
	}

	/// <summary>
	/// 获取值
	/// </summary>
	public T Get()
	{
		if (_initialized) return _value!;
		return _loader!();
	}

	/// <summary>
	/// 是否已初始化
	/// </summary>
	public bool IsInitialized => _initialized;
}
