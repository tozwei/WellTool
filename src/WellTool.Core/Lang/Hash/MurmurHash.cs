namespace WellTool.Core.Lang.Hash;

/// <summary>
/// MurmurHash3算法实现
/// </summary>
public static class MurmurHash
{
	private const uint Seed = 0;

	/// <summary>
	/// 计算32位MurmurHash
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>hash值</returns>
	public static int Hash32(byte[] data)
	{
		return Hash32(data, Seed);
	}

	/// <summary>
	/// 计算32位MurmurHash
	/// </summary>
	/// <param name="data">数据</param>
	/// <param name="seed">种子</param>
	/// <returns>hash值</returns>
	public static int Hash32(byte[] data, uint seed)
	{
		var length = data.Length;
		var h1 = seed;
		var c1 = 0xcc9e2d51u;
		var c2 = 0x1b873593u;

		// 遍历4字节块
		var roundedEnd = (length & ~3u);
		for (uint i = 0; i < roundedEnd; i += 4)
		{
			var k1 = (uint)(data[i] | (data[i + 1] << 8) | (data[i + 2] << 16) | (data[i + 3] << 24));
			k1 *= c1;
			k1 = rotl32(k1, 15);
			k1 *= c2;

			h1 ^= k1;
			h1 = rotl32(h1, 13);
			h1 = h1 * 5 + 0xe6546b64;
		}

		// 处理剩余字节
		var k1_ = 0u;
		var len = length - roundedEnd;
		if (len > 0)
		{
			switch (len)
			{
				case 3: k1_ ^= (uint)data[roundedEnd + 2] << 16; goto case 2;
				case 2: k1_ ^= (uint)data[roundedEnd + 1] << 8; goto case 1;
				case 1:
					k1_ ^= data[roundedEnd];
					k1_ *= c1;
					k1_ = rotl32(k1_, 15);
					k1_ *= c2;
					h1 ^= k1_;
					break;
			}
		}

		// 最终化
		h1 ^= (uint)length;
		h1 = fmix32(h1);
		return (int)h1;
	}

	/// <summary>
	/// 计算64位MurmurHash
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>hash值</returns>
	public static long Hash64(byte[] data)
	{
		return Hash64(data, Seed);
	}

	/// <summary>
	/// 计算64位MurmurHash
	/// </summary>
	/// <param name="data">数据</param>
	/// <param name="seed">种子</param>
	/// <returns>hash值</returns>
	public static long Hash64(byte[] data, uint seed)
	{
		var length = data.Length;
		var h1 = seed;
		var h2 = seed;
		var c1 = 0x239b961bu;
		var c2 = 0xab0e9789u;
		var c3 = 0x38b34ae5u;
		var c4 = 0x1d56c9e7u;

		// 遍历4字节块
		var roundedEnd = (uint)(length & ~3u);
		for (uint i = 0; i < roundedEnd; i += 4)
		{
			var k1 = (uint)(data[i] | (data[i + 1] << 8) | (data[i + 2] << 16) | (data[i + 3] << 24));
			k1 *= c1;
			k1 = rotl32(k1, 15);
			k1 *= c2;
			h1 ^= k1;
			h1 = rotl32(h1, 19);
			h1 += h2;
			h1 = h1 * 5 + 0x561ccd1bu;

			var k2 = k1; // 复用k1的低32位，高32位清零
			h2 ^= k2;
			h2 = rotl32(h2, 17);
			h2 += h1;
			h2 = h2 * 5 + 0x0bcaa747u;
		}

		// 处理剩余字节
		var k1_ = 0u;
		var k2_ = 0u;
		var remaining = length - roundedEnd;
		if (remaining > 0)
		{
			if (remaining >= 3) k1_ ^= (uint)data[roundedEnd + 2] << 16;
			if (remaining >= 2) k1_ ^= (uint)data[roundedEnd + 1] << 8;
			if (remaining >= 1) k1_ ^= data[roundedEnd];

			k1_ *= c1;
			k1_ = rotl32(k1_, 15);
			k1_ *= c2;
			h1 ^= k1_;

			if (remaining >= 2)
			{
				k2_ ^= (uint)data[roundedEnd + 1] << 8;
				k2_ ^= remaining >= 3 ? (uint)data[roundedEnd + 2] << 16 : 0;
				k2_ *= c3;
				k2_ = rotl32(k2_, 16);
				k2_ *= c4;
				h2 ^= k2_;
			}
		}

		// 最终化
		h1 ^= (uint)length;
		h2 ^= (uint)length;
		h1 += h2;
		h2 += h1;
		h1 = fmix32(h1);
		h2 = fmix32(h2);
		h1 += h2;
		h2 += h1;

		return ((long)h1 << 32) | h2;
	}

	private static uint rotl32(uint x, byte r) => (x << r) | (x >> (32 - r));

	private static uint fmix32(uint h)
	{
		h ^= h >> 16;
		h *= 0x85ebca6bu;
		h ^= h >> 13;
		h *= 0xc2b2ae35u;
		h ^= h >> 16;
		return h;
	}
}
