using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace WellTool.Http.Http.Ssl
{
    /// <summary>
    /// SSL套接字工厂构建器
    /// </summary>
    public class SslSocketFactoryBuilder
    {
        private RemoteCertificateValidationCallback _validationCallback;

        /// <summary>
        /// 设置证书验证回调
        /// </summary>
        /// <param name="validationCallback">证书验证回调</param>
        /// <returns>当前构建器</returns>
        public SslSocketFactoryBuilder SetValidationCallback(RemoteCertificateValidationCallback validationCallback)
        {
            _validationCallback = validationCallback;
            return this;
        }

        /// <summary>
        /// 构建SSL套接字工厂
        /// </summary>
        /// <returns>ServerCertificateValidationCallback</returns>
        public RemoteCertificateValidationCallback Build()
        {
            return _validationCallback ?? DefaultSslFactory.GetDefaultCallback();
        }
    }
}