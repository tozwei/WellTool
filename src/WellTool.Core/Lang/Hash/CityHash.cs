using System.Collections;

namespace WellTool.Core.Lang.Hash;

/// <summary>
/// CityHash算法实现
/// </summary>
public static class CityHash
{
	/// <summary>
	/// 计算64位CityHash
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>hash值</returns>
	public static ulong Hash64(byte[] data)
	{
		return Hash64(data, 0);
	}

	/// <summary>
	/// 计算64位CityHash
	/// </summary>
	/// <param name="data">数据</param>
	/// <param name="seed">种子</param>
	/// <returns>hash值</returns>
	public static ulong Hash64(byte[] data, ulong seed)
	{
		var len = data.Length;
		if (len <= 32)
		{
			if (len <= 16)
				return HashLen0to16(data, seed);
			return HashLen17to32(data, seed);
		}
		if (len <= 64)
			return HashLen33to64(data, seed);

		// 对于更长的数据，使用分块哈希
		var x = seed;
		var y = seed * 0x9ae16a3b2f90404dUL + (ulong)len;
		var z = bswap64(y);
		var v = new ulong[2];
		var w = new ulong[2];
		v[0] = y; v[1] = z;
		w[0] = x; w[1] = x;

		var idx = 0;
		var end = len - 64;
		var a = HashLen16(HashLen16(BitConverter.ToUInt64(data, 0), BitConverter.ToUInt64(data, 8), 0x9ae16a3b2f90404dUL), w[0], w[1]);

		while (idx <= end)
		{
			x = x * 0x9ae16a3b2f90404dUL + bswap64(BinaryHash(data, idx, ref idx));
			y = y * 0x9ae16a3b2f90404dUL + bswap64(BinaryHash(data, idx, ref idx));
			z = z * 0x9ae16a3b2f90404dUL + bswap64(BinaryHash(data, idx, ref idx));

			var t = x + 11;
			v[0] += 8;
			v[0] = HashLen16(v[0], BinaryHash(data, idx, ref idx));
			v[0] += t;
			v[1] = HashLen16(v[1], BinaryHash(data, idx, ref idx));
			v[1] += z;
			w[0] = HashLen16(w[0], BinaryHash(data, idx, ref idx));
			w[1] = HashLen16(w[1], BinaryHash(data, idx, ref idx));
		}

		y += HashLen16(v[0], w[0]) + w[1] + z;
		x += HashLen16(v[1], w[1]) + w[0];
		z += HashLen16(v[0], v[1]) + HashLen16(w[0], w[1]);

		var finalBlock = new byte[32];
		Array.Copy(data, Math.Max(0, len - 32), finalBlock, 0, Math.Min(32, len));

		return HashLen16(HashLen16(x, y, z) + v[0] + w[0], HashLen16(x, z, w[1]) + v[1] + HashLen16(y, z, w[0] + v[0]));
	}

	private static ulong HashLen0to16(byte[] data, ulong seed)
	{
		var len = data.Length;
		if (len >= 8)
		{
			var a = BitConverter.ToUInt64(data, 0);
			var b = BitConverter.ToUInt64(data, len - 8);
			return HashLen16(a, b ^ seed);
		}
		if (len >= 4)
		{
			var a = BitConverter.ToUInt32(data, 0);
			var b = BitConverter.ToUInt32(data, Math.Max(0, len - 4));
			return HashLen16(a, b ^ seed);
		}
		if (len > 0)
		{
			var a = data[0];
			var b = data[len >> 1];
			var c = data[len - 1];
			var y = a + (uint)(b * 0x9ae16a3b2f90404dUL >> 32);
			var x = y + (uint)(c * 0x9ae16a3b2f90404dUL >> 32);
			return HashLen16(x, y);
		}
		return seed;
	}

	private static ulong HashLen17to32(byte[] data, ulong seed)
	{
		var len = data.Length;
		var a = HashLen16(BitConverter.ToUInt64(data, len - 16), seed);
		var b = HashLen16(BitConverter.ToUInt64(data, len - 8), BitConverter.ToUInt64(data, len - 16));
		return HashLen16(a ^ b, HashLen16(BitConverter.ToUInt64(data, 0), b));
	}

	private static ulong HashLen33to64(byte[] data, ulong seed)
	{
		var len = data.Length;
		var z = HashLen16(BitConverter.ToUInt64(data, 24), BitConverter.ToUInt64(data, len - 32));
		var y = HashLen16(BitConverter.ToUInt64(data, len - 8), seed ^ ((unchecked((ulong)len) << 1)));
		var x = HashLen16(BitConverter.ToUInt64(data, 0) + ((ulong)len << 1), BitConverter.ToUInt64(data, 8));
		var v = new ulong[2] { x, y };
		var w = new ulong[2];
		w[0] = permute(permute(x, 0) + z, z);
		w[1] = permute(permute(y, 0) + z, z);
		return HashLen16(HashLen16(w[0], w[1]) + x, HashLen16(w[0], w[1]) + z);
	}

	private static ulong[] w = new ulong[2];

	private static ulong HashLen16(ulong u, ulong v)
	{
		return HashLen16(u, v, 0);
	}

	private static ulong HashLen16(ulong u, ulong v, ulong mul)
	{
		var a = (u ^ v) * mul;
		a ^= a >> 47;
		var b = (v ^ a) * mul;
		b ^= b >> 47;
		b *= mul;
		return b;
	}

	private static ulong permute(ulong x, ulong z)
	{
		return ((x * 0x9ae16a3b2f90404dUL + z) * 0x9ae16a3b2f90404dUL);
	}

	private static ulong bswap64(ulong x)
	{
		return ((x & 0xff00000000000000UL) >> 56) |
			   ((x & 0x00ff000000000000UL) >> 40) |
			   ((x & 0x0000ff0000000000UL) >> 24) |
			   ((x & 0x000000ff00000000UL) >> 8) |
			   ((x & 0x00000000ff000000UL) << 8) |
			   ((x & 0x0000000000ff0000UL) << 24) |
			   ((x & 0x000000000000ff00UL) << 40) |
			   ((x & 0x00000000000000ffUL) << 56);
	}

	private static ulong BinaryHash(byte[] data, int start, ref int end)
	{
		end = Math.Min(start + 8, data.Length);
		if (start >= data.Length)
		{
			end = start;
			return 0;
		}
		var result = new byte[8];
		Array.Copy(data, start, result, 0, Math.Min(8, data.Length - start));
		return BitConverter.ToUInt64(result, 0);
	}
}
