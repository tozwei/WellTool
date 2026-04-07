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

namespace WellTool.JWT;

/// <summary>
/// JWT 头部
/// </summary>
public class JWTHeader : Claims
{
    /// <summary>
    /// 算法
    /// </summary>
    public const string ALGORITHM = "alg";
    /// <summary>
    /// 令牌类型
    /// </summary>
    public const string TYPE = "typ";
    /// <summary>
    /// 内容类型
    /// </summary>
    public const string CONTENT_TYPE = "cty";
    /// <summary>
    /// 密钥 ID
    /// </summary>
    public const string KEY_ID = "kid";

    /// <summary>
    /// 构造函数
    /// </summary>
    public JWTHeader()
    {
        SetAlgorithm("HS256");
        SetType("JWT");
    }

    /// <summary>
    /// 获取算法
    /// </summary>
    /// <returns>算法</returns>
    public string? GetAlgorithm()
    {
        return GetClaimAsString(ALGORITHM);
    }

    /// <summary>
    /// 设置算法
    /// </summary>
    /// <param name="algorithm">算法</param>
    /// <returns>this</returns>
    public JWTHeader SetAlgorithm(string algorithm)
    {
        SetClaim(ALGORITHM, algorithm);
        return this;
    }

    /// <summary>
    /// 获取类型
    /// </summary>
    /// <returns>类型</returns>
    public string GetHeaderType()
    {
        return GetClaim(TYPE) as string ?? "";
    }

    /// <summary>
    /// 设置类型
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns>this</returns>
    public JWTHeader SetType(string type)
    {
        SetClaim(TYPE, type);
        return this;
    }

    /// <summary>
    /// 获取密钥ID
    /// </summary>
    /// <returns>密钥ID</returns>
    public string GetKeyId()
    {
        return GetClaim(KEY_ID) as string ?? "";
    }

    /// <summary>
    /// 设置密钥ID
    /// </summary>
    /// <param name="keyId">密钥ID</param>
    /// <returns>this</returns>
    public JWTHeader SetKeyId(string keyId)
    {
        SetClaim(KEY_ID, keyId);
        return this;
    }

    /// <summary>
    /// 设置头部值
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="value">值</param>
    /// <returns>this</returns>
    public JWTHeader SetHeader(string name, object value)
    {
        SetClaim(name, value);
        return this;
    }

    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>值</returns>
    public object? GetValue(string key)
    {
        return GetClaim(key);
    }
}
