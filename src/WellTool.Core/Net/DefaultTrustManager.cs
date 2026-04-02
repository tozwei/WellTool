using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace WellTool.Core.Net
{
    /// <summary>
    /// 默认信任管理器，默认信任所有客户端和服务端证书
    /// </summary>
    public class DefaultTrustManager
    {
        /// <summary>
        /// 默认的全局单例默认信任管理器，默认信任所有客户端和服务端证书
        /// </summary>
        public static DefaultTrustManager Instance { get; } = new DefaultTrustManager();

        /// <summary>
        /// 证书验证回调函数
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="certificate">证书</param>
        /// <param name="chain">证书链</param>
        /// <param name="sslPolicyErrors">SSL策略错误</param>
        /// <returns>是否信任证书</returns>
        public bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // 默认信任所有证书
            return true;
        }
    }
}