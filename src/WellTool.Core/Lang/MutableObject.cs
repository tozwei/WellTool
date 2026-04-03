using System;

namespace WellTool.Core.Lang;

/// <summary>
/// 可变Object封装
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
public class MutableObject<T>
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
	public MutableObject()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableObject(T value)
	{
		_value = value;
	}

	/// <summary>
	/// 获取值，如果为空返回默认值
	/// </summary>
	/// <param name="defaultValue">默认值</param>
	/// <returns>值</returns>
	public T GetOrDefault(T defaultValue) => _value ?? defaultValue;

	public override bool Equals(object obj)
	{
		if (obj is MutableObject<T> other)
			return Equals(_value, other._value);
		return Equals(_value, obj);
	}

	public override int GetHashCode() => _value?.GetHashCode() ?? 0;

	public override string ToString() => _value?.ToString() ?? "null";

	public static implicit operator T(MutableObject<T> mo) => mo._value;
	public static implicit operator MutableObject<T>(T value) => new MutableObject<T>(value);
}
