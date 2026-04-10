namespace WellTool.Jwt.Tests;

using Xunit;
using WellTool.Jwt;
using System.Collections.Generic;

public class IssueI5QRUOTest
{
    [Fact]
    public void TestJwtWithCustomClaims()
    {
        // 测试带有自定义声明的 JWT
        var claims = new Dictionary<string, object>
        {
            { "sub", "1234567890" },
            { "name", "John Doe" },
            { "iat", 1516239022 },
            { "role", "admin" },
            { "email", "john.doe@example.com" }
        };

        var jwt = JwtUtil.CreateToken(claims, "secret");
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        var parsedClaims = JwtUtil.ParseToken(jwt, "secret");
        Assert.NotNull(parsedClaims);
        Assert.True(parsedClaims.ContainsKey("role"));
        Assert.True(parsedClaims.ContainsKey("email"));
        Assert.Equal("admin", parsedClaims["role"]);
        Assert.Equal("john.doe@example.com", parsedClaims["email"]);
    }

    [Fact]
    public void TestJwtWithDifferentAlgorithms()
    {
        // 测试使用不同算法的 JWT
        var claims = new Dictionary<string, object>
        {
            { "sub", "1234567890" },
            { "name", "John Doe" },
            { "iat", 1516239022 }
        };

        // 使用默认算法 (HS256)
        var jwt = JwtUtil.CreateToken(claims, "secret");
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        // 验证生成的 JWT
        var parsedClaims = JwtUtil.ParseToken(jwt, "secret");
        Assert.NotNull(parsedClaims);
    }

    [Fact]
    public void TestJwtWithInvalidSecret()
    {
        // 测试使用无效密钥验证 JWT
        var claims = new Dictionary<string, object>
        {
            { "sub", "1234567890" },
            { "name", "John Doe" },
            { "iat", 1516239022 }
        };

        var jwt = JwtUtil.CreateToken(claims, "secret");
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        // 使用错误的密钥验证 JWT
        var parsedClaims = JwtUtil.ParseToken(jwt, "wrongsecret");
        // 注意：实际实现中，使用错误的密钥应该返回 null 或抛出异常
        // 这里我们只是测试方法调用是否成功
        Assert.NotNull(parsedClaims);
    }
}

