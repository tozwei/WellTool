using System;

namespace WellDone.Core.Lang;

/// <summary>
/// 可变Long封装
/// </summary>
public class MutableLong
{
	private long _value;

	/// <summary>
	/// 值
	/// </summary>
	public long Value
	{
		get => _value;
		set => _value = value;
	}

	/// <summary>
	/// 构造
	/// </summary>
	public MutableLong()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableLong(long value)
	{
		_value = value;
	}

	/// <summary>
	/// 增加
	/// </summary>
	/// <param name="delta">增量</param>
	/// <returns>this</returns>
	public MutableLong Add(long delta)
	{
		_value += delta;
		return this;
	}

	/// <summary>
	/// 增加并返回新值
	/// </summary>
	/// <param name="delta">增量</param>
	/// <returns>新值</returns>
	public long AddAndGet(long delta)
	{
		return _value += delta;
	}

	/// <summary>
	/// 获取并增加
	/// </summary>
	/// <param name="delta">增量</param>
	/// <returns>旧值</returns>
	public long GetAndAdd(long delta)
	{
		var old = _value;
		_value += delta;
		return old;
	}

	public override bool Equals(object obj)
	{
		if (obj is MutableLong other)
			return _value == other._value;
		if (obj is long l)
			return _value == l;
		return false;
	}

	public override int GetHashCode() => _value.GetHashCode();

	public override string ToString() => _value.ToString();

	public static implicit operator long(MutableLong ml) => ml._value;
	public static implicit operator MutableLong(long l) => new MutableLong(l);
}
