using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WellTool.Core.Math;

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
	/// 将null转换为空字符串
	/// </summary>
	/// <param name="str">字符串</param>
	/// <returns>空字符串如果输入为null，否则返回原字符串</returns>
	public static string NullToEmpty(string str) => str ?? string.Empty;

	/// <summary>
	/// 去除字符串开头的空白字符
	/// </summary>
	/// <param name="str">字符串</param>
	/// <returns>去除开头空白字符后的字符串</returns>
	public static string TrimStart(string str)
	{
		return str?.TrimStart() ?? string.Empty;
	}

	/// <summary>
	/// 去除字符串开头的指定字符
	/// </summary>
	/// <param name="str">字符串</param>
	/// <param name="trimChar">要去除的字符</param>
	/// <returns>去除开头指定字符后的字符串</returns>
	public static string TrimStart(string str, char trimChar)
	{
		if (IsEmpty(str))
		{
			return str;
		}
		int startIndex = 0;
		while (startIndex < str.Length && str[startIndex] == trimChar)
		{
			startIndex++;
		}
		return str.Substring(startIndex);
	}

	/// <summary>
	/// 将空字符串转换为默认值
	/// </summary>
	/// <param name="str">字符串</param>
	/// <param name="defaultValue">默认值</param>
	/// <returns>默认值如果输入为空，否则返回原字符串</returns>
	public static string EmptyToDefault(string str, string defaultValue) => IsEmpty(str) ? defaultValue : str;

	/// <summary>
	/// 忽略大小写查找子字符串的位置
	/// </summary>
	/// <param name="str">字符串</param>
	/// <param name="substr">子字符串</param>
	/// <param name="startIndex">起始索引</param>
	/// <returns>子字符串的位置，未找到返回-1</returns>
	public static int IndexOfIgnoreCase(string str, string substr, int startIndex = 0)
	{
		if (IsEmpty(str) || IsEmpty(substr))
		{
			return -1;
		}
		return str.IndexOf(substr, startIndex, StringComparison.OrdinalIgnoreCase);
	}

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
        int start = System.Math.Max(0, fromIndex);
        int end = toIndex < 0 ? str.Length + toIndex + 1 : System.Math.Min(toIndex, str.Length);
        end = System.Math.Max(0, end);
        if (start >= end)
            return string.Empty;
        return str.Substring(start, end - start);
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

	/// <summary>
	/// 转换为布尔值
	/// </summary>
	/// <param name="str">字符串</param>
	/// <returns>布尔值</returns>
	public static bool ToBoolean(string str)
	{
		if (IsEmpty(str))
			return false;
		str = str.Trim().ToLower();
		return str == "true" || str == "1" || str == "yes" || str == "y" || str == "on";
	}

	/// <summary>
	/// 左侧填充
	/// </summary>
	public static string PadPre(string str, int length, char padChar = ' ')
	{
		return PadLeft(str, length, padChar);
	}

	/// <summary>
	/// 左侧填充
	/// </summary>
	public static string PadPre(string str, int length, string padStr)
	{
		if (str == null || str.Length >= length)
			return str;
		var padLength = length - str.Length;
		var padRepeat = padLength / padStr.Length + 1;
		var pad = Repeat(padStr, padRepeat).Substring(0, padLength);
		return pad + str;
	}

	/// <summary>
	/// 右侧填充
	/// </summary>
	public static string PadAfter(string str, int length, char padChar = ' ')
	{
		if (length < 0)
			return string.Empty;
		return PadRight(str, length, padChar);
	}

	/// <summary>
	/// 右侧填充
	/// </summary>
	public static string PadAfter(string str, int length, string padStr)
	{
		if (str == null || str.Length >= length)
			return str;
		var padLength = length - str.Length;
		var padRepeat = padLength / padStr.Length + 1;
		var pad = Repeat(padStr, padRepeat).Substring(0, padLength);
		return str + pad;
	}

	/// <summary>
	/// 索引格式化
	/// </summary>
	public static string IndexedFormat(string template, params object[] args)
	{
		return string.Format(template, args);
	}

	/// <summary>
	/// 是否为数字
	/// </summary>
	public static bool IsNumeric(string str)
	{
		if (IsEmpty(str))
			return false;
		return double.TryParse(str, out _);
	}

	/// <summary>
	/// 去除空字符串或返回null
	/// </summary>
	public static string TrimToNull(string str)
	{
		if (IsBlank(str))
			return null;
		return str.Trim();
	}

	/// <summary>
	/// 去除空字符串或返回空字符串
	/// </summary>
	public static string TrimToEmpty(string str)
	{
		return str?.Trim() ?? string.Empty;
	}

	/// <summary>
	/// 切割字符串为指定长度的片段
	/// </summary>
	public static string[] Cut(string str, int length)
	{
		if (IsEmpty(str) || length <= 0)
			return Array.Empty<string>();
		var list = new System.Collections.Generic.List<string>();
		for (int i = 0; i < str.Length; i += length)
		{
			int end = System.Math.Min(i + length, str.Length);
			list.Add(str.Substring(i, end - i));
		}
		return list.ToArray();
	}

	/// <summary>
	/// 获取指定字符串之前的内容
	/// </summary>
	public static string SubBefore(string str, char separator)
	{
		if (IsEmpty(str))
			return str;
		int index = str.IndexOf(separator);
		return index < 0 ? str : str.Substring(0, index);
	}

	/// <summary>
	/// 获取指定字符串之后的内容
	/// </summary>
	public static string SubAfter(string str, char separator)
	{
		if (IsEmpty(str))
			return str;
		int index = str.IndexOf(separator);
		return index < 0 ? str : str.Substring(index + 1);
	}

	/// <summary>
	/// 获取两个字符串之间的内容
	/// </summary>
	public static string SubBetween(string str, string prefix, string suffix)
	{
		if (IsEmpty(str))
			return str;
		int start = str.IndexOf(prefix);
		if (start < 0)
			return str;
		int end = str.IndexOf(suffix, start + prefix.Length);
		if (end < 0)
			return str;
		return str.Substring(start + prefix.Length, end - start - prefix.Length);
	}

	/// <summary>
	/// 是否包含（忽略大小写）
	/// </summary>
	public static bool ContainsIgnoreCase(string str, string search)
	{
		if (IsEmpty(str) || IsEmpty(search))
			return false;
		return str.Contains(search, StringComparison.OrdinalIgnoreCase);
	}

	/// <summary>
	/// 查找子字符串位置
	/// </summary>
	public static int IndexOf(string str, string search)
	{
		if (IsEmpty(str) || IsEmpty(search))
			return -1;
		return str.IndexOf(search, StringComparison.Ordinal);
	}

	/// <summary>
	/// 从末尾查找子字符串位置
	/// </summary>
	public static int LastIndexOf(string str, string search)
	{
		if (IsEmpty(str) || IsEmpty(search))
			return -1;
		return str.LastIndexOf(search, StringComparison.Ordinal);
	}

	/// <summary>
	/// 是否以指定字符串开头
	/// </summary>
	public static bool StartsWith(string str, string prefix)
	{
		if (IsEmpty(str) || IsEmpty(prefix))
			return false;
		return str.StartsWith(prefix);
	}

	/// <summary>
	/// 检查字符串是否以指定后缀结尾
	/// </summary>
	public static bool EndsWith(string str, string suffix)
	{
		if (IsEmpty(str) || IsEmpty(suffix))
			return false;
		return str.EndsWith(suffix);
	}



	/// <summary>
	/// 将字符串分割为长整型数组
	/// </summary>
	public static long[] SplitToLong(string str, char separator)
	{
		if (IsEmpty(str))
			return Array.Empty<long>();
		return str.Split(separator).Select(s => long.TryParse(s.Trim(), out long result) ? result : 0).ToArray();
	}

	/// <summary>
	/// 将字符串分割为长整型数组
	/// </summary>
	public static long[] SplitToLong(string str, string separator)
	{
		if (IsEmpty(str))
			return Array.Empty<long>();
		return str.Split(new[] { separator }, StringSplitOptions.None).Select(s => long.TryParse(s.Trim(), out long result) ? result : 0).ToArray();
	}

	/// <summary>
	/// 将字符串分割为整型数组
	/// </summary>
	public static int[] SplitToInt(string str, char separator)
	{
		if (IsEmpty(str))
			return Array.Empty<int>();
		return str.Split(separator).Select(s => int.TryParse(s.Trim(), out int result) ? result : 0).ToArray();
	}

	/// <summary>
	/// 将字符串分割为整型数组
	/// </summary>
	public static int[] SplitToInt(string str, string separator)
	{
		if (IsEmpty(str))
			return Array.Empty<int>();
		return str.Split(new[] { separator }, StringSplitOptions.None).Select(s => int.TryParse(s.Trim(), out int result) ? result : 0).ToArray();
	}



	/// <summary>
	/// 按代码点替换字符串
	/// </summary>
	public static string ReplaceByCodePoint(string str, int oldCodePoint, int newCodePoint)
	{
		if (IsEmpty(str))
			return str;
		return str.Replace(char.ConvertFromUtf32(oldCodePoint), char.ConvertFromUtf32(newCodePoint));
	}

	/// <summary>
	/// 按代码点替换字符串指定范围的字符
	/// </summary>
	public static string ReplaceByCodePoint(string str, int start, int end, char replacement)
	{
		if (IsEmpty(str))
			return str;
		
		// 使用 char 数组代替 Rune 列表，以兼容 .NET 标准库 2.1
		char[] chars = str.ToCharArray();
		int actualEnd = System.Math.Min(end, chars.Length);
		
		for (int i = start; i < actualEnd; i++)
		{
			chars[i] = replacement;
		}
		
		return new string(chars);
	}

	/// <summary>
	/// 获取字符串中指定字符的索引
	/// </summary>
	public static int IndexOf(string str, char value, int startIndex)
	{
		if (IsEmpty(str))
			return -1;
		return str.IndexOf(value, startIndex);
	}

	/// <summary>
	/// 获取字符串中指定字符串的索引
	/// </summary>
	public static int IndexOf(string str, string value, int startIndex)
	{
		if (IsEmpty(str) || IsEmpty(value))
			return -1;
		return str.IndexOf(value, startIndex);
	}

	/// <summary>
	/// 获取字符串中指定字符的最后索引
	/// </summary>
	public static int LastIndexOf(string str, char value)
	{
		if (IsEmpty(str))
			return -1;
		return str.LastIndexOf(value);
	}



	/// <summary>
	/// 是否相等（忽略大小写）
	/// </summary>
	public static bool EqualsIgnoreCase(string str1, string str2)
	{
		if (str1 == null && str2 == null)
			return true;
		if (str1 == null || str2 == null)
			return false;
		return str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
	}

	/// <summary>
	/// 是否相等
	/// </summary>
	public static bool Equals(string str1, string str2)
	{
		if (str1 == null && str2 == null)
			return true;
		if (str1 == null || str2 == null)
			return false;
		return str1.Equals(str2);
	}

	/// <summary>
	/// 首字母大写
	/// </summary>
	public static string UpperFirst(string str)
	{
		if (IsEmpty(str))
			return str;
		return char.ToUpper(str[0]) + str.Substring(1);
	}

	/// <summary>
	/// 首字母小写
	/// </summary>
	public static string LowerFirst(string str)
	{
		if (IsEmpty(str))
			return str;
		return char.ToLower(str[0]) + str.Substring(1);
	}

	/// <summary>
	/// 驼峰命名
	/// </summary>
	public static string ToCamelCase(string str)
	{
		if (IsEmpty(str))
			return str;
		var sb = new System.Text.StringBuilder();
		var capitalizeNext = false;
		foreach (char c in str)
		{
			if (c == '_' || c == '-')
			{
				capitalizeNext = true;
			}
			else if (capitalizeNext)
			{
				sb.Append(char.ToUpper(c));
				capitalizeNext = false;
			}
			else
			{
				sb.Append(c);
			}
		}
		return LowerFirst(sb.ToString());
	}

	/// <summary>
	/// 下划线命名
	/// </summary>
	public static string ToUnderScoreCase(string str)
	{
		if (IsEmpty(str))
			return str;
		var sb = new System.Text.StringBuilder();
		for (int i = 0; i < str.Length; i++)
		{
			char c = str[i];
			if (char.IsUpper(c))
			{
				if (i > 0)
					sb.Append('_');
				sb.Append(char.ToLower(c));
			}
			else
			{
				sb.Append(c);
			}
		}
		return sb.ToString();
	}

	/// <summary>
	/// 是否为字母
	/// </summary>
	public static bool IsAlpha(string str)
	{
		if (IsEmpty(str))
			return false;
		foreach (char c in str)
		{
			if (!char.IsLetter(c))
				return false;
		}
		return true;
	}

	/// <summary>
	/// 是否为字母或数字
	/// </summary>
	public static bool IsAlphanumeric(string str)
	{
		if (IsEmpty(str))
			return false;
		foreach (char c in str)
		{
			if (!char.IsLetterOrDigit(c))
				return false;
		}
		return true;
	}

	/// <summary>
	/// 是否为数字
	/// </summary>
	public static bool IsNumber(string str)
	{
		if (IsEmpty(str))
			return false;
		return double.TryParse(str, out _);
	}

	/// <summary>
	/// 分割字符串
	/// </summary>
	public static string[] Split(string str, char separator)
	{
		if (IsEmpty(str))
			return Array.Empty<string>();
		return str.Split(separator);
	}

	/// <summary>
	/// 分割字符串为数组
	/// </summary>
	public static string[] SplitToArray(string str, char separator)
	{
		return Split(str, separator);
	}

	/// <summary>
	/// 拼接字符串
	/// </summary>
	public static string Join(string separator, params string[] values)
	{
		if (values == null || values.Length == 0)
			return string.Empty;
		return string.Join(separator, values);
	}

	/// <summary>
	/// 拼接字符串（带集合）
	/// </summary>
	public static string Join(string separator, System.Collections.Generic.IEnumerable<string> values)
	{
		if (values == null)
			return string.Empty;
		return string.Join(separator, values);
	}

	/// <summary>
	/// 包裹字符串
	/// </summary>
	public static string Wrap(string str, char wrapper)
	{
		if (IsEmpty(str))
			return str;
		return wrapper + str + wrapper;
	}

	/// <summary>
	/// 解除包裹
	/// </summary>
	public static string Unwrap(string str, char wrapper)
	{
		if (IsEmpty(str))
			return str;
		if (str.Length >= 2 && str[0] == wrapper && str[str.Length - 1] == wrapper)
			return str.Substring(1, str.Length - 2);
		return str;
	}

	/// <summary>
	/// 如果缺少后缀则追加
	/// </summary>
	public static string AppendIfMissing(string str, string suffix)
	{
		if (IsEmpty(str) || str.EndsWith(suffix))
			return str;
		return str;
	}

	/// <summary>
	/// 如果缺少后缀则追加
	/// </summary>
	public static string AppendIfMissing(string str, string suffix, bool ignoreCase)
	{
		if (IsEmpty(str))
			return str;
		if (ignoreCase)
		{
			if (str.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
				return str;
		}
		else
		{
			if (str.EndsWith(suffix))
				return str;
		}
		return str + suffix;
	}

	/// <summary>
	/// 如果缺少前缀则前置
	/// </summary>
	public static string PrependIfMissing(string str, string prefix)
	{
		if (IsEmpty(str) || str.StartsWith(prefix))
			return str;
		return str;
	}

	/// <summary>
	/// 如果缺少前缀则前置
	/// </summary>
	public static string PrependIfMissing(string str, string prefix, bool ignoreCase)
	{
		if (IsEmpty(str))
			return str;
		if (ignoreCase)
		{
			if (str.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
				return str;
		}
		else
		{
			if (str.StartsWith(prefix))
				return str;
		}
		return prefix + str;
	}

	/// <summary>
	/// 替换指定位置的字符串
	/// </summary>
	public static string Replace(string str, int start, int end, string replacement)
	{
		if (IsEmpty(str))
			return str;
		return str.Substring(0, start) + replacement + str.Substring(end);
	}

	/// <summary>
	/// 替换字符
	/// </summary>
	public static string ReplaceChars(string str, char search, char replacement)
	{
		if (IsEmpty(str))
			return str;
		return str.Replace(search, replacement);
	}

	/// <summary>
	/// 去除首尾指定字符串
	/// </summary>
	public static string Strip(string str, string prefix, string suffix)
	{
		if (IsEmpty(str))
			return str;
		var result = str;
		if (!string.IsNullOrEmpty(prefix) && result.StartsWith(prefix))
			result = result.Substring(prefix.Length);
		if (!string.IsNullOrEmpty(suffix) && result.EndsWith(suffix))
			result = result.Substring(0, result.Length - suffix.Length);
		return result;
	}

	/// <summary>
	/// 重复字符
	/// </summary>
	public static string Repeat(char c, int count)
	{
		if (count <= 0)
			return string.Empty;
		return new string(c, count);
	}

	/// <summary>
	/// 索引字典
	/// </summary>
	public static System.Collections.Generic.IDictionary<int, char> Indexed(string str)
	{
		var dict = new System.Collections.Generic.Dictionary<int, char>();
		if (!IsEmpty(str))
		{
			for (int i = 0; i < str.Length; i++)
			{
				dict[i] = str[i];
			}
		}
		return dict;
	}

	/// <summary>
	/// 重复并连接字符串
	/// </summary>
	public static string RepeatAndJoin(string str, int count, string separator)
	{
		if (IsEmpty(str) || count <= 0)
			return string.Empty;
		var parts = new string[count];
		for (int i = 0; i < count; i++)
		{
			parts[i] = str;
		}
		return string.Join(separator, parts);
	}

	/// <summary>
	/// 获取最大长度
	/// </summary>
	public static int MaxLength(params string[] strs)
	{
		if (strs == null || strs.Length == 0)
			return 0;
		int max = 0;
		foreach (var str in strs)
		{
			if (str != null && str.Length > max)
			{
				max = str.Length;
			}
		}
		return max;
	}

	/// <summary>
	/// 检查字符串是否包含任何指定的字符
	/// </summary>
	public static bool ContainsAny(string str, params char[] chars)
	{
		if (IsEmpty(str) || chars == null || chars.Length == 0)
			return false;
		foreach (var c in chars)
		{
			if (str.Contains(c))
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// 检查字符串是否包含任何指定的子字符串
	/// </summary>
	public static bool ContainsAny(string str, params string[] substrings)
	{
		if (IsEmpty(str) || substrings == null || substrings.Length == 0)
			return false;
		foreach (var substring in substrings)
		{
			if (str.Contains(substring))
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// 居中对齐字符串
	/// </summary>
	public static string Center(string str, int length, char padChar = ' ')
	{
		if (str == null)
			str = string.Empty;
		if (length <= str.Length)
			return str;
		int padding = length - str.Length;
		int leftPadding = padding / 2;
		int rightPadding = padding - leftPadding;
		return new string(padChar, leftPadding) + str + new string(padChar, rightPadding);
	}

	/// <summary>
	/// 居中对齐字符串
	/// </summary>
	public static string Center(string str, int length, string padStr)
	{
		if (str == null)
			str = string.Empty;
		if (length <= str.Length)
			return str;
		int padding = length - str.Length;
		int leftPadding = padding / 2;
		int rightPadding = padding - leftPadding;
		var leftPad = Repeat(padStr, leftPadding / padStr.Length + 1).Substring(0, leftPadding);
		var rightPad = Repeat(padStr, rightPadding / padStr.Length + 1).Substring(0, rightPadding);
		return leftPad + str + rightPad;
	}
}