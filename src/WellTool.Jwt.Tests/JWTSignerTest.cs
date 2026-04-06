namespace WellTool.Jwt.Tests;

using WellTool.JWT;
using WellTool.JWT.Signers;

public class JWTSignerTest
{
    [Fact]
    public void HS256Test()
    {
        var signer = JWTSignerUtil.HS256("123456"u8.ToArray());
        Assert.NotNull(signer);
        Assert.Equal("HS256", signer.GetAlgorithm());
    }

    [Fact]
    public void HS384Test()
    {
        var signer = JWTSignerUtil.HS384("123456"u8.ToArray());
        Assert.NotNull(signer);
        Assert.Equal("HS384", signer.GetAlgorithm());
    }

    [Fact]
    public void HS512Test()
    {
        var signer = JWTSignerUtil.HS512("123456"u8.ToArray());
        Assert.NotNull(signer);
        Assert.Equal("HS512", signer.GetAlgorithm());
    }

    [Fact]
    public void NoneTest()
    {
        var signer = JWTSignerUtil.None();
        Assert.NotNull(signer);
        Assert.Equal("none", signer.GetAlgorithm());
    }

    [Fact]
    public void SignAndVerifyTest()
    {
        var signer = JWTSignerUtil.HS256("secret"u8.ToArray());
        var data = "test data"u8.ToArray();
        var signature = signer.Sign(data);
        Assert.NotNull(signature);

        var verified = signer.Verify(data, signature);
        Assert.True(verified);
    }

    [Fact]
    public void SignWithEmptyDataTest()
    {
        var signer = JWTSignerUtil.HS256("secret"u8.ToArray());
        var signature = signer.Sign(Array.Empty<byte>());
        Assert.NotNull(signature);
    }

    [Fact]
    public void VerifyWithInvalidSignatureTest()
    {
        var signer = JWTSignerUtil.HS256("secret"u8.ToArray());
        var data = "test data"u8.ToArray();
        var invalidSignature = new byte[] { 0, 1, 2, 3 };
        var verified = signer.Verify(data, invalidSignature);
        Assert.False(verified);
    }

    [Fact]
    public void CreateSignerByAlgorithmTest()
    {
        var signer = JWTSignerUtil.CreateSigner("HS256", "secret"u8.ToArray());
        Assert.NotNull(signer);
    }
}
