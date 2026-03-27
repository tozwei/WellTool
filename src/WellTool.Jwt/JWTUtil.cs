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

namespace WellTool.Jwt;

/// <summary>
/// JWT 工具类
/// </summary>
public static class JWTUtil
{
    /// <summary>
    /// 创建 JWT 令牌
    /// </summary>
    /// <param name="secret">密钥</param>
    /// <param name="issuer">签发人</param>
    /// <param name="subject">主题</param>
    /// <param name="audience">受众</param>
    /// <param name="expiration">过期时间（秒）</param>
    /// <returns>JWT 令牌</returns>
    public static string CreateToken(string secret, string? issuer = null, string? subject = null, string? audience = null, int expiration = 3600)
    {
        var jwt = new JWT
        {
            Payload = {
                Issuer = issuer,
                Subject = subject,
                Audience = audience,
                IssuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Expiration = DateTimeOffset.UtcNow.AddSeconds(expiration).ToUnixTimeSeconds()
            }
        };

        return jwt.Sign(secret);
    }

    /// <summary>
    /// 验证 JWT 令牌
    /// </summary>
    /// <param name="token">JWT 令牌</param>
    /// <param name="secret">密钥</param>
    /// <returns>是否验证通过</returns>
    public static bool VerifyToken(string token, string secret)
    {
        return JWT.Verify(token, secret);
    }

    /// <summary>
    /// 解析 JWT 令牌
    /// </summary>
    /// <param name="token">JWT 令牌</param>
    /// <returns>JWT 对象</returns>
    public static JWT ParseToken(string token)
    {
        return JWT.Parse(token);
    }

    /// <summary>
    /// 检查 JWT 令牌是否过期
    /// </summary>
    /// <param name="token">JWT 令牌</param>
    /// <returns>是否过期</returns>
    public static bool IsExpired(string token)
    {
        var jwt = JWT.Parse(token);
        var expiration = jwt.Payload.Expiration;
        if (!expiration.HasValue)
        {
            return false;
        }
        return expiration.Value < DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
