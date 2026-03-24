using System.Net;
using System.Text;

namespace WellTool.Http;

/// <summary>
/// HTTP 请求�?
/// </summary>
public class HttpRequest : HttpBase<HttpRequest>
{
    private HttpConfig _config = HttpConfig.Create();
    private string _url;
    private Method _httpMethod = Method.GET;
    private HttpClientHandler? _handler;
    private Dictionary<string, object?>? _form;
    private bool _isRest;
    private int _redirectCount;

    #region Static Methods

    /// <summary>
    /// POST 请求
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest Post(string url)
    {
        return Of(url).SetMethod(Method.POST);
    }

    /// <summary>
    /// GET 请求
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest Get(string url)
    {
        return Of(url).SetMethod(Method.GET);
    }

    /// <summary>
    /// HEAD 请求
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest Head(string url)
    {
        return Of(url).SetMethod(Method.HEAD);
    }

    /// <summary>
    /// OPTIONS 请求
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest Options(string url)
    {
        return Of(url).SetMethod(Method.OPTIONS);
    }

    /// <summary>
    /// PUT 请求
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest Put(string url)
    {
        return Of(url).SetMethod(Method.PUT);
    }

    /// <summary>
    /// DELETE 请求
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest Delete(string url)
    {
        return Of(url).SetMethod(Method.DELETE);
    }

    /// <summary>
    /// PATCH 请求
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest Patch(string url)
    {
        return Of(url).SetMethod(Method.PATCH);
    }

    /// <summary>
    /// 构建一�?HTTP 请求
    /// </summary>
    /// <param name="url">URL 链接</param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest Of(string url)
    {
        return new HttpRequest(url);
    }

    /// <summary>
    /// 设置全局默认的连接和读取超时时长
    /// </summary>
    /// <param name="customTimeout">超时时长</param>
    public static void SetGlobalTimeout(int customTimeout)
    {
        HttpGlobalConfig.SetTimeout(customTimeout);
    }

    #endregion

    /// <summary>
    /// 构�?
    /// </summary>
    /// <param name="url">URL</param>
    public HttpRequest(string url)
    {
        _url = url;
    }

    /// <summary>
    /// 获取请求 URL
    /// </summary>
    /// <returns>URL 字符�?/returns>
    public string GetUrl()
    {
        return _url;
    }

    /// <summary>
    /// 设置 URL
    /// </summary>
    /// <param name="url">url 字符�?/param>
    /// <returns>this</returns>
    public HttpRequest SetUrl(string url)
    {
        _url = url;
        return this;
    }

    /// <summary>
    /// 获取 HTTP 请求方法
    /// </summary>
    /// <returns>Method</returns>
    public Method GetMethod()
    {
        return _httpMethod;
    }

    /// <summary>
    /// 设置请求方法
    /// </summary>
    /// <param name="method">HTTP 方法</param>
    /// <returns>HttpRequest</returns>
    public HttpRequest SetMethod(Method httpMethod)
    {
        _httpMethod = httpMethod; return this;
    }


    #region Form

    /// <summary>
    /// 设置表单数据
    /// </summary>
    /// <param name="name">�?/param>
    /// <param name="value">�?/param>
    /// <returns>this</returns>
    public HttpRequest Form(string name, object? value)
    {
        if (string.IsNullOrWhiteSpace(name) || value == null)
        {
            return this;
        }

        // 停用 body
        BodyContent = null;

        if (value is IEnumerable<object> enumerable)
        {
            // 列表对象
            var strValue = string.Join(",", enumerable.Select(x => x?.ToString()));
            PutToForm(name, strValue);
        }
        else if (value.GetType().IsArray)
        {
            // 数组对象
            var array = (Array)value;
            var strValue = string.Join(",", array.Cast<object>().Select(x => x?.ToString()));
            PutToForm(name, strValue);
        }
        else
        {
            // 其他对象一律转换为字符�?
            var strValue = value.ToString();
            PutToForm(name, strValue);
        }

        return this;
    }

    /// <summary>
    /// 设置 map 类型表单数据
    /// </summary>
    /// <param name="formMap">表单内容</param>
    /// <returns>this</returns>
    public HttpRequest Form(IDictionary<string, object?> formMap)
    {
        if (formMap != null && formMap.Count > 0)
        {
            foreach (var kvp in formMap)
            {
                Form(kvp.Key, kvp.Value);
            }
        }
        return this;
    }

    /// <summary>
    /// 获取表单数据
    /// </summary>
    /// <returns>表单 Map</returns>
    public IDictionary<string, object?>? Form()
    {
        return _form;
    }

    #endregion

    #region Body

    /// <summary>
    /// 设置内容主体
    /// </summary>
    /// <param name="body">请求�?/param>
    /// <returns>this</returns>
    public HttpRequest Body(string body)
    {
        return Body(body, null);
    }

    /// <summary>
    /// 设置内容主体
    /// </summary>
    /// <param name="body">请求�?/param>
    /// <param name="contentType">请求体类�?/param>
    /// <returns>this</returns>
    public HttpRequest Body(string body, string? contentType)
    {
        BodyContent = body;
        _form = null; // 当使�?body 时，停止 form 的使�?

        if (!string.IsNullOrEmpty(contentType))
        {
            // Content-Type 自定义设�?
            SetContentType(contentType);
        }
        else
        {
            // 在用户未自定义情况下自动根据内容判断
            contentType = GetContentTypeByRequestBody(body);
            if (!string.IsNullOrEmpty(contentType) && ContentType.IsDefault(GetHeader(Header.CONTENT_TYPE)))
            {
                if (Charset != null)
                {
                    // 附加编码信息
                    contentType = ContentType.Build(contentType, Charset);
                }
                SetContentType(contentType);
            }
        }

        // 判断是否�?rest 请求
        if (!string.IsNullOrEmpty(contentType) &&
            contentType.Contains("json", StringComparison.OrdinalIgnoreCase) ||
            contentType.Contains("xml", StringComparison.OrdinalIgnoreCase))
        {
            _isRest = true;
            ContentLength(Encoding.UTF8.GetByteCount(body));
        }

        return this;
    }

    /// <summary>
    /// 设置主体字节�?
    /// </summary>
    /// <param name="bodyBytes">主体</param>
    /// <returns>this</returns>
    public HttpRequest Body(byte[] bodyBytes)
    {
        if (bodyBytes != null && bodyBytes.Length > 0)
        {
            BodyContent = Encoding.UTF8.GetString(bodyBytes);
        }
        return this;
    }

    #endregion

    #region Config

    /// <summary>
    /// 将新的配置加�?
    /// </summary>
    /// <param name="config">配置</param>
    /// <returns>this</returns>
    public HttpRequest SetConfig(HttpConfig config)
    {
        _config = config;
        return this;
    }

    /// <summary>
    /// 设置超时，单位：毫秒
    /// </summary>
    /// <param name="milliseconds">超时毫秒�?/param>
    /// <returns>this</returns>
    public HttpRequest Timeout(int milliseconds)
    {
        _config.Timeout(milliseconds);
        return this;
    }

    /// <summary>
    /// 设置连接超时，单位：毫秒
    /// </summary>
    /// <param name="milliseconds">超时毫秒�?/param>
    /// <returns>this</returns>
    public HttpRequest SetConnectionTimeout(int milliseconds)
    {
        _config.SetConnectionTimeout(milliseconds);
        return this;
    }

    /// <summary>
    /// 设置读取超时，单位：毫秒
    /// </summary>
    /// <param name="milliseconds">超时毫秒�?/param>
    /// <returns>this</returns>
    public HttpRequest SetReadTimeout(int milliseconds)
    {
        _config.SetReadTimeout(milliseconds);
        return this;
    }

    /// <summary>
    /// 禁用缓存
    /// </summary>
    /// <returns>this</returns>
    public HttpRequest DisableCache()
    {
        _config.DisableCache();
        return this;
    }

    /// <summary>
    /// 设置是否打开重定�?
    /// </summary>
    /// <param name="isFollowRedirects">是否打开重定�?/param>
    /// <returns>this</returns>
    public HttpRequest SetFollowRedirects(bool isFollowRedirects)
    {
        if (isFollowRedirects)
        {
            if (_config.MaxRedirectCount <= 0)
            {
                // 默认两次跳转
                return SetMaxRedirectCount(2);
            }
        }
        else
        {
            // 手动强制关闭重定�?
            if (_config.MaxRedirectCount < 0)
            {
                return SetMaxRedirectCount(0);
            }
        }
        return this;
    }

    /// <summary>
    /// 设置最大重定向次数
    /// </summary>
    /// <param name="maxRedirectCount">最大重定向次数</param>
    /// <returns>this</returns>
    public HttpRequest SetMaxRedirectCount(int maxRedirectCount)
    {
        _config.SetMaxRedirectCount(maxRedirectCount);
        return this;
    }

    /// <summary>
    /// 设置域名验证�?
    /// </summary>
    /// <param name="hostnameVerifier">HostnameVerifier</param>
    /// <returns>this</returns>
    public HttpRequest SetHostnameVerifier(Func<string, string, bool> hostnameVerifier)
    {
        _config.SetHostnameVerifier(hostnameVerifier);
        return this;
    }

    /// <summary>
    /// 设置 HTTP 代理
    /// </summary>
    /// <param name="host">代理主机</param>
    /// <param name="port">代理端口</param>
    /// <returns>this</returns>
    public HttpRequest SetHttpProxy(string host, int port)
    {
        _config.SetHttpProxy(host, port);
        return this;
    }

    /// <summary>
    /// 设置代理
    /// </summary>
    /// <param name="proxy">代理</param>
    /// <returns>this</returns>
    public HttpRequest SetProxy(IWebProxy proxy)
    {
        _config.SetProxy(proxy);
        return this;
    }

    /// <summary>
    /// 采用流方式上传数�?
    /// </summary>
    /// <param name="blockSize">块大小（bytes 数）</param>
    /// <returns>this</returns>
    public HttpRequest SetChunkedStreamingMode(int blockSize)
    {
        _config.SetBlockSize(blockSize);
        return this;
    }

    #endregion

    #region Auth

    /// <summary>
    /// Basic 认证
    /// </summary>
    /// <param name="username">用户�?/param>
    /// <param name="password">密码</param>
    /// <returns>this</returns>
    public HttpRequest BasicAuth(string username, string password)
    {
        return Auth(HttpUtil.BuildBasicAuth(username, password, Charset));
    }

    /// <summary>
    /// Bearer Token 认证
    /// </summary>
    /// <param name="token">令牌内容</param>
    /// <returns>this</returns>
    public HttpRequest BearerAuth(string token)
    {
        return Auth($"Bearer {token}");
    }

    /// <summary>
    /// 认证，简单插�?Authorization �?
    /// </summary>
    /// <param name="content">认证内容</param>
    /// <returns>HttpRequest</returns>
    public HttpRequest Auth(string content)
    {
        SetHeader(Header.AUTHORIZATION, content, true);
        return this;
    }

    #endregion

    #region Execute

    /// <summary>
    /// 执行 Request 请求
    /// </summary>
    /// <returns>HttpResponse</returns>
    public async Task<HttpResponse> ExecuteAsync()
    {
        return await DoExecuteAsync(false);
    }

    /// <summary>
    /// 执行 Request 请求（同步）
    /// </summary>
    /// <returns>HttpResponse</returns>
    public HttpResponse Execute()
    {
        return ExecuteAsync().GetAwaiter().GetResult();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// 执行请求
    /// </summary>
    /// <param name="isAsync">是否异步</param>
    /// <returns>HttpResponse</returns>
    private async Task<HttpResponse> DoExecuteAsync(bool isAsync)
    {
        // 初始�?URL
        UrlWithParamIfGet();

        // 创建 HttpClientHandler
        _handler = new HttpClientHandler
        {
            UseCookies = false,
            AllowAutoRedirect = _config.MaxRedirectCount > 0
        };

        // 设置代理
        if (_config.Proxy != null)
        {
            _handler.Proxy = _config.Proxy;
            _handler.UseProxy = true;
        }

        // 设置域名验证�?
        if (_config.HostnameVerifier != null)
        {
            _handler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => _config.HostnameVerifier(cert?.Subject ?? "", cert?.Issuer ?? "");
        }

        // 创建 HttpClient
        using var client = new HttpClient(_handler)
        {
            Timeout = TimeSpan.FromMilliseconds(Math.Max(_config.ConnectionTimeout, _config.ReadTimeout))
        };

        // 创建请求消息
        var requestMessage = CreateRequestMessage();

        // 发送请�?
        var responseMessage = await client.SendAsync(requestMessage,
            isAsync ? HttpCompletionOption.ResponseHeadersRead : HttpCompletionOption.ResponseContentRead);

        // 处理响应
        var httpResponse = new HttpResponse(responseMessage, _config, Charset, isAsync);

        return httpResponse;
    }

    /// <summary>
    /// 创建请求消息
    /// </summary>
    /// <returns>HttpRequestMessage</returns>
    private HttpRequestMessage CreateRequestMessage()
    {
        var httpMethod = GetHttpMethod(_httpMethod);
        var requestMessage = new HttpRequestMessage(httpMethod, _url);

        // 添加请求�?
        foreach (var header in Headers)
        {
            if (header.Value != null && header.Value.Count > 0)
            {
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        // 设置请求内容
        if (!string.IsNullOrEmpty(BodyContent) || (_form != null && _form.Count > 0))
        {
            var content = CreateContent();
            if (content != null)
            {
                requestMessage.Content = content;
            }
        }

        return requestMessage;
    }

    /// <summary>
    /// 创建请求内容
    /// </summary>
    /// <returns>HttpContent</returns>
    private HttpContent? CreateContent()
    {
        if (!string.IsNullOrEmpty(BodyContent))
        {
            return new StringContent(BodyContent!, Encoding.UTF8, GetHeader(Header.CONTENT_TYPE) ?? "application/json");
        }

        if (_form != null && _form.Count > 0)
        {
            var formContent = new FormUrlEncodedContent(
                _form.Where(f => f.Value != null)
                    .ToDictionary(f => f.Key, f => f.Value?.ToString() ?? string.Empty)
            );
            return formContent;
        }

        return null;
    }

    /// <summary>
    /// 对于 GET 请求将参数加�?URL �?
    /// </summary>
    private void UrlWithParamIfGet()
    {
        if (_httpMethod == Method.GET && !_isRest && _redirectCount <= 0)
        {
            if (_form != null && _form.Count > 0)
            {
                var queryString = HttpUtil.ToParams(_form, Charset);
                if (!string.IsNullOrEmpty(queryString))
                {
                    var separator = _url.Contains('?') ? "&" : "?";
                    _url = $"{_url}{separator}{queryString}";
                }
            }
        }
    }

    /// <summary>
    /// 获取 HTTP 方法
    /// </summary>
    /// <param name="method">Method 枚举</param>
    /// <returns>HttpMethod</returns>
    private static HttpMethod GetHttpMethod(Method method)
    {
        return method switch
        {
            Method.GET => HttpMethod.Get,
            Method.POST => HttpMethod.Post,
            Method.PUT => HttpMethod.Put,
            Method.DELETE => HttpMethod.Delete,
            Method.HEAD => HttpMethod.Head,
            Method.OPTIONS => HttpMethod.Options,
            Method.TRACE => HttpMethod.Trace,
            Method.CONNECT => HttpMethod.Options,
            Method.PATCH => HttpMethod.Patch,
            _ => HttpMethod.Get
        };
    }

    /// <summary>
    /// 将参数加入到 form �?
    /// </summary>
    /// <param name="name">表单属性名</param>
    /// <param name="value">属性�?/param>
    /// <returns>this</returns>
    private HttpRequest PutToForm(string name, string? value)
    {
        if (string.IsNullOrEmpty(name) || value == null)
        {
            return this;
        }

        if (_form == null)
        {
            _form = new Dictionary<string, object?>();
        }

        _form[name] = value;
        return this;
    }

    /// <summary>
    /// 从请求参数的 body 中判断请求的 Content-Type 类型
    /// </summary>
    /// <param name="body">请求参数�?/param>
    /// <returns>Content-Type 类型</returns>
    private static string? GetContentTypeByRequestBody(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return null;
        }

        var trimmedBody = body.Trim();

        // JSON 判断
        if ((trimmedBody.StartsWith('{') && trimmedBody.EndsWith('}')) ||
            (trimmedBody.StartsWith('[') && trimmedBody.EndsWith(']')))
        {
            return "application/json";
        }

        // XML 判断
        if (trimmedBody.StartsWith('<'))
        {
            return "application/xml";
        }

        return null;
    }

    #endregion
}











