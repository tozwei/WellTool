namespace WellTool.Http;

/// <summary>
/// HTTP 头域枚举
/// </summary>
public enum Header
{
    //------------------------------------------------------------- 通用头域
    /// <summary>
    /// 提供验证头，例如：Authorization: Basic YWxhZGRpbjpvcGVuc2VzYW1l
    /// </summary>
    AUTHORIZATION,
    /// <summary>
    /// 提供给代理服务器的用于身份验证的凭证
    /// </summary>
    PROXY_AUTHORIZATION,
    /// <summary>
    /// 提供日期和时间标志，说明报文是什么时间创建的
    /// </summary>
    DATE,
    /// <summary>
    /// 允许客户端和服务器指定与请求/响应连接有关的选项
    /// </summary>
    CONNECTION,
    /// <summary>
    /// 给出发送端使用的 MIME 版本
    /// </summary>
    MIME_VERSION,
    /// <summary>
    /// 如果报文采用了分块传输编码方式，就可以用这个首部列出位于报文拖挂部分的首部集合
    /// </summary>
    TRAILER,
    /// <summary>
    /// 告知接收端为了保证报文的可靠传输，对报文采用了什么编码方式
    /// </summary>
    TRANSFER_ENCODING,
    /// <summary>
    /// 给出了发送端可能想要"升级"使用的新版本和协议
    /// </summary>
    UPGRADE,
    /// <summary>
    /// 显示了报文经过的中间节点
    /// </summary>
    VIA,
    /// <summary>
    /// 指定请求和响应遵循的缓存机制
    /// </summary>
    CACHE_CONTROL,
    /// <summary>
    /// 用来包含实现特定的指令
    /// </summary>
    PRAGMA,
    /// <summary>
    /// 请求表示提交内容类型或返回返回内容的 MIME 类型
    /// </summary>
    CONTENT_TYPE,

    //------------------------------------------------------------- 请求头域
    /// <summary>
    /// 指定请求资源的 Internet 主机和端口号
    /// </summary>
    HOST,
    /// <summary>
    /// 指定请求的源资源地址
    /// </summary>
    REFERER,
    /// <summary>
    /// 指定请求的域
    /// </summary>
    ORIGIN,
    /// <summary>
    /// HTTP 客户端运行的浏览器类型的详细信息
    /// </summary>
    USER_AGENT,
    /// <summary>
    /// 指定客户端能够接收的内容类型
    /// </summary>
    ACCEPT,
    /// <summary>
    /// 指定 HTTP 客户端浏览器用来展示返回信息所优先选择的语言
    /// </summary>
    ACCEPT_LANGUAGE,
    /// <summary>
    /// 指定客户端浏览器可以支持的 web 服务器返回内容压缩编码类型
    /// </summary>
    ACCEPT_ENCODING,
    /// <summary>
    /// 浏览器可以接受的字符编码集
    /// </summary>
    ACCEPT_CHARSET,
    /// <summary>
    /// HTTP 请求发送时，会把保存在该请求域名下的所有 cookie 值一起发送给 web 服务器
    /// </summary>
    COOKIE,
    /// <summary>
    /// 请求的内容长度
    /// </summary>
    CONTENT_LENGTH,

    //------------------------------------------------------------- 响应头域
    /// <summary>
    /// 提供 WWW 验证响应头
    /// </summary>
    WWW_AUTHENTICATE,
    /// <summary>
    /// Cookie
    /// </summary>
    SET_COOKIE,
    /// <summary>
    /// Content-Encoding
    /// </summary>
    CONTENT_ENCODING,
    /// <summary>
    /// Content-Disposition
    /// </summary>
    CONTENT_DISPOSITION,
    /// <summary>
    /// ETag
    /// </summary>
    ETAG,
    /// <summary>
    /// 重定向指示到的 URL
    /// </summary>
    LOCATION
}

/// <summary>
/// Header 扩展方法
/// </summary>
public static class HeaderExtensions
{
    private static readonly Dictionary<Header, string> HeaderValues = new()
    {
        { Header.AUTHORIZATION, "Authorization" },
        { Header.PROXY_AUTHORIZATION, "Proxy-Authorization" },
        { Header.DATE, "Date" },
        { Header.CONNECTION, "Connection" },
        { Header.MIME_VERSION, "MIME-Version" },
        { Header.TRAILER, "Trailer" },
        { Header.TRANSFER_ENCODING, "Transfer-Encoding" },
        { Header.UPGRADE, "Upgrade" },
        { Header.VIA, "Via" },
        { Header.CACHE_CONTROL, "Cache-Control" },
        { Header.PRAGMA, "Pragma" },
        { Header.CONTENT_TYPE, "Content-Type" },
        { Header.HOST, "Host" },
        { Header.REFERER, "Referer" },
        { Header.ORIGIN, "Origin" },
        { Header.USER_AGENT, "User-Agent" },
        { Header.ACCEPT, "Accept" },
        { Header.ACCEPT_LANGUAGE, "Accept-Language" },
        { Header.ACCEPT_ENCODING, "Accept-Encoding" },
        { Header.ACCEPT_CHARSET, "Accept-Charset" },
        { Header.COOKIE, "Cookie" },
        { Header.CONTENT_LENGTH, "Content-Length" },
        { Header.WWW_AUTHENTICATE, "WWW-Authenticate" },
        { Header.SET_COOKIE, "Set-Cookie" },
        { Header.CONTENT_ENCODING, "Content-Encoding" },
        { Header.CONTENT_DISPOSITION, "Content-Disposition" },
        { Header.ETAG, "ETag" },
        { Header.LOCATION, "Location" }
    };

    /// <summary>
    /// 获取 Header 的字符串值
    /// </summary>
    /// <param name="header">Header 枚举</param>
    /// <returns>Header 字符串值</returns>
    public static string GetValue(this Header header)
    {
        return HeaderValues.TryGetValue(header, out var value) ? value : header.ToString();
    }

    /// <summary>
    /// 从字符串获取 Header 枚举
    /// </summary>
    /// <param name="value">Header 字符串值</param>
    /// <returns>Header 枚举</returns>
    public static Header? FromValue(string value)
    {
        foreach (var kvp in HeaderValues)
        {
            if (kvp.Value.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return kvp.Key;
            }
        }
        return null;
    }
}
