namespace WellTool.Core.Lang.Hash;

/// <summary>
/// Hash计算接口
/// </summary>
/// <typeparam name="T">被计算hash的对象类型</typeparam>
public interface IHash<T>
{
	/// <summary>
	/// 计算Hash值
	/// </summary>
	/// <param name="t">对象</param>
	/// <returns>hash值</returns>
	object HashIt(T t);
}

/// <summary>
/// 32位Hash计算接口
/// </summary>
/// <typeparam name="T">被计算hash的对象类型</typeparam>
public interface IHash32<T> : IHash<T>
{
	/// <summary>
	/// 计算Hash值
	/// </summary>
	/// <param name="t">对象</param>
	/// <returns>hash值</returns>
	int Hash32(T t);

	/// <inheritdoc />
	object IHash<T>.HashIt(T t) => Hash32(t);
}

/// <summary>
/// 64位Hash计算接口
/// </summary>
/// <typeparam name="T">被计算hash的对象类型</typeparam>
public interface IHash64<T> : IHash<T>
{
	/// <summary>
	/// 计算Hash值
	/// </summary>
	/// <param name="t">对象</param>
	/// <returns>hash值</returns>
	long Hash64(T t);

	/// <inheritdoc />
	object IHash<T>.HashIt(T t) => Hash64(t);
}

/// <summary>
/// 128位Hash计算接口
/// </summary>
/// <typeparam name="T">被计算hash的对象类型</typeparam>
public interface IHash128<T> : IHash<T>
{
	/// <summary>
	/// 计算Hash值
	/// </summary>
	/// <param name="t">对象</param>
	/// <returns>hash值</returns>
	Number128 Hash128(T t);

	/// <inheritdoc />
	object IHash<T>.HashIt(T t) => Hash128(t);
}

/// <summary>
/// 128位数字，用于存储128位hash值
/// </summary>
public readonly struct Number128 : IEquatable<Number128>
{
	/// <summary>
	/// 高64位
	/// </summary>
	public long High { get; }

	/// <summary>
	/// 低64位
	/// </summary>
	public long Low { get; }

	/// <summary>
	/// 构造
	/// </summary>
	public Number128(long high, long low)
	{
		High = high;
		Low = low;
	}

	/// <inheritdoc />
	public bool Equals(Number128 other) => High == other.High && Low == other.Low;

	/// <inheritdoc />
	public override bool Equals(object? obj) => obj is Number128 other && Equals(other);

	/// <inheritdoc />
	public override int GetHashCode() => HashCode.Combine(High, Low);

	/// <inheritdoc />
	public override string ToString() => $"{High:x16}{Low:x16}";
}
