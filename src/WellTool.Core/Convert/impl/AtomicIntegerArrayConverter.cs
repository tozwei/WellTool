using System;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// AtomicIntegerArray转换器
/// </summary>
public class AtomicIntegerArrayConverter : AbstractConverter<AtomicIntegerArray>
{
	/// <summary>
	/// 内部转换
	/// </summary>
	/// <param name="value">值</param>
	/// <returns>结果</returns>
	protected override AtomicIntegerArray ConvertInternal(object value)
	{
		var intArray = Convert.To<int[]>(value);
		return new AtomicIntegerArray(intArray);
	}
}

/// <summary>
/// AtomicIntegerArray封装类
/// </summary>
public class AtomicIntegerArray
{
	private readonly int[] _array;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="length">长度</param>
	public AtomicIntegerArray(int length)
	{
		_array = new int[length];
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="array">数组</param>
	public AtomicIntegerArray(int[] array)
	{
		_array = (int[])array.Clone();
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
	public int Get(int index) => _array[index];

	/// <summary>
	/// 设置值
	/// </summary>
	/// <param name="index">索引</param>
	/// <param name="newValue">新值</param>
	public void Set(int index, int newValue)
	{
		_array[index] = newValue;
	}
}
