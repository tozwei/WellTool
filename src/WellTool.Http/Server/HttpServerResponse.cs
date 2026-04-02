using System.IO;
using System.Net;
using System.Text;
using System.Net.Mime;

namespace WellTool.Http.Server
{
    /// <summary>
    /// HTTP服务器响应封装
    /// </summary>
    public class HttpServerResponse
    {
        private readonly HttpListenerResponse _response;
        private Encoding _charset;
        private bool _isSendCode;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="response">HttpListener响应对象</param>
        public HttpServerResponse(HttpListenerResponse response)
        {
            _response = response;
        }

        /// <summary>
        /// 获取或设置状态码
        /// </summary>
        public int StatusCode
        {
            get => _response.StatusCode;
            set => _response.StatusCode = value;
        }

        /// <summary>
        /// 获取或设置Content-Type
        /// </summary>
        public string ContentType
        {
            get => _response.ContentType;
            set => _response.ContentType = value;
        }

        /// <summary>
        /// 获取或设置响应编码
        /// </summary>
        public Encoding Charset
        {
            get => _charset;
            set => _charset = value;
        }

        /// <summary>
        /// 获取原始HttpListenerResponse
        /// </summary>
        public HttpListenerResponse RawResponse => _response;

        /// <summary>
        /// 获取响应头
        /// </summary>
        public WebHeaderCollection Headers => _response.Headers;

        /// <summary>
        /// 获取响应输出流
        /// </summary>
        public Stream OutputStream => _response.OutputStream;

        /// <summary>
        /// 添加响应头
        /// </summary>
        /// <param name="name">头名称</param>
        /// <param name="value">头值</param>
        public void AddHeader(string name, string value)
        {
            _response.AddHeader(name, value);
        }

        /// <summary>
        /// 设置响应头
        /// </summary>
        /// <param name="name">头名称</param>
        /// <param name="value">头值</param>
        public void SetHeader(string name, string value)
        {
            _response.Headers.Set(name, value);
        }

        /// <summary>
        /// 发送HTTP状态码
        /// </summary>
        /// <param name="statusCode">HTTP状态码</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse Send(int statusCode)
        {
            return Send(statusCode, 0);
        }

        /// <summary>
        /// 发送HTTP状态码
        /// </summary>
        /// <param name="statusCode">HTTP状态码</param>
        /// <param name="bodyLength">响应体长度，0表示不定长度</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse Send(int statusCode, long bodyLength)
        {
            if (_isSendCode)
            {
                throw new InvalidOperationException("Http status code has been sent!");
            }

            _response.StatusCode = statusCode;
            _response.SendChunked = bodyLength <= 0;

            _isSendCode = true;
            return this;
        }

        /// <summary>
        /// 发送200 OK状态码
        /// </summary>
        /// <returns>自身引用</returns>
        public HttpServerResponse SendOk()
        {
            return Send((int)HttpStatusCode.OK);
        }

        /// <summary>
        /// 发送200 OK状态码
        /// </summary>
        /// <param name="bodyLength">响应体长度</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse SendOk(long bodyLength)
        {
            return Send((int)HttpStatusCode.OK, bodyLength);
        }

        /// <summary>
        /// 发送404 Not Found错误
        /// </summary>
        /// <param name="content">错误内容</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse Send404(string content = "404 Not Found")
        {
            return SendError((int)HttpStatusCode.NotFound, content);
        }

        /// <summary>
        /// 发送错误页面
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <param name="content">错误内容</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse SendError(int errorCode, string content)
        {
            Send(errorCode);
            ContentType = "text/html;charset=utf-8";
            Write(content);
            return this;
        }

        /// <summary>
        /// 写入文本内容
        /// </summary>
        /// <param name="content">文本内容</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse Write(string content)
        {
            var charset = _charset ?? Encoding.UTF8;
            var buffer = charset.GetBytes(content);
            return Write(buffer);
        }

        /// <summary>
        /// 写入文本内容
        /// </summary>
        /// <param name="content">文本内容</param>
        /// <param name="contentType">Content-Type</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse Write(string content, string contentType)
        {
            ContentType = contentType;
            return Write(content);
        }

        /// <summary>
        /// 写入字节数组
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse Write(byte[] data)
        {
            if (!_isSendCode)
            {
                SendOk(data.Length);
            }
            _response.ContentLength64 = data.Length;
            _response.OutputStream.Write(data, 0, data.Length);
            return this;
        }

        /// <summary>
        /// 写入字节数组
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="contentType">Content-Type</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse Write(byte[] data, string contentType)
        {
            ContentType = contentType;
            return Write(data);
        }

        /// <summary>
        /// 写入流内容
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse Write(Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            var data = ms.ToArray();
            return Write(data);
        }

        /// <summary>
        /// 写入流内容
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="contentType">Content-Type</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse Write(Stream stream, string contentType)
        {
            ContentType = contentType;
            return Write(stream);
        }

        /// <summary>
        /// 设置Content-Type
        /// </summary>
        /// <param name="contentType">Content-Type</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse SetContentType(string contentType)
        {
            if (!string.IsNullOrEmpty(contentType) && _charset != null && !contentType.Contains("charset="))
            {
                contentType = $"{contentType};charset={_charset.WebName}";
            }
            ContentType = contentType;
            return this;
        }

        /// <summary>
        /// 设置Content-Length
        /// </summary>
        /// <param name="contentLength">内容长度</param>
        /// <returns>自身引用</returns>
        public HttpServerResponse SetContentLength(long contentLength)
        {
            _response.ContentLength64 = contentLength;
            return this;
        }

        /// <summary>
        /// 设置Content-Disposition头（用于文件下载）
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void SetContentDisposition(string fileName)
        {
            var disposition = new ContentDisposition
            {
                FileName = fileName
            };
            AddHeader("Content-Disposition", disposition.ToString());
        }

        /// <summary>
        /// 重定向到指定URL
        /// </summary>
        /// <param name="url">目标URL</param>
        public void Redirect(string url)
        {
            _response.Redirect(url);
        }

        /// <summary>
        /// 关闭响应
        /// </summary>
        public void Close()
        {
            try
            {
                _response.OutputStream.Close();
            }
            finally
            {
                _response.Close();
            }
        }

        /// <summary>
        /// 关闭响应并终止连接
        /// </summary>
        public void Abort()
        {
            _response.Abort();
        }
    }
}
