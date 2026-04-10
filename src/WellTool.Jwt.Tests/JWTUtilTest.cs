namespace WellTool.Jwt.Tests;

using WellTool.Jwt;

public class JwtUtilTest
{
    [Fact]
    public void CreateTokenTest()
    {
        byte[] key = "1234"u8.ToArray();
        var token = JwtUtil.CreateToken("secret");
        Assert.NotNull(token);
        Assert.Contains(".", token);
    }

    [Fact]
    public void ParseTokenTest()
    {
        var rightToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9." +
                "eyJzdWIiOiIxMjM0NTY3ODkwIiwiYWRtaW4iOnRydWUsIm5hbWUiOiJsb29seSJ9." +
                "U2aQkC2THYV9L0fTN-yBBI7gmo5xhmvMhATtu8v0zEA";

        var jwt = JwtUtil.ParseToken(rightToken);

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
    public void ParseNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => JwtUtil.ParseToken(null));
    }

    [Fact]
    public void VerifyTokenTest()
    {
        // 创建一个新的 JWT 令牌进行测试
        var token = JwtUtil.CreateToken("123456");
        var verify = JwtUtil.VerifyToken(token, "123456");
        Assert.True(verify);
    }
}
