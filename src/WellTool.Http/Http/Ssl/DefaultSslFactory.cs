using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace WellTool.Http.Http.Ssl
{
    /// <summary>
    /// 默认SSL工厂
    /// </summary>
    public class DefaultSslFactory
    {
        /// <summary>
        /// 获取默认的ServerCertificateValidationCallback
        /// </summary>
        /// <returns>ServerCertificateValidationCallback</returns>
        public static RemoteCertificateValidationCallback GetDefaultCallback()
        {
            return (sender, certificate, chain, sslPolicyErrors) =>
            {
                return sslPolicyErrors == SslPolicyErrors.None;
            };
        }
    }
}