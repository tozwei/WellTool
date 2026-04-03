namespace WellTool.Http.Webservice
{
    /// <summary>
    /// Jakarta SOAP协议版本枚举
    /// </summary>
    public enum JakartaSoapProtocol
    {
        /// <summary>
        /// SOAP 1.1协议
        /// </summary>
        SOAP_1_1,

        /// <summary>
        /// SOAP 1.2协议
        /// </summary>
        SOAP_1_2
    }

    /// <summary>
    /// Jakarta SOAP协议扩展方法
    /// </summary>
    public static class JakartaSoapProtocolExtensions
    {
        /// <summary>
        /// SOAP 1.1 命名空间URI
        /// </summary>
        public const string Soap11NamespaceUri = "http://schemas.xmlsoap.org/soap/envelope/";

        /// <summary>
        /// SOAP 1.2 命名空间URI
        /// </summary>
        public const string Soap12NamespaceUri = "http://www.w3.org/2003/05/soap-envelope";

        /// <summary>
        /// 获取协议对应的Content-Type
        /// </summary>
        /// <param name="protocol">SOAP协议</param>
        /// <returns>Content-Type</returns>
        public static string GetContentType(this JakartaSoapProtocol protocol)
        {
            return protocol switch
            {
                JakartaSoapProtocol.SOAP_1_1 => "text/xml;charset=",
                JakartaSoapProtocol.SOAP_1_2 => "application/soap+xml;charset=",
                _ => throw new SoapRuntimeException($"Unsupported protocol: {protocol}")
            };
        }

        /// <summary>
        /// 获取协议对应的命名空间
        /// </summary>
        /// <param name="protocol">SOAP协议</param>
        /// <returns>命名空间URI</returns>
        public static string GetNamespaceUri(this JakartaSoapProtocol protocol)
        {
            return protocol switch
            {
                JakartaSoapProtocol.SOAP_1_1 => Soap11NamespaceUri,
                JakartaSoapProtocol.SOAP_1_2 => Soap12NamespaceUri,
                _ => throw new SoapRuntimeException($"Unsupported protocol: {protocol}")
            };
        }
    }
}
