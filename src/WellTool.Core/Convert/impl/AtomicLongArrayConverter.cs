using System;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// AtomicLongArray转换器
/// </summary>
public class AtomicLongArrayConverter : AbstractConverter<AtomicLongArray>
{
	/// <summary>
	/// 内部转换
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>结果</returns>
	protected override AtomicLongArray ConvertInternal(object value)
	{
		var longArray = Convert.To<long[]>(value);
		return new AtomicLongArray(longArray);
	}
}

/// <summary>
/// AtomicLongArray封装类
/// </summary>
public class AtomicLongArray
{
	private readonly long[] _array;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="length">长度</param>
	public AtomicLongArray(int length)
	{
		_array = new long[length];
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="array">数组</param>
	public AtomicLongArray(long[] array)
	{
		_array = (long[])array.Clone();
	}

	/// <summary>
	/// 获取长度
	/// </summary>
	public int Length => _array.Length;

	/// <summary>
	/// 获取值
	/// </summary>
	/// <param name="index">索引</param>
	/// <returns>值</returns>
	public long Get(int index) => _array[index];

	/// <summary>
	/// 设置值
	/// </summary>
	/// <param name="index">索引</param>
	/// <param name="newValue">新值</param>
	public void Set(int index, long newValue)
	{
		_array[index] = newValue;
	}
}
