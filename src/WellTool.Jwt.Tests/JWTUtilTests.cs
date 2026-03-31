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

using Xunit;

namespace WellTool.Jwt.Tests;

public class JWTUtilTests
{
    [Fact]
    public void TestCreateToken()
    {
        // 测试创建 JWT token
        var secret = "secretkey";
        var issuer = "WellTool";
        var subject = "test";
        var audience = "users";
        var token = WellTool.JWT.JWTUtil.CreateToken(secret, issuer, subject, audience);
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }

    [Fact]
    public void TestParseToken()
    {
        // 测试解析 JWT token
        var secret = "secretkey";
        var token = WellTool.JWT.JWTUtil.CreateToken(secret);
        var parsedToken = WellTool.JWT.JWTUtil.ParseToken(token);
        Assert.NotNull(parsedToken);
    }

    [Fact]
    public void TestVerifyToken()
    {
        // 测试验证 JWT token
        var secret = "secretkey";
        var token = WellTool.JWT.JWTUtil.CreateToken(secret);
        var isValid = WellTool.JWT.JWTUtil.VerifyToken(token, secret);
        Assert.True(isValid);
    }

    [Fact]
    public void TestIsExpired()
    {
        // 测试检查 JWT token 是否过期
        var secret = "secretkey";
        var token = WellTool.JWT.JWTUtil.CreateToken(secret);
        var isExpired = WellTool.JWT.JWTUtil.IsExpired(token);
        Assert.False(isExpired);
    }
}
