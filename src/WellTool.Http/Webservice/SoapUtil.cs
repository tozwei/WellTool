using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace WellTool.Http.Webservice
{
    /// <summary>
    /// SOAP相关工具类
    /// </summary>
    public static class SoapUtil
    {
        /// <summary>
        /// SOAP 1.1 命名空间
        /// </summary>
        public const string Soap11Namespace = "http://schemas.xmlsoap.org/soap/envelope/";

        /// <summary>
        /// SOAP 1.2 命名空间
        /// </summary>
        public const string Soap12Namespace = "http://www.w3.org/2003/05/soap-envelope";

        /// <summary>
        /// 创建SOAP客户端，默认使用SOAP 1.1版本协议
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        /// <returns><see cref="SoapClient"/></returns>
        public static SoapClient CreateClient(string url)
        {
            return SoapClient.Create(url);
        }

        /// <summary>
        /// 创建SOAP客户端
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        /// <param name="protocol">协议，见<see cref="SoapProtocol"/></param>
        /// <returns><see cref="SoapClient"/></returns>
        public static SoapClient CreateClient(string url, SoapProtocol protocol)
        {
            return SoapClient.Create(url, protocol);
        }

        /// <summary>
        /// 创建SOAP客户端
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        /// <param name="protocol">协议，见<see cref="SoapProtocol"/></param>
        /// <param name="namespaceUri">方法上的命名空间URI</param>
        /// <returns><see cref="SoapClient"/></returns>
        public static SoapClient CreateClient(string url, SoapProtocol protocol, string namespaceUri)
        {
            return SoapClient.Create(url, protocol, namespaceUri);
        }

        /// <summary>
        /// 创建SOAP 1.1信封
        /// </summary>
        /// <param name="body">SOAP body内容</param>
        /// <returns>SOAP信封</returns>
        public static string CreateSoap11Envelope(string body)
        {
            return CreateSoapEnvelope(body, SoapProtocol.SOAP_1_1);
        }

        /// <summary>
        /// 创建SOAP 1.2信封
        /// </summary>
        /// <param name="body">SOAP body内容</param>
        /// <returns>SOAP信封</returns>
        public static string CreateSoap12Envelope(string body)
        {
            return CreateSoapEnvelope(body, SoapProtocol.SOAP_1_2);
        }

        /// <summary>
        /// 创建SOAP信封
        /// </summary>
        /// <param name="body">SOAP body内容</param>
        /// <param name="protocol">SOAP协议</param>
        /// <returns>SOAP信封</returns>
        public static string CreateSoapEnvelope(string body, SoapProtocol protocol)
        {
            var ns = protocol.GetNamespace();
            var prefix = protocol == SoapProtocol.SOAP_1_1 ? "soap" : "soap12";
            
            return $"<?xml version=\"1.0\" encoding=\"utf-8\"?><{prefix}:Envelope xmlns:{prefix}=\"{ns}\"><{prefix}:Body>{body}</{prefix}:Body></{prefix}:Envelope>";
        }

        /// <summary>
        /// 解析SOAP响应
        /// </summary>
        /// <param name="response">SOAP响应</param>
        /// <returns>SOAP body内容</returns>
        public static string ParseSoapResponse(string response)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);

            var namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceManager.AddNamespace("soap", Soap11Namespace);
            namespaceManager.AddNamespace("soap12", Soap12Namespace);

            var bodyNode = xmlDoc.SelectSingleNode("//soap:Body", namespaceManager) ??
                          xmlDoc.SelectSingleNode("//soap12:Body", namespaceManager);

            return bodyNode?.InnerXml ?? string.Empty;
        }

        /// <summary>
        /// 解析SOAP响应为XElement
        /// </summary>
        /// <param name="response">SOAP响应</param>
        /// <returns>SOAP body的XElement</returns>
        public static XElement ParseSoapResponseAsElement(string response)
        {
            var xmlDoc = XDocument.Parse(response);
            
            var soapNs = xmlDoc.Root?.GetDefaultNamespace();
            if (soapNs == null || soapNs.NamespaceName == "")
            {
                // 尝试使用XmlNamespaceManager获取命名空间
                var nsmgr = new XmlNamespaceManager(new NameTable());
                foreach (var attr in xmlDoc.Root?.Attributes())
                {
                    if (attr.Name.LocalName.StartsWith("xmlns") || attr.Name.LocalName == "soap" || attr.Name.LocalName == "soap12")
                    {
                        if (attr.Value.Contains("soap") && attr.Value.Contains("envelope"))
                        {
                            soapNs = XNamespace.Get(attr.Value);
                            break;
                        }
                    }
                }
            }

            if (soapNs != null && !string.IsNullOrEmpty(soapNs.NamespaceName))
            {
                var bodyName = soapNs + "Body";
                return xmlDoc.Root?.Element(bodyName);
            }
            
            return xmlDoc.Root?.Element("Body");
        }

        /// <summary>
        /// 格式化SOAP XML
        /// </summary>
        /// <param name="xml">XML字符串</param>
        /// <param name="indent">是否缩进</param>
        /// <returns>格式化后的XML</returns>
        public static string Format(string xml, bool indent = true)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);

                var settings = new XmlWriterSettings
                {
                    Indent = indent,
                    IndentChars = indent ? "  " : "",
                    NewLineChars = indent ? "\n" : "",
                    NewLineHandling = NewLineHandling.Replace
                };

                var sb = new StringBuilder();
                using (var writer = XmlWriter.Create(sb, settings))
                {
                    xmlDoc.WriteTo(writer);
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new SoapRuntimeException("Failed to format XML", ex);
            }
        }

        /// <summary>
        /// SOAPMessage 转为字符串
        /// </summary>
        /// <param name="xmlContent">XML内容</param>
        /// <param name="pretty">是否格式化</param>
        /// <returns>SOAP XML字符串</returns>
        public static string ToString(string xmlContent, bool pretty)
        {
            return ToString(xmlContent, pretty, Encoding.UTF8);
        }

        /// <summary>
        /// SOAPMessage 转为字符串
        /// </summary>
        /// <param name="xmlContent">XML内容</param>
        /// <param name="pretty">是否格式化</param>
        /// <param name="charset">编码</param>
        /// <returns>SOAP XML字符串</returns>
        public static string ToString(string xmlContent, bool pretty, Encoding charset)
        {
            if (pretty)
            {
                try
                {
                    return Format(xmlContent, true);
                }
                catch
                {
                    return xmlContent;
                }
            }
            return xmlContent;
        }

        /// <summary>
        /// 从响应中提取错误信息
        /// </summary>
        /// <param name="response">SOAP响应</param>
        /// <returns>错误信息，如果没有错误则返回null</returns>
        public static string ExtractFault(string response)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);

            var namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceManager.AddNamespace("soap", Soap11Namespace);
            namespaceManager.AddNamespace("soap12", Soap12Namespace);

            // SOAP 1.1 Fault
            var faultNode = xmlDoc.SelectSingleNode("//soap:Fault", namespaceManager);
            if (faultNode != null)
            {
                var faultString = faultNode.SelectSingleNode("faultstring", namespaceManager);
                return faultString?.InnerText ?? faultNode.InnerText;
            }

            // SOAP 1.2 Fault
            faultNode = xmlDoc.SelectSingleNode("//soap12:Fault", namespaceManager);
            if (faultNode != null)
            {
                var reason = faultNode.SelectSingleNode("soap12:Reason/soap12:Text", namespaceManager);
                return reason?.InnerText ?? faultNode.InnerText;
            }

            return null;
        }

        /// <summary>
        /// 检查响应是否包含SOAP错误
        /// </summary>
        /// <param name="response">SOAP响应</param>
        /// <returns>是否存在错误</returns>
        public static bool HasFault(string response)
        {
            return !string.IsNullOrEmpty(ExtractFault(response));
        }

        /// <summary>
        /// 创建SOAP 1.1方法调用元素
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="namespaceUri">命名空间URI</param>
        /// <returns>XML元素</returns>
        public static XElement CreateSoap11Method(string methodName, string namespaceUri)
        {
            return new XElement(
                XNamespace.Get(Soap11Namespace) + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soap", Soap11Namespace),
                new XElement(XNamespace.Get(Soap11Namespace) + "Body",
                    new XElement(XNamespace.Get(namespaceUri) + methodName)
                )
            );
        }

        /// <summary>
        /// 创建SOAP 1.2方法调用元素
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="namespaceUri">命名空间URI</param>
        /// <returns>XML元素</returns>
        public static XElement CreateSoap12Method(string methodName, string namespaceUri)
        {
            return new XElement(
                XNamespace.Get(Soap12Namespace) + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soap12", Soap12Namespace),
                new XElement(XNamespace.Get(Soap12Namespace) + "Body",
                    new XElement(XNamespace.Get(namespaceUri) + methodName)
                )
            );
        }
    }
}
