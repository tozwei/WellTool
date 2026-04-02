using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace WellTool.Http.Http.Ssl
{
    /// <summary>
    /// 自定义协议支持的SSL工厂
    /// 支持设置特定的SSL/TLS协议版本
    /// </summary>
    public class CustomProtocolsSslFactory
    {
        private readonly SslProtocols[] _protocols;
        private readonly RemoteCertificateValidationCallback _validationCallback;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="protocols">支持的协议列表</param>
        public CustomProtocolsSslFactory(params SslProtocols[] protocols)
        {
            _protocols = protocols;
            _validationCallback = DefaultSslFactory.GetDefaultCallback();
        }

        /// <summary>
        /// 获取启用的协议列表
        /// </summary>
        public SslProtocols[] Protocols => _protocols;

        /// <summary>
        /// 创建SSLStream
        /// </summary>
        /// <param name="innerStream">内部流</param>
        /// <param name="leaveInnerStreamOpen">是否保持内部流打开</param>
        /// <param name="targetHost">目标主机</param>
        /// <returns>SSLStream</returns>
        public SslStream CreateSslStream(System.IO.Stream innerStream, bool leaveInnerStreamOpen, string targetHost)
        {
            var sslStream = new SslStream(
                innerStream,
                leaveInnerStreamOpen,
                _validationCallback);

            var enabledProtocols = _protocols?.Length > 0 
                ? CombineProtocols(_protocols) 
                : SslProtocols.Tls12;

            sslStream.AuthenticateAsClient(targetHost, null, enabledProtocols, false);

            return sslStream;
        }

        /// <summary>
        /// 创建SslStream并执行握手
        /// </summary>
        /// <param name="innerStream">内部流</param>
        /// <param name="leaveInnerStreamOpen">是否保持内部流打开</param>
        /// <param name="targetHost">目标主机</param>
        /// <param name="clientCertificates">客户端证书</param>
        /// <param name="certificatePolicy">证书验证策略</param>
        /// <returns>已认证的SSLStream</returns>
        public SslStream CreateAuthenticatedStream(
            System.IO.Stream innerStream, 
            bool leaveInnerStreamOpen, 
            string targetHost,
            X509CertificateCollection clientCertificates = null,
            SslCertificateTrustMaterial certificatePolicy = null)
        {
            var enabledProtocols = _protocols?.Length > 0 ? CombineProtocols(_protocols) : SslProtocols.Tls12;
            var validationCallback = certificatePolicy?.ValidationCallback ?? _validationCallback;

            var sslStream = new SslStream(
                innerStream,
                leaveInnerStreamOpen,
                validationCallback);

            sslStream.AuthenticateAsClient(targetHost, clientCertificates, enabledProtocols, false);

            return sslStream;
        }

        /// <summary>
        /// 合并协议数组
        /// </summary>
        private static SslProtocols CombineProtocols(SslProtocols[] protocols)
        {
            var result = SslProtocols.None;
            foreach (var p in protocols)
            {
                result |= p;
            }
            return result;
        }
    }

    /// <summary>
    /// SSL证书信任材料，用于配置证书验证策略
    /// </summary>
    public class SslCertificateTrustMaterial
    {
        /// <summary>
        /// 证书选择回调
        /// </summary>
        public LocalCertificateSelectionCallback SelectionCallback { get; set; }

        /// <summary>
        /// 证书验证回调
        /// </summary>
        public RemoteCertificateValidationCallback ValidationCallback { get; set; }
    }
}
