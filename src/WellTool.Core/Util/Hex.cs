using System;

namespace WellDone.Core.Util;

/// <summary>
/// Hex工具类（别名）
/// </summary>
public static class Hex
{
	/// <summary>
	/// 将字节数组转换为十六进制字符串
	/// </summary>
	/// <param name="bytes">字节数组</param>
	/// <returns>十六进制字符串</returns>
	public static string Encode(byte[] bytes) => HexUtil.Encode(bytes);

	/// <summary>
	/// 将十六进制字符串转换为字节数组
	/// </summary>
	/// <param name="hex">十六进制字符串</param>
	/// <returns>字节数组</returns>
	public static byte[] Decode(string hex) => HexUtil.Decode(hex);
}
