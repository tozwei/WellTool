using System;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// AtomicLongArray杞崲鍣?
/// </summary>
public class AtomicLongArrayConverter : AbstractConverter<AtomicLongArray>
{
	/// <summary>
	/// 鍐呴儴杞崲
	/// </summary>
	/// <param name="value">鍊?/param>
	/// <returns>缁撴灉</returns>
	protected override AtomicLongArray ConvertInternal(object value)
	{
		if (value is long[] longArray)
		{
			return new AtomicLongArray(longArray);
		}
		throw new InvalidOperationException("Cannot convert to long array");
	}
}

/// <summary>
/// AtomicLongArray灏佽绫?
/// </summary>
public class AtomicLongArray
{
	private readonly long[] _array;

	/// <summary>
	/// 鏋勯€?
	/// </summary>
	/// <param name="length">闀垮害</param>
	public AtomicLongArray(int length)
	{
		_array = new long[length];
	}

	/// <summary>
	/// 鏋勯€?
	/// </summary>
	/// <param name="array">鏁扮粍</param>
	public AtomicLongArray(long[] array)
	{
		_array = (long[])array.Clone();
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
	public long Get(int index) => _array[index];

	/// <summary>
	/// 璁剧疆鍊?
	/// </summary>
	/// <param name="index">绱㈠紩</param>
	/// <param name="newValue">鏂板€?/param>
	public void Set(int index, long newValue)
	{
		_array[index] = newValue;
	}
}

