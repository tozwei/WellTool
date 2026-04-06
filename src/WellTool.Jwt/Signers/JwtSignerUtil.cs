using System.Text;

namespace WellTool.JWT.Signers;

/// <summary>
/// JWT签名器工具类
/// </summary>
public static class JwtSignerUtil
{
    /// <summary>
    /// 获取无签名签名器
    /// </summary>
    /// <returns>无签名签名器</returns>
    public static IJwtSigner None()
    {
        return NoneJwtSigner.None;
    }

    /// <summary>
    /// 创建 HS256 签名器
    /// </summary>
    /// <param name="key">密钥</param>
    /// <returns>签名器</returns>
    public static IJwtSigner HS256(byte[] key)
    {
        return CreateSigner("HS256", key);
    }

    /// <summary>
    /// 创建 HS384 签名器
    /// </summary>
    /// <param name="key">密钥</param>
    /// <returns>签名器</returns>
    public static IJwtSigner HS384(byte[] key)
    {
        return CreateSigner("HS384", key);
    }

    /// <summary>
    /// 创建 HS512 签名器
    /// </summary>
    /// <param name="key">密钥</param>
    /// <returns>签名器</returns>
    public static IJwtSigner HS512(byte[] key)
    {
        return CreateSigner("HS512", key);
    }

    /// <summary>
    /// 创建签名器
    /// </summary>
    /// <param name="algorithmId">算法ID</param>
    /// <param name="key">密钥</param>
    /// <returns>签名器</returns>
    public static IJwtSigner CreateSigner(string algorithmId, byte[] key)
    {
        if (NoneJwtSigner.IsNone(algorithmId))
        {
            return NoneJwtSigner.None;
        }

        if (algorithmId.StartsWith("HS"))
        {
            return new HMacJwtSigner(algorithmId, key);
        }

        throw new NotSupportedException($"Unsupported algorithm: {algorithmId}");
    }

    /// <summary>
    /// 创建签名器
    /// </summary>
    /// <param name="algorithmId">算法ID</param>
    /// <param name="key">密钥</param>
    /// <returns>签名器</returns>
    public static IJwtSigner CreateSigner(string algorithmId, string key)
    {
        return CreateSigner(algorithmId, Encoding.UTF8.GetBytes(key));
    }
}
