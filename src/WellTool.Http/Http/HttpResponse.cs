using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace WellTool.Http;

/// <summary>
/// HTTP 响应�?
/// </summary>
public class HttpResponse : HttpBase<HttpResponse>, IDisposable
{
    private readonly HttpResponseMessage _responseMessage;
    private readonly HttpConfig _config;
    private readonly bool _isAsync;
    private byte[]? _bodyBytes;
    private bool _disposed;

    /// <summary>
    /// 构�?
    /// </summary>
    /// <param name="responseMessage">HTTP 响应消息</param>
    /// <param name="config">HTTP 配置</param>
    /// <param name="charset">编码</param>
    /// <param name="isAsync">是否异步</param>
    /// <param name="isIgnoreBody">是否忽略读取响应�?/param>
    public HttpResponse(HttpResponseMessage responseMessage, HttpConfig config, Encoding? charset, bool isAsync, bool isIgnoreBody = false)
    {
        _responseMessage = responseMessage;
        _config = config;
        Charset = charset ?? Encoding.UTF8;
        _isAsync = isAsync;

        // 初始化状态码和头信息
        Status = (int)responseMessage.StatusCode;
        InitHeaders();

        // 同步情况下强制同�?
        if (!isAsync && !isIgnoreBody)
        {
            ForceSync();
        }
    }

    /// <summary>
    /// 获取状态码
    /// </summary>
    public int Status { get; private set; }

    /// <summary>
    /// 请求是否成功，判断依据为：状态码范围�?200~299 �?
    /// </summary>
    /// <returns>是否成功请求</returns>
    public bool IsOk()
    {
        return Status >= 200 && Status < 300;
    }

    /// <summary>
    /// 同步
    /// 如果为异步状态，则暂时不读取服务器中响应的内容，而是持有 Http 链接的流
    /// 当调用此方法时，异步状态转为同步状态，此时�?Http 链接流中读取 body 内容并暂存在内容�?
    /// </summary>
    /// <returns>this</returns>
    public HttpResponse Sync()
    {
        return _isAsync ? ForceSync() : this;
    }

    #region Response Headers

    /// <summary>
    /// 获取内容编码
    /// </summary>
    /// <returns>String</returns>
    public string? ContentEncoding()
    {
        return _responseMessage.Content.Headers.ContentEncoding?.FirstOrDefault();
    }

    /// <summary>
    /// 获取内容长度
    /// </summary>
    /// <returns>长度�?1 表示服务端未返回或长度无�?/returns>
    public long ContentLength()
    {
        var contentLength = _responseMessage.Content.Headers.ContentLength ?? -1L;

        if (contentLength > 0 && (IsChunked() || !string.IsNullOrEmpty(ContentEncoding())))
        {
            // 按照 HTTP 协议规范，在 Transfer-Encoding �?Content-Encoding 设置�?Content-Length 无效
            contentLength = -1;
        }

        return contentLength;
    }

    /// <summary>
    /// 是否�?gzip 压缩过的内容
    /// </summary>
    /// <returns>是否�?gzip 压缩过的内容</returns>
    public bool IsGzip()
    {
        var contentEncoding = ContentEncoding();
        return "gzip".Equals(contentEncoding, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 是否�?zlib(Deflate) 压缩过的内容
    /// </summary>
    /// <returns>是否�?zlib(Deflate) 压缩过的内容</returns>
    public bool IsDeflate()
    {
        var contentEncoding = ContentEncoding();
        return "deflate".Equals(contentEncoding, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 是否�?Transfer-Encoding:Chunked 的内�?
    /// </summary>
    /// <returns>是否�?Transfer-Encoding:Chunked 的内�?/returns>
    public bool IsChunked()
    {
        return _responseMessage.Headers.TransferEncodingChunked == true;
    }

    /// <summary>
    /// 获取本次请求服务器返回的 Cookie 信息
    /// </summary>
    /// <returns>Cookie 字符�?/returns>
    public string? GetCookieStr()
    {
        return GetHeader(Header.SET_COOKIE);
    }

    /// <summary>
    /// 获取 Cookie �?
    /// </summary>
    /// <param name="name">Cookie �?/param>
    /// <returns>Cookie �?/returns>
    public string? GetCookieValue(string name)
    {
        var cookies = _responseMessage.Headers.GetValues("Set-Cookie");
        if (cookies != null)
        {
            foreach (var cookie in cookies)
            {
                if (cookie.StartsWith($"{name}=", StringComparison.OrdinalIgnoreCase))
                {
                    var endIndex = cookie.IndexOf(';', 1);
                    return endIndex > 0 ? cookie.Substring(name.Length + 1, endIndex - name.Length - 1) : cookie.Substring(name.Length + 1);
                }
            }
        }
        return null;
    }

    #endregion

    #region Body

    /// <summary>
    /// 获得服务区响应流
    /// </summary>
    /// <returns>响应�?/returns>
    public async Task<Stream?> BodyStreamAsync()
    {
        if (_isAsync)
        {
            return await _responseMessage.Content.ReadAsStreamAsync();
        }
        return null;
    }

    /// <summary>
    /// 获取响应流字节码
    /// </summary>
    /// <returns>byte[]</returns>
    public override byte[]? BodyBytes()
    {
        Sync();
        return _bodyBytes;
    }

    /// <summary>
    /// 获取响应主体
    /// </summary>
    /// <returns>String</returns>
    public string? Body()
    {
        var bytes = BodyBytes();
        if (bytes == null)
        {
            return null;
        }
        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    /// 将响应内容写出到文件
    /// </summary>
    /// <param name="targetFileOrDir">写出到的文件或目�?/param>
    /// <returns>写出 bytes �?/returns>
    public long WriteBody(string targetFileOrDir)
    {
        var filePath = targetFileOrDir;

        // 如果是目录，则从 URL 获取文件�?
        if (Directory.Exists(targetFileOrDir))
        {
            var fileName = GetFileNameFromDisposition() ?? Path.GetFileName(_responseMessage.RequestMessage?.RequestUri?.AbsolutePath) ?? "download";
            filePath = Path.Combine(targetFileOrDir, fileName);
        }
        else
        {
            // 确保目标文件的目录存在
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        var bytes = BodyBytes() ?? Array.Empty<byte>();
        File.WriteAllBytes(filePath, bytes);
        return bytes.Length;
    }

    /// <summary>
    /// 下载远程文件数据
    /// </summary>
    /// <returns>文件数据</returns>
    public async Task<byte[]> DownloadBytesAsync()
    {
        return await _responseMessage.Content.ReadAsByteArrayAsync();
    }

    #endregion

    #region Dispose

    /// <summary>
    /// 关闭响应
    /// </summary>
    public void Close()
    {
        Dispose();
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        if (!_disposed)
        {
            _responseMessage.Dispose();
            _disposed = true;
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// 初始化头信息
    /// </summary>
    private void InitHeaders()
    {
        // 添加响应�?
        foreach (var header in _responseMessage.Headers)
        {
            foreach (var value in header.Value)
            {
                SetHeader(header.Key, value, false);
            }
        }

        // 添加 Content �?
        foreach (var header in _responseMessage.Content.Headers)
        {
            foreach (var value in header.Value)
            {
                SetHeader(header.Key, value, false);
            }
        }
    }

    /// <summary>
    /// 强制同步
    /// </summary>
    /// <returns>this</returns>
    private HttpResponse ForceSync()
    {
        try
        {
            ReadBody();
        }
        catch (Exception ex) when (ex is FileNotFoundException || ex.Message.Contains("404"))
        {
            // 服务器无返回内容，忽略之
        }
        finally
        {
            Close();
        }
        return this;
    }

    /// <summary>
    /// 读取主体
    /// </summary>
    private void ReadBody()
    {
        if (_bodyBytes != null)
        {
            return;
        }

        try
        {
            _bodyBytes = _responseMessage.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
        }
        catch (Exception ex) when (_config.IgnoreEOFError && (ex is EndOfStreamException || ex.Message.Contains("EOF")))
        {
            // 忽略 EOF 错误
        }
    }

    /// <summary>
    /// �?Content-Disposition 头中获取文件�?
    /// </summary>
    /// <returns>文件�?/returns>
    private string? GetFileNameFromDisposition()
    {
        var disposition = _responseMessage.Content.Headers.ContentDisposition;
        if (disposition != null && !string.IsNullOrEmpty(disposition.FileNameStar))
        {
            return disposition.FileNameStar;
        }

        if (disposition != null && !string.IsNullOrEmpty(disposition.FileName))
        {
            return disposition.FileName;
        }

        var headerValue = GetHeader(Header.CONTENT_DISPOSITION);
        if (!string.IsNullOrEmpty(headerValue))
        {
            // 尝试解析 filename 参数
            var filenameIndex = headerValue.IndexOf("filename=", StringComparison.OrdinalIgnoreCase);
            if (filenameIndex >= 0)
            {
                var start = filenameIndex + 9; // "filename=".Length
                var end = headerValue.IndexOf(';', start);
                if (end < 0)
                {
                    end = headerValue.Length;
                }

                var filename = headerValue.Substring(start, end - start).Trim('"', ' ');
                if (!string.IsNullOrEmpty(filename))
                {
                    return filename;
                }
            }
        }

        return null;
    }

    #endregion
}




