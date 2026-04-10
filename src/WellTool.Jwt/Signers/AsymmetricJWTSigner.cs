using System.Text;
using WellTool.Crypto.Asymmetric;
using WellTool.Core.Codec;

namespace WellTool.Jwt.Signers;

/// <summary>
/// 非对称加密JWT签名封装
/// </summary>
public class AsymmetricJWTSigner : IJwtSigner
{
    private Encoding _encoding = Encoding.UTF8;
    private readonly Sign _sign;
    private readonly string _algorithm;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="algorithm">算法字符串表示</param>
    /// <param name="key">公钥或私钥，公钥用于验证签名，私钥用于产生签名</param>
    public AsymmetricJWTSigner(string algorithm, object key)
    {
        _algorithm = algorithm;
        _sign = WellTool.Crypto.Asymmetric.Sign.Create(WellTool.Crypto.Asymmetric.SignAlgorithm.RSA_SHA256);
    }

    /// <summary>
    /// 设置编码
    /// </summary>
    /// <param name="encoding">编码</param>
    /// <returns>this</returns>
    public AsymmetricJWTSigner SetEncoding(Encoding encoding)
    {
        _encoding = encoding;
        return this;
    }

    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="headerBase64">头部Base64</param>
    /// <param name="payloadBase64">负载Base64</param>
    /// <returns>签名</returns>
    public string Sign(string headerBase64, string payloadBase64)
    {
        var dataStr = $"{headerBase64}.{payloadBase64}";
        var data = _encoding.GetBytes(dataStr);
        var signed = _sign.SignData(data);
        return Base64.EncodeUrlSafe(signed);
    }

    /// <summary>
    /// 验签
    /// </summary>
    /// <param name="headerBase64">头部Base64</param>
    /// <param name="payloadBase64">负载Base64</param>
    /// <param name="signBase64">签名Base64</param>
    /// <returns>是否验签通过</returns>
    public bool Verify(string headerBase64, string payloadBase64, string signBase64)
    {
        var dataStr = $"{headerBase64}.{payloadBase64}";
        var data = _encoding.GetBytes(dataStr);
        var signed = Convert.FromBase64String(signBase64);
        return _sign.VerifyData(data, signed);
    }

    /// <summary>
    /// 获取算法
    /// </summary>
    /// <returns>算法</returns>
    public string GetAlgorithm()
    {
        return _algorithm;
    }

    /// <summary>
    /// 获取算法ID
    /// </summary>
    /// <returns>算法ID</returns>
    public string GetAlgorithmId()
    {
        return AlgorithmUtil.GetId(GetAlgorithm());
    }
}
