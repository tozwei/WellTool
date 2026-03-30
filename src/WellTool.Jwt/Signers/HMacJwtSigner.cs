using System.Text;

namespace WellTool.Jwt.Signers;

/// <summary>
/// HMAC JWT签名器
/// </summary>
public class HMacJwtSigner : IJwtSigner
{
    private readonly System.Security.Cryptography.HMAC _hmac;
    private readonly string _algorithm;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="algorithm">算法名称</param>
    /// <param name="key">密钥</param>
    public HMacJwtSigner(string algorithm, byte[] key)
    {
        _algorithm = algorithm;
        _hmac = CreateHmac(algorithm, key);
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="algorithm">算法名称</param>
    /// <param name="key">密钥</param>
    public HMacJwtSigner(string algorithm, string key)
        : this(algorithm, Encoding.UTF8.GetBytes(key))
    {
    }

    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="headerBase64">Base64编码的头部</param>
    /// <param name="payloadBase64">Base64编码的载荷</param>
    /// <returns>签名后的字符串</returns>
    public string Sign(string headerBase64, string payloadBase64)
    {
        var unsignedToken = $"{headerBase64}.{payloadBase64}";
        var signature = _hmac.ComputeHash(Encoding.UTF8.GetBytes(unsignedToken));
        return Base64UrlEncode(signature);
    }

    /// <summary>
    /// 验证签名
    /// </summary>
    /// <param name="headerBase64">Base64编码的头部</param>
    /// <param name="payloadBase64">Base64编码的载荷</param>
    /// <param name="signatureBase64">Base64编码的签名</param>
    /// <returns>是否验证通过</returns>
    public bool Verify(string headerBase64, string payloadBase64, string signatureBase64)
    {
        var expectedSignature = Sign(headerBase64, payloadBase64);
        return expectedSignature == signatureBase64;
    }

    /// <summary>
    /// 获取算法名称
    /// </summary>
    /// <returns>算法名称</returns>
    public string GetAlgorithm()
    {
        return _algorithm;
    }

    /// <summary>
    /// 创建HMAC实例
    /// </summary>
    /// <param name="algorithm">算法名称</param>
    /// <param name="key">密钥</param>
    /// <returns>HMAC实例</returns>
    private static System.Security.Cryptography.HMAC CreateHmac(string algorithm, byte[] key)
    {
        return algorithm switch
        {
            "HS256" => new System.Security.Cryptography.HMACSHA256(key),
            "HS384" => new System.Security.Cryptography.HMACSHA384(key),
            "HS512" => new System.Security.Cryptography.HMACSHA512(key),
            _ => throw new NotSupportedException($"Unsupported HMAC algorithm: {algorithm}")
        };
    }

    /// <summary>
    /// Base64 URL编码
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns>Base64 URL编码后的字符串</returns>
    private static string Base64UrlEncode(byte[] bytes)
    {
        return Convert.ToBase64String(bytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }
}
