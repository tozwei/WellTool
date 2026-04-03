using System;
using System.Collections.Generic;
using System.Web;

namespace WellTool.Core.Util;

/// <summary>
/// HTML工具类
/// </summary>
public static class HTMLUtil
{
	/// <summary>
	/// HTML编码
	/// </summary>
	/// <param name="input">输入字符串</param>
	/// <returns>编码后的字符串</returns>
	public static string Escape(string input) => HttpUtility.HtmlEncode(input);

	/// <summary>
	/// HTML解码
	/// </summary>
	/// <param name="input">编码后的字符串</param>
	/// <returns>原始字符串</returns>
	public static string Unescape(string input) => HttpUtility.HtmlDecode(input);

	/// <summary>
	/// HTML编码（属性值）
	/// </summary>
	/// <param name="input">输入字符串</param>
	/// <returns>编码后的字符串</returns>
	public static string EscapeAttribute(string input)
	{
		if (string.IsNullOrEmpty(input))
			return input;
		return input.Replace("&", "&amp;")
				   .Replace("<", "&lt;")
				   .Replace(">", "&gt;")
				   .Replace("\"", "&quot;")
				   .Replace("'", "&#39;");
	}

	/// <summary>
	/// 移除HTML标签
	/// </summary>
	/// <param name="input">HTML字符串</param>
	/// <returns>纯文本</returns>
	public static string RemoveTags(string input)
	{
		if (string.IsNullOrEmpty(input))
			return input;
		// 简单实现，实际应使用正则
		return input.Replace("<", "&lt;").Replace(">", "&gt;");
	}
}
