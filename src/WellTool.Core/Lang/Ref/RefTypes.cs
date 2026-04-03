namespace WellTool.Core.lang.refs;

using System;

/// <summary>
/// 引用类型枚举
/// </summary>
public enum ReferenceType
{
	/// <summary>
	/// 强引用
	/// </summary>
	Strong,
	/// <summary>
	/// 软引用
	/// </summary>
	Soft,
	/// <summary>
	/// 弱引用
	/// </summary>
	Weak,
	/// <summary>
	/// 虚引用
	/// </summary>
	Phantom
}

/// <summary>
/// 引用工厂
/// </summary>
public static class Ref
{
	/// <summary>
	/// 创建引用
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="value">值</param>
	/// <param name="type">引用类型</param>
	/// <returns>引用</returns>
	public static IRef<T> Create<T>(T? value, ReferenceType type) where T : class
	{
		return type switch
		{
			ReferenceType.Strong => new StrongRef<T>(value),
			ReferenceType.Soft => new SoftRef<T>(value),
			ReferenceType.Weak => new WeakRef<T>(value),
			ReferenceType.Phantom => new PhantomRef<T>(value),
			_ => new StrongRef<T>(value)
		};
	}
}

/// <summary>
/// 强引用对象
/// </summary>
public class StrongObj<T> : IRef<T> where T : class
{
	private T? _value;

	public StrongObj(T? value) => _value = value;
	public T? Value { get => _value; set => _value = value; }
	public bool IsNull => _value == null;
}

/// <summary>
/// 软引用对象
/// </summary>
public class SoftObj<T> : IRef<T> where T : class
{
	private T? _value;

	public SoftObj(T? value) => _value = value;
	public T? Value { get => _value; set => _value = value; }
	public bool IsNull => _value == null;
}

/// <summary>
/// 弱引用对象
/// </summary>
public class WeakObj<T> : IRef<T> where T : class
{
	private WeakReference<T>? _ref;

	public WeakObj(T? value) => _ref = value != null ? new WeakReference<T>(value) : null;
	public T? Value
	{
		get => _ref?.TryGetTarget(out var t) == true ? t : null;
		set => _ref = value != null ? new WeakReference<T>(value) : null;
	}
	public bool IsNull => _ref == null || !_ref.TryGetTarget(out _);
}

/// <summary>
/// 虚引用对象
/// </summary>
public class PhantomObj<T> : IRef<T> where T : class
{
	private T? _value;

	public PhantomObj(T? value) => _value = value;
	public T? Value { get => _value; set => _value = value; }
	public bool IsNull => _value == null;
}
