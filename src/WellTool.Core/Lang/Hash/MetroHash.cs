using System;

namespace WellTool.Core.Lang.Hash;

/// <summary>
/// MetroHash算法实现
/// </summary>
public static class MetroHash
{
	/// <summary>
	/// 计算64位MetroHash
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>hash值</returns>
	public static ulong Hash64(byte[] data)
	{
		return Hash64(data, 0);
	}

	/// <summary>
	/// 计算64位MetroHash
	/// </summary>
	/// <param name="data">数据</param>
	/// <param name="seed">种子</param>
	/// <returns>hash值</returns>
	public static ulong Hash64(byte[] data, ulong seed)
	{
		var len = data.Length;
		seed ^= (ulong)len;

		var state = new ulong[4];
		state[0] = 0x9ae16a3b1923243UL ^ seed;
		state[1] = 0x079b84d9c46d47f5UL ^ seed;
		state[2] = 0x1735cc9ba41e165dUL ^ seed;
		state[3] = 0x2885465686b42da5UL ^ seed;

		var idx = 0;

		// 处理8字节块
		var end8 = len - (len % 8);
		for (; idx < end8; idx += 8)
		{
			state[0] += BitConverter.ToUInt64(data, idx);
			MetroHash64_IV(state);
			state[0] ^= state[0] >> 33;
			state[0] *= 0xc2b2ae63d16a2e3aUL;
			state[0] ^= state[0] >> 33;
			state[0] *= 0xc2b2ae63d16a2e3aUL;
			state[0] ^= state[0] >> 33;
		}

		// 处理剩余字节
		var remaining = len - idx;
		if (remaining > 0)
		{
			var buffer = new byte[8];
			Array.Copy(data, idx, buffer, 0, remaining);
			state[0] += BitConverter.ToUInt64(buffer, 0);
			MetroHash64_IV(state);
			state[0] ^= state[0] >> 33;
			state[0] *= 0xc2b2ae63d16a2e3aUL;
			state[0] ^= state[0] >> 33;
			state[0] *= 0xc2b2ae63d16a2e3aUL;
			state[0] ^= state[0] >> 33;
		}

		state[0] ^= state[0] >> 33;
		state[0] *= 0xc2b2ae63d16a2e3aUL;
		state[0] ^= state[0] >> 33;

		return state[0];
	}

	private static void MetroHash64_IV(ulong[] h)
	{
		h[0] ^= h[0] >> 33;
		h[0] *= 0xff51afd7ed558ccdUL;
		h[0] ^= h[0] >> 33;
		h[0] *= 0xc4ceb9fe1a85ec53UL;
		h[0] ^= h[0] >> 33;

		h[1] ^= h[1] >> 33;
		h[1] *= 0xff51afd7ed558ccdUL;
		h[1] ^= h[1] >> 33;
		h[1] *= 0xc4ceb9fe1a85ec53UL;
		h[1] ^= h[1] >> 33;

		h[2] ^= h[2] >> 33;
		h[2] *= 0xff51afd7ed558ccdUL;
		h[2] ^= h[2] >> 33;
		h[2] *= 0xc4ceb9fe1a85ec53UL;
		h[2] ^= h[2] >> 33;

		h[3] ^= h[3] >> 33;
		h[3] *= 0xff51afd7ed558ccdUL;
		h[3] ^= h[3] >> 33;
		h[3] *= 0xc4ceb9fe1a85ec53UL;
		h[3] ^= h[3] >> 33;
	}
}
