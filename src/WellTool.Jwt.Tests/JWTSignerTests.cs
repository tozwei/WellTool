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

public class JWTSignerTests
{
    [Fact]
    public void TestHMacSigning()
    {
        // 测试 HMAC 签名
        var jwt = new WellTool.JWT.JWT();
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

        // 验证签名
        var parsedJwt = WellTool.JWT.JWT.Of(token);
        parsedJwt.SetKey(secret);
        var isValid = parsedJwt.Verify();
        Assert.True(isValid);
    }

    [Fact]
    public void TestDifferentSecrets()
    {
        // 测试不同密钥的签名和验证
        var jwt = new WellTool.JWT.JWT();
        jwt.Payload
            .SetIssuer("WellTool")
            .SetSubject("test")
            .SetIssuedAt(DateTime.UtcNow)
            .SetExpiresAt(DateTime.UtcNow.AddHours(1));

        var secret1 = "secretkey1";
        var secret2 = "secretkey2";

        jwt.SetKey(secret1);
        var token = jwt.Sign();
        
        // 使用正确的密钥验证
        var parsedJwt1 = WellTool.JWT.JWT.Of(token);
        parsedJwt1.SetKey(secret1);
        var isValidWithCorrectSecret = parsedJwt1.Verify();
        Assert.True(isValidWithCorrectSecret);

        // 使用错误的密钥验证
        var parsedJwt2 = WellTool.JWT.JWT.Of(token);
        parsedJwt2.SetKey(secret2);
        var isValidWithWrongSecret = parsedJwt2.Verify();
        Assert.False(isValidWithWrongSecret);
    }
}
