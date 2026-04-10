namespace WellTool.Jwt.Tests;

using WellTool.Jwt;
using WellTool.Jwt.Signers;

public class JWTTest
{
    [Fact]
    public void CreateHs256Test()
    {
        byte[] key = "1234567890"u8.ToArray();
        var jwt = JWT.Create()
            .SetPayload("sub", "1234567890")
            .SetPayload("name", "looly")
            .SetPayload("admin", true)
            .SetExpiresAt(DateTime.Parse("2022-01-01"))
            .SetKey(key);

        var token = jwt.Sign();
        Assert.NotNull(token);
        Assert.Contains(".", token);

        var rightToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
            "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6Imxvb2x5IiwiYWRtaW4iOnRydWUsImV4cCI6MTY0MDk2NjQwMH0." +
            "8siIwEMHf-DRyUjVElS_yipb6Mo3c1z0wFiheGXWGQw";

        Assert.True(JWT.Of(rightToken).SetKey(key).Verify());
    }

    [Fact]
    public void ParseTest()
    {
        var rightToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9." +
            "eyJzdWIiOiIxMjM0NTY3ODkwIiwiYWRtaW4iOnRydWUsIm5hbWUiOiJsb29seSJ9." +
            "U2aQkC2THYV9L0fTN-yBBI7gmo5xhmvMhATtu8v0zEA";

        var jwt = JWT.Of(rightToken);

        Assert.True(jwt.SetKey("1234567890"u8.ToArray()).Verify());

        // header
        Assert.Equal("JWT", jwt.GetHeader(JWTHeader.TYPE));
        Assert.Equal("HS256", jwt.GetHeader(JWTHeader.ALGORITHM));
        Assert.Null(jwt.GetHeader(JWTHeader.CONTENT_TYPE));

        // payload
        Assert.Equal("1234567890", jwt.GetPayload("sub"));
        Assert.Equal("looly", jwt.GetPayload("name"));
        Assert.Equal(true, jwt.GetPayload("admin"));
    }

    [Fact]
    public void CreateNoneTest()
    {
        var jwt = JWT.Create()
            .SetPayload("sub", "1234567890")
            .SetPayload("name", "looly")
            .SetPayload("admin", true)
            .SetSigner(JwtSignerUtil.None());

        var token = jwt.Sign();
        Assert.NotNull(token);
    }

    [Fact]
    public void SetKeyBytesTest()
    {
        var jwt = JWT.Create()
            .SetPayload("sub", "test")
            .SetKey("secret"u8.ToArray());

        Assert.NotNull(jwt);
    }

    [Fact]
    public void SetKeyStringTest()
    {
        var jwt = JWT.Create()
            .SetPayload("sub", "test")
            .SetKey("secret");

        Assert.NotNull(jwt);
    }

    [Fact]
    public void GetHeaderTest()
    {
        var jwt = JWT.Of("eyJhbGciOiJIUzI1NiJ9.e30.gCx6PgYUPoZAAg-YJg4i3wV9E8q3jKLm4n5p6q7r8s");
        Assert.Equal("HS256", jwt.GetHeader(JWTHeader.ALGORITHM));
    }

    [Fact]
    public void GetPayloadTest()
    {
        var token = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIn0.test";
        var jwt = JWT.Of(token);
        Assert.Equal("1234567890", jwt.GetPayload("sub"));
    }

    [Fact]
    public void SetPayloadTest()
    {
        var jwt = JWT.Create()
            .SetPayload("name", "test")
            .SetPayload("age", 25)
            .SetSigner(JwtSignerUtil.None());

        var parsed = JWT.Of(jwt.Sign());
        Assert.Equal("test", parsed.GetPayload("name"));
        Assert.Equal(25, parsed.GetPayload("age"));
    }

    [Fact]
    public void SetExpiresAtTest()
    {
        var expires = DateTime.UtcNow.AddHours(1);
        var jwt = JWT.Create()
            .SetPayload("sub", "test")
            .SetExpiresAt(expires)
            .SetSigner(JwtSignerUtil.None());

        var token = jwt.Sign();
        Assert.Contains(".", token);
    }

    [Fact]
    public void SetNotBeforeTest()
    {
        var nbf = DateTime.UtcNow;
        var jwt = JWT.Create()
            .SetPayload("sub", "test")
            .SetNotBefore(nbf)
            .SetSigner(JwtSignerUtil.None());

        var token = jwt.Sign();
        Assert.Contains(".", token);
    }
}