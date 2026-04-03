using System.Text;

namespace WellTool.Core.Util;

/// <summary>
/// 字符串工具类
/// </summary>
public static class StrUtil
{
    public const string EMPTY = "";
    public const char C_SPACE = ' ';
    public const string SPACE = " ";
    public const char C_DOT = '.';
    public const string DOT = ".";
    public const string DOUBLE_DOT = "..";
    public const char C_SLASH = '/';
    public const string SLASH = "/";
    public const char C_BACKSLASH = '\\';
    public const string BACKSLASH = "\\";
    public const char C_CR = '\r';
    public const string CR = "\r";
    public const char C_LF = '\n';
    public const string LF = "\n";
    public const string CRLF = "\r\n";
    public const char C_UNDERLINE = '_';
    public const string UNDERLINE = "_";
    public const string DASHED = "-";
    public const char C_COMMA = ',';
    public const string COMMA = ",";
    public const char C_DELIM_START = '{';
    public const string DELIM_START = "{";
    public const char C_DELIM_END = '}';
    public const string DELIM_END = "}";
    public const char C_BRACKET_START = '[';
    public const string BRACKET_START = "[";
    public const char C_BRACKET_END = ']';
    public const string BRACKET_END = "]";
    public const char C_COLON = ':';
    public const string COLON = ":";
    public const string NULL = "null";

    /// <summary>
    /// 如果对象是字符串是否为空白
    /// </summary>
    public static bool isBlankIfStr(object? obj)
    {
        if (obj == null)
        {
            return true;
        }
        if (obj is string str)
        {
            return isBlank(str);
        }
        return false;
    }

    /// <summary>
    /// 如果对象是字符串是否为空串
    /// </summary>
    public static bool isEmptyIfStr(object? obj)
    {
        if (obj == null)
        {
            return true;
        }
        if (obj is string str)
        {
            return isEmpty(str);
        }
        return false;
    }

    /// <summary>
    /// 字符串是否为空白
    /// </summary>
    public static bool isBlank(string? str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// 字符串是否为空
    /// </summary>
    public static bool isEmpty(string? str)
    {
        return string.IsNullOrEmpty(str);
    }

    /// <summary>
    /// 字符串是否不为空白
    /// </summary>
    public static bool isNotBlank(string? str)
    {
        return !isBlank(str);
    }

    /// <summary>
    /// 字符串是否不为空
    /// </summary>
    public static bool isNotEmpty(string? str)
    {
        return !isEmpty(str);
    }

    /// <summary>
    /// 去除字符串两端空白
    /// </summary>
    public static string trim(string? str)
    {
        return str?.Trim() ?? "";
    }

    /// <summary>
    /// 去除字符串两端空白
    /// </summary>
    public static string trimToEmpty(string? str)
    {
        return str?.Trim() ?? "";
    }

    /// <summary>
    /// 去除字符串前端空白
    /// </summary>
    public static string trimStart(string? str)
    {
        return str?.TrimStart() ?? "";
    }

    /// <summary>
    /// 去除字符串后端空白
    /// </summary>
    public static string trimEnd(string? str)
    {
        return str?.TrimEnd() ?? "";
    }

    /// <summary>
    /// 给定字符串数组全部做去首尾空格
    /// </summary>
    public static void trim(string?[]? strs)
    {
        if (strs == null)
        {
            return;
        }
        for (int i = 0; i < strs.Length; i++)
        {
            if (strs[i] != null)
            {
                strs[i] = trim(strs[i]);
            }
        }
    }

    /// <summary>
    /// 将对象转为字符串
    /// </summary>
    public static string? utf8Str(object? obj)
    {
        return str(obj, Encoding.UTF8);
    }

    /// <summary>
    /// 将对象转为字符串
    /// </summary>
    public static string? str(object? obj, Encoding? charset = null)
    {
        if (obj == null)
        {
            return null;
        }
        if (obj is string str)
        {
            return str;
        }
        if (obj is byte[] bytes)
        {
            return str(bytes, charset);
        }
        return obj.ToString();
    }

    /// <summary>
    /// 将byte数组转为字符串
    /// </summary>
    public static string? str(byte[]? bytes, Encoding? charset = null)
    {
        if (bytes == null)
        {
            return null;
        }
        if (charset == null)
        {
            return Encoding.Default.GetString(bytes);
        }
        return charset.GetString(bytes);
    }

    /// <summary>
    /// 调用对象的toString方法，null会返回"null"
    /// </summary>
    public static string toString(object? obj)
    {
        return obj?.ToString() ?? "null";
    }

    /// <summary>
    /// 调用对象的toString方法，null会返回null
    /// </summary>
    public static string? toStringOrNull(object? obj)
    {
        return obj?.ToString();
    }

    /// <summary>
    /// 调用对象的toString方法，null会返回空字符串
    /// </summary>
    public static string toStringOrEmpty(object? obj)
    {
        return obj?.ToString() ?? "";
    }

    /// <summary>
    /// 创建StringBuilder对象
    /// </summary>
    public static StringBuilder builder()
    {
        return new StringBuilder();
    }

    /// <summary>
    /// 创建StringBuilder对象
    /// </summary>
    public static StringBuilder builder(int capacity)
    {
        return new StringBuilder(capacity);
    }

    /// <summary>
    /// 反转字符串
    /// </summary>
    public static string reverse(string? str)
    {
        if (isBlank(str))
        {
            return str ?? "";
        }
        char[] chars = str!.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    /// <summary>
    /// 将已有字符串填充为规定长度
    /// </summary>
    public static string fillBefore(string? str, char filledChar, int len)
    {
        return fill(str, filledChar, len, true);
    }

    /// <summary>
    /// 将已有字符串填充为规定长度
    /// </summary>
    public static string fillAfter(string? str, char filledChar, int len)
    {
        return fill(str, filledChar, len, false);
    }

    /// <summary>
    /// 将已有字符串填充为规定长度
    /// </summary>
    public static string fill(string? str, char filledChar, int len, bool isPre)
    {
        if (str == null)
        {
            str = "";
        }
        int strLen = str.Length;
        if (strLen > len)
        {
            return str;
        }
        string filledStr = repeat(filledChar, len - strLen);
        return isPre ? filledStr + str : str + filledStr;
    }

    /// <summary>
    /// 重复指定字符
    /// </summary>
    public static string repeat(char c, int count)
    {
        return new string(c, count);
    }

    /// <summary>
    /// 生成随机UUID
    /// </summary>
    public static string uuid()
    {
        return Guid.NewGuid().ToString();
    }

    /// <summary>
    /// 格式化文本
    /// </summary>
    public static string format(string template, IDictionary<string, object> map, bool ignoreNull = true)
    {
        if (isEmpty(template))
        {
            return template ?? "";
        }
        var result = template;
        foreach (var kvp in map)
        {
            if (ignoreNull && kvp.Value == null)
            {
                continue;
            }
            result = result.Replace("{" + kvp.Key + "}", kvp.Value?.ToString() ?? "");
        }
        return result;
    }

    /// <summary>
    /// 截取字符串指定长度
    /// </summary>
    public static string? truncateUtf8(string? str, int maxBytes)
    {
        return truncateByByteLength(str, Encoding.UTF8, maxBytes, 4, true);
    }

    /// <summary>
    /// 截取字符串指定长度
    /// </summary>
    public static string? truncateByByteLength(string? str, Encoding charset, int maxBytesLength, int factor, bool appendDots)
    {
        if (str == null || str.Length * factor <= maxBytesLength)
        {
            return str;
        }
        byte[] sba = charset.GetBytes(str);
        if (sba.Length <= maxBytesLength)
        {
            return str;
        }
        int dotsBytesLength = charset.GetBytes("...").Length;
        int limitBytes;
        if (appendDots && maxBytesLength > dotsBytesLength)
        {
            limitBytes = maxBytesLength - dotsBytesLength;
        }
        else
        {
            limitBytes = maxBytesLength;
        }
        byte[] limited = new byte[limitBytes];
        Array.Copy(sba, 0, limited, 0, limitBytes);
        string result = charset.GetString(limited);
        if (appendDots && maxBytesLength > dotsBytesLength)
        {
            return result + "...";
        }
        return result;
    }
}
