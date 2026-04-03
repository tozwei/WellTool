using System;

namespace WellDone.Core.Lang;

/// <summary>
/// 可变Short封装
/// </summary>
public class MutableShort
{
	private short _value;

	/// <summary>
	/// 值
	/// </summary>
	public short Value
	{
		get => _value;
		set => _value = value;
	}

	/// <summary>
	/// 构造
	/// </summary>
	public MutableShort()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableShort(short value)
	{
		_value = value;
	}

	public override bool Equals(object obj)
	{
		if (obj is MutableShort other)
			return _value == other._value;
		if (obj is short s)
			return _value == s;
		return false;
	}

	public override int GetHashCode() => _value;

	public override string ToString() => _value.ToString();

	public static implicit operator short(MutableShort ms) => ms._value;
	public static implicit operator MutableShort(short s) => new MutableShort(s);
}
