using System.Net;

namespace WellTool.Http;

/// <summary>
/// HTTP 配置项
/// </summary>
public class HttpConfig
{
    /// <summary>
    /// 创建默认 HTTP 配置信息
    /// </summary>
    /// <returns>HttpConfig</returns>
    public static HttpConfig Create() => new();

    /// <summary>
    /// 默认连接超时（毫秒）
    /// </summary>
    public int ConnectionTimeout { get; set; } = HttpGlobalConfig.Timeout;

    /// <summary>
    /// 默认读取超时（毫秒）
    /// </summary>
    public int ReadTimeout { get; set; } = HttpGlobalConfig.Timeout;

    /// <summary>
    /// 是否禁用缓存
    /// </summary>
    public bool IsDisableCache { get; set; }

    /// <summary>
    /// 最大重定向次数
    /// </summary>
    public int MaxRedirectCount { get; set; } = HttpGlobalConfig.MaxRedirectCount;

    /// <summary>
    /// 代理
    /// </summary>
    public IWebProxy? Proxy { get; set; }

    /// <summary>
    /// HostnameVerifier，用于 HTTPS 安全连接
    /// </summary>
    public Func<string, string, bool>? HostnameVerifier { get; set; }

    /// <summary>
    /// Chuncked 块大小，0 或小于 0 表示不设置 Chunked 模式
    /// </summary>
    public int BlockSize { get; set; }

    /// <summary>
    /// 是否忽略响应读取时可能的 EOF 异常
    /// </summary>
    public bool IgnoreEOFError { get; set; } = HttpGlobalConfig.IgnoreEOFError;

    /// <summary>
    /// 是否忽略解码 URL
    /// </summary>
    public bool DecodeUrl { get; set; } = HttpGlobalConfig.IsDecodeUrl;

    /// <summary>
    /// 重定向时是否使用拦截器
    /// </summary>
    public bool InterceptorOnRedirect { get; set; }

    /// <summary>
    /// 自动重定向时是否处理 cookie
    /// </summary>
    public bool FollowRedirectsCookie { get; set; }

    /// <summary>
    /// 是否使用默认 Content-Type
    /// </summary>
    public bool UseDefaultContentTypeIfNull { get; set; } = true;

    /// <summary>
    /// 是否忽略 Content-Length
    /// </summary>
    public bool IgnoreContentLength { get; set; }

    /// <summary>
    /// 设置超时，单位：毫秒
    /// </summary>
    /// <param name="milliseconds">超时毫秒数</param>
    /// <returns>this</returns>
    public HttpConfig Timeout(int milliseconds)
    {
        ConnectionTimeout = milliseconds;
        ReadTimeout = milliseconds;
        return this;
    }

    /// <summary>
    /// 设置连接超时，单位：毫秒
    /// </summary>
    /// <param name="milliseconds">超时毫秒数</param>
    /// <returns>this</returns>
    public HttpConfig SetConnectionTimeout(int milliseconds)
    {
        ConnectionTimeout = milliseconds;
        return this;
    }

    /// <summary>
    /// 设置读取超时，单位：毫秒
    /// </summary>
    /// <param name="milliseconds">超时毫秒数</param>
    /// <returns>this</returns>
    public HttpConfig SetReadTimeout(int milliseconds)
    {
        ReadTimeout = milliseconds;
        return this;
    }

    /// <summary>
    /// 禁用缓存
    /// </summary>
    /// <returns>this</returns>
    public HttpConfig DisableCache()
    {
        IsDisableCache = true;
        return this;
    }

    /// <summary>
    /// 设置最大重定向次数
    /// </summary>
    /// <param name="maxRedirectCount">最大重定向次数</param>
    /// <returns>this</returns>
    public HttpConfig SetMaxRedirectCount(int maxRedirectCount)
    {
        MaxRedirectCount = Math.Max(maxRedirectCount, 0);
        return this;
    }

    /// <summary>
    /// 设置域名验证器
    /// </summary>
    /// <param name="hostnameVerifier">HostnameVerifier</param>
    /// <returns>this</returns>
    public HttpConfig SetHostnameVerifier(Func<string, string, bool> hostnameVerifier)
    {
        HostnameVerifier = hostnameVerifier;
        return this;
    }

    /// <summary>
    /// 设置 HTTP 代理
    /// </summary>
    /// <param name="host">代理主机</param>
    /// <param name="port">代理端口</param>
    /// <returns>this</returns>
    public HttpConfig SetHttpProxy(string host, int port)
    {
        Proxy = new WebProxy(host, port);
        return this;
    }

    /// <summary>
    /// 设置代理
    /// </summary>
    /// <param name="proxy">代理</param>
    /// <returns>this</returns>
    public HttpConfig SetProxy(IWebProxy proxy)
    {
        Proxy = proxy;
        return this;
    }

    /// <summary>
    /// 采用流方式上传数据，无需本地缓存数据
    /// </summary>
    /// <param name="blockSize">块大小（bytes 数），0 或小于 0 表示不设置 Chunked 模式</param>
    /// <returns>this</returns>
    public HttpConfig SetBlockSize(int blockSize)
    {
        BlockSize = blockSize;
        return this;
    }

    /// <summary>
    /// 设置是否忽略响应读取时可能的 EOF 异常
    /// </summary>
    /// <param name="ignoreEOFError">是否忽略 EOF 异常</param>
    /// <returns>this</returns>
    public HttpConfig SetIgnoreEOFError(bool ignoreEOFError)
    {
        IgnoreEOFError = ignoreEOFError;
        return this;
    }

    /// <summary>
    /// 设置是否忽略解码 URL
    /// </summary>
    /// <param name="decodeUrl">是否忽略解码 URL</param>
    /// <returns>this</returns>
    public HttpConfig SetDecodeUrl(bool decodeUrl)
    {
        DecodeUrl = decodeUrl;
        return this;
    }

    /// <summary>
    /// 重定向时是否使用拦截器
    /// </summary>
    /// <param name="interceptorOnRedirect">重定向时是否使用拦截器</param>
    /// <returns>this</returns>
    public HttpConfig SetInterceptorOnRedirect(bool interceptorOnRedirect)
    {
        InterceptorOnRedirect = interceptorOnRedirect;
        return this;
    }

    /// <summary>
    /// 自动重定向时是否处理 cookie
    /// </summary>
    /// <param name="followRedirectsCookie">自动重定向时是否处理 cookie</param>
    /// <returns>this</returns>
    public HttpConfig SetFollowRedirectsCookie(bool followRedirectsCookie)
    {
        FollowRedirectsCookie = followRedirectsCookie;
        return this;
    }

    /// <summary>
    /// 设置是否使用默认 Content-Type
    /// </summary>
    /// <param name="useDefaultContentTypeIfNull">是否使用默认 Content-Type</param>
    /// <returns>this</returns>
    public HttpConfig SetUseDefaultContentTypeIfNull(bool useDefaultContentTypeIfNull)
    {
        UseDefaultContentTypeIfNull = useDefaultContentTypeIfNull;
        return this;
    }

    /// <summary>
    /// 设置是否忽略 Content-Length
    /// </summary>
    /// <param name="ignoreContentLength">是否忽略 Content-Length</param>
    /// <returns>this</returns>
    public HttpConfig SetIgnoreContentLength(bool ignoreContentLength)
    {
        IgnoreContentLength = ignoreContentLength;
        return this;
    }
}
