namespace WellTool.Http.Cookie;

/// <summary>
/// Cookie 管理器
/// </summary>
public static class GlobalCookieManager
{
    // 线程本地的 Cookie 存储
    private static readonly ThreadLocal<ThreadLocalCookieStore> _cookieStore = new ThreadLocal<ThreadLocalCookieStore>(() => new ThreadLocalCookieStore());

    /// <summary>
    /// 自定义 Cookie 管理器
    /// </summary>
    public static void SetCookieManager(object? customCookieManager)
    {
        // 目前使用默认的 ThreadLocalCookieStore，未来可以支持自定义 Cookie 管理器
    }

    /// <summary>
    /// 获取 Cookie 管理器
    /// </summary>
    public static object? GetCookieManager()
    {
        return null;
    }

    /// <summary>
    /// 获取指定域名下的 Cookie
    /// </summary>
    public static List<object> GetCookies(Uri uri)
    {
        return _cookieStore.Value.Get(uri);
    }

    /// <summary>
    /// 添加 Cookie 到请求
    /// </summary>
    public static void Add(IDictionary<string, List<string>> headers, Uri uri)
    {
        var cookies = GetCookies(uri);
        if (cookies.Count > 0)
        {
            var cookieHeader = string.Join("; ", cookies.Select(c => c.ToString()));
            if (!headers.TryGetValue("Cookie", out var cookieValues))
            {
                cookieValues = new List<string>();
                headers["Cookie"] = cookieValues;
            }
            cookieValues.Add(cookieHeader);
        }
    }

    /// <summary>
    /// 存储响应的 Cookie
    /// </summary>
    public static void Store(Uri uri, IDictionary<string, List<string>> headers)
    {
        if (headers.TryGetValue("Set-Cookie", out var setCookieValues))
        {
            foreach (var setCookieValue in setCookieValues)
            {
                var cookie = Cookie.Parse(setCookieValue);
                if (cookie != null)
                {
                    _cookieStore.Value.Add(uri, cookie);
                }
            }
        }
    }

    /// <summary>
    /// 关闭 Cookie 管理
    /// </summary>
    public static void CloseCookie()
    {
        _cookieStore.Value.Clear();
        SetCookieManager(null);
    }
}
