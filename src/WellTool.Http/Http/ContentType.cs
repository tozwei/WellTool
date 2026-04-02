using System.Text;

namespace WellTool.Http;

/// <summary>
/// Content-Type 内容类型
/// </summary>
public static class ContentType
{
    /// <summary>
    /// application/json
    /// </summary>
    public const string JSON = "application/json";

    /// <summary>
    /// application/xml
    /// </summary>
    public const string XML = "application/xml";

    /// <summary>
    /// text/html
    /// </summary>
    public const string HTML = "text/html";

    /// <summary>
    /// text/plain
    /// </summary>
    public const string TEXT = "text/plain";

    /// <summary>
    /// application/x-www-form-urlencoded
    /// </summary>
    public const string FORM_URLENCODED = "application/x-www-form-urlencoded";

    /// <summary>
    /// multipart/form-data
    /// </summary>
    public const string MULTIPART = "multipart/form-data";

    /// <summary>
    /// application/octet-stream
    /// </summary>
    public const string OCTET_STREAM = "application/octet-stream";

    /// <summary>
    /// 判断是否为默认的 Content-Type
    /// </summary>
    /// <param name="contentType">Content-Type</param>
    /// <returns>是否为默认</returns>
    public static bool IsDefault(string? contentType)
    {
        return string.IsNullOrEmpty(contentType) ||
               contentType.Equals(TEXT, StringComparison.OrdinalIgnoreCase) ||
               contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 构建带编码的 Content-Type
    /// </summary>
    /// <param name="contentType">Content-Type</param>
    /// <param name="charset">字符集</param>
    /// <returns>带编码的 Content-Type</returns>
    public static string Build(string contentType, Encoding charset)
    {
        if (string.IsNullOrEmpty(contentType))
        {
            return contentType;
        }

        // 如果已经包含 charset 信息，则不添加
        if (contentType.Contains("charset=", StringComparison.OrdinalIgnoreCase))
        {
            return contentType;
        }

        return $"{contentType};charset={charset.WebName.ToUpper()}";
    }

    /// <summary>
    /// 根据内容获取 Content-Type
    /// </summary>
    public static string? Get(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return null;
        }

        var trimmedBody = body.Trim();

        // JSON 判断
        if ((trimmedBody.StartsWith('{') && trimmedBody.EndsWith('}')) ||
            (trimmedBody.StartsWith('[') && trimmedBody.EndsWith(']')))
        {
            return JSON;
        }

        // HTML 判断
        if (trimmedBody.StartsWith("<html") || trimmedBody.StartsWith("<!DOCTYPE html") ||
            trimmedBody.StartsWith("<body") || trimmedBody.StartsWith("<div") ||
            trimmedBody.StartsWith("<p") || trimmedBody.StartsWith("<span"))
        {
            return HTML;
        }

        // XML 判断（排除 HTML）
        if (trimmedBody.StartsWith("<?xml") || (trimmedBody.StartsWith('<') && !trimmedBody.StartsWith("<html")))
        {
            return XML;
        }

        return null;
    }

    /// <summary>
    /// 从 Content-Type 中获取编码
    /// </summary>
    /// <param name="contentType">Content-Type</param>
    /// <returns>编码</returns>
    public static Encoding? GetEncoding(string? contentType)
    {
        if (string.IsNullOrEmpty(contentType))
        {
            return null;
        }

        var charsetIndex = contentType.IndexOf("charset=", StringComparison.OrdinalIgnoreCase);
        if (charsetIndex >= 0)
        {
            var start = charsetIndex + 8; // "charset=".Length
            var end = contentType.IndexOf(';', start);
            if (end < 0)
            {
                end = contentType.Length;
            }

            var charsetName = contentType.Substring(start, end - start).Trim().Trim('"').Trim('\'');

            try
            {
                return Encoding.GetEncoding(charsetName);
            }
            catch
            {
                // 如果指定的编码不存在，返回 null
                return null;
            }
        }

        return null;
    }
}
