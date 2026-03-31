namespace WellTool.JWT.Signers;

/// <summary>
/// JWT签名器接口
/// </summary>
public interface IJwtSigner
{
    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="headerBase64">Base64编码的头部</param>
    /// <param name="payloadBase64">Base64编码的载荷</param>
    /// <returns>签名后的字符串</returns>
    string Sign(string headerBase64, string payloadBase64);

    /// <summary>
    /// 验证签名
    /// </summary>
    /// <param name="headerBase64">Base64编码的头部</param>
    /// <param name="payloadBase64">Base64编码的载荷</param>
    /// <param name="signatureBase64">Base64编码的签名</param>
    /// <returns>是否验证通过</returns>
    bool Verify(string headerBase64, string payloadBase64, string signatureBase64);

    /// <summary>
    /// 获取算法名称
    /// </summary>
    /// <returns>算法名称</returns>
    string GetAlgorithm();

    /// <summary>
    /// 获取算法ID
    /// </summary>
    /// <returns>算法ID</returns>
    string GetAlgorithmId();
}
