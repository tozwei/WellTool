using System;
using System.Threading;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// AtomicReference杞崲鍣?
/// </summary>
/// <typeparam name="T">寮曠敤绫诲瀷</typeparam>
public class AtomicReferenceConverter<T> : AbstractConverter<AtomicReference<T>>
{
	/// <summary>
	/// 鍐呴儴杞崲
	/// </summary>
	/// <param name="value">鍊?/param>
	/// <returns>缁撴灉</returns>
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
	/// 鍐呴儴杞崲
	/// </summary>
	/// <param name="value">鍊?/param>
	/// <returns>缁撴灉</returns>
	protected override AtomicReference ConvertInternal(object value)
	{
		return new AtomicReference(value);
	}
}

/// <summary>
/// AtomicReference灏佽绫?
/// </summary>
/// <typeparam name="T">寮曠敤绫诲瀷</typeparam>
public class AtomicReference<T>
{
	private T _value;

	/// <summary>
	/// 鏋勯€?
	/// </summary>
	/// <param name="initialValue">鍒濆鍊?/param>
	public AtomicReference(T initialValue)
	{
		_value = initialValue;
	}

	/// <summary>
	/// 鏋勯€?
	/// </summary>
	public AtomicReference()
	{
	}

	/// <summary>
	/// 鑾峰彇鍊?
	/// </summary>
	public T Value => _value;

	/// <summary>
	/// 璁剧疆鍊?
	/// </summary>
	/// <param name="newValue">鏂板€?/param>
	public void Set(T newValue)
	{
		_value = newValue;
	}

	/// <summary>
	/// 鑾峰彇骞惰缃?
	/// </summary>
	/// <param name="newValue">鏂板€?/param>
	/// <returns>鏃у€?/returns>
	public T GetAndSet(T newValue)
	{
		var old = _value;
		_value = newValue;
		return old;
	}
}

/// <summary>
/// AtomicReference闈炴硾鍨嬪皝瑁?
/// </summary>
public class AtomicReference
{
	private object _value;

	/// <summary>
	/// 鏋勯€?
	/// </summary>
	/// <param name="initialValue">鍒濆鍊?/param>
	public AtomicReference(object initialValue)
	{
		_value = initialValue;
	}

	/// <summary>
	/// 鏋勯€?
	/// </summary>
	public AtomicReference()
	{
	}

	/// <summary>
	/// 鑾峰彇鍊?
	/// </summary>
	public object Value => _value;

	/// <summary>
	/// 璁剧疆鍊?
	/// </summary>
	/// <param name="newValue">鏂板€?/param>
	public void Set(object newValue)
	{
		_value = newValue;
	}
}

