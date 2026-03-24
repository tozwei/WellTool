namespace WellTool.Http.Cookie;

/// <summary>
/// 线程本地的 Cookie 存储（简化版本 - 暂未实现完整功能）
/// </summary>
public class ThreadLocalCookieStore
{
    /// <summary>
    /// 添加 Cookie（暂未实现）
    /// </summary>
    public void Add(Uri uri, object cookie)
    {
        // TODO: 未来实现
    }

    /// <summary>
    /// 获取指定域名的所有 Cookie（暂未实现）
    /// </summary>
    public List<object> Get(Uri uri)
    {
        return new List<object>();
    }

    /// <summary>
    /// 移除指定域名的 Cookie（暂未实现）
    /// </summary>
    public void Remove(Uri uri)
    {
        // TODO: 未来实现
    }

    /// <summary>
    /// 清空所有 Cookie（暂未实现）
    /// </summary>
    public void Clear()
    {
        // TODO: 未来实现
    }
}
