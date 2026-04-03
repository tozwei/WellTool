using System.IO;
using System.Text;
using System.Xml;

namespace WellTool.Http.Webservice
{
    /// <summary>
    /// Jakarta SOAP相关工具类
    /// </summary>
    public static class JakartaSoapUtil
    {
        /// <summary>
        /// 创建Jakarta SOAP客户端，默认使用soap1.1版本协议
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        /// <returns><see cref="JakartaSoapClient"/></returns>
        public static JakartaSoapClient CreateClient(string url)
        {
            return JakartaSoapClient.Create(url);
        }

        /// <summary>
        /// 创建Jakarta SOAP客户端
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        /// <param name="protocol">协议，见<see cref="JakartaSoapProtocol"/></param>
        /// <returns><see cref="JakartaSoapClient"/></returns>
        public static JakartaSoapClient CreateClient(string url, JakartaSoapProtocol protocol)
        {
            return JakartaSoapClient.Create(url, protocol);
        }

        /// <summary>
        /// 创建Jakarta SOAP客户端
        /// </summary>
        /// <param name="url">WebService的URL地址</param>
        /// <param name="protocol">协议，见<see cref="JakartaSoapProtocol"/></param>
        /// <param name="namespaceUri">方法上的命名空间URI</param>
        /// <returns><see cref="JakartaSoapClient"/></returns>
        public static JakartaSoapClient CreateClient(string url, JakartaSoapProtocol protocol, string namespaceUri)
        {
            return JakartaSoapClient.Create(url, protocol, namespaceUri);
        }

        /// <summary>
        /// SOAP消息转为字符串
        /// </summary>
        /// <param name="xmlString">SOAP XML字符串</param>
        /// <param name="pretty">是否格式化</param>
        /// <returns>格式化后的SOAP XML字符串</returns>
        public static string ToString(string xmlString, bool pretty)
        {
            return ToString(xmlString, pretty, Encoding.UTF8);
        }

        /// <summary>
        /// SOAP消息转为字符串
        /// </summary>
        /// <param name="xmlString">SOAP XML字符串</param>
        /// <param name="pretty">是否格式化</param>
        /// <param name="charset">编码</param>
        /// <returns>格式化后的SOAP XML字符串</returns>
        public static string ToString(string xmlString, bool pretty, Encoding charset)
        {
            if (!pretty)
            {
                return xmlString;
            }

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlString);
                
                var stringBuilder = new StringBuilder();
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineChars = "\n",
                    NewLineHandling = NewLineHandling.Replace,
                    Encoding = charset
                };

                using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
                {
                    xmlDoc.WriteTo(xmlWriter);
                }

                return stringBuilder.ToString();
            }
            catch
            {
                return xmlString;
            }
        }
    }
}
