using System;
using System.Collections.Generic;
using System.Web;

namespace WellDone.Core.Util;

/// <summary>
/// URLUtil工具类
/// </summary>
public static class URLUtil
{
	/// <summary>
	/// URL编码
	/// </summary>
	/// <param name="input">输入字符串</param>
	/// <returns>编码后的字符串</returns>
	public static string Encode(string input) => HttpUtility.UrlEncode(input);

	/// <summary>
	/// URL编码
	/// </summary>
	/// <param name="input">输入字符串</param>
	/// <param name="encoding">编码</param>
	/// <returns>编码后的字符串</returns>
	public static string Encode(string input, System.Text.Encoding encoding) => HttpUtility.UrlEncode(input, encoding);

	/// <summary>
	/// URL解码
	/// </summary>
	/// <param name="input">编码后的字符串</param>
	/// <returns>原始字符串</returns>
	public static string Decode(string input) => HttpUtility.UrlDecode(input);

	/// <summary>
	/// URL解码
	/// </summary>
	/// <param name="input">编码后的字符串</param>
	/// <param name="encoding">编码</param>
	/// <returns>原始字符串</returns>
	public static string Decode(string input, System.Text.Encoding encoding) => HttpUtility.UrlDecode(input, encoding);

	/// <summary>
	/// 获取URL路径
	/// </summary>
	/// <param name="url">URL</param>
	/// <returns>路径</returns>
	public static string GetPath(string url)
	{
		if (string.IsNullOrEmpty(url))
			return url;
		var uri = new Uri(url);
		return uri.AbsolutePath;
	}

	/// <summary>
	/// 获取URL文件名
	/// </summary>
	/// <param name="url">URL</param>
	/// <returns>文件名</returns>
	public static string GetFileName(string url)
	{
		if (string.IsNullOrEmpty(url))
			return url;
		return System.IO.Path.GetFileName(new Uri(url).AbsolutePath);
	}

	/// <summary>
	/// 获取URL参数
	/// </summary>
	/// <param name="url">URL</param>
	/// <returns>参数字典</returns>
	public static Dictionary<string, string> GetParams(string url)
	{
		var result = new Dictionary<string, string>();
		if (string.IsNullOrEmpty(url))
			return result;

		var uri = new Uri(url);
		var query = uri.Query;
		if (string.IsNullOrEmpty(query))
			return result;

		query = query.TrimStart('?');
		var pairs = query.Split('&');
		foreach (var pair in pairs)
		{
			var kv = pair.Split('=');
			if (kv.Length >= 2)
			{
				result[kv[0]] = Decode(kv[1]);
			}
		}
		return result;
	}
}
