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

namespace WellTool.Jwt.Tests;

public class JWTUtilTests
{
    [Fact]
    public void TestCreateToken()
    {
        // 测试创建 JWT token
        var payload = new { userId = 1, username = "testuser" };
        var secret = "secretkey";
        var token = WellTool.Jwt.JWTUtil.CreateToken(payload, secret);
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }

    [Fact]
    public void TestParseToken()
    {
        // 测试解析 JWT token
        var payload = new { userId = 1, username = "testuser" };
        var secret = "secretkey";
        var token = WellTool.Jwt.JWTUtil.CreateToken(payload, secret);
        var parsedPayload = WellTool.Jwt.JWTUtil.ParseToken(token, secret);
        Assert.NotNull(parsedPayload);
    }

    [Fact]
    public void TestValidateToken()
    {
        // 测试验证 JWT token
        var payload = new { userId = 1, username = "testuser" };
        var secret = "secretkey";
        var token = WellTool.Jwt.JWTUtil.CreateToken(payload, secret);
        var isValid = WellTool.Jwt.JWTUtil.ValidateToken(token, secret);
        Assert.True(isValid);
    }
}
