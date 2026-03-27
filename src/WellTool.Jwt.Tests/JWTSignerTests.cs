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

public class JWTSignerTests
{
    [Fact]
    public void TestHMacSigning()
    {
        // 测试 HMAC 签名
        var jwt = new JWT
        {
            Payload = {
                Issuer = "WellTool",
                Subject = "test",
                IssuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Expiration = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()
            }
        };

        var secret = "secretkey";
        var token = jwt.Sign(secret);
        Assert.NotNull(token);
        Assert.NotEmpty(token);

        // 验证签名
        var isValid = JWT.Verify(token, secret);
        Assert.True(isValid);
    }

    [Fact]
    public void TestDifferentSecrets()
    {
        // 测试不同密钥的签名和验证
        var jwt = new JWT
        {
            Payload = {
                Issuer = "WellTool",
                Subject = "test",
                IssuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Expiration = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()
            }
        };

        var secret1 = "secretkey1";
        var secret2 = "secretkey2";

        var token = jwt.Sign(secret1);
        
        // 使用正确的密钥验证
        var isValidWithCorrectSecret = JWT.Verify(token, secret1);
        Assert.True(isValidWithCorrectSecret);

        // 使用错误的密钥验证
        var isValidWithWrongSecret = JWT.Verify(token, secret2);
        Assert.False(isValidWithWrongSecret);
    }
}
