using System;
using System.Security.Cryptography;
using System.Text;

namespace WellTool.Core.Lang;

/// <summary>
/// 提供通用唯一识别码（UUID）实现
/// </summary>
[Serializable]
public sealed class UUID : IComparable<UUID>
{
	private static readonly SecureRandom NUMBER_GENERATOR = new SecureRandom();

	/// <summary>
	/// 此UUID的最高64有效位
	/// </summary>
	private readonly long _mostSigBits;

	/// <summary>
	/// 此UUID的最低64有效位
	/// </summary>
	private readonly long _leastSigBits;

	/// <summary>
	/// 私有构造
	/// </summary>
	/// <param name="mostSigBits">最高有效位</param>
	/// <param name="leastSigBits">最低有效位</param>
	public UUID(long mostSigBits, long leastSigBits)
	{
		_mostSigBits = mostSigBits;
		_leastSigBits = leastSigBits;
	}

	/// <summary>
	/// 获取类型4（伪随机生成的）UUID
	/// </summary>
	/// <returns>随机生成的UUID</returns>
	public static UUID FastUUID()
	{
		return RandomUUID(false);
	}

	/// <summary>
	/// 获取类型4（伪随机生成的）UUID
	/// </summary>
	/// <returns>随机生成的UUID</returns>
	public static UUID RandomUUID()
	{
		return RandomUUID(true);
	}

	/// <summary>
	/// 获取类型4（伪随机生成的）UUID
	/// </summary>
	/// <param name="isSecure">是否使用SecureRandom</param>
	/// <returns>随机生成的UUID</returns>
	public static UUID RandomUUID(bool isSecure)
	{
		var randomBytes = new byte[16];
		if (isSecure)
		{
			var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomBytes);
		}
		else
		{
			var rng = new Random();
			rng.NextBytes(randomBytes);
		}

		randomBytes[6] = (byte)((randomBytes[6] & 0x0f) | 0x40);
		randomBytes[8] = (byte)((randomBytes[8] & 0x3f) | 0x80);

		return new UUID(
			BitConverter.ToInt64(randomBytes, 0),
			BitConverter.ToInt64(randomBytes, 8));
	}

	/// <summary>
	/// 根据指定的字节数组获取类型3（基于名称的）UUID
	/// </summary>
	/// <param name="name">用于构造UUID的字节数组</param>
	/// <returns>根据指定数组生成的UUID</returns>
	public static UUID NameUUIDFromBytes(byte[] name)
	{
		using var md5 = MD5.Create();
		var md5Bytes = md5.ComputeHash(name);

		md5Bytes[6] = (byte)((md5Bytes[6] & 0x0f) | 0x30);
		md5Bytes[8] = (byte)((md5Bytes[8] & 0x3f) | 0x80);

		return new UUID(
			BitConverter.ToInt64(md5Bytes, 0),
			BitConverter.ToInt64(md5Bytes, 8));
	}

	/// <summary>
	/// 根据UUID字符串创建UUID
	/// </summary>
	/// <param name="name">UUID字符串</param>
	/// <returns>UUID</returns>
	public static UUID FromString(string name)
	{
		var components = name.Split('-');
		if (components.Length != 5)
		{
			throw new FormatException("Invalid UUID string: " + name);
		}

		long mostSigBits = System.Convert.ToInt64(components[0], 16);
		mostSigBits <<= 16;
		mostSigBits |= System.Convert.ToInt64(components[1], 16);
		mostSigBits <<= 16;
		mostSigBits |= System.Convert.ToInt64(components[2], 16);

		long leastSigBits = System.Convert.ToInt64(components[3], 16);
		leastSigBits <<= 48;
		leastSigBits |= System.Convert.ToInt64(components[4], 16);

		return new UUID(mostSigBits, leastSigBits);
	}

	/// <summary>
	/// 返回此UUID的最低有效64位
	/// </summary>
	public long GetLeastSignificantBits() => _leastSigBits;

	/// <summary>
	/// 返回此UUID的最高有效64位
	/// </summary>
	public long GetMostSignificantBits() => _mostSigBits;

	/// <summary>
	/// 获取UUID版本号
	/// </summary>
	public int Version => (int)((_mostSigBits >> 12) & 0x0f);

	/// <summary>
	/// 获取UUID变体号
	/// </summary>
	public int Variant => (int)(((_leastSigBits >>> (64 - (int)((uint)(_leastSigBits >> 62)))) & (_leastSigBits >> 63)));

	/// <summary>
	/// 返回UUID的字符串表现形式
	/// </summary>
	/// <returns>UUID字符串</returns>
	public override string ToString() => ToString(false);

	/// <summary>
	/// 返回UUID的字符串表现形式
	/// </summary>
	/// <param name="isSimple">是否简单模式（不带-）</param>
	/// <returns>UUID字符串</returns>
	public string ToString(bool isSimple)
	{
		var sb = new StringBuilder(isSimple ? 32 : 36);

		sb.Append(Digits(_mostSigBits >> 32, 8));
		if (!isSimple) sb.Append('-');
		sb.Append(Digits(_mostSigBits >> 16, 4));
		if (!isSimple) sb.Append('-');
		sb.Append(Digits(_mostSigBits, 4));
		if (!isSimple) sb.Append('-');
		sb.Append(Digits(_leastSigBits >> 48, 4));
		if (!isSimple) sb.Append('-');
		sb.Append(Digits(_leastSigBits, 12));

		return sb.ToString();
	}

	/// <summary>
	/// 返回UUID的哈希码
	/// </summary>
	public override int GetHashCode()
	{
		long hilo = _mostSigBits ^ _leastSigBits;
		return ((int)(hilo >> 32)) ^ (int)hilo;
	}

	/// <summary>
	/// 判断两个UUID是否相等
	/// </summary>
	public override bool Equals(object obj)
	{
		if (obj == null || obj.GetType() != typeof(UUID))
		{
			return false;
		}
		var id = (UUID)obj;
		return _mostSigBits == id._mostSigBits && _leastSigBits == id._leastSigBits;
	}

	/// <summary>
	/// 比较两个UUID
	/// </summary>
	public int CompareTo(UUID val)
	{
		int compare = _mostSigBits.CompareTo(val._mostSigBits);
		if (compare == 0)
		{
			compare = _leastSigBits.CompareTo(val._leastSigBits);
		}
		return compare;
	}

	/// <summary>
	/// 返回指定数字对应的hex值
	/// </summary>
	private static string Digits(long val, int digits)
	{
		long hi = 1L << (digits * 4);
		return (hi | (val & (hi - 1))).ToString("x").Substring(1);
	}
}

internal class SecureRandom
{
	private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

	public void GetBytes(byte[] data)
	{
		_rng.GetBytes(data);
	}
}
