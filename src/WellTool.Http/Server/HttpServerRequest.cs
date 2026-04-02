using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using WellTool.Http.Server.Action;

namespace WellTool.Http.Server
{
    /// <summary>
    /// HTTP服务器请求封装
    /// </summary>
    public class HttpServerRequest
    {
        private readonly HttpListenerRequest _request;
        private Encoding _charset;
        private NameValueCollection _params;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="request">HttpListener请求对象</param>
        public HttpServerRequest(HttpListenerRequest request)
        {
            _request = request;
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
        /// 获取请求头
        /// </summary>
        public NameValueCollection Headers => _request.Headers;

        /// <summary>
        /// 获取查询字符串
        /// </summary>
        public NameValueCollection QueryString => _request.QueryString;

        /// <summary>
        /// 获取请求编码，默认UTF-8
        /// </summary>
        public Encoding Charset
        {
            get
            {
                if (_charset == null)
                {
                    var contentType = ContentType;
                    if (!string.IsNullOrEmpty(contentType))
                    {
                        var charset = GetCharsetFromContentType(contentType);
                        _charset = charset ?? Encoding.UTF8;
                    }
                    else
                    {
                        _charset = Encoding.UTF8;
                    }
                }
                return _charset;
            }
        }

        /// <summary>
        /// 获取Content-Type头
        /// </summary>
        public string ContentType => _request.ContentType;

        /// <summary>
        /// 获取User-Agent头
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
        /// 是否为GET请求
        /// </summary>
        public bool IsGetMethod => "GET".Equals(Method, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 是否为POST请求
        /// </summary>
        public bool IsPostMethod => "POST".Equals(Method, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 获取请求体文本
        /// </summary>
        /// <returns>请求体内容</returns>
        public string GetBody()
        {
            return GetBody(Charset);
        }

        /// <summary>
        /// 获取请求体文本
        /// </summary>
        /// <param name="charset">编码</param>
        /// <returns>请求体内容</returns>
        public string GetBody(Encoding charset)
        {
            using var reader = new StreamReader(_request.InputStream, charset);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 获取请求体字节数组
        /// </summary>
        /// <returns>请求体字节数组</returns>
        public byte[] GetBodyBytes()
        {
            using var ms = new MemoryStream();
            _request.InputStream.CopyTo(ms);
            return ms.ToArray();
        }

        /// <summary>
        /// 获取请求体流
        /// </summary>
        /// <returns>请求体流</returns>
        public Stream GetBodyStream()
        {
            return _request.InputStream;
        }

        /// <summary>
        /// 获取指定名称的参数值（取第一个）
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns>参数值</returns>
        public string GetParam(string name)
        {
            var params2 = GetParams();
            return params2.Get(name);
        }

        /// <summary>
        /// 获取指定名称的所有参数值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns>参数值列表</returns>
        public string[] GetParams(string name)
        {
            var params2 = GetParams();
            return params2.GetValues(name);
        }

        /// <summary>
        /// 获取所有请求参数（包含URL参数和Body参数）
        /// </summary>
        /// <returns>参数字典</returns>
        public NameValueCollection GetParams()
        {
            if (_params != null)
            {
                return _params;
            }

            _params = new NameValueCollection();

            // 解析URL中的参数
            var query = QueryString;
            if (query != null && query.Count > 0)
            {
                foreach (string key in query.AllKeys)
                {
                    _params.Add(key, query[key]);
                }
            }

            // 解析Body中的参数
            var contentType = ContentType;
            if (!string.IsNullOrEmpty(contentType) && contentType.Contains("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
            {
                var body = GetBody();
                if (!string.IsNullOrEmpty(body))
                {
                    var bodyParams = HttpUtility.ParseQueryString(body, Charset);
                    foreach (string key in bodyParams.AllKeys)
                    {
                        _params.Add(key, bodyParams[key]);
                    }
                }
            }

            return _params;
        }

        /// <summary>
        /// 获取请求头中的指定值
        /// </summary>
        /// <param name="headerKey">头名称</param>
        /// <returns>头值</returns>
        public string GetHeader(string headerKey)
        {
            return Headers?.Get(headerKey);
        }

        /// <summary>
        /// 获取请求头中的指定值，并转换编码
        /// </summary>
        /// <param name="headerKey">头名称</param>
        /// <param name="targetCharset">目标编码</param>
        /// <returns>头值</returns>
        public string GetHeader(string headerKey, Encoding targetCharset)
        {
            var header = GetHeader(headerKey);
            if (!string.IsNullOrEmpty(header))
            {
                // 从ISO-8859-1转换到目标编码
                var isoBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(header);
                var targetBytes = Encoding.Convert(Encoding.GetEncoding("ISO-8859-1"), targetCharset, isoBytes);
                return targetCharset.GetString(targetBytes);
            }
            return null;
        }

        /// <summary>
        /// 获取客户端真实IP地址
        /// <para>
        /// 默认检测的Header:
        /// 1、X-Forwarded-For
        /// 2、X-Real-IP
        /// 3、Proxy-Client-IP
        /// 4、WL-Proxy-Client-IP
        /// </para>
        /// </summary>
        /// <param name="otherHeaderNames">其他自定义头</param>
        /// <returns>客户端IP地址</returns>
        public string GetClientIP(params string[] otherHeaderNames)
        {
            string[] headers = { "X-Forwarded-For", "X-Real-IP", "Proxy-Client-IP", "WL-Proxy-Client-IP", "HTTP_CLIENT_IP", "HTTP_X_FORWARDED_FOR" };

            if (otherHeaderNames != null && otherHeaderNames.Length > 0)
            {
                var allHeaders = new List<string>(otherHeaderNames);
                allHeaders.AddRange(headers);
                headers = allHeaders.ToArray();
            }

            return GetClientIPByHeader(headers);
        }

        /// <summary>
        /// 获取客户端真实IP地址
        /// </summary>
        /// <param name="headerNames">自定义头列表</param>
        /// <returns>客户端IP地址</returns>
        public string GetClientIPByHeader(params string[] headerNames)
        {
            string ip = null;

            foreach (var header in headerNames)
            {
                ip = GetHeader(header);
                if (!string.IsNullOrEmpty(ip) && !IsUnknownIP(ip))
                {
                    // 多个IP时取第一个
                    if (ip.Contains(","))
                    {
                        ip = ip.Split(',')[0].Trim();
                    }
                    return ip;
                }
            }

            // 如果没有通过Header获取到IP，返回远程地址
            return RemoteEndPoint?.Address?.ToString();
        }

        /// <summary>
        /// 判断IP是否未知
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns>是否未知</returns>
        private static bool IsUnknownIP(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return true;
            }
            return ip == "unknown" || IPAddress.None.ToString().Equals(ip, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 从Content-Type中提取编码
        /// </summary>
        /// <param name="contentType">Content-Type值</param>
        /// <returns>编码</returns>
        private static Encoding GetCharsetFromContentType(string contentType)
        {
            if (string.IsNullOrEmpty(contentType))
            {
                return null;
            }

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

        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <returns>Cookie值</returns>
        public string GetCookie(string name)
        {
            var cookie = _request.Cookies[name];
            return cookie?.Value;
        }

        /// <summary>
        /// 获取所有Cookie
        /// </summary>
        /// <returns>Cookie集合</returns>
        public CookieCollection Cookies => _request.Cookies;

        /// <summary>
        /// 获取请求是否使用HTTPS
        /// </summary>
        public bool IsSecureConnection => _request.IsSecureConnection;

        /// <summary>
        /// 获取是否来自本地
        /// </summary>
        public bool IsLocal => _request.IsLocal;

        /// <summary>
        /// 获取原始HttpListenerRequest
        /// </summary>
        public HttpListenerRequest RawRequest => _request;
    }
}
