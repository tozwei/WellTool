using System;
using System.Text;

namespace WellTool.Core.Util;

/// <summary>
/// Hex工具类
/// </summary>
public static class HexUtil
{
	private static readonly char[] HEX_CHARS = "0123456789abcdef".ToCharArray();

	/// <summary>
	/// 将字节数组转换为十六进制字符串
	/// </summary>
	/// <param name="bytes">字节数组</param>
	/// <returns>十六进制字符串</returns>
	public static string Encode(string str) => Encode(str, Encoding.UTF8);

	/// <summary>
	/// 将字符串转换为十六进制字符串
	/// </summary>
	/// <param name="str">字符串</param>
	/// <param name="encoding">编码</param>
	/// <returns>十六进制字符串</returns>
	public static string Encode(string str, Encoding encoding)
	{
		if (string.IsNullOrEmpty(str))
			return str;
		return Encode(encoding.GetBytes(str));
	}

	/// <summary>
	/// 将字节数组转换为十六进制字符串
	/// </summary>
	/// <param name="bytes">字节数组</param>
	/// <returns>十六进制字符串</returns>
	public static string Encode(byte[] bytes)
	{
		if (bytes == null || bytes.Length == 0)
			return string.Empty;

		var result = new char[bytes.Length * 2];
		for (int i = 0; i < bytes.Length; i++)
		{
			result[i * 2] = HEX_CHARS[(bytes[i] >> 4) & 0xF];
			result[i * 2 + 1] = HEX_CHARS[bytes[i] & 0xF];
		}
		return new string(result);
	}

	/// <summary>
	/// 将十六进制字符串转换为字节数组
	/// </summary>
	/// <param name="hex">十六进制字符串</param>
	/// <returns>字节数组</returns>
	public static byte[] Decode(string hex)
	{
		if (string.IsNullOrEmpty(hex))
			return Array.Empty<byte>();

		if (hex.Length % 2 != 0)
			throw new ArgumentException("Hex string must have an even length");

		var bytes = new byte[hex.Length / 2];
		for (int i = 0; i < bytes.Length; i++)
		{
			bytes[i] = (byte)(HexToInt(hex[i * 2]) << 4 | HexToInt(hex[i * 2 + 1]));
		}
		return bytes;
	}

	/// <summary>
	/// 将十六进制字符串转换为字符串
	/// </summary>
	/// <param name="hex">十六进制字符串</param>
	/// <returns>字符串</returns>
	public static string DecodeToString(string hex) => DecodeToString(hex, Encoding.UTF8);

	/// <summary>
	/// 将十六进制字符串转换为字符串
	/// </summary>
	/// <param name="hex">十六进制字符串</param>
	/// <param name="encoding">编码</param>
	/// <returns>字符串</returns>
	public static string DecodeToString(string hex, Encoding encoding)
	{
		var bytes = Decode(hex);
		return encoding.GetString(bytes);
	}

	private static int HexToInt(char c)
	{
		if (c >= '0' && c <= '9')
			return c - '0';
		if (c >= 'a' && c <= 'f')
			return c - 'a' + 10;
		if (c >= 'A' && c <= 'F')
			return c - 'A' + 10;
		throw new ArgumentException($"Invalid hex character: {c}");
	}
}
