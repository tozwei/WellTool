namespace WellTool.Jwt.Tests;

using WellTool.Jwt;

public class JWTValidatorTest
{
    [Fact]
    public void ValidateTest()
    {
        // 创建一个新的 JWT 令牌进行测试
        var key = "1234567890"u8.ToArray();
        var jwt = JWT.Create()
            .SetPayload("sub", "1234567890")
            .SetPayload("admin", true)
            .SetPayload("name", "olly")
            .SetKey(key);

        var token = jwt.Sign();
        var parsedJwt = JwtUtil.ParseToken(token);
        var validator = new JWTValidator(parsedJwt, key);
        Assert.True(validator.Validate());
    }

    [Fact]
    public void ValidateExpiredTest()
    {
        // Create an expired token
        var jwt = JWT.Create()
            .SetPayload("sub", "test")
            .SetExpiresAt(DateTime.UtcNow.AddDays(-1))
            .SetKey("secret"u8.ToArray());

        var token = jwt.Sign();
        var parsedJwt = JwtUtil.ParseToken(token);
        var validator = new JWTValidator(parsedJwt, "secret"u8.ToArray());
        Assert.False(validator.Validate());
    }

    [Fact]
    public void ValidateNotYetValidTest()
    {
        // Create a token not yet valid
        var jwt = JWT.Create()
            .SetPayload("sub", "test")
            .SetNotBefore(DateTime.UtcNow.AddDays(1))
            .SetKey("secret"u8.ToArray());

        var token = jwt.Sign();
        var parsedJwt = JwtUtil.ParseToken(token);
        var validator = new JWTValidator(parsedJwt, "secret"u8.ToArray());
        Assert.False(validator.Validate());
    }

    [Fact]
    public void ValidateWithWrongKeyTest()
    {
        var token = "eyJhbGciOiJIUzI1NiJ9." +
                "eyJzdWIiOiIxMjM0NTY3ODkwIiwiYWRtaW4iOnRydWUsIm5hbWUiOiJsb29seSJ9." +
                "U2aQkC2THYV9L0fTN-yBBI7gmo5xhmvMhATtu8v0zEA";

        var jwt = JwtUtil.ParseToken(token);
        var validator = new JWTValidator(jwt, "wrongkey"u8.ToArray());
        Assert.False(validator.Validate());
    }

    [Fact]
    public void ValidateValidTokenTest()
    {
        var key = "testsecret"u8.ToArray();
        var jwt = JWT.Create()
            .SetPayload("sub", "test")
            .SetExpiresAt(DateTime.UtcNow.AddHours(1))
            .SetKey(key);  

        var token = jwt.Sign();
        var parsedJwt = JwtUtil.ParseToken(token);
        var validator = new JWTValidator(parsedJwt, key);
        Assert.True(validator.Validate());
    }
}
