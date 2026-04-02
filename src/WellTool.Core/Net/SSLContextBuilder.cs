using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using WellTool.Core.Builder;
using WellTool.Core.IO;
using WellTool.Core.Util;

namespace WellTool.Core.Net
{
    /// <summary>
    /// SSLContext构建器，可以自定义：<br>
    /// <ul>
    ///     <li>协议（protocol），默认TLS</li>
    ///     <li>信任管理器，默认DefaultTrustManager，即信任全部</li>
    /// </ul>
    /// <p>
    /// 构建后可获得SslStream，用于处理SSL连接
    /// </p>
    /// </summary>
    public class SSLContextBuilder : Builder<SslStream>
    {
        private string _protocol = SSLProtocols.TLS;
        private X509Certificate2 _certificate;
        private bool _clientCertRequired = false;
        private RemoteCertificateValidationCallback _remoteCertificateValidationCallback = DefaultTrustManager.Instance.ValidateCertificate;

        /// <summary>
        /// 创建 SSLContextBuilder
        /// </summary>
        /// <returns>SSLContextBuilder</returns>
        public static SSLContextBuilder Create()
        {
            return new SSLContextBuilder();
        }

        /// <summary>
        /// 设置协议。例如TLS等
        /// </summary>
        /// <param name="protocol">协议</param>
        /// <returns>自身</returns>
        public SSLContextBuilder SetProtocol(string protocol)
        {
            if (StrUtil.IsNotBlank(protocol))
            {
                _protocol = protocol;
            }
            return this;
        }

        /// <summary>
        /// 设置客户端证书
        /// </summary>
        /// <param name="certificate">客户端证书</param>
        /// <returns>自身</returns>
        public SSLContextBuilder SetCertificate(X509Certificate2 certificate)
        {
            _certificate = certificate;
            return this;
        }

        /// <summary>
        /// 设置是否需要客户端证书
        /// </summary>
        /// <param name="clientCertRequired">是否需要客户端证书</param>
        /// <returns>自身</returns>
        public SSLContextBuilder SetClientCertRequired(bool clientCertRequired)
        {
            _clientCertRequired = clientCertRequired;
            return this;
        }

        /// <summary>
        /// 设置远程证书验证回调
        /// </summary>
        /// <param name="remoteCertificateValidationCallback">远程证书验证回调</param>
        /// <returns>自身</returns>
        public SSLContextBuilder SetRemoteCertificateValidationCallback(RemoteCertificateValidationCallback remoteCertificateValidationCallback)
        {
            if (remoteCertificateValidationCallback != null)
            {
                _remoteCertificateValidationCallback = remoteCertificateValidationCallback;
            }
            return this;
        }

        /// <summary>
        /// 构建SslStream
        /// </summary>
        /// <returns>SslStream</returns>
        public SslStream Build()
        {
            throw new NotImplementedException("Build method requires a base stream");
        }

        /// <summary>
        /// 构建SslStream
        /// </summary>
        /// <param name="innerStream">基础流</param>
        /// <param name="targetHost">目标主机</param>
        /// <returns>SslStream</returns>
        public SslStream Build(System.IO.Stream innerStream, string targetHost)
        {
            try
            {
                var sslStream = new SslStream(
                    innerStream,
                    false,
                    _remoteCertificateValidationCallback,
                    null
                );

                sslStream.AuthenticateAsClient(
                    targetHost,
                    _certificate != null ? new X509Certificate2Collection(_certificate) : null,
                    _protocol,
                    false
                );

                return sslStream;
            }
            catch (Exception e)
            {
                throw new IORuntimeException(e);
            }
        }
    }
}