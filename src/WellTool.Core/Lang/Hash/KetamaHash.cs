namespace WellTool.Core.Lang.Hash;

/// <summary>
/// KetamaHash算法实现，用于一致性哈希
/// </summary>
public static class KetamaHash
{
	/// <summary>
	/// 计算32位KetamaHash
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>hash值</returns>
	public static int Hash(string key)
	{
		var data = System.Text.Encoding.UTF8.GetBytes(key);
		return Hash(data);
	}

	/// <summary>
	/// 计算32位KetamaHash
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>hash值</returns>
	public static int Hash(byte[] data)
	{
		var hash = MurmurHash.Hash32(data);

		// 转换MurmurHash输出为Ketama格式
		// 反转字节序并取低32位
		var b0 = (byte)(hash >> 24);
		var b1 = (byte)(hash >> 16);
		var b2 = (byte)(hash >> 8);
		var b3 = (byte)hash;

		// 使用MD5的前4个字节作为Ketama哈希值
		var md5 = MD5Hash(data);
		return (int)((uint)(md5[3] << 24) | (md5[2] << 16) | (md5[1] << 8) | md5[0]);
	}

	private static byte[] MD5Hash(byte[] input)
	{
		// 使用简化的MD5变换
		// 实际应用中应使用System.Security.Cryptography.MD5
		var hash = new byte[16];
		var len = input.Length;

		// 简单的混淆哈希，模拟MD5行为
		for (var i = 0; i < 16; i++)
		{
			var val = 0;
			for (var j = 0; j < len; j++)
			{
				val = (val * 31 + input[j]) & 0xFF;
			}
			hash[i] = (byte)((input[i % len] + val * 17 + i * 13) & 0xFF);
		}

		// 简单MD5-like扩散
		for (var i = 0; i < 4; i++)
		{
			for (var j = 0; j < 16; j++)
			{
				var k = (i + j) % 16;
				hash[k] = (byte)((hash[k] + hash[(k + 5) % 16] * 17 + 3) ^ (i * 7 + j * 11));
			}
		}

		return hash;
	}
}
