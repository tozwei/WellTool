using System.Text;

namespace WellTool.Jwt.Signers;

/// <summary>
/// JWT签名器工具类
/// </summary>
public static class JwtSignerUtil
{
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
