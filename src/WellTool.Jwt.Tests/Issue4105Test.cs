namespace WellTool.Jwt.Tests;

using Xunit;
using WellTool.Jwt;
using System.Collections.Generic;

public class Issue4105Test
{
    [Fact]
    public void TestJwtWithNumericClaims()
    {
        // 测试带有数字类型声明的 JWT
        var claims = new Dictionary<string, object>
        {
            { "sub", "1234567890" },
            { "name", "John Doe" },
            { "iat", 1516239022 },
            { "age", 30 },
            { "score", 95.5 }
        };

        var jwt = JwtUtil.CreateToken(claims, "secret");
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        var parsedClaims = JwtUtil.ParseToken(jwt, "secret");
        Assert.NotNull(parsedClaims);
        Assert.True(parsedClaims.ContainsKey("age"));
        Assert.True(parsedClaims.ContainsKey("score"));
        Assert.Equal(30, parsedClaims["age"]);
        Assert.Equal(95.5, parsedClaims["score"]);
    }

    [Fact]
    public void TestJwtWithBooleanClaims()
    {
        // 测试带有布尔类型声明的 JWT
        var claims = new Dictionary<string, object>
        {
            { "sub", "1234567890" },
            { "name", "John Doe" },
            { "iat", 1516239022 },
            { "admin", true },
            { "active", false }
        };

        var jwt = JwtUtil.CreateToken(claims, "secret");
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        var parsedClaims = JwtUtil.ParseToken(jwt, "secret");
        Assert.NotNull(parsedClaims);
        Assert.True(parsedClaims.ContainsKey("admin"));
        Assert.True(parsedClaims.ContainsKey("active"));
        Assert.True((bool)parsedClaims["admin"]);
        Assert.False((bool)parsedClaims["active"]);
    }

    [Fact]
    public void TestJwtWithArrayClaims()
    {
        // 测试带有数组类型声明的 JWT
        var claims = new Dictionary<string, object>
        {
            { "sub", "1234567890" },
            { "name", "John Doe" },
            { "iat", 1516239022 },
            { "roles", new string[] { "admin", "user", "editor" } }
        };

        var jwt = JwtUtil.CreateToken(claims, "secret");
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        var parsedClaims = JwtUtil.ParseToken(jwt, "secret");
        Assert.NotNull(parsedClaims);
        Assert.True(parsedClaims.ContainsKey("roles"));
        var roles = parsedClaims["roles"] as System.Collections.IEnumerable;
        Assert.NotNull(roles);
    }
}

