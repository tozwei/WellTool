namespace WellTool.Jwt.Tests;

using Xunit;
using WellTool.Jwt;
using System.Collections.Generic;

public class Issue3205Test
{
    [Fact]
    public void TestJwtWithNullClaims()
    {
        // 测试带有 null 值的声明的 JWT
        var claims = new Dictionary<string, object>
        {
            { "sub", "1234567890" },
            { "name", "John Doe" },
            { "iat", 1516239022 },
            { "optional", null }
        };

        var jwt = JwtUtil.CreateToken(claims, "secret");
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        var parsedClaims = JwtUtil.ParseToken(jwt, "secret");
        Assert.NotNull(parsedClaims);
        Assert.True(parsedClaims.ContainsKey("optional"));
        Assert.Null(parsedClaims["optional"]);
    }

    [Fact]
    public void TestJwtWithEmptyClaims()
    {
        // 测试带有空声明的 JWT
        var claims = new Dictionary<string, object>();

        var jwt = JwtUtil.CreateToken(claims, "secret");
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        var parsedClaims = JwtUtil.ParseToken(jwt, "secret");
        Assert.NotNull(parsedClaims);
    }

    [Fact]
    public void TestJwtWithLongSecret()
    {
        // 测试使用长密钥的 JWT
        var claims = new Dictionary<string, object>
        {
            { "sub", "1234567890" },
            { "name", "John Doe" },
            { "iat", 1516239022 }
        };

        // 使用长密钥
        var longSecret = new string('a', 100);
        var jwt = JwtUtil.CreateToken(claims, longSecret);
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        var parsedClaims = JwtUtil.ParseToken(jwt, longSecret);
        Assert.NotNull(parsedClaims);
        Assert.True(parsedClaims.ContainsKey("sub"));
    }
}

