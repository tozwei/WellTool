using System;
using System.Text;

namespace WellDone.Core.Util;

/// <summary>
/// CharsetUtil编码工具类
/// </summary>
public static class CharsetUtil
{
	/// <summary>
	/// UTF-8编码
	/// </summary>
	public static readonly Encoding UTF_8 = Encoding.UTF8;

	/// <summary>
	/// GBK编码
	/// </summary>
	public static readonly Encoding GBK = Encoding.GetEncoding("GBK");

	/// <summary>
	/// ISO-8859-1编码
	/// </summary>
	public static readonly Encoding ISO_8859_1 = Encoding.GetEncoding("ISO-8859-1");

	/// <summary>
	/// ASCII编码
	/// </summary>
	public static readonly Encoding ASCII = Encoding.ASCII;

	/// <summary>
	/// 获取编码
	/// </summary>
	/// <param name="charsetName">编码名称</param>
	/// <returns>编码</returns>
	public static Encoding GetEncoding(string charsetName)
	{
		if (string.IsNullOrEmpty(charsetName))
			return UTF_8;
		return Encoding.GetEncoding(charsetName);
	}
}
