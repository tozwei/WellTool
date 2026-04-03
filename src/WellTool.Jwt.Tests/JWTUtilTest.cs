namespace WellTool.Jwt.Tests;

using Well.Jwt;

public class JWTUtilTest
{
    [Fact]
    public void CreateTokenTest()
    {
        byte[] key = "1234"u8.ToArray();
        var map = new Dictionary<string, object>
        {
            { "uid", 123 },
            { "expire_time", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + 1000 * 60 * 60 * 24 * 15 }
        };

        var token = JWTUtil.CreateToken(map, key);
        Assert.NotNull(token);
        Assert.Contains(".", token);
    }

    [Fact]
    public void ParseTokenTest()
    {
        var rightToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9." +
                "eyJzdWIiOiIxMjM0NTY3ODkwIiwiYWRtaW4iOnRydWUsIm5hbWUiOiJsb29seSJ9." +
                "U2aQkC2THYV9L0fTN-yBBI7gmo5xhmvMhATtu8v0zEA";

        var jwt = JWTUtil.ParseToken(rightToken);

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
        Assert.Throws<ArgumentNullException>(() => JWTUtil.ParseToken(null));
    }

    [Fact]
    public void VerifyTokenTest()
    {
        var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
                "eyJ1c2VyX25hbWUiOiJhZG1pbiIsInNjb3BlIjpbImFsbCJdLCJleHAiOjE2MjQwMDQ4MjIsInVzZXJJZCI6MSwiYXV0aG9yaXRpZXMiOlsiUk9MRV_adminIiwiU1lfTUVOVV8xIiwiUk9MRV91c2VyIiwiU1lfTUVOVV8yIl0sImp0aSI6ImQ0YzVlYjgwLTA5ZTctNGU0ZC1hZTg3LTVkNGI5M2FhNmFiNiIsImNsaWVudF9pZCI6ImhhbmR5LXNob3AifQ." +
                "aixF1eKlAKS_k3ynFnStE7-IRGiD5YaqznvK2xEjBew";

        var verify = JWTUtil.Verify(token, "123456"u8.ToArray());
        Assert.True(verify);
    }
}
