namespace WellTool.Core.lang;

using System;

/// <summary>
/// 引用接口
/// </summary>
public interface IRef<T>
{
	/// <summary>
	/// 获取引用对象
	/// </summary>
	T? Value { get; set; }

	/// <summary>
	/// 引用是否被回收
	/// </summary>
	bool IsNull { get; }
}

/// <summary>
/// 强引用
/// </summary>
public class StrongRef<T> : IRef<T>
{
	private T? _value;

	public StrongRef(T? value)
	{
		_value = value;
	}

	public T? Value
	{
		get => _value;
		set => _value = value;
	}

	public bool IsNull => _value == null;
}

/// <summary>
/// 弱引用
/// </summary>
public class WeakRef<T> : IRef<T> where T : class
{
	private WeakReference<T>? _ref;

	public WeakRef(T? value)
	{
		_ref = value != null ? new WeakReference<T>(value) : null;
	}

	public T? Value
	{
		get
		{
			if (_ref?.TryGetTarget(out var target) == true)
				return target;
			return null;
		}
		set
		{
			_ref = value != null ? new WeakReference<T>(value) : null;
		}
	}

	public bool IsNull => _ref == null || !_ref.TryGetTarget(out _);
}

/// <summary>
/// 软引用
/// </summary>
public class SoftRef<T> : IRef<T> where T : class
{
	private T? _value;

	public SoftRef(T? value)
	{
		_value = value;
	}

	public T? Value
	{
		get => _value;
		set => _value = value;
	}

	public bool IsNull => _value == null;
}

/// <summary>
/// 虚引用
/// </summary>
public class PhantomRef<T> : IRef<T> where T : class
{
	private T? _value;

	public PhantomRef(T? value)
	{
		_value = value;
	}

	public T? Value
	{
		get => _value;
		set => _value = value;
	}

	public bool IsNull => _value == null;
}

/// <summary>
/// 引用类型枚举
/// </summary>
public enum RefType
{
	Strong,
	Weak,
	Soft,
	Phantom
}
