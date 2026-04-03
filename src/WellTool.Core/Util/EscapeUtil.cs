namespace WellTool.Core.Util;

/// <summary>
/// 转义工具类
/// </summary>
public static class EscapeUtil
{
	/// <summary>
	/// 转义HTML特殊字符
	/// </summary>
	/// <param name="text">文本</param>
	/// <returns>转义后的文本</returns>
	public static string Escape(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}

		return text
			.Replace("&", "&amp;")
			.Replace("<", "&lt;")
			.Replace(">", "&gt;")
			.Replace("\"", "&quot;")
			.Replace("'", "&#39;");
	}

	/// <summary>
	/// 反转义HTML特殊字符
	/// </summary>
	/// <param name="text">转义后的文本</param>
	/// <returns>原始文本</returns>
	public static string Unescape(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}

		return text
			.Replace("&amp;", "&")
			.Replace("&lt;", "<")
			.Replace("&gt;", ">")
			.Replace("&quot;", "\"")
			.Replace("&#39;", "'")
			.Replace("&nbsp;", " ");
	}

	/// <summary>
	/// 转义JavaScript字符串
	/// </summary>
	/// <param name="text">文本</param>
	/// <returns>转义后的文本</returns>
	public static string EscapeJs(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}

		return text
			.Replace("\\", "\\\\")
			.Replace("\"", "\\\"")
			.Replace("'", "\\'")
			.Replace("\r", "\\r")
			.Replace("\n", "\\n")
			.Replace("\t", "\\t");
	}

	/// <summary>
	/// 转义XML特殊字符
	/// </summary>
	/// <param name="text">文本</param>
	/// <returns>转义后的文本</returns>
	public static string EscapeXml(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}

		return text
			.Replace("&", "&amp;")
			.Replace("<", "&lt;")
			.Replace(">", "&gt;")
			.Replace("\"", "&quot;")
			.Replace("'", "&apos;");
	}
}
