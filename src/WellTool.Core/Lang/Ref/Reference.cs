using System.Runtime.CompilerServices;

namespace WellTool.Core.Lang.Ref;

/// <summary>
/// 引用类型接口
/// </summary>
/// <typeparam name="T">引用对象类型</typeparam>
public interface IReference<T>
{
	/// <summary>
	/// 获取引用对象
	/// </summary>
	T? Get();

	/// <summary>
	/// 设置引用对象
	/// </summary>
	/// <param name="value">值</param>
	void Set(T? value);
}

/// <summary>
/// 强引用包装
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
public class StrongReference<T> : IReference<T> where T : class
{
	private T? _value;

	/// <inheritdoc />
	public T? Get() => _value;

	/// <inheritdoc />
	public void Set(T? value) => _value = value;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public StrongReference(T? value = null)
	{
		_value = value;
	}
}

/// <summary>
/// 弱引用包装
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
public class WeakReference<T> : IReference<T> where T : class
{
	private readonly System.WeakReference<T> _weakReference;

	/// <inheritdoc />
	public T? Get()
	{
		_weakReference.TryGetTarget(out var value);
		return value;
	}

	/// <inheritdoc />
	public void Set(T? value)
	{
		_weakReference.SetTarget(value!);
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public WeakReference(T? value = null)
	{
		_weakReference = new System.WeakReference<T>(value!);
	}

	/// <summary>
	/// 是否被回收
	/// </summary>
	public bool IsAlive => _weakReference.TryGetTarget(out _);
}

/// <summary>
/// 软引用包装（.NET中WeakReference可近似替代）
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
public class SoftReference<T> : IReference<T> where T : class
{
	private System.WeakReference<T> _softReference;

	/// <inheritdoc />
	public T? Get()
	{
		_softReference.TryGetTarget(out var value);
		return value;
	}

	/// <inheritdoc />
	public void Set(T? value)
	{
		_softReference = new System.WeakReference<T>(value!);
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public SoftReference(T? value = null)
	{
		_softReference = new System.WeakReference<T>(value!);
	}

	/// <summary>
	/// 是否被回收
	/// </summary>
	public bool IsAlive => _softReference.TryGetTarget(out _);
}

/// <summary>
/// 幻影引用（仅用于追踪对象回收）
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
public class PhantomReference<T> : IReference<T> where T : class
{
	/// <inheritdoc />
	public T? Get() => null; // PhantomReference永远返回null

	/// <inheritdoc />
	public void Set(T? value) { /* 幻影引用不能设置值 */ }
}
