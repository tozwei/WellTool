using System.Collections;
using System.Text;

namespace WellTool.Http;

/// <summary>
/// HTTP 基类
/// </summary>
/// <typeparam name="T">子类类型，方便链式编�?/typeparam>
public abstract class HttpBase<T> where T : HttpBase<T>
{
    /// <summary>
    /// 默认的请求编码、URL �?encode、decode 编码
    /// </summary>
    protected static readonly Encoding DEFAULT_CHARSET = Encoding.UTF8;

    /// <summary>
    /// HTTP/1.0
    /// </summary>
    public const string HTTP_1_0 = "HTTP/1.0";

    /// <summary>
    /// HTTP/1.1
    /// </summary>
    public const string HTTP_1_1 = "HTTP/1.1";

    /// <summary>
    /// 是否聚合重复请求�?
    /// </summary>
    protected bool IsHeaderAggregated { get; set; }

    /// <summary>
    /// 存储头信�?
    /// </summary>
    protected Dictionary<string, List<string>> Headers { get; } = new();

    /// <summary>
    /// 编码
    /// </summary>
    protected Encoding? Charset { get; set; } = DEFAULT_CHARSET;

    /// <summary>
    /// HTTP 版本
    /// </summary>
    protected string HttpVersion { get; set; } = HTTP_1_1;

    /// <summary>
    /// 存储主体内容
    /// </summary>
    protected string? BodyContent { get; set; }

    #region Headers

    /// <summary>
    /// 根据 name 获取头信�?
    /// </summary>
    /// <param name="name">Header �?/param>
    /// <returns>Header �?/returns>
    public string? GetHeader(string name)
    {
        var values = GetHeaderList(name);
        return values?.FirstOrDefault();
    }

    /// <summary>
    /// 根据 name 获取头信息列�?
    /// </summary>
    /// <param name="name">Header �?/param>
    /// <returns>Header 值列�?/returns>
    public List<string>? GetHeaderList(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        var trimmedName = name.Trim();

        // 不区分大小写查找
        foreach (var kvp in Headers)
        {
            if (kvp.Key.Equals(trimmedName, StringComparison.OrdinalIgnoreCase))
            {
                return kvp.Value;
            }
        }

        return null;
    }

    /// <summary>
    /// 根据 name 获取头信�?
    /// </summary>
    /// <param name="name">Header �?/param>
    /// <returns>Header �?/returns>
    public string? GetHeader(Header name)
    {
        if (name == default)
        {
            return null;
        }
        return GetHeader(name.GetValue());
    }

    /// <summary>
    /// 设置一�?header
    /// </summary>
    /// <param name="name">Header �?/param>
    /// <param name="value">Header �?/param>
    /// <param name="isOverride">是否覆盖已有�?/param>
    /// <returns>T 本身</returns>
    public T SetHeader(string name, string? value, bool isOverride)
    {
        if (!string.IsNullOrEmpty(name) && value != null)
        {
            var trimmedName = name.Trim();
            var trimmedValue = value.Trim();

            if (isOverride || !Headers.ContainsKey(trimmedName))
            {
                Headers[trimmedName] = new List<string> { trimmedValue };
            }
            else
            {
                Headers[trimmedName].Add(trimmedValue);
            }
        }
        return (T)this;
    }

    /// <summary>
    /// 设置一�?header
    /// </summary>
    /// <param name="name">Header �?/param>
    /// <param name="value">Header �?/param>
    /// <param name="isOverride">是否覆盖已有�?/param>
    /// <returns>T 本身</returns>
    public T SetHeader(Header name, string? value, bool isOverride)
    {
        return SetHeader(name.GetValue(), value, isOverride);
    }

    /// <summary>
    /// 设置一�?header（覆盖模式）
    /// </summary>
    /// <param name="name">Header �?/param>
    /// <param name="value">Header �?/param>
    /// <returns>T 本身</returns>
    public T SetHeader(Header name, string? value)
    {
        return SetHeader(name.GetValue(), value, true);
    }

    /// <summary>
    /// 设置一�?header（覆盖模式）
    /// </summary>
    /// <param name="name">Header �?/param>
    /// <param name="value">Header �?/param>
    /// <returns>T 本身</returns>
    public T SetHeader(string name, string? value)
    {
        return SetHeader(name, value, true);
    }

    /// <summary>
    /// 设置 Content-Type
    /// </summary>
    /// <param name="contentType">Content-Type</param>
    /// <returns>T 本身</returns>
    public T SetContentType(string contentType)
    {
        return SetHeader(Header.CONTENT_TYPE, contentType);
    }

    /// <summary>
    /// 设置请求�?
    /// </summary>
    /// <param name="headers">请求�?/param>
    /// <param name="isOverride">是否覆盖已有头信�?/param>
    /// <returns>this</returns>
    public T HeaderMap(IDictionary<string, string> headers, bool isOverride)
    {
        if (headers == null || headers.Count == 0)
        {
            return (T)this;
        }

        foreach (var entry in headers)
        {
            SetHeader(entry.Key, entry.Value ?? string.Empty, isOverride);
        }
        return (T)this;
    }

    /// <summary>
    /// 设置请求头（不覆盖原有请求头�?
    /// </summary>
    /// <param name="headers">请求�?/param>
    /// <returns>this</returns>
    public T SetHeaders(IDictionary<string, List<string>> headers)
    {
        return SetHeaders(headers, false);
    }

    /// <summary>
    /// 设置请求�?
    /// </summary>
    /// <param name="headers">请求�?/param>
    /// <param name="isOverride">是否覆盖已有头信�?/param>
    /// <returns>this</returns>
    public T SetHeaders(IDictionary<string, List<string>> headers, bool isOverride)
    {
        if (headers == null || headers.Count == 0)
        {
            return (T)this;
        }

        foreach (var entry in headers)
        {
            foreach (var value in entry.Value)
            {
                SetHeader(entry.Key, value ?? string.Empty, isOverride);
            }
        }
        return (T)this;
    }

    /// <summary>
    /// 新增请求头（不覆盖原有请求头�?
    /// </summary>
    /// <param name="headers">请求�?/param>
    /// <returns>this</returns>
    public T AddHeaders(IDictionary<string, string> headers)
    {
        if (headers == null || headers.Count == 0)
        {
            return (T)this;
        }

        foreach (var entry in headers)
        {
            SetHeader(entry.Key, entry.Value ?? string.Empty, false);
        }
        return (T)this;
    }

    /// <summary>
    /// 移除一个头信息
    /// </summary>
    /// <param name="name">Header �?/param>
    /// <returns>this</returns>
    public T RemoveHeader(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            var trimmedName = name.Trim();
            // 不区分大小写查找并移除
            var headerToRemove = Headers.Keys.FirstOrDefault(key => key.Equals(trimmedName, StringComparison.OrdinalIgnoreCase));
            if (headerToRemove != null)
            {
                Headers.Remove(headerToRemove);
            }
        }
        return (T)this;
    }

    /// <summary>
    /// 移除一个头信息
    /// </summary>
    /// <param name="name">Header �?/param>
    /// <returns>this</returns>
    public T RemoveHeader(Header name)
    {
        return RemoveHeader(name.GetValue());
    }

    /// <summary>
    /// 获取 headers
    /// </summary>
    /// <returns>Headers Map</returns>
    public IReadOnlyDictionary<string, List<string>> GetAllHeaders()
    {
        return Headers;
    }

    /// <summary>
    /// 清除所有头信息
    /// </summary>
    /// <returns>this</returns>
    public T ClearHeaders()
    {
        Headers.Clear();
        return (T)this;
    }

    /// <summary>
    /// 设置是否需要聚合重复的请求�?
    /// </summary>
    /// <param name="aggregate">是否需要聚�?/param>
    /// <returns>this</returns>
    public T HeaderAggregation(bool aggregate)
    {
        IsHeaderAggregated = aggregate;
        return (T)this;
    }

    /// <summary>
    /// 获取是否需要聚合请求头状�?
    /// </summary>
    /// <returns>isHeaderAggregated 请求头聚合状�?/returns>
    public bool GetIsHeaderAggregated()
    {
        return IsHeaderAggregated;
    }

    #endregion

    #region Content-Length

    /// <summary>
    /// 设置内容长度
    /// </summary>
    /// <param name="value">长度</param>
    /// <returns>T 本身</returns>
    public T ContentLength(int value)
    {
        return SetHeader(Header.CONTENT_LENGTH, value.ToString());
    }

    #endregion

    #region HTTP Version

    /// <summary>
    /// 返回 http 版本
    /// </summary>
    /// <returns>String</returns>
    public string GetHttpVersion()
    {
        return HttpVersion;
    }

    /// <summary>
    /// 设置 http 版本
    /// </summary>
    /// <param name="httpVersion">Http 版本</param>
    /// <returns>this</returns>
    public T SetHttpVersion(string httpVersion)
    {
        HttpVersion = httpVersion;
        return (T)this;
    }

    #endregion

    #region BodyBytes

    /// <summary>
    /// 获取 bodyBytes 存储字节�?
    /// </summary>
    /// <returns>byte[]</returns>
    public virtual byte[]? BodyBytes()
    {
        return BodyContent != null ? Encoding.UTF8.GetBytes(BodyContent) : null;
    }

    #endregion

    #region Charset

    /// <summary>
    /// 返回字符�?
    /// </summary>
    /// <returns>字符�?/returns>
    public string? GetCharset()
    {
        return Charset?.WebName;
    }

    /// <summary>
    /// 设置字符�?
    /// </summary>
    /// <param name="charset">字符�?/param>
    /// <returns>T 自己</returns>
    public T SetCharset(string charset)
    {
        if (!string.IsNullOrEmpty(charset))
        {
            try
            {
                Charset = Encoding.GetEncoding(charset);
            }
            catch
            {
                // 如果指定的编码不存在，使�?UTF-8
                Charset = Encoding.UTF8;
            }
        }
        return (T)this;
    }

    /// <summary>
    /// 设置字符�?
    /// </summary>
    /// <param name="charset">字符�?/param>
    /// <returns>T 自己</returns>
    public T SetCharset(Encoding? charset)
    {
        if (charset != null)
        {
            Charset = charset;
        }
        return (T)this;
    }

    #endregion

    #region ToString

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns>字符串表�?/returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("Headers: ").AppendLine();
        foreach (var entry in Headers)
        {
            sb.Append("    ").Append(entry.Key).Append(": ")
                .Append(string.Join(",", entry.Value)).AppendLine();
        }

        sb.Append("Body: ").AppendLine();
        sb.Append("    ").Append(BodyContent ?? "(null)").AppendLine();

        return sb.ToString();
    }

    #endregion
}






















