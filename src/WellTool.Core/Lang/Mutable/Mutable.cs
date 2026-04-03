namespace WellTool.Core.Lang.Mutable;

/// <summary>
/// 可变值接口
/// </summary>
/// <typeparam name="T">值类型</typeparam>
public interface IMutable<T>
{
	/// <summary>
	/// 获取值
	/// </summary>
	T Value { get; set; }
}

/// <summary>
/// 可变布尔值
/// </summary>
public class MutableBool : IMutable<bool>
{
	/// <inheritdoc />
	public bool Value { get; set; }

	/// <summary>
	/// 构造，默认false
	/// </summary>
	public MutableBool() { }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableBool(bool value)
	{
		Value = value;
	}

	/// <inheritdoc />
	public override string ToString() => Value.ToString();
}

/// <summary>
/// 可变字节值
/// </summary>
public class MutableByte : IMutable<byte>
{
	/// <inheritdoc />
	public byte Value { get; set; }

	/// <summary>
	/// 构造，默认0
	/// </summary>
	public MutableByte() { }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableByte(byte value)
	{
		Value = value;
	}

	/// <inheritdoc />
	public override string ToString() => Value.ToString();
}

/// <summary>
/// 可变整数值
/// </summary>
public class MutableInt : IMutable<int>
{
	/// <inheritdoc />
	public int Value { get; set; }

	/// <summary>
	/// 构造，默认0
	/// </summary>
	public MutableInt() { }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableInt(int value)
	{
		Value = value;
	}

	/// <inheritdoc />
	public override string ToString() => Value.ToString();
}

/// <summary>
/// 可变长整数值
/// </summary>
public class MutableLong : IMutable<long>
{
	/// <inheritdoc />
	public long Value { get; set; }

	/// <summary>
	/// 构造，默认0
	/// </summary>
	public MutableLong() { }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableLong(long value)
	{
		Value = value;
	}

	/// <inheritdoc />
	public override string ToString() => Value.ToString();
}

/// <summary>
/// 可变单精度浮点值
/// </summary>
public class MutableFloat : IMutable<float>
{
	/// <inheritdoc />
	public float Value { get; set; }

	/// <summary>
	/// 构造，默认0
	/// </summary>
	public MutableFloat() { }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableFloat(float value)
	{
		Value = value;
	}

	/// <inheritdoc />
	public override string ToString() => Value.ToString();
}

/// <summary>
/// 可变双精度浮点值
/// </summary>
public class MutableDouble : IMutable<double>
{
	/// <inheritdoc />
	public double Value { get; set; }

	/// <summary>
	/// 构造，默认0
	/// </summary>
	public MutableDouble() { }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableDouble(double value)
	{
		Value = value;
	}

	/// <inheritdoc />
	public override string ToString() => Value.ToString();
}

/// <summary>
/// 可变对象值
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
public class MutableObject<T> : IMutable<T?> where T : class
{
	/// <inheritdoc />
	public T? Value { get; set; }

	/// <summary>
	/// 构造，默认null
	/// </summary>
	public MutableObject() { }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">初始值</param>
	public MutableObject(T? value)
	{
		Value = value;
	}

	/// <inheritdoc />
	public override string ToString() => Value?.ToString() ?? "null";
}
