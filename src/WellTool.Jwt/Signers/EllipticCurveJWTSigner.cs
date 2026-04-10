using System;
using WellTool.Core.Codec;

namespace WellTool.Jwt.Signers;

/// <summary>
/// 椭圆曲线（Elliptic Curve）的JWT签名器。<br>
/// 按照https://datatracker.ietf.org/doc/html/rfc7518#section-3.4,<br>
/// Elliptic Curve Digital Signature Algorithm (ECDSA)算法签名需要转换DER格式为pair (R, S)
/// </summary>
public class EllipticCurveJWTSigner : AsymmetricJWTSigner
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="algorithm">算法</param>
    /// <param name="key">密钥</param>
    public EllipticCurveJWTSigner(string algorithm, object key) : base(algorithm, key)
    {
    }

    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="headerBase64">头部Base64</param>
    /// <param name="payloadBase64">负载Base64</param>
    /// <returns>签名</returns>
    public new string Sign(string headerBase64, string payloadBase64)
    {
        var dataStr = $"{headerBase64}.{payloadBase64}";
        var data = System.Text.Encoding.UTF8.GetBytes(dataStr);
        var derSignature = base.Sign(headerBase64, payloadBase64);
        var signed = Convert.FromBase64String(derSignature);
        var outputLength = GetSignatureByteArrayLength(GetAlgorithm());
        var concatSignature = DerToConcat(signed, outputLength);
        return Base64.EncodeUrlSafe(concatSignature);
    }

    /// <summary>
    /// 验签
    /// </summary>
    /// <param name="headerBase64">头部Base64</param>
    /// <param name="payloadBase64">负载Base64</param>
    /// <param name="signBase64">签名Base64</param>
    /// <returns>是否验签通过</returns>
    public new bool Verify(string headerBase64, string payloadBase64, string signBase64)
    {
        var dataStr = $"{headerBase64}.{payloadBase64}";
        var data = System.Text.Encoding.UTF8.GetBytes(dataStr);
        var signed = Convert.FromBase64String(signBase64);
        var derSignature = ConcatToDer(signed);
        var derSignatureBase64 = Convert.ToBase64String(derSignature);
        return base.Verify(headerBase64, payloadBase64, derSignatureBase64);
    }

    /// <summary>
    /// 获取签名长度
    /// </summary>
    /// <param name="alg">算法</param>
    /// <returns>长度</returns>
    private static int GetSignatureByteArrayLength(string alg)
    {
        switch (alg)
        {
            case "ES256":
            case "SHA256withECDSA":
                return 64;
            case "ES384":
            case "SHA384withECDSA":
                return 96;
            case "ES512":
            case "SHA512withECDSA":
                return 132;
            default:
                throw new JWTException($"Unsupported Algorithm: {alg}");
        }
    }

    /// <summary>
    /// 将DER格式转换为Concat格式
    /// </summary>
    /// <param name="derSignature">DER格式签名</param>
    /// <param name="outputLength">输出长度</param>
    /// <returns>Concat格式签名</returns>
    private static byte[] DerToConcat(byte[] derSignature, int outputLength)
    {
        if (derSignature.Length < 8 || derSignature[0] != 48)
        {
            throw new JWTException("Invalid ECDSA signature format");
        }

        int offset;
        if (derSignature[1] > 0)
        {
            offset = 2;
        }
        else if (derSignature[1] == 0x81)
        {
            offset = 3;
        }
        else
        {
            throw new JWTException("Invalid ECDSA signature format");
        }

        byte rLength = derSignature[offset + 1];

        int i = rLength;
        while ((i > 0) && (derSignature[(offset + 2 + rLength) - i] == 0))
        {
            i--;
        }

        byte sLength = derSignature[offset + 2 + rLength + 1];

        int j = sLength;
        while ((j > 0) && (derSignature[(offset + 2 + rLength + 2 + sLength) - j] == 0))
        {
            j--;
        }

        int rawLen = Math.Max(i, j);
        rawLen = Math.Max(rawLen, outputLength / 2);

        if ((derSignature[offset - 1] & 0xff) != derSignature.Length - offset
            || (derSignature[offset - 1] & 0xff) != 2 + rLength + 2 + sLength
            || derSignature[offset] != 2
            || derSignature[offset + 2 + rLength] != 2)
        {
            throw new JWTException("Invalid ECDSA signature format");
        }

        byte[] concatSignature = new byte[2 * rawLen];

        Array.Copy(derSignature, (offset + 2 + rLength) - i, concatSignature, rawLen - i, i);
        Array.Copy(derSignature, (offset + 2 + rLength + 2 + sLength) - j, concatSignature, 2 * rawLen - j, j);

        return concatSignature;
    }

    /// <summary>
    /// 将Concat格式转换为DER格式
    /// </summary>
    /// <param name="jwsSignature">JWS签名</param>
    /// <returns>DER格式签名</returns>
    private static byte[] ConcatToDer(byte[] jwsSignature)
    {
        int rawLen = jwsSignature.Length / 2;

        int i = rawLen;

        while ((i > 0) && (jwsSignature[rawLen - i] == 0))
        {
            i--;
        }

        int j = i;

        if (jwsSignature[rawLen - i] < 0)
        {
            j += 1;
        }

        int k = rawLen;

        while ((k > 0) && (jwsSignature[2 * rawLen - k] == 0))
        {
            k--;
        }

        int l = k;

        if (jwsSignature[2 * rawLen - k] < 0)
        {
            l += 1;
        }

        int len = 2 + j + 2 + l;

        if (len > 255)
        {
            throw new JWTException("Invalid ECDSA signature format");
        }

        int offset;
        byte[] derSignature;

        if (len < 128)
        {
            derSignature = new byte[2 + 2 + j + 2 + l];
            offset = 1;
        }
        else
        {
            derSignature = new byte[3 + 2 + j + 2 + l];
            derSignature[1] = 0x81;
            offset = 2;
        }

        derSignature[0] = 48;
        derSignature[offset++] = (byte)len;
        derSignature[offset++] = 2;
        derSignature[offset++] = (byte)j;

        Array.Copy(jwsSignature, rawLen - i, derSignature, (offset + j) - i, i);

        offset += j;

        derSignature[offset++] = 2;
        derSignature[offset++] = (byte)l;

        Array.Copy(jwsSignature, 2 * rawLen - k, derSignature, (offset + l) - k, k);

        return derSignature;
    }
}
