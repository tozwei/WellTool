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

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WellTool.Jwt;

/// <summary>
/// JWT 负载
/// </summary>
public class JWTPayload
{
    /// <summary>
    /// 签发人
    /// </summary>
    [JsonPropertyName("iss")]
    public string? Issuer { get; set; }

    /// <summary>
    /// 签发时间
    /// </summary>
    [JsonPropertyName("iat")]
    public long? IssuedAt { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    [JsonPropertyName("exp")]
    public long? Expiration { get; set; }

    /// <summary>
    /// 主题
    /// </summary>
    [JsonPropertyName("sub")]
    public string? Subject { get; set; }

    /// <summary>
    /// 受众
    /// </summary>
    [JsonPropertyName("aud")]
    public string? Audience { get; set; }

    /// <summary>
    /// 生效时间
    /// </summary>
    [JsonPropertyName("nbf")]
    public long? NotBefore { get; set; }

    /// <summary>
    /// 令牌 ID
    /// </summary>
    [JsonPropertyName("jti")]
    public string? Id { get; set; }

    /// <summary>
    /// 自定义声明
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, object>? Extensions { get; set; }
}
