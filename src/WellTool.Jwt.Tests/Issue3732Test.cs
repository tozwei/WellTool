namespace WellTool.Jwt.Tests;

using Xunit;
using WellTool.Jwt;
using System.Collections.Generic;

public class Issue3732Test
{
    [Fact]
    public void TestJwtWithExpiration()
    {
        // 测试带有过期时间的 JWT
        var claims = new Dictionary<string, object>
        {
            { "sub", "1234567890" },
            { "name", "John Doe" },
            { "iat", 1516239022 },
            { "exp", 1516239022 + 3600 } // 1小时后过期
        };

        var jwt = JwtUtil.CreateToken(claims, "secret");
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        var parsedClaims = JwtUtil.ParseToken(jwt, "secret");
        Assert.NotNull(parsedClaims);
        Assert.True(parsedClaims.ContainsKey("exp"));
    }

    [Fact]
    public void TestJwtWithIssuer()
    {
        // 测试带有签发者的 JWT
        var claims = new Dictionary<string, object>
        {
            { "sub", "1234567890" },
            { "name", "John Doe" },
            { "iat", 1516239022 },
            { "iss", "https://example.com" }
        };

        var jwt = JwtUtil.CreateToken(claims, "secret");
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        var parsedClaims = JwtUtil.ParseToken(jwt, "secret");
        Assert.NotNull(parsedClaims);
        Assert.True(parsedClaims.ContainsKey("iss"));
        Assert.Equal("https://example.com", parsedClaims["iss"]);
    }

    [Fact]
    public void TestJwtWithAudience()
    {
        // 测试带有受众的 JWT
        var claims = new Dictionary<string, object>
        {
            { "sub", "1234567890" },
            { "name", "John Doe" },
            { "iat", 1516239022 },
            { "aud", "https://api.example.com" }
        };

        var jwt = JwtUtil.CreateToken(claims, "secret");
        Assert.NotNull(jwt);
        Assert.NotEmpty(jwt);

        var parsedClaims = JwtUtil.ParseToken(jwt, "secret");
        Assert.NotNull(parsedClaims);
        Assert.True(parsedClaims.ContainsKey("aud"));
        Assert.Equal("https://api.example.com", parsedClaims["aud"]);
    }
}

