using System;
using System.Text;

namespace WellTool.Core.Util;

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
	public static readonly Encoding GBK;

	/// <summary>
	/// ISO-8859-1编码
	/// </summary>
	public static readonly Encoding ISO_8859_1 = Encoding.GetEncoding("ISO-8859-1");

	/// <summary>
	/// ASCII编码
	/// </summary>
	public static readonly Encoding ASCII = Encoding.ASCII;

	/// <summary>
	/// 静态构造函数
	/// </summary>
	static CharsetUtil()
	{
		try
		{
			// 尝试获取GBK编码
			GBK = Encoding.GetEncoding("GBK");
		}
		catch
		{
			// 如果GBK编码不可用，使用UTF-8作为替代
			GBK = UTF_8;
		}
	}

	/// <summary>
	/// 获取编码
	/// </summary>
	/// <param name="charsetName">编码名称</param>
	/// <returns>编码</returns>
	public static Encoding GetEncoding(string charsetName)
	{
		if (string.IsNullOrEmpty(charsetName))
			return UTF_8;
		
		try
		{
			return Encoding.GetEncoding(charsetName);
		}
		catch
		{
			// 如果指定的编码不可用，返回UTF-8
			return UTF_8;
		}
	}
}
