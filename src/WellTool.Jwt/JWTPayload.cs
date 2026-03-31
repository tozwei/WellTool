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

using System;
using System.Collections.Generic;

namespace WellTool.JWT;

/// <summary>
/// JWT 负载
/// </summary>
public class JWTPayload : Claims, RegisteredPayload<JWTPayload>
{
    /// <summary>
    /// jwt签发者
    /// </summary>
    public const string ISSUER = "iss";
    /// <summary>
    /// jwt所面向的用户
    /// </summary>
    public const string SUBJECT = "sub";
    /// <summary>
    /// 接收jwt的一方
    /// </summary>
    public const string AUDIENCE = "aud";
    /// <summary>
    /// jwt的过期时间，这个过期时间必须要大于签发时间
    /// </summary>
    public const string EXPIRES_AT = "exp";
    /// <summary>
    /// 生效时间，定义在什么时间之前，该jwt都是不可用的.
    /// </summary>
    public const string NOT_BEFORE = "nbf";
    /// <summary>
    /// jwt的签发时间
    /// </summary>
    public const string ISSUED_AT = "iat";
    /// <summary>
    /// jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击。
    /// </summary>
    public const string JWT_ID = "jti";

    /// <summary>
    /// 设置 jwt签发者("iss")的Payload值
    /// </summary>
    /// <param name="issuer">jwt签发者</param>
    /// <returns>this</returns>
    public JWTPayload SetIssuer(string issuer)
    {
        return SetPayload(ISSUER, issuer);
    }

    /// <summary>
    /// 设置jwt所面向的用户("sub")的Payload值
    /// </summary>
    /// <param name="subject">jwt所面向的用户</param>
    /// <returns>this</returns>
    public JWTPayload SetSubject(string subject)
    {
        return SetPayload(SUBJECT, subject);
    }

    /// <summary>
    /// 设置接收jwt的一方("aud")的Payload值
    /// </summary>
    /// <param name="audience">接收jwt的一方</param>
    /// <returns>this</returns>
    public JWTPayload SetAudience(params string[] audience)
    {
        return SetPayload(AUDIENCE, audience);
    }

    /// <summary>
    /// 设置jwt的过期时间("exp")的Payload值，这个过期时间必须要大于签发时间
    /// </summary>
    /// <param name="expiresAt">jwt的过期时间</param>
    /// <returns>this</returns>
    public JWTPayload SetExpiresAt(DateTime expiresAt)
    {
        return SetPayload(EXPIRES_AT, ((DateTimeOffset)expiresAt).ToUnixTimeSeconds());
    }

    /// <summary>
    /// 设置不可用时间点界限("nbf")的Payload值
    /// </summary>
    /// <param name="notBefore">不可用时间点界限，在这个时间点之前，jwt不可用</param>
    /// <returns>this</returns>
    public JWTPayload SetNotBefore(DateTime notBefore)
    {
        return SetPayload(NOT_BEFORE, ((DateTimeOffset)notBefore).ToUnixTimeSeconds());
    }

    /// <summary>
    /// 设置jwt的签发时间("iat")
    /// </summary>
    /// <param name="issuedAt">签发时间</param>
    /// <returns>this</returns>
    public JWTPayload SetIssuedAt(DateTime issuedAt)
    {
        return SetPayload(ISSUED_AT, ((DateTimeOffset)issuedAt).ToUnixTimeSeconds());
    }

    /// <summary>
    /// 设置jwt的唯一身份标识("jti")
    /// </summary>
    /// <param name="jwtId">唯一身份标识</param>
    /// <returns>this</returns>
    public JWTPayload SetJWTId(string jwtId)
    {
        return SetPayload(JWT_ID, jwtId);
    }

    /// <summary>
    /// 设置Payload值
    /// </summary>
    /// <param name="name">payload名</param>
    /// <param name="value">payload值</param>
    /// <returns>this</returns>
    public JWTPayload SetPayload(string name, object value)
    {
        base.SetClaim(name, value);
        return this;
    }
}
