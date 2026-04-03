using System;
using System.Text;

namespace WellDone.Core.Util;

/// <summary>
/// Base64工具类
/// </summary>
public static class Base64Util
{
	/// <summary>
	/// Base64编码
	/// </summary>
	/// <param name="input">输入字符串</param>
	/// <returns>Base64编码字符串</returns>
	public static string Encode(string input) => Encode(input, Encoding.UTF8);

	/// <summary>
	/// Base64编码
	/// </summary>
	/// <param name="input">输入字符串</param>
	/// <param name="encoding">编码</param>
	/// <returns>Base64编码字符串</returns>
	public static string Encode(string input, Encoding encoding)
	{
		if (string.IsNullOrEmpty(input))
			return input;
		return Encode(encoding.GetBytes(input));
	}

	/// <summary>
	/// Base64编码
	/// </summary>
	/// <param name="data">字节数组</param>
	/// <returns>Base64编码字符串</returns>
	public static string Encode(byte[] data)
	{
		if (data == null || data.Length == 0)
			return string.Empty;
		return Convert.ToBase64String(data);
	}

	/// <summary>
	/// Base64解码
	/// </summary>
	/// <param name="input">Base64编码字符串</param>
	/// <returns>原始字符串</returns>
	public static string Decode(string input) => Decode(input, Encoding.UTF8);

	/// <summary>
	/// Base64解码
	/// </summary>
	/// <param name="input">Base64编码字符串</param>
	/// <param name="encoding">编码</param>
	/// <returns>原始字符串</returns>
	public static string Decode(string input, Encoding encoding)
	{
		if (string.IsNullOrEmpty(input))
			return input;
		return encoding.GetString(DecodeBytes(input));
	}

	/// <summary>
	/// Base64解码
	/// </summary>
	/// <param name="input">Base64编码字符串</param>
	/// <returns>字节数组</returns>
	public static byte[] DecodeBytes(string input)
	{
		if (string.IsNullOrEmpty(input))
			return Array.Empty<byte>();
		return Convert.FromBase64String(input);
	}
}

/// <summary>
/// Base64别名
/// </summary>
public static class Base64
{
	/// <summary>
	/// Base64编码
	/// </summary>
	public static string Encode(string input) => Base64Util.Encode(input);

	/// <summary>
	/// Base64编码
	/// </summary>
	public static string Encode(byte[] data) => Base64Util.Encode(data);

	/// <summary>
	/// Base64解码
	/// </summary>
	public static string Decode(string input) => Base64Util.Decode(input);

	/// <summary>
	/// Base64解码
	/// </summary>
	public static byte[] DecodeBytes(string input) => Base64Util.DecodeBytes(input);
}
