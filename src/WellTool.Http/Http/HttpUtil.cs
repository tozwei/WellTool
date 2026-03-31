using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WellTool.Http;

/// <summary>
/// HTTP 请求工具类
/// </summary>
public static partial class HttpUtil
{
   /// <summary>
    /// Content-Type 中的编码信息正则
    /// </summary>
    private static readonly Regex CharsetPattern = new(@"charset\s*=\s*([a-z0-9-]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// 检测是否为 HTTPS
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>是否 HTTPS</returns>
    public static bool IsHttps(string url)
    {
        return url.StartsWith("https:", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 检测是否为 HTTP
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>是否 HTTP</returns>
    public static bool IsHttp(string url)
    {
        return url.StartsWith("http:", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 创建 HTTP 请求对象
    /// </summary>
    /// <param name="method">方法枚举</param>
    /// <param name="url">请求�?URL</param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest CreateRequest(Method method, string url)
    {
        return HttpRequest.Of(url).SetMethod(method);
    }

    /// <summary>
    /// 创建 HTTP GET 请求对象
    /// </summary>
    /// <param name="url">请求�?URL</param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest CreateGet(string url)
    {
        return CreateGet(url, false);
    }

    /// <summary>
    /// 创建 HTTP GET 请求对象
    /// </summary>
    /// <param name="url">请求�?URL</param>
    /// <param name="isFollowRedirects">是否打开重定�?/param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest CreateGet(string url, bool isFollowRedirects)
    {
        return HttpRequest.Get(url).SetFollowRedirects(isFollowRedirects);
    }

    /// <summary>
    /// 创建 HTTP POST 请求对象
    /// </summary>
    /// <param name="url">请求�?URL</param>
    /// <returns>HttpRequest</returns>
    public static HttpRequest CreatePost(string url)
    {
        return HttpRequest.Post(url);
    }

    /// <summary>
    /// 发�?GET 请求
    /// </summary>
    /// <param name="urlString">网址</param>
    /// <param name="customCharset">自定义请求字符集</param>
    /// <returns>返回内容</returns>
    public static string? Get(string urlString, Encoding? customCharset = null)
    {
        return HttpRequest.Get(urlString).SetCharset(customCharset).Execute().Body();
    }

    /// <summary>
    /// 发�?GET 请求
    /// </summary>
    /// <param name="urlString">网址</param>
    /// <param name="timeout">超时时长（毫秒）</param>
    /// <returns>返回内容</returns>
    public static string? Get(string urlString, int timeout)
    {
        return HttpRequest.Get(urlString).Timeout(timeout).Execute().Body();
    }

    /// <summary>
    /// 发�?GET 请求
    /// </summary>
    /// <param name="urlString">网址</param>
    /// <param name="paramMap">GET 表单数据</param>
    /// <returns>返回数据</returns>
    public static string? Get(string urlString, Dictionary<string, object?> paramMap)
    {
        return HttpRequest.Get(urlString).Form(paramMap).Execute().Body();
    }

    /// <summary>
    /// 发�?GET 请求
    /// </summary>
    /// <param name="urlString">网址</param>
    /// <param name="paramMap">GET 表单数据</param>
    /// <param name="timeout">超时时长（毫秒）</param>
    /// <returns>返回数据</returns>
    public static string? Get(string urlString, Dictionary<string, object?> paramMap, int timeout)
    {
        return HttpRequest.Get(urlString).Form(paramMap).Timeout(timeout).Execute().Body();
    }

    /// <summary>
    /// 发�?POST 请求
    /// </summary>
    /// <param name="urlString">网址</param>
    /// <param name="paramMap">POST 表单数据</param>
    /// <returns>返回数据</returns>
    public static string? Post(string urlString, Dictionary<string, object?> paramMap)
    {
        return Post(urlString, paramMap, HttpGlobalConfig.Timeout);
    }

    /// <summary>
    /// 发�?POST 请求
    /// </summary>
    /// <param name="urlString">网址</param>
    /// <param name="paramMap">POST 表单数据</param>
    /// <param name="timeout">超时时长（毫秒）</param>
    /// <returns>返回数据</returns>
    public static string? Post(string urlString, Dictionary<string, object?> paramMap, int timeout)
    {
        return HttpRequest.Post(urlString).Form(paramMap).Timeout(timeout).Execute().Body();
    }

    /// <summary>
    /// 发�?POST 请求
    /// </summary>
    /// <param name="urlString">网址</param>
    /// <param name="body">POST 请求�?/param>
    /// <returns>返回数据</returns>
    public static string? Post(string urlString, string body)
    {
        return Post(urlString, body, HttpGlobalConfig.Timeout);
    }

    /// <summary>
    /// 发�?POST 请求
    /// </summary>
    /// <param name="urlString">网址</param>
    /// <param name="body">POST 请求�?/param>
    /// <param name="timeout">超时时长（毫秒）</param>
    /// <returns>返回数据</returns>
    public static string? Post(string urlString, string body, int timeout)
    {
        return HttpRequest.Post(urlString).Timeout(timeout).Body(body).Execute().Body();
    }

    /// <summary>
    /// 下载远程文本
    /// </summary>
    /// <param name="url">请求�?url</param>
    /// <param name="customCharset">自定义的字符�?/param>
    /// <returns>文本</returns>
    public static string? DownloadString(string url, Encoding? customCharset = null)
    {
        var request = HttpRequest.Get(url);
        if (customCharset != null)
        {
            request.SetCharset(customCharset);
        }
        return request.Execute().Body();
    }

    /// <summary>
    /// 下载远程文件
    /// </summary>
    /// <param name="url">请求�?url</param>
    /// <param name="destFile">目标文件路径</param>
    /// <returns>文件大小</returns>
    public static long DownloadFile(string url, string destFile)
    {
        return HttpRequest.Get(url).Execute().WriteBody(destFile);
    }

    /// <summary>
    /// 下载远程文件
    /// </summary>
    /// <param name="url">请求�?url</param>
    /// <param name="destFile">目标文件路径</param>
    /// <param name="timeout">超时（毫秒）</param>
    /// <returns>文件大小</returns>
    public static long DownloadFile(string url, string destFile, int timeout)
    {
        return HttpRequest.Get(url).Timeout(timeout).Execute().WriteBody(destFile);
    }

    /// <summary>
    /// 下载字节数据
    /// </summary>
    /// <param name="url">请求�?url</param>
    /// <returns>字节数据</returns>
    public static byte[] DownloadBytes(string url)
    {
        return HttpRequest.Get(url).Execute().BodyBytes() ?? Array.Empty<byte>();
    }

    /// <summary>
    /// �?Map 形式�?Form 表单数据转换�?Url 参数形式
    /// </summary>
    /// <param name="paramMap">表单数据</param>
    /// <returns>url 参数</returns>
    public static string ToParams(IDictionary<string, object?> paramMap)
    {
        return ToParams(paramMap, Encoding.UTF8);
    }

    /// <summary>
    /// �?Map 形式�?Form 表单数据转换�?Url 参数形式
    /// </summary>
    /// <param name="paramMap">表单数据</param>
    /// <param name="charset">编码</param>
    /// <returns>url 参数</returns>
    public static string ToParams(IDictionary<string, object?> paramMap, Encoding? charset)
    {
        return ToParams(paramMap, charset, false);
    }

    /// <summary>
    /// �?Map 形式�?Form 表单数据转换�?Url 参数形式
    /// </summary>
    /// <param name="paramMap">表单数据</param>
    /// <param name="charset">编码</param>
    /// <param name="isFormUrlEncoded">是否�?x-www-form-urlencoded 模式</param>
    /// <returns>url 参数</returns>
    public static string ToParams(IDictionary<string, object?> paramMap, Encoding? charset, bool isFormUrlEncoded)
    {
        if (paramMap == null || paramMap.Count == 0)
        {
            return string.Empty;
        }

        var encoding = charset ?? Encoding.UTF8;
        var parts = new List<string>();

        foreach (var kvp in paramMap)
        {
            if (string.IsNullOrEmpty(kvp.Key))
            {
                continue;
            }

            var value = kvp.Value?.ToString() ?? string.Empty;
            var encodedKey = UrlEncode(kvp.Key, encoding);
            var encodedValue = UrlEncode(value, encoding);

            if (isFormUrlEncoded)
            {
                // x-www-form-urlencoded 模式下空格编码为 '+'
                encodedKey = encodedKey.Replace("%20", "+");
                encodedValue = encodedValue.Replace("%20", "+");
            }

            parts.Add($"{encodedKey}={encodedValue}");
        }

        return string.Join("&", parts);
    }

    /// <summary>
    /// �?URL 参数做编�?
    /// </summary>
    /// <param name="urlWithParams">url 和参�?/param>
    /// <param name="charset">编码</param>
    /// <returns>编码后的 url 和参�?/returns>
    public static string EncodeParams(string urlWithParams, Encoding? charset)
    {
        if (string.IsNullOrWhiteSpace(urlWithParams))
        {
            return string.Empty;
        }

        var encoding = charset ?? Encoding.UTF8;
        var questionMarkIndex = urlWithParams.IndexOf('?');

        if (questionMarkIndex <= 0 && !urlWithParams.Contains('='))
        {
            return urlWithParams;
        }

        if (questionMarkIndex > 0)
        {
            var urlPart = urlWithParams.Substring(0, questionMarkIndex);
            var paramPart = urlWithParams.Substring(questionMarkIndex + 1);

            if (string.IsNullOrWhiteSpace(paramPart))
            {
                return urlPart;
            }

            paramPart = NormalizeParams(paramPart, encoding);
            return $"{urlPart}?{paramPart}";
        }

        // 没有？的情况，直接处理参数
        return NormalizeParams(urlWithParams, encoding);
    }

    /// <summary>
    /// 标准化参数字符串
    /// </summary>
    /// <param name="paramPart">参数字符�?/param>
    /// <param name="charset">编码</param>
    /// <returns>标准化的参数字符�?/returns>
    public static string NormalizeParams(string paramPart, Encoding? charset)
    {
        if (string.IsNullOrEmpty(paramPart))
        {
            return paramPart;
        }

        // 去除开头的？
        paramPart = paramPart.TrimStart('?');

        // 去除结尾的&
        paramPart = paramPart.TrimEnd('&');

        if (string.IsNullOrEmpty(paramPart))
        {
            return string.Empty;
        }

        var encoding = charset ?? Encoding.UTF8;
        var builder = new StringBuilder(paramPart.Length + 16);
        var len = paramPart.Length;
        string? name = null;
        var pos = 0;

        for (var i = 0; i < len; i++)
        {
            var c = paramPart[i];
            if (c == '=')
            {
                if (name == null)
                {
                    name = pos == i ? string.Empty : paramPart.Substring(pos, i - pos);
                    pos = i + 1;
                }
            }
            else if (c == '&')
            {
                if (name == null)
                {
                    if (pos != i)
                    {
                        name = paramPart.Substring(pos, i - pos);
                        builder.Append(UrlEncode(name, encoding)).Append('=');
                    }
                }
                else
                {
                    builder.Append(UrlEncode(name, encoding)).Append('=')
                        .Append(UrlEncode(paramPart.Substring(pos, i - pos), encoding)).Append('&');
                }
                name = null;
                pos = i + 1;
            }
        }

        // 结尾处理
        if (name != null)
        {
            builder.Append(UrlEncode(name, encoding)).Append('=');
        }
        if (pos != len)
        {
            if (name == null && pos > 0)
            {
                // 单独的字符串（如&d）作为 key，value 为空
                builder.Append(UrlEncode(paramPart.Substring(pos), encoding)).Append('=');
            }
            else if (name != null)
            {
                builder.Append(UrlEncode(paramPart.Substring(pos), encoding));
            }
        }

        // �?结尾则去除之
        if (builder.Length > 0 && builder[^1] == '&')
        {
            builder.Length--;
        }

        return builder.ToString();
    }

    /// <summary>
    /// �?URL 参数解析�?Map
    /// </summary>
    /// <param name="paramsStr">参数字符�?/param>
    /// <param name="charset">字符�?/param>
    /// <returns>参数 Map</returns>
    public static Dictionary<string, string> DecodeParamMap(string paramsStr, Encoding? charset = null)
    {
        var result = new Dictionary<string, string>();
        if (string.IsNullOrWhiteSpace(paramsStr))
        {
            return result;
        }

        var encoding = charset ?? Encoding.UTF8;

        // 去除开头的？
        var cleanParamsStr = paramsStr.TrimStart('?');
        if (string.IsNullOrEmpty(cleanParamsStr))
        {
            return result;
        }

        var pairs = cleanParamsStr.Split('&', StringSplitOptions.RemoveEmptyEntries);

        foreach (var pair in pairs)
        {
            var equalIndex = pair.IndexOf('=');
            if (equalIndex > 0)
            {
                var key = UrlDecode(pair.Substring(0, equalIndex), encoding);
                var value = equalIndex < pair.Length - 1
                    ? UrlDecode(pair.Substring(equalIndex + 1), encoding)
                    : string.Empty;

                if (!result.ContainsKey(key))
                {
                    result[key] = value;
                }
            }
            else if (equalIndex == 0)
            {
                // =开头的情况
                result[string.Empty] = equalIndex < pair.Length - 1
                    ? UrlDecode(pair.Substring(1), encoding)
                    : string.Empty;
            }
            else
            {
                // 没有=的情况
                var decodedPair = UrlDecode(pair, encoding);
                if (!string.IsNullOrEmpty(decodedPair) && !result.ContainsKey(decodedPair))
                {
                    result[decodedPair] = string.Empty;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// �?Content-Type 中获得字符集
    /// </summary>
    /// <param name="contentType">Content-Type</param>
    /// <returns>字符�?/returns>
    public static string? GetCharset(string? contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
        {
            return null;
        }

        var match = CharsetPattern.Match(contentType);
        return match.Success ? match.Groups[1].Value : null;
    }

    /// <summary>
    /// 根据文件扩展名获�?MimeType
    /// </summary>
    /// <param name="filePath">文件路径或文件名</param>
    /// <param name="defaultValue">当获�?MimeType �?null 时的默认�?/param>
    /// <returns>MimeType</returns>
    public static string? GetMimeType(string filePath, string? defaultValue = null)
    {
        var extension = Path.GetExtension(filePath)?.ToLowerInvariant();
        return extension switch
        {
            ".txt" => "text/plain",
            ".html" or ".htm" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            ".json" => "application/json",
            ".xml" => "application/xml",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".svg" => "image/svg+xml",
            ".pdf" => "application/pdf",
            ".zip" => "application/zip",
            ".rar" => "application/x-rar-compressed",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".ppt" => "application/vnd.ms-powerpoint",
            ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            _ => defaultValue
        };
    }

    /// <summary>
    /// 构建 Basic Auth 验证信息
    /// </summary>
    /// <param name="username">账号</param>
    /// <param name="password">密码</param>
    /// <param name="charset">编码</param>
    /// <returns>密码验证信息</returns>
    public static string BuildBasicAuth(string username, string password, Encoding? charset = null)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentNullException(nameof(username));
        }
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        var encoding = charset ?? Encoding.UTF8;
        var data = $"{username}:{password}";
        var bytes = encoding.GetBytes(data);
        var base64 = Convert.ToBase64String(bytes);
        return $"Basic {base64}";
    }

    /// <summary>
    /// URL 编码
    /// </summary>
    /// <param name="value">要编码的�?/param>
    /// <param name="encoding">编码</param>
    /// <returns>编码后的�?/returns>
    internal static string UrlEncode(string value, Encoding encoding)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        // 使用 HttpUtility 进行 URL 编码
        var encoded = HttpUtility.UrlEncode(value, encoding)
            ?.Replace("+", "%20") ?? string.Empty; // 将 + 替换为%20，保持标准 URL 编码

        // 将小写十六进制转换为大写
        return System.Text.RegularExpressions.Regex.Replace(encoded, @"%[0-9a-fA-F]{2}", m => m.Value.ToUpper());
    }

    /// <summary>
    /// URL 解码
    /// </summary>
    /// <param name="value">要解码的�?/param>
    /// <param name="encoding">编码</param>
    /// <returns>解码后的�?/returns>
    internal static string UrlDecode(string value, Encoding encoding)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        return HttpUtility.UrlDecode(value, encoding) ?? string.Empty;
    }


    /// <summary>
    /// 对 URL 参数进行编码
    /// </summary>
    public static string ENCODE_PARAMS(string urlWithParams, Encoding? charset)
    {
        return EncodeParams(urlWithParams, charset);
    }

    /// <summary>
    /// 将 URL 参数解析为 Map
    /// </summary>
    public static Dictionary<string, List<string>> DECODE_PARAMS(string paramsStr, Encoding? charset)
    {
        var simpleMap = DecodeParamMap(paramsStr, charset);
        var listMap = new Dictionary<string, List<string>>();
        foreach (var kvp in simpleMap)
        {
            listMap[kvp.Key] = new List<string> { kvp.Value };
        }
        return listMap;
    }

    /// <summary>
    /// 将 URL 参数解析为 Map
    /// </summary>
    public static Dictionary<string, List<string>> DECODE_PARAM_MAP(string paramsStr, Encoding? charset)
    {
        var simpleMap = DecodeParamMap(paramsStr, charset);
        var listMap = new Dictionary<string, List<string>>();
        foreach (var kvp in simpleMap)
        {
            listMap[kvp.Key] = new List<string> { kvp.Value };
        }
        return listMap;
    }

    /// <summary>
    /// 将表单数据加到 URL 中
    /// </summary>
    public static string UrlWithForm(string url, IDictionary<string, object?> form, Encoding? charset, bool isEncodeParams)
    {
        var queryString = ToParams(form, charset, isEncodeParams);
        return UrlWithForm(url, queryString, charset, isEncodeParams);
    }

    /// <summary>
    /// 将查询字符串加到 URL 中
    /// </summary>
    public static string UrlWithForm(string url, string queryString, Encoding? charset, bool isEncode)
    {
        if (string.IsNullOrWhiteSpace(queryString))
        {
            if (url.Contains("?"))
            {
                return isEncode ? EncodeParams(url, charset) : url;
            }
            return url;
        }

        var urlBuilder = new StringBuilder(url.Length + queryString.Length + 16);
        var qmIndex = url.IndexOf('?');

        if (qmIndex > 0)
        {
            urlBuilder.Append(isEncode ? EncodeParams(url, charset) : url);
            if (!url.EndsWith("&"))
            {
                urlBuilder.Append('&');
            }
        }
        else
        {
            urlBuilder.Append(url);
            if (qmIndex < 0)
            {
                urlBuilder.Append('?');
            }
        }

        urlBuilder.Append(isEncode ? EncodeParams(queryString, charset) : queryString);
        return urlBuilder.ToString();
    }
}
