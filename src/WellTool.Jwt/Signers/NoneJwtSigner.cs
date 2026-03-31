namespace WellTool.JWT.Signers;

/// <summary>
/// 无签名JWT签名器
/// </summary>
public class NoneJwtSigner : IJwtSigner
{
    /// <summary>
    /// 无签名实例
    /// </summary>
    public static readonly NoneJwtSigner None = new();

    /// <summary>
    /// 私有构造函数
    /// </summary>
    private NoneJwtSigner()
    {
    }

    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="headerBase64">Base64编码的头部</param>
    /// <param name="payloadBase64">Base64编码的载荷</param>
    /// <returns>空字符串</returns>
    public string Sign(string headerBase64, string payloadBase64)
    {
        return string.Empty;
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
        return string.IsNullOrEmpty(signatureBase64);
    }

    /// <summary>
    /// 获取算法名称
    /// </summary>
    /// <returns>算法名称</returns>
    public string GetAlgorithm()
    {
        return "none";
    }

    /// <summary>
    /// 获取算法ID
    /// </summary>
    /// <returns>算法ID</returns>
    public string GetAlgorithmId()
    {
        return "none";
    }

    /// <summary>
    /// 判断算法是否为none
    /// </summary>
    /// <param name="algorithm">算法名称</param>
    /// <returns>是否为none</returns>
    public static bool IsNone(string algorithm)
    {
        return "none".Equals(algorithm, StringComparison.OrdinalIgnoreCase);
    }
}
