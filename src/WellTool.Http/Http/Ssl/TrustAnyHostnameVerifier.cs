namespace WellTool.Http.Ssl;

/// <summary>
/// HTTPS 域名校验 - 信任任意域名
/// </summary>
public class TrustAnyHostnameVerifier
{
    /// <summary>
    /// 验证域名（始终返回 true）
    /// </summary>
    /// <param name="hostname">主机名</param>
    /// <param name="sslProtocol">SSL 协议</param>
    /// <returns>始终返回 true</returns>
    public static bool Verify(string hostname, string sslProtocol)
    {
        return true; // 直接返回 true，不验证
    }
}
