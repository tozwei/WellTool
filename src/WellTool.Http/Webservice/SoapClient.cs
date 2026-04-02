using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WellTool.Http.Webservice
{
    /// <summary>
    /// SOAP客户端
    /// <para>
    /// 用于构建SOAP消息并通过HTTP接口发送。
    /// SOAP消息本质上是XML文本，可以通过调用 GetMsgStr 方法获取消息体。
    /// </para>
    /// <para>使用示例：</para>
    /// <code>
    /// var client = SoapClient.Create(url)
    ///     .SetMethod(methodName, namespaceUri)
    ///     .SetCharset(Encoding.GetEncoding("GBK"))
    ///     .SetParam("param1", "XXX");
    /// 
    /// string response = client.Send();
    /// </code>
    /// </summary>
    public class SoapClient
    {
        /// <summary>
        /// SOAP 1.1 XML消息体的Content-Type
        /// </summary>
        private const string ContentTypeSoap11TextXml = "text/xml;charset=";
        
        /// <summary>
        /// SOAP 1.2 XML消息体的Content-Type
        /// </summary>
        private const string ContentTypeSoap12SoapXml = "application/soap+xml;charset=";

        private string _url;
        private SoapProtocol _protocol;
        private Encoding _charset;
        private string _namespaceUri;
        private int _connectionTimeout;
        private int _readTimeout;
        
        private XNamespace _soapEnvelopeNs;
        private XNamespace _methodNs;
        private XDocument _soapEnvelope;
        private XElement _soapBody;
        private XElement _methodElement;

        /// <summary>
        /// 存储的header信息
        /// </summary>
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

        /// <summary>
        /// 存储的参数
        /// </summary>
        private readonly Dictionary<string, object> _params = new Dictionary<string, object>();

        /// <summary>
        /// 构造函数，默认使用SOAP 1.1版本协议
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        public SoapClient(string url)
        {
            _url = url;
            _protocol = SoapProtocol.SOAP_1_1;
            _charset = Encoding.UTF8;
            _connectionTimeout = 30000;
            _readTimeout = 30000;
            Init(_protocol);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        /// <param name="protocol">协议版本，见<see cref="SoapProtocol"/></param>
        public SoapClient(string url, SoapProtocol protocol) : this(url)
        {
            _protocol = protocol;
            Init(protocol);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        /// <param name="protocol">协议版本，见<see cref="SoapProtocol"/></param>
        /// <param name="namespaceUri">方法上的命名空间URI</param>
        public SoapClient(string url, SoapProtocol protocol, string namespaceUri) : this(url, protocol)
        {
            _namespaceUri = namespaceUri;
        }

        /// <summary>
        /// 创建SOAP客户端，默认使用SOAP 1.1版本协议
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        /// <returns>SoapClient实例</returns>
        public static SoapClient Create(string url)
        {
            return new SoapClient(url);
        }

        /// <summary>
        /// 创建SOAP客户端
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        /// <param name="protocol">协议版本，见<see cref="SoapProtocol"/></param>
        /// <returns>SoapClient实例</returns>
        public static SoapClient Create(string url, SoapProtocol protocol)
        {
            return new SoapClient(url, protocol);
        }

        /// <summary>
        /// 创建SOAP客户端
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        /// <param name="protocol">协议版本，见<see cref="SoapProtocol"/></param>
        /// <param name="namespaceUri">方法上的命名空间URI</param>
        /// <returns>SoapClient实例</returns>
        public static SoapClient Create(string url, SoapProtocol protocol, string namespaceUri)
        {
            return new SoapClient(url, protocol, namespaceUri);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="protocol">协议版本枚举</param>
        /// <returns>this</returns>
        public SoapClient Init(SoapProtocol protocol)
        {
            _soapEnvelopeNs = protocol.GetNamespace();
            _soapEnvelope = new XDocument(
                new XDeclaration("1.0", _charset.WebName, "true"),
                new XElement(_soapEnvelopeNs + "Envelope",
                    new XAttribute(XNamespace.Xmlns + "soap", _soapEnvelopeNs.NamespaceName),
                    new XElement(_soapEnvelopeNs + "Body")
                )
            );
            _soapBody = _soapEnvelope.Root?.Element(_soapEnvelopeNs + "Body");
            return this;
        }

        /// <summary>
        /// 重置SOAP客户端，用于客户端复用
        /// <para>
        /// 重置后需调用SetMethod方法重新指定请求方法，
        /// 并调用SetParam方法重新定义参数
        /// </para>
        /// </summary>
        /// <returns>this</returns>
        public SoapClient Reset()
        {
            _methodElement = null;
            _params.Clear();
            _headers.Clear();
            Init(_protocol);
            return this;
        }

        /// <summary>
        /// 设置编码
        /// </summary>
        /// <param name="charset">编码</param>
        /// <returns>this</returns>
        public SoapClient SetCharset(Encoding charset)
        {
            _charset = charset;
            return this;
        }

        /// <summary>
        /// 设置编码
        /// </summary>
        /// <param name="charsetName">编码名称</param>
        /// <returns>this</returns>
        public SoapClient SetCharset(string charsetName)
        {
            _charset = Encoding.GetEncoding(charsetName);
            return this;
        }

        /// <summary>
        /// 设置WebService请求地址
        /// </summary>
        /// <param name="url">WebService请求地址</param>
        /// <returns>this</returns>
        public SoapClient SetUrl(string url)
        {
            _url = url;
            return this;
        }

        /// <summary>
        /// 增加SOAP头信息
        /// </summary>
        /// <param name="name">头信息标签名</param>
        /// <param name="value">头信息值</param>
        /// <returns>this</returns>
        public SoapClient AddSoapHeader(string name, string value)
        {
            _headers[name] = value;
            return this;
        }

        /// <summary>
        /// 设置请求方法
        /// <para>
        /// 方法名自动识别前缀，前缀和方法名使用":"分隔
        /// 当识别到前缀后，自动添加xmlns属性，关联到默认的namespaceUri
        /// </para>
        /// </summary>
        /// <param name="methodName">方法名（可有前缀也可无，如"ns:methodName"）</param>
        /// <returns>this</returns>
        public SoapClient SetMethod(string methodName)
        {
            return SetMethod(methodName, _namespaceUri ?? string.Empty);
        }

        /// <summary>
        /// 设置请求方法
        /// <para>
        /// 方法名自动识别前缀，前缀和方法名使用":"分隔
        /// 当识别到前缀后，自动添加xmlns属性，关联到传入的namespaceUri
        /// </para>
        /// </summary>
        /// <param name="methodName">方法名（可有前缀也可无，如"ns:methodName"）</param>
        /// <param name="namespaceUri">命名空间URI</param>
        /// <returns>this</returns>
        public SoapClient SetMethod(string methodName, string namespaceUri)
        {
            _methodNs = namespaceUri;
            
            string prefix = string.Empty;
            string localName = methodName;

            if (methodName.Contains(":"))
            {
                var parts = methodName.Split(':');
                prefix = parts[0];
                localName = parts[1];
                
                // 添加命名空间声明
                var existingNs = _soapEnvelope.Root?.GetNamespaceOfPrefix(prefix);
                if (existingNs == null && !string.IsNullOrEmpty(namespaceUri))
                {
                    _soapEnvelope.Root?.Add(new XAttribute(XNamespace.Xmlns + prefix, namespaceUri));
                }
            }

            var methodQName = string.IsNullOrEmpty(prefix) 
                ? _soapEnvelopeNs + localName
                : XNamespace.Get(namespaceUri) + localName;

            _methodElement = new XElement(methodQName);
            _soapBody?.Add(_methodElement);
            
            return this;
        }

        /// <summary>
        /// 设置方法参数，使用方法的前缀
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值，可以是字符串、字典或XmlElement</param>
        /// <returns>this</returns>
        public SoapClient SetParam(string name, object value)
        {
            return SetParam(name, value, true);
        }

        /// <summary>
        /// 设置方法参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值，可以是字符串、字典或XmlElement</param>
        /// <param name="useMethodPrefix">是否使用方法的命名空间前缀</param>
        /// <returns>this</returns>
        public SoapClient SetParam(string name, object value, bool useMethodPrefix)
        {
            if (_methodElement == null)
            {
                throw new SoapRuntimeException("Please call SetMethod first before setting parameters.");
            }

            if (value is Dictionary<string, object> dictValue)
            {
                // 创建一个包装元素
                var wrapperElement = new XElement(name);
                foreach (var kvp in dictValue)
                {
                    SetParam(wrapperElement, kvp.Key, kvp.Value, useMethodPrefix);
                }
                _methodElement.Add(wrapperElement);
            }
            else if (value is XElement xElement)
            {
                // XmlElement子节点
                var wrapperElement = new XElement(name);
                wrapperElement.Add(xElement);
                _methodElement.Add(wrapperElement);
            }
            else
            {
                // 单个值
                SetParam(_methodElement, name, value, useMethodPrefix);
            }

            return this;
        }

        /// <summary>
        /// 批量设置参数，使用方法的前缀
        /// </summary>
        /// <param name="parameters">参数列表</param>
        /// <returns>this</returns>
        public SoapClient SetParams(Dictionary<string, object> parameters)
        {
            return SetParams(parameters, true);
        }

        /// <summary>
        /// 批量设置参数
        /// </summary>
        /// <param name="parameters">参数列表</param>
        /// <param name="useMethodPrefix">是否使用方法的命名空间前缀</param>
        /// <returns>this</returns>
        public SoapClient SetParams(Dictionary<string, object> parameters, bool useMethodPrefix)
        {
            foreach (var kvp in parameters)
            {
                SetParam(kvp.Key, kvp.Value, useMethodPrefix);
            }
            return this;
        }

        /// <summary>
        /// 获取方法节点
        /// <para>用于创建子节点等操作</para>
        /// </summary>
        /// <returns>方法元素</returns>
        public XElement GetMethodElement()
        {
            return _methodElement;
        }

        /// <summary>
        /// 获取SOAP请求消息
        /// </summary>
        /// <param name="pretty">是否格式化</param>
        /// <returns>消息字符串</returns>
        public string GetMsgStr(bool pretty)
        {
            var settings = new XmlWriterSettings
            {
                Indent = pretty,
                IndentChars = pretty ? "  " : "",
                OmitXmlDeclaration = false
            };

            using var memoryStream = new MemoryStream();
            // 写入XML声明头
            var declaration = $"<?xml version=\"1.0\" encoding=\"{_charset.WebName}\"?>";
            var declarationBytes = _charset.GetBytes(declaration);
            memoryStream.Write(declarationBytes, 0, declarationBytes.Length);
            
            using (var xmlWriter = XmlWriter.Create(memoryStream, settings))
            {
                _soapEnvelope?.Root?.WriteTo(xmlWriter);
                xmlWriter.Flush();
            }
            
            memoryStream.Position = 0;
            using var reader = new StreamReader(memoryStream, _charset);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 将SOAP消息的XML内容输出到流
        /// </summary>
        /// <param name="outputStream">输出流</param>
        /// <returns>this</returns>
        public SoapClient Write(Stream outputStream)
        {
            var msgStr = GetMsgStr(false);
            var bytes = _charset.GetBytes(msgStr);
            outputStream.Write(bytes, 0, bytes.Length);
            return this;
        }

        /// <summary>
        /// 设置超时，单位：毫秒
        /// <para>超时包括：</para>
        /// <list type="number">
        ///     <item>连接超时</item>
        ///     <item>读取响应超时</item>
        /// </list>
        /// </summary>
        /// <param name="milliseconds">超时毫秒数</param>
        /// <returns>this</returns>
        public SoapClient Timeout(int milliseconds)
        {
            SetConnectionTimeout(milliseconds);
            SetReadTimeout(milliseconds);
            return this;
        }

        /// <summary>
        /// 设置连接超时，单位：毫秒
        /// </summary>
        /// <param name="milliseconds">超时毫秒数</param>
        /// <returns>this</returns>
        public SoapClient SetConnectionTimeout(int milliseconds)
        {
            _connectionTimeout = milliseconds;
            return this;
        }

        /// <summary>
        /// 设置读取超时，单位：毫秒
        /// </summary>
        /// <param name="milliseconds">超时毫秒数</param>
        /// <returns>this</returns>
        public SoapClient SetReadTimeout(int milliseconds)
        {
            _readTimeout = milliseconds;
            return this;
        }

        /// <summary>
        /// 执行WebService请求，即发送SOAP内容
        /// </summary>
        /// <returns>返回结果</returns>
        public string Send()
        {
            return Send(false);
        }

        /// <summary>
        /// 执行WebService请求，即发送SOAP内容
        /// </summary>
        /// <param name="pretty">是否格式化响应</param>
        /// <returns>返回结果</returns>
        public string Send(bool pretty)
        {
            var response = SendForResponse();
            var body = response;
            
            if (pretty)
            {
                try
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(body);
                    body = FormatXml(xmlDoc);
                }
                catch
                {
                    // 如果格式化失败，返回原始内容
                }
            }
            
            return body;
        }

        /// <summary>
        /// 发送请求，获取响应字符串
        /// </summary>
        /// <returns>响应内容</returns>
        public string SendForResponse()
        {
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);
            
            client.Timeout = TimeSpan.FromMilliseconds(Math.Max(_connectionTimeout, _readTimeout));

            var contentType = GetXmlContentType();
            var soapMessage = GetMsgStr(false);
            
            var httpContent = new StringContent(soapMessage, _charset, contentType);
            
            // SOAP 1.1 需要 SOAPAction 头
            if (_protocol == SoapProtocol.SOAP_1_1)
            {
                httpContent.Headers.Add("SOAPAction", GetSoapAction());
            }
            
            // 添加自定义头
            foreach (var header in _headers)
            {
                if (header.Key.Equals("SOAPAction", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                httpContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            try
            {
                var response = client.PostAsync(_url, httpContent).Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw new SoapRuntimeException("SOAP request failed", ex);
            }
        }

        /// <summary>
        /// 异步发送请求
        /// </summary>
        /// <returns>响应内容</returns>
        public async Task<string> SendAsync()
        {
            return await SendAsync(false);
        }

        /// <summary>
        /// 异步发送请求
        /// </summary>
        /// <param name="pretty">是否格式化响应</param>
        /// <returns>响应内容</returns>
        public async Task<string> SendAsync(bool pretty)
        {
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);
            
            client.Timeout = TimeSpan.FromMilliseconds(Math.Max(_connectionTimeout, _readTimeout));

            var contentType = GetXmlContentType();
            var soapMessage = GetMsgStr(false);
            
            var httpContent = new StringContent(soapMessage, _charset, contentType);
            
            if (_protocol == SoapProtocol.SOAP_1_1)
            {
                httpContent.Headers.Add("SOAPAction", GetSoapAction());
            }

            foreach (var header in _headers)
            {
                if (header.Key.Equals("SOAPAction", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                httpContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            try
            {
                var response = await client.PostAsync(_url, httpContent);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                
                if (pretty)
                {
                    try
                    {
                        var xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(body);
                        body = FormatXml(xmlDoc);
                    }
                    catch
                    {
                        // 如果格式化失败，返回原始内容
                    }
                }
                
                return body;
            }
            catch (Exception ex)
            {
                throw new SoapRuntimeException("SOAP request failed", ex);
            }
        }

        /// <summary>
        /// 获取请求的Content-Type，附加编码信息
        /// </summary>
        /// <returns>请求的Content-Type</returns>
        private string GetXmlContentType()
        {
            return _protocol switch
            {
                SoapProtocol.SOAP_1_1 => ContentTypeSoap11TextXml + _charset.WebName,
                SoapProtocol.SOAP_1_2 => ContentTypeSoap12SoapXml + _charset.WebName,
                _ => throw new SoapRuntimeException($"Unsupported protocol: {_protocol}")
            };
        }

        /// <summary>
        /// 获取SOAPAction头
        /// </summary>
        /// <returns>SOAPAction</returns>
        private string GetSoapAction()
        {
            if (_methodElement == null)
            {
                return string.Empty;
            }

            var methodName = _methodElement.Name.LocalName;
            return string.IsNullOrEmpty(_namespaceUri) 
                ? $"\"{methodName}\"" 
                : $"\"{_namespaceUri.TrimEnd('/')}/{methodName}\"";
        }

        /// <summary>
        /// 设置方法参数（内部方法）
        /// </summary>
        private XElement SetParam(XElement parentElement, string name, object value, bool useMethodPrefix)
        {
            var paramElement = new XElement(name, value?.ToString());
            parentElement.Add(paramElement);
            return paramElement;
        }

        /// <summary>
        /// 格式化XML
        /// </summary>
        private static string FormatXml(XmlDocument xmlDoc)
        {
            var stringBuilder = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Replace
            };

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                xmlDoc.WriteTo(xmlWriter);
            }

            return stringBuilder.ToString();
        }
    }
}
