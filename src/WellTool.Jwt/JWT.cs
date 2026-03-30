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
using WellTool.Jwt.Signers;

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
    /// 签名器
    /// </summary>
    private IJwtSigner? _signer;

    /// <summary>
    /// 令牌的各个部分
    /// </summary>
    private string[]? _tokens;

    /// <summary>
    /// 编码
    /// </summary>
    private Encoding _encoding = Encoding.UTF8;

    /// <summary>
    /// 创建空的JWT对象
    /// </summary>
    /// <returns>JWT</returns>
    public static JWT Create()
    {
        return new JWT();
    }

    /// <summary>
    /// 创建并解析JWT对象
    /// </summary>
    /// <param name="token">JWT Token字符串，格式为xxxx.yyyy.zzzz</param>
    /// <returns>JWT</returns>
    public static JWT Of(string token)
    {
        return new JWT(token);
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public JWT()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="token">JWT Token字符串，格式为xxxx.yyyy.zzzz</param>
    public JWT(string token)
    {
        Parse(token);
    }

    /// <summary>
    /// 解析JWT内容
    /// </summary>
    /// <param name="token">JWT Token字符串，格式为xxxx.yyyy.zzzz</param>
    /// <returns>this</returns>
    public JWT Parse(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new JWTException("Token String must be not blank!");
        }

        var tokens = SplitToken(token);
        _tokens = tokens;
        Header.Parse(tokens[0], _encoding);
        Payload.Parse(tokens[1], _encoding);
        Signature = tokens[2];

        return this;
    }

    /// <summary>
    /// 设置编码
    /// </summary>
    /// <param name="encoding">编码</param>
    /// <returns>this</returns>
    public JWT SetEncoding(Encoding encoding)
    {
        _encoding = encoding;
        return this;
    }

    /// <summary>
    /// 设置密钥，如果头部指定了算法，直接使用，否则默认算法是：HS256(HmacSHA256)
    /// </summary>
    /// <param name="key">密钥</param>
    /// <returns>this</returns>
    public JWT SetKey(byte[] key)
    {
        return SetSigner(GetAlgorithm() ?? "HS256", key);
    }

    /// <summary>
    /// 设置密钥
    /// </summary>
    /// <param name="key">密钥</param>
    /// <returns>this</returns>
    public JWT SetKey(string key)
    {
        return SetKey(_encoding.GetBytes(key));
    }

    /// <summary>
    /// 设置签名算法
    /// </summary>
    /// <param name="algorithmId">签名算法ID，如HS256</param>
    /// <param name="key">密钥</param>
    /// <returns>this</returns>
    public JWT SetSigner(string algorithmId, byte[] key)
    {
        return SetSigner(JwtSignerUtil.CreateSigner(algorithmId, key));
    }

    /// <summary>
    /// 设置签名算法
    /// </summary>
    /// <param name="algorithmId">签名算法ID，如HS256</param>
    /// <param name="key">密钥</param>
    /// <returns>this</returns>
    public JWT SetSigner(string algorithmId, string key)
    {
        return SetSigner(algorithmId, _encoding.GetBytes(key));
    }

    /// <summary>
    /// 设置签名算法
    /// </summary>
    /// <param name="signer">签名算法</param>
    /// <returns>this</returns>
    public JWT SetSigner(IJwtSigner signer)
    {
        _signer = signer;

        // 检查头信息中是否有算法信息
        var algorithm = Header.GetAlgorithm();
        if (string.IsNullOrWhiteSpace(algorithm))
        {
            Header.SetAlgorithm(signer.GetAlgorithm());
        }

        return this;
    }

    /// <summary>
    /// 获取JWT算法签名器
    /// </summary>
    /// <returns>JWT算法签名器</returns>
    public IJwtSigner? GetSigner()
    {
        return _signer;
    }

    /// <summary>
    /// 获取算法ID(alg)头信息
    /// </summary>
    /// <returns>算法头信息</returns>
    public string? GetAlgorithm()
    {
        return Header.GetAlgorithm();
    }

    /// <summary>
    /// 签名生成JWT字符串
    /// </summary>
    /// <returns>JWT字符串</returns>
    public string Sign()
    {
        return Sign(true);
    }

    /// <summary>
    /// 签名生成JWT字符串
    /// </summary>
    /// <param name="addTypeIfNot">如果'typ'头不存在，是否赋值默认值</param>
    /// <returns>JWT字符串</returns>
    public string Sign(bool addTypeIfNot)
    {
        return Sign(_signer!, addTypeIfNot);
    }

    /// <summary>
    /// 签名生成JWT字符串
    /// </summary>
    /// <param name="signer">JWT签名器</param>
    /// <returns>JWT字符串</returns>
    public string Sign(IJwtSigner signer)
    {
        return Sign(signer, true);
    }

    /// <summary>
    /// 签名生成JWT字符串
    /// </summary>
    /// <param name="signer">JWT签名器</param>
    /// <param name="addTypeIfNot">如果'typ'头不存在，是否赋值默认值</param>
    /// <returns>JWT字符串</returns>
    public string Sign(IJwtSigner signer, bool addTypeIfNot)
    {
        if (signer == null)
        {
            throw new JWTException("No Signer provided!");
        }

        // 检查tye信息
        if (addTypeIfNot)
        {
            var type = Header.GetType();
            if (string.IsNullOrWhiteSpace(type))
            {
                Header.SetType("JWT");
            }
        }

        // 检查头信息中是否有算法信息
        var algorithm = GetAlgorithm();
        if (string.IsNullOrWhiteSpace(algorithm))
        {
            Header.SetAlgorithm(signer.GetAlgorithm());
        }

        var headerBase64 = Base64UrlEncode(Header.ToString(), _encoding);
        var payloadBase64 = Base64UrlEncode(Payload.ToString(), _encoding);
        var signature = signer.Sign(headerBase64, payloadBase64);

        Signature = signature;
        return $"{headerBase64}.{payloadBase64}.{signature}";
    }

    /// <summary>
    /// 验证JWT Token是否有效
    /// </summary>
    /// <returns>是否有效</returns>
    public bool Verify()
    {
        return Verify(_signer);
    }

    /// <summary>
    /// 验证JWT Token是否有效
    /// </summary>
    /// <param name="signer">签名器（签名算法）</param>
    /// <returns>是否有效</returns>
    public bool Verify(IJwtSigner? signer)
    {
        if (signer == null)
        {
            // 如果无签名器提供，默认认为是无签名JWT信息
            signer = NoneJwtSigner.None;
        }

        // 用户定义alg为none但是签名器不是NoneJwtSigner
        if (NoneJwtSigner.IsNone(GetAlgorithm()) && !(signer is NoneJwtSigner))
        {
            throw new JWTException($"Alg is 'none' but use: {signer.GetType().Name}!");
        }

        // alg非none，但签名器是NoneJWTSigner
        if (signer is NoneJwtSigner && !NoneJwtSigner.IsNone(GetAlgorithm()))
        {
            throw new JWTException("Alg is not 'none' but use NoneJwtSigner!");
        }

        var tokens = _tokens;
        if (tokens == null || tokens.Length != 3)
        {
            throw new JWTException("No token to verify!");
        }

        return signer.Verify(tokens[0], tokens[1], tokens[2]);
    }

    /// <summary>
    /// 将JWT字符串拆分为3部分，无加密算法则最后一部分是""
    /// </summary>
    /// <param name="token">JWT Token</param>
    /// <returns>三部分内容</returns>
    private static string[] SplitToken(string token)
    {
        var tokens = token.Split('.');
        if (tokens.Length != 3)
        {
            throw new JWTException($"The token was expected 3 parts, but got {tokens.Length}.");
        }
        return tokens;
    }

    /// <summary>
    /// Base64 URL 编码
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="encoding">编码</param>
    /// <returns>Base64 URL 编码后的字符串</returns>
    private static string Base64UrlEncode(string input, Encoding encoding)
    {
        var bytes = encoding.GetBytes(input);
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
    /// <param name="encoding">编码</param>
    /// <returns>解码后的字符串</returns>
    internal static string Base64UrlDecode(string input, Encoding encoding)
    {
        input = input.Replace('-', '+').Replace('_', '/');
        while (input.Length % 4 != 0)
        {
            input += '=';
        }
        var bytes = Convert.FromBase64String(input);
        return encoding.GetString(bytes);
    }
}
