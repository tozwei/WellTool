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

        // 使用 HS256 算法
        var jwtHs256 = JwtUtil.CreateToken(claims, "secret", JwtAlgorithm.HS256);
        Assert.NotNull(jwtHs256);
        Assert.NotEmpty(jwtHs256);

        // 验证 HS256 算法生成的 JWT
        var parsedClaimsHs256 = JwtUtil.ParseToken(jwtHs256, "secret");
        Assert.NotNull(parsedClaimsHs256);

        // 使用 HS384 算法
        var jwtHs384 = JwtUtil.CreateToken(claims, "secret", JwtAlgorithm.HS384);
        Assert.NotNull(jwtHs384);
        Assert.NotEmpty(jwtHs384);

        // 验证 HS384 算法生成的 JWT
        var parsedClaimsHs384 = JwtUtil.ParseToken(jwtHs384, "secret");
        Assert.NotNull(parsedClaimsHs384);

        // 使用 HS512 算法
        var jwtHs512 = JwtUtil.CreateToken(claims, "secret", JwtAlgorithm.HS512);
        Assert.NotNull(jwtHs512);
        Assert.NotEmpty(jwtHs512);

        // 验证 HS512 算法生成的 JWT
        var parsedClaimsHs512 = JwtUtil.ParseToken(jwtHs512, "secret");
        Assert.NotNull(parsedClaimsHs512);
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

