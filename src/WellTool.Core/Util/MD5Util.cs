using System;
using System.Security.Cryptography;
using System.Text;

namespace WellTool.Core.Util;

/// <summary>
/// MD5工具类
/// </summary>
public static class MD5Util
{
	/// <summary>
	/// 计算MD5
	/// </summary>
	/// <param name="input">输入字符串</param>
	/// <returns>MD5哈希（32位小写）</returns>
	public static string Hash(string input) => Hash(input, Encoding.UTF8);

	/// <summary>
	/// 计算MD5
	/// </summary>
	/// <param name="input">输入字符串</param>
	/// <param name="encoding">编码</param>
	/// <returns>MD5哈希（32位小写）</returns>
	public static string Hash(string input, Encoding encoding)
	{
		if (string.IsNullOrEmpty(input))
			return input;
		return Hash(encoding.GetBytes(input));
	}

	/// <summary>
	/// 计算MD5
	/// </summary>
	/// <param name="data">输入数据</param>
	/// <returns>MD5哈希（32位小写）</returns>
	public static string Hash(byte[] data)
	{
		if (data == null || data.Length == 0)
			return string.Empty;
		using var md5 = MD5.Create();
		var hash = md5.ComputeHash(data);
		return BitConverter.ToString(hash).Replace("-", "").ToLower();
	}

	/// <summary>
	/// 计算MD5（16位）
	/// </summary>
	/// <param name="input">输入字符串</param>
	/// <returns>MD5哈希（16位小写）</returns>
	public static string Hash16(string input) => Hash16(input, Encoding.UTF8);

	/// <summary>
	/// 计算MD5（16位）
	/// </summary>
	/// <param name="input">输入字符串</param>
	/// <param name="encoding">编码</param>
	/// <returns>MD5哈希（16位小写）</returns>
	public static string Hash16(string input, Encoding encoding)
	{
		var hash = Hash(input, encoding);
		return hash.Length >= 24 ? hash.Substring(8, 16) : hash;
	}
}

/// <summary>
/// MD5别名
/// </summary>
public static class MD5
{
	/// <summary>
	/// 计算MD5
	/// </summary>
	/// <param name="input">输入</param>
	/// <returns>MD5哈希</returns>
	public static string Compute(string input) => MD5Util.Hash(input);

	/// <summary>
	/// 计算MD5
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>MD5哈希</returns>
	public static string Compute(byte[] data) => MD5Util.Hash(data);
}
