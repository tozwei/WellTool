using System;

namespace WellTool.Core.Util;

/// <summary>
/// StrUtil字符串工具类
/// </summary>
public static class StrUtil
{
	/// <summary>
	/// 是否为空
	/// </summary>
	public static bool IsEmpty(string str) => string.IsNullOrEmpty(str);

	/// <summary>
	/// 是否不为空
	/// </summary>
	public static bool IsNotEmpty(string str) => !IsEmpty(str);

	/// <summary>
	/// 是否为空或空白
	/// </summary>
	public static bool IsBlank(string str) => string.IsNullOrWhiteSpace(str);

	/// <summary>
	/// 是否不为空或空白
	/// </summary>
	public static bool IsNotBlank(string str) => !IsBlank(str);

	/// <summary>
	/// 空字符串
	/// </summary>
	public static string Empty() => string.Empty;

	/// <summary>
	/// 空字符串常量
	/// </summary>
	public static readonly string EMPTY = string.Empty;

	/// <summary>
	/// 斜杠常量
	/// </summary>
	public static readonly string SLASH = "/";

	/// <summary>
	/// 空字符串数组
	/// </summary>
	public static string[] EmptyArray() => Array.Empty<string>();

	/// <summary>
	/// 字符串连接
	/// </summary>
	public static string Concat(params string[] strs) => string.Concat(strs);

	/// <summary>
	/// 格式化
	/// </summary>
	public static string Format(string template, params object[] args) => string.Format(template, args);

	/// <summary>
	/// 移除字符串后缀
	/// </summary>
	/// <param name="str">字符串</param>
	/// <param name="suffix">后缀</param>
	/// <returns>移除后缀后的字符串</returns>
	public static string RemoveSuffix(string str, string suffix)
	{
		if (IsEmpty(str) || IsEmpty(suffix))
		{
			return str;
		}
		if (str.EndsWith(suffix))
		{
			return str.Substring(0, str.Length - suffix.Length);
		}
		return str;
	}

	/// <summary>
	/// 清理空白字符
	/// </summary>
	/// <param name="str">字符串</param>
	/// <returns>清理后的字符串</returns>
	public static string CleanBlank(string str)
	{
		if (IsEmpty(str))
		{
			return str;
		}
		return str.Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\n", "");
	}

	/// <summary>
	/// 子字符串
	/// </summary>
	public static string Sub(string str, int fromIndex, int toIndex)
	{
		if (IsEmpty(str))
			return str;
		return str.Substring(fromIndex, Math.Min(toIndex, str.Length) - fromIndex);
	}

	/// <summary>
	/// 截断
	/// </summary>
	public static string Sub(string str, int length)
	{
		if (IsEmpty(str) || str.Length <= length)
			return str;
		return str.Substring(0, length);
	}

	/// <summary>
	/// 去除空白
	/// </summary>
	public static string Trim(string str) => str?.Trim();

	/// <summary>
	/// 去除左边空白
	/// </summary>
	public static string TrimLeft(string str) => str?.TrimStart();

	/// <summary>
	/// 去除右边空白
	/// </summary>
	public static string TrimRight(string str) => str?.TrimEnd();

	/// <summary>
	/// 转大写
	/// </summary>
	public static string Upper(string str) => str?.ToUpper();

	/// <summary>
	/// 转小写
	/// </summary>
	public static string Lower(string str) => str?.ToLower();

	/// <summary>
	/// 是否包含
	/// </summary>
	public static bool Contains(string str, string search) => str?.Contains(search) ?? false;

	/// <summary>
	/// 是否以...开头
	/// </summary>
	public static bool StartWith(string str, string prefix) => str?.StartsWith(prefix) ?? false;

	/// <summary>
	/// 是否以...结尾
	/// </summary>
	public static bool EndWith(string str, string suffix) => str?.EndsWith(suffix) ?? false;

	/// <summary>
	/// 重复
	/// </summary>
	public static string Repeat(string str, int count)
	{
		if (IsEmpty(str) || count <= 0)
			return string.Empty;
		var sb = new System.Text.StringBuilder(str.Length * count);
		for (int i = 0; i < count; i++)
		{
			sb.Append(str);
		}
		return sb.ToString();
	}

	/// <summary>
	/// 填充
	/// </summary>
	public static string PadLeft(string str, int length, char padChar = ' ')
	{
		if (str == null || str.Length >= length)
			return str ?? string.Empty;
		return new string(padChar, length - str.Length) + str;
	}

	/// <summary>
	/// 填充
	/// </summary>
	public static string PadRight(string str, int length, char padChar = ' ')
	{
		if (str == null || str.Length >= length)
			return str ?? string.Empty;
		return str + new string(padChar, length - str.Length);
	}

	/// <summary>
	/// 反转
	/// </summary>
	public static string Reverse(string str)
	{
		if (IsEmpty(str))
			return str;
		var chars = str.ToCharArray();
		Array.Reverse(chars);
		return new string(chars);
	}

	/// <summary>
	/// 移除前缀
	/// </summary>
	public static string RemovePrefix(string str, string prefix)
	{
		if (IsEmpty(str) || IsEmpty(prefix))
			return str;
		if (str.StartsWith(prefix))
			return str.Substring(prefix.Length);
		return str;
	}
}
