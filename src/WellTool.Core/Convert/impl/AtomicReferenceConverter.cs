using System;
using System.Threading;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// AtomicReference转换器
/// </summary>
/// <typeparam name="T">引用类型</typeparam>
public class AtomicReferenceConverter<T> : AbstractConverter<AtomicReference<T>>
{
	/// <summary>
	/// 内部转换
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>结果</returns>
	protected override AtomicReference<T> ConvertInternal(object value)
	{
		var targetValue = value is T t ? t : (T)value;
		return new AtomicReference<T>(targetValue);
	}
}

/// <summary>
/// AtomicReferenceConverter
/// </summary>
public class AtomicReferenceConverter : AbstractConverter<AtomicReference>
{
	/// <summary>
	/// 内部转换
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>结果</returns>
	protected override AtomicReference ConvertInternal(object value)
	{
		return new AtomicReference(value);
	}
}

/// <summary>
/// AtomicReference封装类
/// </summary>
/// <typeparam name="T">引用类型</typeparam>
public class AtomicReference<T>
{
	private T _value;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="initialValue">初始值</param>
	public AtomicReference(T initialValue)
	{
		_value = initialValue;
	}

	/// <summary>
	/// 构造
	/// </summary>
	public AtomicReference()
	{
	}

	/// <summary>
	/// 获取值
	/// </summary>
	public T Value => _value;

	/// <summary>
	/// 设置值
	/// </summary>
	/// <param name="newValue">新值</param>
	public void Set(T newValue)
	{
		_value = newValue;
	}

	/// <summary>
	/// 获取并设置
	/// </summary>
	/// <param name="newValue">新值</param>
	/// <returns>旧值</returns>
	public T GetAndSet(T newValue)
	{
		var old = _value;
		_value = newValue;
		return old;
	}
}

/// <summary>
/// AtomicReference非泛型封装
/// </summary>
public class AtomicReference
{
	private object _value;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="initialValue">初始值</param>
	public AtomicReference(object initialValue)
	{
		_value = initialValue;
	}

	/// <summary>
	/// 构造
	/// </summary>
	public AtomicReference()
	{
	}

	/// <summary>
	/// 获取值
	/// </summary>
	public object Value => _value;

	/// <summary>
	/// 设置值
	/// </summary>
	/// <param name="newValue">新值</param>
	public void Set(object newValue)
	{
		_value = newValue;
	}
}
