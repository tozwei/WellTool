using WellTool.Core.Map;
using WellTool.Core.Util;

namespace WellTool.Jwt.Signers;

/// <summary>
/// 算法工具类，算法和JWT算法ID对应表
/// </summary>
public class AlgorithmUtil
{
    private static readonly BiMap<string, string> Map;

    static AlgorithmUtil()
    {
        Map = new BiMap<string, string>();
        Map.Add("HS256", "HmacSHA256");
        Map.Add("HS384", "HmacSHA384");
        Map.Add("HS512", "HmacSHA512");

        Map.Add("HMD5", "HmacMD5");
        Map.Add("HSHA1", "HmacSHA1");
        Map.Add("SM4CMAC", "SM4CMAC");

        Map.Add("RS256", "SHA256withRSA");
        Map.Add("RS384", "SHA384withRSA");
        Map.Add("RS512", "SHA512withRSA");

        Map.Add("ES256", "SHA256withECDSA");
        Map.Add("ES384", "SHA384withECDSA");
        Map.Add("ES512", "SHA512withECDSA");

        Map.Add("PS256", "SHA256WithRSA/PSS");
        Map.Add("PS384", "SHA384WithRSA/PSS");
        Map.Add("PS512", "SHA512WithRSA/PSS");

        Map.Add("RMD2", "MD2withRSA");
        Map.Add("RMD5", "MD5withRSA");
        Map.Add("RSHA1", "SHA1withRSA");
        Map.Add("DNONE", "NONEwithDSA");
        Map.Add("DSHA1", "SHA1withDSA");
        Map.Add("ENONE", "NONEwithECDSA");
        Map.Add("ESHA1", "SHA1withECDSA");
    }

    /// <summary>
    /// 获取算法，用户传入算法ID返回算法名，传入算法名返回本身
    /// </summary>
    /// <param name="idOrAlgorithm">算法ID或算法名</param>
    /// <returns>算法名</returns>
    public static string GetAlgorithm(string idOrAlgorithm)
    {
        return ObjectUtil.DefaultIfNull(GetAlgorithmById(idOrAlgorithm), idOrAlgorithm);
    }

    /// <summary>
    /// 获取算法ID，用户传入算法名返回ID，传入算法ID返回本身
    /// </summary>
    /// <param name="idOrAlgorithm">算法ID或算法名</param>
    /// <returns>算法ID</returns>
    public static string GetId(string idOrAlgorithm)
    {
        return ObjectUtil.DefaultIfNull(GetIdByAlgorithm(idOrAlgorithm), idOrAlgorithm);
    }

    /// <summary>
    /// 根据JWT算法ID获取算法
    /// </summary>
    /// <param name="id">JWT算法ID</param>
    /// <returns>算法</returns>
    private static string GetAlgorithmById(string id)
    {
        string upperId = id.ToUpper();
        return Map.ContainsKey(upperId) ? Map[upperId] : null;
    }

    /// <summary>
    /// 根据算法获取JWT算法ID
    /// </summary>
    /// <param name="algorithm">算法</param>
    /// <returns>JWT算法ID</returns>
    private static string GetIdByAlgorithm(string algorithm)
    {
        return Map.GetKey(algorithm);
    }
}
