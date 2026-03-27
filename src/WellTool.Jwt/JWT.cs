// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Text;
using System.Text.Json;

namespace WellTool.Jwt;

/// <summary>
/// JWT 类
/// </summary>
public class JWT
{
    /// <summary>
    /// 头部
    /// </summary>
    public JWTHeader Header { get; set; } = new();

    /// <summary>
    /// 负载
    /// </summary>
    public JWTPayload Payload { get; set; } = new();

    /// <summary>
    /// 签名
    /// </summary>
    public string? Signature { get; set; }

    /// <summary>
    /// 生成 JWT 令牌
    /// </summary>
    /// <param name="secret">密钥</param>
    /// <returns>JWT 令牌</returns>
    public string Sign(string secret)
    {
        var headerJson = JsonSerializer.Serialize(Header);
        var payloadJson = JsonSerializer.Serialize(Payload);

        var headerBase64 = Base64UrlEncode(headerJson);
        var payloadBase64 = Base64UrlEncode(payloadJson);

        var unsignedToken = $"{headerBase64}.{payloadBase64}";
        var signature = HmacSign(unsignedToken, secret);
        var signatureBase64 = Base64UrlEncode(signature);

        Signature = signatureBase64;
        return $"{unsignedToken}.{signatureBase64}";
    }

    /// <summary>
    /// 验证 JWT 令牌
    /// </summary>
    /// <param name="token">JWT 令牌</param>
    /// <param name="secret">密钥</param>
    /// <returns>是否验证通过</returns>
    public static bool Verify(string token, string secret)
    {
        try
        {
            var parts = token.Split('.');
            if (parts.Length != 3)
            {
                return false;
            }

            var headerBase64 = parts[0];
            var payloadBase64 = parts[1];
            var signatureBase64 = parts[2];

            var unsignedToken = $"{headerBase64}.{payloadBase64}";
            var expectedSignature = HmacSign(unsignedToken, secret);
            var expectedSignatureBase64 = Base64UrlEncode(expectedSignature);

            return signatureBase64 == expectedSignatureBase64;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 解析 JWT 令牌
    /// </summary>
    /// <param name="token">JWT 令牌</param>
    /// <returns>JWT 对象</returns>
    public static JWT Parse(string token)
    {
        var parts = token.Split('.');
        if (parts.Length != 3)
        {
            throw new JWTException("Invalid JWT token format");
        }

        var headerBase64 = parts[0];
        var payloadBase64 = parts[1];
        var signatureBase64 = parts[2];

        var headerJson = Base64UrlDecode(headerBase64);
        var payloadJson = Base64UrlDecode(payloadBase64);

        var header = JsonSerializer.Deserialize<JWTHeader>(headerJson);
        var payload = JsonSerializer.Deserialize<JWTPayload>(payloadJson);

        if (header == null || payload == null)
        {
            throw new JWTException("Failed to parse JWT token");
        }

        return new JWT
        {
            Header = header,
            Payload = payload,
            Signature = signatureBase64
        };
    }

    /// <summary>
    /// Base64 URL 编码
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>Base64 URL 编码后的字符串</returns>
    private static string Base64UrlEncode(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        return Base64UrlEncode(bytes);
    }

    /// <summary>
    /// Base64 URL 编码
    /// </summary>
    /// <param name="bytes">输入字节数组</param>
    /// <returns>Base64 URL 编码后的字符串</returns>
    private static string Base64UrlEncode(byte[] bytes)
    {
        return Convert.ToBase64String(bytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }

    /// <summary>
    /// Base64 URL 解码
    /// </summary>
    /// <param name="input">Base64 URL 编码后的字符串</param>
    /// <returns>解码后的字符串</returns>
    private static string Base64UrlDecode(string input)
    {
        input = input.Replace('-', '+').Replace('_', '/');
        while (input.Length % 4 != 0)
        {
            input += '=';
        }
        var bytes = Convert.FromBase64String(input);
        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    /// HMAC 签名
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="secret">密钥</param>
    /// <returns>签名字节数组</returns>
    private static byte[] HmacSign(string input, string secret)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(secret));
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
    }
}
