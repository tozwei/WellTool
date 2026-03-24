using System.Text;
using System.Text.RegularExpressions;

namespace WellTool.Http;

/// <summary>
/// HTML 工具类
/// </summary>
public static class HtmlUtil
{
    public const string NBSP = "&nbsp;";
    public const string AMP = "&amp;";
    public const string QUOTE = "&quot;";
    public const string APOS = "&#039;";
    public const string LT = "&lt;";
    public const string GT = "&gt;";

    public const string RE_HTML_MARK = "(<[^<]*?>)|(<[\\s]*?/[^<]*?>)|(<[^<]*?/[\\s]*?>)";
    public const string RE_HTML_EMPTY_MARK = "<(\\w+)([^>]*)>\\s*</\\1>";
    public const string RE_SCRIPT = "<[\\s]*?script[^>]*?>.*?<[\\s]*?\\/[\\s]*?script[\\s]*?>";

    private static readonly char[][] TEXT = new char[256][];

    static HtmlUtil()
    {
        // ASCII 码值最大的是【0x7f=127】，扩展 ASCII 码值最大的是【0xFF=255】
        for (int i = 0; i < 256; i++)
        {
            TEXT[i] = new char[] { (char)i };
        }

        // 特殊 HTML 字符
        TEXT['\''] = "&#039;".ToCharArray(); // 单引号
        TEXT['"'] = QUOTE.ToCharArray(); // 双引号
        TEXT['&'] = AMP.ToCharArray(); // &符
        TEXT['<'] = LT.ToCharArray(); // 小于号
        TEXT['>'] = GT.ToCharArray(); // 大于号
        TEXT[' '] = NBSP.ToCharArray(); // 不断开空格
    }

    /// <summary>
    /// 转义文本中的 HTML 字符为安全的字符
    /// </summary>
    /// <param name="text">被转义的文本</param>
    /// <returns>转义后的文本</returns>
    public static string Escape(string text)
    {
        return Encode(text);
    }

    /// <summary>
    /// 还原被转义的 HTML 特殊字符
    /// </summary>
    /// <param name="htmlStr">包含转义符的 HTML 内容</param>
    /// <returns>转换后的字符串</returns>
    public static string Unescape(string htmlStr)
    {
        if (string.IsNullOrWhiteSpace(htmlStr))
        {
            return htmlStr;
        }

        return System.Net.WebUtility.HtmlDecode(htmlStr);
    }

    /// <summary>
    /// 清除所有 HTML 标签，但是不删除标签内的内容
    /// </summary>
    /// <param name="content">文本</param>
    /// <returns>清除标签后的文本</returns>
    public static string CleanHtmlTag(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return content;
        }

        return Regex.Replace(content, RE_HTML_MARK, "", RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 清除所有 HTML 空标签
    /// </summary>
    /// <param name="content">文本</param>
    /// <returns>清除空标签后的文本</returns>
    public static string CleanEmptyTag(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return content;
        }

        return Regex.Replace(content, RE_HTML_EMPTY_MARK, "", RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 清除指定 HTML 标签和被标签包围的内容（不区分大小写）
    /// </summary>
    /// <param name="content">文本</param>
    /// <param name="tagNames">要清除的标签</param>
    /// <returns>去除标签后的文本</returns>
    public static string RemoveHtmlTag(string content, params string[] tagNames)
    {
        return RemoveHtmlTag(content, true, tagNames);
    }

    /// <summary>
    /// 清除指定 HTML 标签，不包括内容（不区分大小写）
    /// </summary>
    /// <param name="content">文本</param>
    /// <param name="tagNames">要清除的标签</param>
    /// <returns>去除标签后的文本</returns>
    public static string UnwrapHtmlTag(string content, params string[] tagNames)
    {
        return RemoveHtmlTag(content, false, tagNames);
    }

    /// <summary>
    /// 清除指定 HTML 标签（不区分大小写）
    /// </summary>
    /// <param name="content">文本</param>
    /// <param name="withTagContent">是否去掉被包含在标签中的内容</param>
    /// <param name="tagNames">要清除的标签</param>
    /// <returns>去除标签后的文本</returns>
    public static string RemoveHtmlTag(string content, bool withTagContent, params string[] tagNames)
    {
        if (string.IsNullOrEmpty(content) || tagNames == null || tagNames.Length == 0)
        {
            return content;
        }

        foreach (var tagName in tagNames)
        {
            if (string.IsNullOrWhiteSpace(tagName))
            {
                continue;
            }

            var trimmedTagName = tagName.Trim();
            string regex;

            if (withTagContent)
            {
                // 标签及其包含内容
                regex = $"(?i)<{trimmedTagName}(\\s+[^>]*?)?/?>(.*?</{trimmedTagName}>)?";
            }
            else
            {
                // 标签不包含内容
                regex = $"(?i)<{trimmedTagName}(\\s+[^>]*?)?/?>|</?{trimmedTagName}>";
            }

            content = Regex.Replace(content, regex, "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        return content;
    }

    /// <summary>
    /// 去除 HTML 标签中的属性
    /// </summary>
    /// <param name="content">文本</param>
    /// <param name="attrs">属性名（不区分大小写）</param>
    /// <returns>处理后的文本</returns>
    public static string RemoveHtmlAttr(string content, params string[] attrs)
    {
        if (string.IsNullOrEmpty(content) || attrs == null || attrs.Length == 0)
        {
            return content;
        }

        foreach (var attr in attrs)
        {
            // (?i) 表示忽略大小写
            var regex = $"(?i)(\\s*{attr}\\s*=\\s*)" +
                "(" +
                // name="xxxx"
                "([\"][^\"]+?[\"])|" +
                // name=xxx > 或者 name=xxx> 或者 name=xxx name2=xxx
                "([^>]+?\\s*(?=\\s|>))" +
                ")";

            content = Regex.Replace(content, regex, "", RegexOptions.IgnoreCase);
        }

        // issue#I8YV0K 去除尾部空格
        content = Regex.Replace(content, "\\s+(>|/>)", "$1");

        return content;
    }

    /// <summary>
    /// 去除指定标签的所有属性
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="tagNames">指定标签</param>
    /// <returns>处理后的文本</returns>
    public static string RemoveAllHtmlAttr(string content, params string[] tagNames)
    {
        if (string.IsNullOrEmpty(content) || tagNames == null || tagNames.Length == 0)
        {
            return content;
        }

        foreach (var tagName in tagNames)
        {
            var regex = $"(?i)<{tagName}[^>]*?>";
            content = Regex.Replace(content, regex, $"<{tagName}>");
        }

        return content;
    }

    /// <summary>
    /// 过滤 HTML 文本，防止 XSS 攻击
    /// </summary>
    public static string Filter(string htmlContent)
    {
        return new HTMLFilter().Filter(htmlContent);
    }

    /// <summary>
    /// 编码 HTML 文本（公开方法）
    /// </summary>
    public static string Encode(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        var buffer = new StringBuilder(text.Length + (text.Length >> 2));

        foreach (var c in text)
        {
            if (c < 256)
            {
                buffer.Append(TEXT[c]);
            }
            else
            {
                buffer.Append(c);
            }
        }

        return buffer.ToString();
    }

    /// <summary>
    /// 解码 HTML 文本（公开方法）
    /// </summary>
    public static string Decode(string html)
    {
        return Unescape(html);
    }

    /// <summary>
    /// 从 Content-Type 头中获取字符集
    /// </summary>
    /// <param name="contentType">Content-Type 头</param>
    /// <returns>字符集名称</returns>
    public static string? GetCharsetFromHeader(string? contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
        {
            return null;
        }

        var match = Regex.Match(contentType, "charset\\s*=\\s*([\\w-]+)", RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value : null;
    }
}
