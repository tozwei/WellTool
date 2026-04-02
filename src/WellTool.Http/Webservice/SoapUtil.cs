using System.Xml;

namespace WellTool.Http.Webservice
{
    /// <summary>
    /// SOAP工具类
    /// </summary>
    public static class SoapUtil
    {
        /// <summary>
        /// 创建SOAP 1.1信封
        /// </summary>
        /// <param name="body">SOAP body内容</param>
        /// <returns>SOAP信封</returns>
        public static string CreateSoap11Envelope(string body)
        {
            return $"<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body>{body}</soap:Body></soap:Envelope>";
        }

        /// <summary>
        /// 创建SOAP 1.2信封
        /// </summary>
        /// <param name="body">SOAP body内容</param>
        /// <returns>SOAP信封</returns>
        public static string CreateSoap12Envelope(string body)
        {
            return $"<?xml version=\"1.0\" encoding=\"utf-8\"?><soap12:Envelope xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\"><soap12:Body>{body}</soap12:Body></soap12:Envelope>";
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
            namespaceManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaceManager.AddNamespace("soap12", "http://www.w3.org/2003/05/soap-envelope");

            var bodyNode = xmlDoc.SelectSingleNode("//soap:Body", namespaceManager) ??
                          xmlDoc.SelectSingleNode("//soap12:Body", namespaceManager);

            return bodyNode?.InnerXml ?? string.Empty;
        }
    }
}