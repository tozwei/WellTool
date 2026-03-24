namespace WellTool.Http;

/// <summary>
/// HTTP 状态码
/// </summary>
public static class HttpStatus
{
    /// <summary>
    /// 判断是否为重定向状态码
    /// </summary>
    /// <param name="statusCode">状态码</param>
    /// <returns>是否为重定向</returns>
    public static bool IsRedirected(int statusCode)
    {
        return statusCode is 300 or 301 or 302 or 303 or 307 or 308;
    }

    /// <summary>
    /// 临时重定向
    /// </summary>
    public const int HTTP_TEMP_REDIRECT = 307;

    /// <summary>
    /// 永久重定向
    /// </summary>
    public const int HTTP_PERMANENT_REDIRECT = 308;
}
