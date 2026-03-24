namespace WellTool.Http;

/// <summary>
/// HTTP 全局配置
/// </summary>
public static class HttpGlobalConfig
{
    private static int _timeout = 30000; // 默认 30 秒超时
    private static int _maxRedirectCount = 2;
    private static bool _ignoreEOFError = false;
    private static bool _isDecodeUrl = true;
    private static bool _trustAnyHost = false;

    /// <summary>
    /// 获取或设置全局默认的连接和读取超时时长（毫秒）
    /// </summary>
    public static int Timeout
    {
        get => _timeout;
        set => _timeout = value;
    }

    /// <summary>
    /// 获取或设置最大重定向次数
    /// </summary>
    public static int MaxRedirectCount
    {
        get => _maxRedirectCount;
        set => _maxRedirectCount = value;
    }

    /// <summary>
    /// 获取或设置是否忽略响应读取时可能的 EOF 异常
    /// </summary>
    public static bool IgnoreEOFError
    {
        get => _ignoreEOFError;
        set => _ignoreEOFError = value;
    }

    /// <summary>
    /// 获取或设置是否忽略解码 URL
    /// </summary>
    public static bool IsDecodeUrl
    {
        get => _isDecodeUrl;
        set => _isDecodeUrl = value;
    }

    /// <summary>
    /// 获取或设置是否信任任意 HTTPS 主机
    /// </summary>
    public static bool TrustAnyHost
    {
        get => _trustAnyHost;
        set => _trustAnyHost = value;
    }

    /// <summary>
    /// 设置全局默认的连接和读取超时时长
    /// </summary>
    /// <param name="customTimeout">超时时长（毫秒）</param>
    public static void SetTimeout(int customTimeout)
    {
        _timeout = customTimeout;
    }

    /// <summary>
    /// 设置全局最大重定向次数
    /// </summary>
    /// <param name="count">重定向次数</param>
    public static void SetMaxRedirectCount(int count)
    {
        _maxRedirectCount = count;
    }

    /// <summary>
    /// 允许 PATCH 方法（如果需要）
    /// </summary>
    public static void AllowPatch()
    {
        // .NET 中不需要特殊处理，PATCH 方法原生支持
    }
}
