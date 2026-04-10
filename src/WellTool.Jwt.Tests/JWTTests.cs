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

public class JWTTests
{
    [Fact]
    public void TestJWTConstruction()
    {
        // 测试 JWT 构造函数
        var jwt = new WellTool.Jwt.JWT();
        Assert.NotNull(jwt);
        Assert.NotNull(jwt.Header);
        Assert.NotNull(jwt.Payload);
    }

    [Fact]
    public void TestSign()
    {
        // 测试 JWT 签名
        var jwt = new WellTool.Jwt.JWT();
        jwt.Payload
            .SetIssuer("WellTool")
            .SetSubject("test")
            .SetIssuedAt(DateTime.UtcNow)
            .SetExpiresAt(DateTime.UtcNow.AddHours(1));

        var secret = "secretkey";
        jwt.SetKey(secret);
        var token = jwt.Sign();
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }

    [Fact]
    public void TestParse()
    {
        // 测试 JWT 解析
        var jwt = new WellTool.Jwt.JWT();
        jwt.Payload
            .SetIssuer("WellTool")
            .SetSubject("test")
            .SetIssuedAt(DateTime.UtcNow)
            .SetExpiresAt(DateTime.UtcNow.AddHours(1));

        var secret = "secretkey";
        jwt.SetKey(secret);
        var token = jwt.Sign();
        var parsedJwt = WellTool.Jwt.JWT.Of(token);
        Assert.NotNull(parsedJwt);
        Assert.NotNull(parsedJwt.Header);
        Assert.NotNull(parsedJwt.Payload);
    }

    [Fact]
    public void TestVerify()
    {
        // 测试 JWT 验证
        var jwt = new WellTool.Jwt.JWT();
        jwt.Payload
            .SetIssuer("WellTool")
            .SetSubject("test")
            .SetIssuedAt(DateTime.UtcNow)
            .SetExpiresAt(DateTime.UtcNow.AddHours(1));

        var secret = "secretkey";
        jwt.SetKey(secret);
        var token = jwt.Sign();
        var parsedJwt = WellTool.Jwt.JWT.Of(token);
        parsedJwt.SetKey(secret);
        var isValid = parsedJwt.Verify();
        Assert.True(isValid);
    }
}
