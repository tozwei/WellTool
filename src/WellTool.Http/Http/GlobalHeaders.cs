namespace WellTool.Http;

/// <summary>
/// 全局头部信息
/// 所有 HTTP 请求将共用此全局头部信息，除非在 HttpRequest 中自定义头部信息覆盖之
/// </summary>
public class GlobalHeaders
{
    private static readonly object _lock = new();
    private readonly Dictionary<string, List<string>> _headers = new();

    /// <summary>
    /// 单例实例
    /// </summary>
    public static GlobalHeaders Instance { get; } = new();

    /// <summary>
    /// 构造
    /// </summary>
    private GlobalHeaders()
    {
        PutDefault(false);
    }

    /// <summary>
    /// 加入默认的头部信息
    /// </summary>
    /// <param name="isReset">是否重置所有头部信息（删除自定义保留默认）</param>
    /// <returns>this</returns>
    public GlobalHeaders PutDefault(bool isReset)
    {
        if (isReset)
        {
            _headers.Clear();
        }

        // 设置默认请求头
        SetHeader(Header.ACCEPT.GetValue(), "text/html,application/json,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8", true);
        SetHeader(Header.ACCEPT_ENCODING.GetValue(), "gzip, deflate", true);
        SetHeader(Header.USER_AGENT.GetValue(), "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36 WellTool", true);

        return this;
    }

    #region Headers

    /// <summary>
    /// 根据 name 获取头信息
    /// </summary>
    /// <param name="name">Header 名</param>
    /// <returns>Header 值</returns>
    public string? GetHeader(string name)
    {
        var values = GetHeaderList(name);
        return values?.FirstOrDefault();
    }

    /// <summary>
    /// 根据 name 获取头信息列表
    /// </summary>
    /// <param name="name">Header 名</param>
    /// <returns>Header 值</returns>
    public List<string>? GetHeaderList(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        return _headers.TryGetValue(name.Trim(), out var values) ? values : null;
    }

    /// <summary>
    /// 根据 name 获取头信息
    /// </summary>
    /// <param name="name">Header 名</param>
    /// <returns>Header 值</returns>
    public string? GetHeader(Header name)
    {
        if (name == default)
        {
            return null;
        }
        return GetHeader(name.GetValue());
    }

    /// <summary>
    /// 设置一个 header
    /// </summary>
    /// <param name="name">Header 名</param>
    /// <param name="value">Header 值</param>
    /// <param name="isOverride">是否覆盖已有值</param>
    /// <returns>this</returns>
    public GlobalHeaders SetHeader(string name, string value, bool isOverride)
    {
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
        {
            lock (_lock)
            {
                var trimmedName = name.Trim();

                if (isOverride || !_headers.ContainsKey(trimmedName))
                {
                    _headers[trimmedName] = new List<string> { value.Trim() };
                }
                else
                {
                    _headers[trimmedName].Add(value.Trim());
                }
            }
        }
        return this;
    }

    /// <summary>
    /// 设置一个 header
    /// </summary>
    /// <param name="name">Header 名</param>
    /// <param name="value">Header 值</param>
    /// <param name="isOverride">是否覆盖已有值</param>
    /// <returns>this</returns>
    public GlobalHeaders SetHeader(Header name, string value, bool isOverride)
    {
        return SetHeader(name.GetValue(), value, isOverride);
    }

    /// <summary>
    /// 设置请求头
    /// </summary>
    /// <param name="headers">请求头</param>
    /// <returns>this</returns>
    public GlobalHeaders SetHeaders(IDictionary<string, List<string>> headers)
    {
        if (headers == null || headers.Count == 0)
        {
            return this;
        }

        foreach (var entry in headers)
        {
            foreach (var value in entry.Value)
            {
                SetHeader(entry.Key, value ?? string.Empty, false);
            }
        }
        return this;
    }

    /// <summary>
    /// 移除一个头信息
    /// </summary>
    /// <param name="name">Header 名</param>
    /// <returns>this</returns>
    public GlobalHeaders RemoveHeader(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            lock (_lock)
            {
                _headers.Remove(name.Trim());
            }
        }
        return this;
    }

    /// <summary>
    /// 移除一个头信息
    /// </summary>
    /// <param name="name">Header 名</param>
    /// <returns>this</returns>
    public GlobalHeaders RemoveHeader(Header name)
    {
        return RemoveHeader(name.GetValue());
    }

    /// <summary>
    /// 获取 headers
    /// </summary>
    /// <returns>Headers Map</returns>
    public IReadOnlyDictionary<string, List<string>> GetAllHeaders()
    {
        lock (_lock)
        {
            return new Dictionary<string, List<string>>(_headers);
        }
    }

    /// <summary>
    /// 清除所有头信息
    /// </summary>
    /// <returns>this</returns>
    public GlobalHeaders ClearHeaders()
    {
        lock (_lock)
        {
            _headers.Clear();
        }
        return this;
    }

    #endregion
}
