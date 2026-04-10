using System;
using System.Collections.Generic;

namespace WellTool.Core.Crypto.Asymmetric;

/// <summary>
/// 签名算法类型
/// </summary>
public enum SignAlgorithm
{
    // The RSA signature algorithm
    NONEwithRSA,

    // The MD2/MD5 with RSA Encryption signature algorithm
    MD2withRSA,
    MD5withRSA,

    // The signature algorithm with SHA-* and the RSA
    SHA1withRSA,
    SHA256withRSA,
    SHA384withRSA,
    SHA512withRSA,

    // The Digital Signature Algorithm
    NONEwithDSA,
    // The DSA with SHA-1 signature algorithm
    SHA1withDSA,

    // The ECDSA signature algorithms
    NONEwithECDSA,
    SHA1withECDSA,
    SHA256withECDSA,
    SHA384withECDSA,
    SHA512withECDSA,

    // 需要BC库加入支持
    SHA256withRSA_PSS,
    SHA384withRSA_PSS,
    SHA512withRSA_PSS
}

/// <summary>
/// SignAlgorithm扩展方法
/// </summary>
public static class SignAlgorithmExtensions
{
    private static readonly Dictionary<SignAlgorithm, string> AlgorithmValues = new Dictionary<SignAlgorithm, string>
    {
        { SignAlgorithm.NONEwithRSA, "NONEwithRSA" },
        { SignAlgorithm.MD2withRSA, "MD2withRSA" },
        { SignAlgorithm.MD5withRSA, "MD5withRSA" },
        { SignAlgorithm.SHA1withRSA, "SHA1withRSA" },
        { SignAlgorithm.SHA256withRSA, "SHA256withRSA" },
        { SignAlgorithm.SHA384withRSA, "SHA384withRSA" },
        { SignAlgorithm.SHA512withRSA, "SHA512withRSA" },
        { SignAlgorithm.NONEwithDSA, "NONEwithDSA" },
        { SignAlgorithm.SHA1withDSA, "SHA1withDSA" },
        { SignAlgorithm.NONEwithECDSA, "NONEwithECDSA" },
        { SignAlgorithm.SHA1withECDSA, "SHA1withECDSA" },
        { SignAlgorithm.SHA256withECDSA, "SHA256withECDSA" },
        { SignAlgorithm.SHA384withECDSA, "SHA384withECDSA" },
        { SignAlgorithm.SHA512withECDSA, "SHA512withECDSA" },
        { SignAlgorithm.SHA256withRSA_PSS, "SHA256WithRSA/PSS" },
        { SignAlgorithm.SHA384withRSA_PSS, "SHA384WithRSA/PSS" },
        { SignAlgorithm.SHA512withRSA_PSS, "SHA512WithRSA/PSS" }
    };

    /// <summary>
    /// 获取算法字符串表示，区分大小写
    /// </summary>
    /// <param name="algorithm">签名算法</param>
    /// <returns>算法字符串表示</returns>
    public static string GetValue(this SignAlgorithm algorithm)
    {
        if (AlgorithmValues.TryGetValue(algorithm, out var value))
        {
            return value;
        }
        throw new ArgumentException($"Unknown SignAlgorithm: {algorithm}");
    }
}
