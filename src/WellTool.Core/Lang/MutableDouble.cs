using System;

namespace WellDone.Core.Lang;

/// <summary>
/// 可变Double封装
/// </summary>
public class MutableDouble
{
	private double _value;

	/// <summary>
	/// 值
	/// </summary>
	public double Value
	{
		get => _value;
		set => _value = value;
	}

	/// <summary>
	/// 构造
	/// </summary>
	public MutableDouble()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableDouble(double value)
	{
		_value = value;
	}

	public override bool Equals(object obj)
	{
		if (obj is MutableDouble other)
			return _value.Equals(other._value);
		if (obj is double d)
			return _value.Equals(d);
		return false;
	}

	public override int GetHashCode() => _value.GetHashCode();

	public override string ToString() => _value.ToString();

	public static implicit operator double(MutableDouble md) => md._value;
	public static implicit operator MutableDouble(double d) => new MutableDouble(d);
}
