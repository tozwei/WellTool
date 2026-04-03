using System;

namespace WellTool.Core.Lang;

/// <summary>
/// 可变对象封装
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
public class Mutable<T>
{
	private T _value;

	/// <summary>
	/// 值
	/// </summary>
	public T Value
	{
		get => _value;
		set => _value = value;
	}

	/// <summary>
	/// 构造
	/// </summary>
	public Mutable()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public Mutable(T value)
	{
		_value = value;
	}

	/// <summary>
	/// 获取值
	/// </summary>
	public T Get() => _value;

	/// <summary>
	/// 设置值
	/// </summary>
	/// <param name="value">值</param>
	public void Set(T value)
	{
		_value = value;
	}

	/// <summary>
	/// 设置新值并返回旧值
	/// </summary>
	/// <param name="newValue">新值</param>
	/// <returns>旧值</returns>
	public T GetAndSet(T newValue)
	{
		var old = _value;
		_value = newValue;
		return old;
	}

	/// <summary>
	/// 隐式转换
	/// </summary>
	public static implicit operator T(Mutable<T> mutable) => mutable._value;

	/// <summary>
	/// 隐式转换
	/// </summary>
	public static implicit operator Mutable<T>(T value) => new Mutable<T>(value);
}
