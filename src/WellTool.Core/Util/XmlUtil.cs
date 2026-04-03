using System;
using System.Text;

namespace WellTool.Core.Util;

/// <summary>
/// XmlUtil工具类
/// </summary>
public static class XmlUtil
{
	/// <summary>
	/// XML编码
	/// </summary>
	public static string Escape(string input)
	{
		if (string.IsNullOrEmpty(input))
			return input;
		return input.Replace("&", "&amp;")
				   .Replace("<", "&lt;")
				   .Replace(">", "&gt;")
				   .Replace("\"", "&quot;")
				   .Replace("'", "&apos;");
	}

	/// <summary>
	/// XML解码
	/// </summary>
	public static string Unescape(string input)
	{
		if (string.IsNullOrEmpty(input))
			return input;
		return input.Replace("&apos;", "'")
				   .Replace("&quot;", "\"")
				   .Replace("&gt;", ">")
				   .Replace("&lt;", "<")
				   .Replace("&amp;", "&");
	}

	/// <summary>
	/// 是否为XML安全字符
	/// </summary>
	public static bool IsXmlSafe(char c)
	{
		return c == 0x9 || c == 0xA || c == 0xD ||
			   (c >= 0x20 && c <= 0xD7FF) ||
			   (c >= 0xE000 && c <= 0xFFFD) ||
			   (c >= 0x10000 && c <= 0x10FFFF);
	}

	/// <summary>
	/// 转义无效XML字符
	/// </summary>
	public static string EscapeInvalid(string input)
	{
		if (string.IsNullOrEmpty(input))
			return input;
		var sb = new StringBuilder(input.Length);
		foreach (char c in input)
		{
			if (IsXmlSafe(c))
				sb.Append(c);
		}
		return sb.ToString();
	}
}
