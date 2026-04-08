using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WellTool.Core.Util;

/// <summary>
/// 正则表达式工具类
/// </summary>
public static class ReUtil
{
	/// <summary>
	/// 判断字符串是否匹配正则表达式
	/// </summary>
	/// <param name="pattern">正则表达式</param>
	/// <param name="text">文本</param>
	/// <returns>是否匹配</returns>
	public static bool IsMatch(string pattern, string text)
	{
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
		{
			return false;
		}
		return Regex.IsMatch(text, pattern);
	}

	/// <summary>
	/// 从文本中获取匹配的第一个值
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="pattern">正则表达式</param>
	/// <returns>匹配的值</returns>
	public static string GetFirst(string text, string pattern)
	{
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
		{
			return null;
		}
		var match = Regex.Match(text, pattern);
		return match.Success ? match.Value : null;
	}

	/// <summary>
	/// 从文本中获取所有匹配的值
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="pattern">正则表达式</param>
	/// <returns>匹配的值列表</returns>
	public static List<string> GetAll(string text, string pattern)
	{
		var result = new List<string>();
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
		{
			return result;
		}
		var matches = Regex.Matches(text, pattern);
		foreach (Match match in matches)
		{
			result.Add(match.Value);
		}
		return result;
	}

	/// <summary>
	/// 替换匹配的文本
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="pattern">正则表达式</param>
	/// <param name="replacement">替换文本</param>
	/// <returns>替换后的文本</returns>
	public static string Replace(string text, string pattern, string replacement)
	{
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
		{
			return text;
		}
		return Regex.Replace(text, pattern, replacement ?? string.Empty);
	}

	/// <summary>
	/// 替换匹配的文本（使用MatchEvaluator）
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="pattern">正则表达式</param>
	/// <param name="evaluator">替换回调</param>
	/// <returns>替换后的文本</returns>
	public static string Replace(string text, string pattern, MatchEvaluator evaluator)
	{
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
		{
			return text;
		}
		return Regex.Replace(text, pattern, evaluator ?? (m => m.Value));
	}

	/// <summary>
	/// 分割文本
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="pattern">正则表达式</param>
	/// <returns>分割后的数组</returns>
	public static string[] Split(string text, string pattern)
	{
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
		{
			return new[] { text };
		}
		return Regex.Split(text, pattern);
	}

	/// <summary>
	/// 获取分组值
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="pattern">正则表达式</param>
	/// <param name="groupIndex">分组索引</param>
	/// <returns>分组值</returns>
	public static string GetGroupValue(string text, string pattern, int groupIndex)
	{
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
		{
			return null;
		}
		var match = Regex.Match(text, pattern);
		if (match.Success && match.Groups.Count > groupIndex)
		{
			return match.Groups[groupIndex].Value;
		}
		return null;
	}

	/// <summary>
	/// 获取命名分组值
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="pattern">正则表达式</param>
	/// <param name="groupName">分组名称</param>
	/// <returns>分组值</returns>
	public static string GetGroupValue(string text, string pattern, string groupName)
	{
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
		{
			return null;
		}
		var match = Regex.Match(text, pattern);
		if (match.Success && match.Groups[groupName].Success)
		{
			return match.Groups[groupName].Value;
		}
		return null;
	}

	/// <summary>
	/// 执行正则表达式匹配
	/// </summary>
	/// <param name="pattern">正则表达式</param>
	/// <param name="text">文本</param>
	/// <returns>匹配结果</returns>
	public static Match Match(string pattern, string text)
	{
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
		{
			return null;
		}
		return Regex.Match(text, pattern);
	}

	/// <summary>
	/// 执行正则表达式匹配，获取所有匹配结果
	/// </summary>
	/// <param name="pattern">正则表达式</param>
	/// <param name="text">文本</param>
	/// <returns>匹配结果列表</returns>
	public static List<Match> GetMatchs(string pattern, string text)
	{
		var result = new List<Match>();
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
		{
			return result;
		}
		var matches = Regex.Matches(text, pattern);
		foreach (Match match in matches)
		{
			result.Add(match);
		}
		return result;
	}

	/// <summary>
	/// 替换所有匹配的文本
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="pattern">正则表达式</param>
	/// <param name="replacement">替换文本</param>
	/// <returns>替换后的文本</returns>
	public static string ReplaceAll(string text, string pattern, string replacement)
	{
		return Replace(text, pattern, replacement);
	}

	/// <summary>
	/// 替换第一个匹配的文本
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="pattern">正则表达式</param>
	/// <param name="replacement">替换文本</param>
	/// <returns>替换后的文本</returns>
	public static string ReplaceFirst(string text, string pattern, string replacement)
	{
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
		{
			return text;
		}
		var match = Regex.Match(text, pattern);
		if (!match.Success)
		{
			return text;
		}
		return text.Substring(0, match.Index) + (replacement ?? string.Empty) + text.Substring(match.Index + match.Length);
	}

	/// <summary>
	/// 删除第一个匹配的文本
	/// </summary>
	/// <param name="pattern">正则表达式</param>
	/// <param name="text">文本</param>
	/// <returns>删除后的文本</returns>
	public static string DelFirst(string pattern, string text)
	{
		return ReplaceFirst(text, pattern, string.Empty);
	}

	/// <summary>
	/// 删除所有匹配的文本
	/// </summary>
	/// <param name="pattern">正则表达式</param>
	/// <param name="text">文本</param>
	/// <returns>删除后的文本</returns>
	public static string DelAll(string pattern, string text)
	{
		return ReplaceAll(text, pattern, string.Empty);
	}

	/// <summary>
	/// 检查文本是否包含匹配的内容
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="pattern">正则表达式</param>
	/// <returns>是否包含</returns>
	public static bool Contains(string text, string pattern)
	{
		return IsMatch(pattern, text);
	}

	/// <summary>
	/// 转义正则表达式特殊字符
	/// </summary>
	/// <param name="text">文本</param>
	/// <returns>转义后的文本</returns>
	public static string Escape(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}
		return Regex.Escape(text);
	}
}
