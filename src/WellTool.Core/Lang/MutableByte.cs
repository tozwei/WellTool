using System;

namespace WellTool.Core.Lang;

/// <summary>
/// 可变Byte封装
/// </summary>
public class MutableByte
{
	private byte _value;

	/// <summary>
	/// 值
	/// </summary>
	public byte Value
	{
		get => _value;
		set => _value = value;
	}

	/// <summary>
	/// 构造
	/// </summary>
	public MutableByte()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableByte(byte value)
	{
		_value = value;
	}

	public override bool Equals(object obj)
	{
		if (obj is MutableByte other)
			return _value == other._value;
		if (obj is byte b)
			return _value == b;
		return false;
	}

	public override int GetHashCode() => _value;

	public override string ToString() => _value.ToString();

	public static implicit operator byte(MutableByte mb) => mb._value;
	public static implicit operator MutableByte(byte b) => new MutableByte(b);
}
