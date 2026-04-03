using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace WellTool.Http.SSL
{
    /// <summary>
    /// 自定义支持协议类型的SSL Socket Factory
    /// </summary>
    public class CustomProtocolsSslFactory
    {
        /// <summary>
        /// 支持的协议列表
        /// </summary>
        protected readonly string[] Protocols;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="protocols">支持协议列表，如 "TLSv1.2", "TLSv1.3"</param>
        public CustomProtocolsSslFactory(params string[] protocols)
        {
            Protocols = protocols;
        }

        /// <summary>
        /// 创建SSL Stream
        /// </summary>
        /// <param name="stream">基础流</param>
        /// <param name="hostname">主机名</param>
        /// <param name="leaveStreamOpen">是否保持流打开</param>
        /// <returns>SSL Stream</returns>
        public virtual SslStream CreateSslStream(Stream stream, string hostname, bool leaveStreamOpen = false)
        {
            var sslStream = new SslStream(
                stream,
                leaveStreamOpen,
                ValidateServerCertificate,
                SelectLocalCertificate);

            var enabledProtocols = GetEnabledProtocols();
            if (enabledProtocols != SslProtocols.None)
            {
                sslStream.AuthenticateAsClient(hostname);
            }
            else
            {
                sslStream.AuthenticateAsClient(hostname);
            }

            return sslStream;
        }

        /// <summary>
        /// 获取启用的协议
        /// </summary>
        /// <returns>启用的协议</returns>
        protected virtual SslProtocols GetEnabledProtocols()
        {
            if (Protocols == null || Protocols.Length == 0)
            {
                return SslProtocols.None;
            }

            var result = SslProtocols.None;
            foreach (var protocol in Protocols)
            {
                var p = protocol.ToUpperInvariant();
                if (p == "SSLv3")
                    result |= SslProtocols.Ssl3;
                else if (p == "TLSv1")
                    result |= SslProtocols.Tls;
                else if (p == "TLSv1.1")
                    result |= SslProtocols.Tls11;
                else if (p == "TLSv1.2")
                    result |= SslProtocols.Tls12;
#if NETCOREAPP3_0_OR_GREATER
                else if (p == "TLSv1.3")
                    result |= SslProtocols.Tls13;
#endif
            }
            return result;
        }

        /// <summary>
        /// 验证服务器证书
        /// </summary>
        protected virtual bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            // 默认实现，接受所有证书
            // 在生产环境中应进行适当的验证
            return true;
        }

        /// <summary>
        /// 选择本地证书
        /// </summary>
        protected virtual X509Certificate SelectLocalCertificate(
            object sender,
            string targetHost,
            X509CertificateCollection localCertificates,
            X509Certificate remoteCertificate,
            string[] acceptableIssuers)
        {
            if (localCertificates != null && localCertificates.Count > 0)
            {
                return localCertificates[0];
            }
            return null;
        }
    }
}
