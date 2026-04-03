using System;

namespace WellDone.Core.Lang;

/// <summary>
/// 可变Float封装
/// </summary>
public class MutableFloat
{
	private float _value;

	/// <summary>
	/// 值
	/// </summary>
	public float Value
	{
		get => _value;
		set => _value = value;
	}

	/// <summary>
	/// 构造
	/// </summary>
	public MutableFloat()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableFloat(float value)
	{
		_value = value;
	}

	public override bool Equals(object obj)
	{
		if (obj is MutableFloat other)
			return _value.Equals(other._value);
		if (obj is float f)
			return _value.Equals(f);
		return false;
	}

	public override int GetHashCode() => _value.GetHashCode();

	public override string ToString() => _value.ToString();

	public static implicit operator float(MutableFloat mf) => mf._value;
	public static implicit operator MutableFloat(float f) => new MutableFloat(f);
}
