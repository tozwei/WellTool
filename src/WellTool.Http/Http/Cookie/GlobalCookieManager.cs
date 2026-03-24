namespace WellTool.Http.Cookie;

/// <summary>
/// Cookie 管理器（简化版本 - 暂未实现完整功能）
/// </summary>
public static class GlobalCookieManager
{
    /// <summary>
    /// 自定义 Cookie 管理器（暂未实现）
    /// </summary>
    public static void SetCookieManager(object? customCookieManager)
    {
        // TODO: 未来实现完整的 Cookie 管理功能
        // 目前仅作为占位方法，避免编译错误
    }

    /// <summary>
    /// 获取 Cookie 管理器（暂未实现）
    /// </summary>
    public static object? GetCookieManager()
    {
        return null;
    }

    /// <summary>
    /// 获取指定域名下的 Cookie（暂未实现）
    /// </summary>
    public static List<object> GetCookies(Uri uri)
    {
        return new List<object>();
    }

    /// <summary>
    /// 添加 Cookie 到请求（暂未实现）
    /// </summary>
    public static void Add(IDictionary<string, List<string>> headers, Uri uri)
    {
        // TODO: 未来实现
    }

    /// <summary>
    /// 存储响应的 Cookie（暂未实现）
    /// </summary>
    public static void Store(Uri uri, IDictionary<string, List<string>> headers)
    {
        // TODO: 未来实现
    }

    /// <summary>
    /// 关闭 Cookie 管理（暂未实现）
    /// </summary>
    public static void CloseCookie()
    {
        SetCookieManager(null);
    }
}
