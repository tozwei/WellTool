using System;

namespace WellDone.Core.Lang;

/// <summary>
/// 可变Int封装
/// </summary>
public class MutableInt
{
	private int _value;

	/// <summary>
	/// 值
	/// </summary>
	public int Value
	{
		get => _value;
		set => _value = value;
	}

	/// <summary>
	/// 构造
	/// </summary>
	public MutableInt()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableInt(int value)
	{
		_value = value;
	}

	/// <summary>
	/// 增加
	/// </summary>
	/// <param name="delta">增量</param>
	/// <returns>this</returns>
	public MutableInt Add(int delta)
	{
		_value += delta;
		return this;
	}

	/// <summary>
	/// 增加并返回新值
	/// </summary>
	/// <param name="delta">增量</param>
	/// <returns>新值</returns>
	public int AddAndGet(int delta)
	{
		return _value += delta;
	}

	/// <summary>
	/// 获取并增加
	/// </summary>
	/// <param name="delta">增量</param>
	/// <returns>旧值</returns>
	public int GetAndAdd(int delta)
	{
		var old = _value;
		_value += delta;
		return old;
	}

	/// <summary>
	/// 递增
	/// </summary>
	/// <returns>this</returns>
	public MutableInt Increment()
	{
		return Add(1);
	}

	/// <summary>
	/// 递减
	/// </summary>
	/// <returns>this</returns>
	public MutableInt Decrement()
	{
		return Add(-1);
	}

	public override bool Equals(object obj)
	{
		if (obj is MutableInt other)
			return _value == other._value;
		if (obj is int i)
			return _value == i;
		return false;
	}

	public override int GetHashCode() => _value;

	public override string ToString() => _value.ToString();

	public static implicit operator int(MutableInt mi) => mi._value;
	public static implicit operator MutableInt(int i) => new MutableInt(i);
}
