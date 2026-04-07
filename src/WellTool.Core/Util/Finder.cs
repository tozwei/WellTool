namespace WellTool.Core.Util;

using System;

/// <summary>
/// 查找器接口
/// </summary>
public interface IFinder
{
	/// <summary>
	/// 查找
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="regex">正则表达式</param>
	/// <returns>查找结果</returns>
	string[] Find(string text, string regex);

	/// <summary>
	/// 查找第一个
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="regex">正则表达式</param>
	/// <returns>查找结果</returns>
	string? FindFirst(string text, string regex);
}

/// <summary>
/// 默认查找器实现
/// </summary>
public class Finder : IFinder
{
	public string[] Find(string text, string regex)
	{
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(regex))
			return Array.Empty<string>();

		try
		{
			var regexObj = new System.Text.RegularExpressions.Regex(regex);
			var matches = regexObj.Matches(text);
			var results = new string[matches.Count];
			for (int i = 0; i < matches.Count; i++)
			{
				results[i] = matches[i].Value;
			}
			return results;
		}
		catch
		{
			return Array.Empty<string>();
		}
	}

	public string? FindFirst(string text, string regex)
	{
		var results = Find(text, regex);
		return results.Length > 0 ? results[0] : null;
	}
}
