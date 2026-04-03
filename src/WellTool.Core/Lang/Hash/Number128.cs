namespace WellTool.Core.Lang.Hash;

/// <summary>
/// 128位数字工具类
/// </summary>
public static class Number128Util
{
	/// <summary>
	/// 从字节数组创建128位数字
	/// </summary>
	/// <param name="bytes">16字节数组</param>
	/// <returns>128位数字</returns>
	public static Number128 FromBytes(byte[] bytes)
	{
		if (bytes.Length < 16)
			throw new ArgumentException("Byte array must be at least 16 bytes", nameof(bytes));

		var high = BitConverter.ToInt64(bytes, 0);
		var low = BitConverter.ToInt64(bytes, 8);
		return new Number128(high, low);
	}

	/// <summary>
	/// 转换为字节数组
	/// </summary>
	/// <param name="number">128位数字</param>
	/// <returns>16字节数组</returns>
	public static byte[] ToBytes(Number128 number)
	{
		var bytes = new byte[16];
		Array.Copy(BitConverter.GetBytes(number.High), 0, bytes, 0, 8);
		Array.Copy(BitConverter.GetBytes(number.Low), 0, bytes, 8, 8);
		return bytes;
	}

	/// <summary>
	/// 转换为十六进制字符串
	/// </summary>
	/// <param name="number">128位数字</param>
	/// <returns>32字符十六进制字符串</returns>
	public static string ToHexString(Number128 number)
	{
		return $"{number.High:x16}{number.Low:x16}";
	}

	/// <summary>
	/// 从十六进制字符串解析
	/// </summary>
	/// <param name="hex">32字符十六进制字符串</param>
	/// <returns>128位数字</returns>
	public static Number128 FromHexString(string hex)
	{
		if (string.IsNullOrEmpty(hex))
			throw new ArgumentException("Hex string cannot be empty", nameof(hex));

		hex = hex.Trim().Replace("-", "");
		if (hex.Length != 32)
			throw new ArgumentException("Hex string must be 32 characters", nameof(hex));

		var high = System.Convert.ToInt64(hex[..16], 16);
		var low = System.Convert.ToInt64(hex[16..], 16);
		return new Number128(high, low);
	}

	/// <summary>
	/// 获取高位64位
	/// </summary>
	/// <param name="number">128位数字</param>
	/// <returns>高位</returns>
	public static long GetHigh(Number128 number) => number.High;

	/// <summary>
	/// 获取低位64位
	/// </summary>
	/// <param name="number">128位数字</param>
	/// <returns>低位</returns>
	public static long GetLow(Number128 number) => number.Low;
}
