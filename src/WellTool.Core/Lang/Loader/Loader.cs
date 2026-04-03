namespace WellTool.Core.Lang.Loader;

/// <summary>
/// 延迟加载器接口
/// </summary>
/// <typeparam name="T">加载对象类型</typeparam>
public interface ILoader<T>
{
	/// <summary>
	/// 加载对象
	/// </summary>
	/// <returns>加载的对象</returns>
	T Load();
}

/// <summary>
/// 延迟加载器抽象基类
/// </summary>
/// <typeparam name="T">加载对象类型</typeparam>
public abstract class LoaderBase<T> : ILoader<T>
{
	/// <summary>
	/// 是否已加载
	/// </summary>
	protected bool IsLoaded { get; private set; }

	/// <summary>
	/// 加载的对象
	/// </summary>
	protected T? Value { get; private set; }

	/// <summary>
	/// 获取加载的对象
	/// </summary>
	/// <returns>加载的对象</returns>
	public T Get()
	{
		if (!IsLoaded)
		{
			Value = Load();
			IsLoaded = true;
		}
		return Value!;
	}

	/// <summary>
	/// 加载对象
	/// </summary>
	/// <returns>加载的对象</returns>
	protected abstract T Load();

	/// <inheritdoc />
	T ILoader<T>.Load() => Get();
}

/// <summary>
/// 延迟加载器，使用Func委托
/// </summary>
/// <typeparam name="T">加载对象类型</typeparam>
public class LazyLoader<T> : LoaderBase<T>
{
	private readonly Func<T> _func;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="func">延迟加载函数</param>
	public LazyLoader(Func<T> func)
	{
		_func = func ?? throw new ArgumentNullException(nameof(func));
	}

	/// <inheritdoc />
	protected override T Load() => _func();
}

/// <summary>
/// 原子加载器，确保只有一个线程加载
/// </summary>
/// <typeparam name="T">加载对象类型</typeparam>
public class AtomicLoader<T> : ILoader<T>
{
	private readonly Func<T> _func;
	private readonly object _lock = new();
	private T? _value;
	private bool _loaded;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="func">加载函数</param>
	public AtomicLoader(Func<T> func)
	{
		_func = func ?? throw new ArgumentNullException(nameof(func));
	}

	/// <inheritdoc />
	public T Load()
	{
		if (_loaded)
			return _value!;

		lock (_lock)
		{
			if (_loaded)
				return _value!;

			_value = _func();
			_loaded = true;
			return _value;
		}
	}

	/// <summary>
	/// 获取加载的对象
	/// </summary>
	/// <returns>加载的对象</returns>
	public T Get() => Load();
}

/// <summary>
/// 延迟函数加载器，支持异常延迟抛出
/// </summary>
/// <typeparam name="T">加载对象类型</typeparam>
public class LazyFunLoader<T> : ILoader<T>
{
	private readonly Func<T> _func;
	private Func<Exception, T>? _exceptionHandler;

	/// <summary>
	/// 是否已加载
	/// </summary>
	public bool IsLoaded { get; private set; }

	/// <summary>
	/// 加载的对象
	/// </summary>
	public T? Value { get; private set; }

	/// <summary>
	/// 异常
	/// </summary>
	public Exception? Exception { get; private set; }

	/// <summary>
	/// 是否失败
	/// </summary>
	public bool IsFailed => Exception != null;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="func">延迟加载函数</param>
	public LazyFunLoader(Func<T> func)
	{
		_func = func ?? throw new ArgumentNullException(nameof(func));
	}

	/// <summary>
	/// 设置异常处理器
	/// </summary>
	/// <param name="handler">异常处理函数</param>
	/// <returns>this</returns>
	public LazyFunLoader<T> OnException(Func<Exception, T> handler)
	{
		_exceptionHandler = handler;
		return this;
	}

	/// <inheritdoc />
	public T Load()
	{
		if (IsLoaded)
		{
			if (IsFailed && _exceptionHandler != null)
				return _exceptionHandler(Exception!);
			return Value!;
		}

		try
		{
			Value = _func();
			IsLoaded = true;
			return Value;
		}
		catch (Exception ex)
		{
			Exception = ex;
			IsLoaded = true;
			if (_exceptionHandler != null)
				return _exceptionHandler(ex);
			throw;
		}
	}

	/// <summary>
	/// 获取加载的对象，如果失败则返回默认值
	/// </summary>
	/// <param name="defaultValue">默认值</param>
	/// <returns>加载的对象或默认值</returns>
	public T GetOrDefault(T defaultValue)
	{
		if (!IsLoaded)
		{
			try
			{
				Load();
			}
			catch
			{
				return defaultValue;
			}
		}
		return IsFailed ? defaultValue : Value!;
	}
}
