using System;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// AtomicIntegerArray杞崲鍣?
/// </summary>
public class AtomicIntegerArrayConverter : AbstractConverter<AtomicIntegerArray>
{
	/// <summary>
	/// 鍐呴儴杞崲
	/// </summary>
	/// <param name="value">鍊?/param>
	/// <returns>缁撴灉</returns>
	protected override AtomicIntegerArray ConvertInternal(object value)
	{
		if (value is int[] intArray)
		{
			return new AtomicIntegerArray(intArray);
		}
		throw new InvalidOperationException("Cannot convert to int array");
	}
}

/// <summary>
/// AtomicIntegerArray灏佽绫?
/// </summary>
public class AtomicIntegerArray
{
	private readonly int[] _array;

	/// <summary>
	/// 鏋勯€?
	/// </summary>
	/// <param name="length">闀垮害</param>
	public AtomicIntegerArray(int length)
	{
		_array = new int[length];
	}

	/// <summary>
	/// 鏋勯€?
	/// </summary>
	/// <param name="array">鏁扮粍</param>
	public AtomicIntegerArray(int[] array)
	{
		_array = (int[])array.Clone();
	}

	/// <summary>
	/// 鑾峰彇闀垮害
	/// </summary>
	public int Length => _array.Length;

	/// <summary>
	/// 鑾峰彇鍊?
	/// </summary>
	/// <param name="index">绱㈠紩</param>
	/// <returns>鍊?/returns>
	public int Get(int index) => _array[index];

	/// <summary>
	/// 璁剧疆鍊?
	/// </summary>
	/// <param name="index">绱㈠紩</param>
	/// <param name="newValue">鏂板€?/param>
	public void Set(int index, int newValue)
	{
		_array[index] = newValue;
	}
}

