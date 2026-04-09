namespace WellTool.Http.Cookie;

/// <summary>
/// 线程本地的 Cookie 存储
/// </summary>
public class ThreadLocalCookieStore
{
    // 存储 Cookie 的字典，键为域名，值为 Cookie 列表
    private readonly Dictionary<string, List<Cookie>> _cookies = new Dictionary<string, List<Cookie>>();

    /// <summary>
    /// 添加 Cookie
    /// </summary>
    public void Add(Uri uri, object cookie)
    {
        if (cookie is Cookie cookieObj)
        {
            var domain = cookieObj.Domain ?? uri.Host;
            if (!_cookies.TryGetValue(domain, out var cookieList))
            {
                cookieList = new List<Cookie>();
                _cookies[domain] = cookieList;
            }

            // 检查是否已存在同名 Cookie
            var existingCookieIndex = cookieList.FindIndex(c => c.Name == cookieObj.Name && c.Path == cookieObj.Path);
            if (existingCookieIndex >= 0)
            {
                // 更新现有 Cookie
                cookieList[existingCookieIndex] = cookieObj;
            }
            else
            {
                // 添加新 Cookie
                cookieList.Add(cookieObj);
            }

            // 清理过期的 Cookie
            CleanExpiredCookies();
        }
    }

    /// <summary>
    /// 获取指定域名的所有 Cookie
    /// </summary>
    public List<object> Get(Uri uri)
    {
        var result = new List<object>();
        var domain = uri.Host;

        // 清理过期的 Cookie
        CleanExpiredCookies();

        // 获取指定域名的 Cookie
        if (_cookies.TryGetValue(domain, out var cookieList))
        {
            // 过滤出路径匹配的 Cookie
            var path = uri.AbsolutePath;
            var matchingCookies = cookieList.Where(c => path.StartsWith(c.Path)).ToList();
            result.AddRange(matchingCookies);
        }

        return result;
    }

    /// <summary>
    /// 移除指定域名的 Cookie
    /// </summary>
    public void Remove(Uri uri)
    {
        var domain = uri.Host;
        _cookies.Remove(domain);
    }

    /// <summary>
    /// 清空所有 Cookie
    /// </summary>
    public void Clear()
    {
        _cookies.Clear();
    }

    /// <summary>
    /// 清理过期的 Cookie
    /// </summary>
    private void CleanExpiredCookies()
    {
        var now = DateTime.Now;
        foreach (var domain in _cookies.Keys.ToList())
        {
            var cookieList = _cookies[domain];
            var validCookies = cookieList.Where(c => !c.Expires.HasValue || c.Expires.Value > now).ToList();
            if (validCookies.Count == 0)
            {
                _cookies.Remove(domain);
            }
            else
            {
                _cookies[domain] = validCookies;
            }
        }
    }
}
