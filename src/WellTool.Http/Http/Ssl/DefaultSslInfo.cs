using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace WellTool.Http.Ssl;

/// <summary>
/// 默认的全局 SSL 配置，当用户未设置相关信息时，使用默认设置
/// </summary>
public static class DefaultSslInfo
{
    /// <summary>
    /// 默认信任全部的域名校验器
    /// </summary>
    public static readonly RemoteCertificateValidationCallback TrustAnyHostValidator =
        (sender, certificate, chain, sslPolicyErrors) => true;

    /// <summary>
    /// 默认的 SSL 协议
    /// </summary>
#if NETSTANDARD2_1
    public const SslProtocols DefaultSslProtocol = SslProtocols.Tls12;
#else
    public const SslProtocols DefaultSslProtocol = SslProtocols.Tls12 | SslProtocols.Tls13;
#endif
}
