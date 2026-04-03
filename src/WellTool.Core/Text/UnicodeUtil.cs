using WellTool.Core.Util;

namespace WellTool.Core.Text;

/// <summary>
/// 提供Unicode字符串和普通字符串之间的转换
/// </summary>
public static class UnicodeUtil
{
    /// <summary>
    /// Unicode字符串转为普通字符串
    /// Unicode字符串的表现方式为：\\uXXXX
    /// </summary>
    /// <param name="unicode">Unicode字符串</param>
    /// <returns>普通字符串</returns>
    public static string ToString(string? unicode)
    {
        if (string.IsNullOrWhiteSpace(unicode))
        {
            return unicode ?? "";
        }

        int len = unicode.Length;
        var sb = new System.Text.StringBuilder(len);
        int pos = 0;
        while ((pos = StrUtil.IndexOfIgnoreCase(unicode, "\\u", pos)) != -1)
        {
            sb.Append(unicode.AsSpan(0, pos));
            if (pos + 5 < len)
            {
                try
                {
                    char c = (char)int.Parse(unicode.Substring(pos + 2, 4), System.Globalization.NumberStyles.HexNumber);
                    sb.Append(c);
                    pos += 6;
                }
                catch (FormatException)
                {
                    sb.Append(unicode.AsSpan(pos, 2));
                    pos += 2;
                }
            }
            else
            {
                break;
            }
        }

        if (pos < len)
        {
            sb.Append(unicode.AsSpan(pos));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 字符编码为Unicode形式
    /// </summary>
    /// <param name="c">被编码的字符</param>
    /// <returns>Unicode字符串</returns>
    public static string ToUnicode(char c) => HexUtil.ToUnicodeHex(c);

    /// <summary>
    /// 字符串编码为Unicode形式
    /// </summary>
    /// <param name="str">被编码的字符串</param>
    /// <returns>Unicode字符串</returns>
    public static string ToUnicode(string str) => ToUnicode(str, true);

    /// <summary>
    /// 字符串编码为Unicode形式
    /// </summary>
    /// <param name="str">被编码的字符串</param>
    /// <param name="isSkipAscii">是否跳过ASCII字符（只跳过可见字符）</param>
    /// <returns>Unicode字符串</returns>
    public static string ToUnicode(string str, bool isSkipAscii)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        int len = str.Length;
        var unicode = new System.Text.StringBuilder(str.Length * 6);
        for (int i = 0; i < len; i++)
        {
            char c = str[i];
            if (isSkipAscii && CharUtil.IsAsciiPrintable(c))
            {
                unicode.Append(c);
            }
            else
            {
                unicode.Append(HexUtil.ToUnicodeHex(c));
            }
        }
        return unicode.ToString();
    }
}
