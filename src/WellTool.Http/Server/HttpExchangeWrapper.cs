using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace WellTool.Http.Server
{
    /// <summary>
    /// HttpListener请求上下文包装类，提供增强方法和缓存
    /// </summary>
    public class HttpExchangeWrapper
    {
        private readonly HttpListenerRequest _request;
        private readonly HttpListenerResponse _response;
        private Encoding _charsetCache;
        private NameValueCollection _paramsCache;
        private NameValueCollection _headersCache;
        private string _bodyCache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="request">HttpListener请求</param>
        /// <param name="response">HttpListener响应</param>
        public HttpExchangeWrapper(HttpListenerRequest request, HttpListenerResponse response)
        {
            _request = request;
            _response = response;
        }

        /// <summary>
        /// 获取原始请求对象
        /// </summary>
        public HttpListenerRequest Request => _request;

        /// <summary>
        /// 获取原始响应对象
        /// </summary>
        public HttpListenerResponse Response => _response;

        /// <summary>
        /// 获取请求包装对象
        /// </summary>
        public HttpServerRequest GetRequest()
        {
            return new HttpServerRequest(_request);
        }

        /// <summary>
        /// 获取响应包装对象
        /// </summary>
        public HttpServerResponse GetResponse()
        {
            return new HttpServerResponse(_response);
        }

        /// <summary>
        /// 获取HTTP方法
        /// </summary>
        public string Method => _request.HttpMethod;

        /// <summary>
        /// 获取请求URL
        /// </summary>
        public string Url => _request.Url?.ToString();

        /// <summary>
        /// 获取请求路径
        /// </summary>
        public string Path => _request.Url?.LocalPath;

        /// <summary>
        /// 获取查询字符串
        /// </summary>
        public string Query => _request.Url?.Query;

        /// <summary>
        /// 获取请求头
        /// </summary>
        public NameValueCollection Headers => _headersCache ??= _request.Headers;

        /// <summary>
        /// 获取指定请求头
        /// </summary>
        /// <param name="name">头名称</param>
        /// <returns>头值</returns>
        public string GetHeader(string name)
        {
            return Headers?.Get(name);
        }

        /// <summary>
        /// 获取Content-Type
        /// </summary>
        public string ContentType => _request.ContentType;

        /// <summary>
        /// 获取请求编码
        /// </summary>
        public Encoding Charset
        {
            get
            {
                if (_charsetCache == null)
                {
                    var contentType = ContentType;
                    if (!string.IsNullOrEmpty(contentType))
                    {
                        _charsetCache = GetCharsetFromContentType(contentType) ?? Encoding.UTF8;
                    }
                    else
                    {
                        _charsetCache = Encoding.UTF8;
                    }
                }
                return _charsetCache;
            }
        }

        /// <summary>
        /// 获取User-Agent
        /// </summary>
        public string UserAgent => _request.UserAgent;

        /// <summary>
        /// 获取远程地址
        /// </summary>
        public IPEndPoint RemoteEndPoint => _request.RemoteEndPoint;

        /// <summary>
        /// 获取本地地址
        /// </summary>
        public IPEndPoint LocalEndPoint => _request.LocalEndPoint;

        /// <summary>
        /// 获取请求体
        /// </summary>
        public string Body
        {
            get
            {
                if (_bodyCache == null)
                {
                    using var reader = new StreamReader(_request.InputStream, Charset);
                    _bodyCache = reader.ReadToEnd();
                }
                return _bodyCache;
            }
        }

        /// <summary>
        /// 获取请求参数
        /// </summary>
        public NameValueCollection Params => _paramsCache ??= ParseParams();

        /// <summary>
        /// 获取指定参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns>参数值</returns>
        public string GetParam(string name)
        {
            return Params?.Get(name);
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <param name="headerNames">自定义头列表</param>
        /// <returns>客户端IP</returns>
        public string GetClientIP(params string[] headerNames)
        {
            string[] headers = { "X-Forwarded-For", "X-Real-IP", "Proxy-Client-IP", "WL-Proxy-Client-IP", "HTTP_CLIENT_IP", "HTTP_X_FORWARDED_FOR" };

            if (headerNames != null && headerNames.Length > 0)
            {
                var allHeaders = new System.Collections.Generic.List<string>(headerNames);
                allHeaders.AddRange(headers);
                headers = allHeaders.ToArray();
            }

            foreach (var header in headers)
            {
                var ip = GetHeader(header);
                if (!string.IsNullOrEmpty(ip) && ip != "unknown")
                {
                    if (ip.Contains(","))
                    {
                        ip = ip.Split(',')[0].Trim();
                    }
                    return ip;
                }
            }

            return RemoteEndPoint?.Address?.ToString();
        }

        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <returns>Cookie值</returns>
        public string GetCookie(string name)
        {
            return _request.Cookies[name]?.Value;
        }

        /// <summary>
        /// 获取所有Cookie
        /// </summary>
        public CookieCollection Cookies => _request.Cookies;

        /// <summary>
        /// 是否为GET请求
        /// </summary>
        public bool IsGetMethod => "GET".Equals(Method, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 是否为POST请求
        /// </summary>
        public bool IsPostMethod => "POST".Equals(Method, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 是否使用HTTPS
        /// </summary>
        public bool IsSecureConnection => _request.IsSecureConnection;

        /// <summary>
        /// 解析请求参数
        /// </summary>
        private NameValueCollection ParseParams()
        {
            var result = new NameValueCollection();

            // URL参数
            var query = _request.QueryString;
            if (query != null && query.Count > 0)
            {
                foreach (string key in query.AllKeys)
                {
                    result.Add(key, query[key]);
                }
            }

            // Body参数
            var contentType = ContentType;
            if (!string.IsNullOrEmpty(contentType) && contentType.Contains("application/x-www-form-urlencoded"))
            {
                var body = Body;
                if (!string.IsNullOrEmpty(body))
                {
                    var parts = body.Split('&');
                    foreach (var part in parts)
                    {
                        var kv = part.Split('=');
                        var key = kv.Length > 0 ? System.Web.HttpUtility.UrlDecode(kv[0]) : "";
                        var value = kv.Length > 1 ? System.Web.HttpUtility.UrlDecode(kv[1]) : "";
                        result.Add(key, value);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 从Content-Type提取编码
        /// </summary>
        private static Encoding GetCharsetFromContentType(string contentType)
        {
            var parts = contentType.Split(';');
            foreach (var part in parts)
            {
                var trimmed = part.Trim();
                if (trimmed.StartsWith("charset=", StringComparison.OrdinalIgnoreCase))
                {
                    var charset = trimmed.Substring(8).Trim('"', ' ');
                    try
                    {
                        return Encoding.GetEncoding(charset);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
