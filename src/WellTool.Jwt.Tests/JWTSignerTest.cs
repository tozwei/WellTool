namespace WellTool.Jwt.Tests;

using Xunit;
using WellTool.Jwt;
using WellTool.Jwt.Signers;

public class JWTSignerTest
{
    [Fact]
    public void HS256Test()
    {
        var signer = WellTool.Jwt.Signers.JwtSignerUtil.HS256("123456"u8.ToArray());
        Assert.NotNull(signer);
        Assert.Equal("HS256", signer.GetAlgorithm());
    }

    [Fact]
    public void HS384Test()
    {
        var signer = JwtSignerUtil.HS384("123456"u8.ToArray());
        Assert.NotNull(signer);
        Assert.Equal("HS384", signer.GetAlgorithm());
    }

    [Fact]
    public void HS512Test()
    {
        var signer = JwtSignerUtil.HS512("123456"u8.ToArray());
        Assert.NotNull(signer);
        Assert.Equal("HS512", signer.GetAlgorithm());
    }

    [Fact]
    public void NoneTest()
    {
        var signer = JwtSignerUtil.None();
        Assert.NotNull(signer);
        Assert.Equal("none", signer.GetAlgorithm());
    }

    [Fact]
    public void SignAndVerifyTest()
    {
        var signer = JwtSignerUtil.HS256("secret"u8.ToArray());
        var headerBase64 = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
        var payloadBase64 = "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ";
        var signature = signer.Sign(headerBase64, payloadBase64);
        Assert.NotNull(signature);

        var verified = signer.Verify(headerBase64, payloadBase64, signature);
        Assert.True(verified);
    }

    [Fact]
    public void SignWithEmptyDataTest()
    {
        var signer = JwtSignerUtil.HS256("secret"u8.ToArray());
        var headerBase64 = "";
        var payloadBase64 = "";
        var signature = signer.Sign(headerBase64, payloadBase64);
        Assert.NotNull(signature);
    }

    [Fact]
    public void VerifyWithInvalidSignatureTest()
    {
        var signer = JwtSignerUtil.HS256("secret"u8.ToArray());
        var headerBase64 = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
        var payloadBase64 = "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ";
        var invalidSignature = "invalid";
        var verified = signer.Verify(headerBase64, payloadBase64, invalidSignature);
        Assert.False(verified);
    }

    [Fact]
    public void CreateSignerByAlgorithmTest()
    {
        var signer = JwtSignerUtil.CreateSigner("HS256", "secret"u8.ToArray());
        Assert.NotNull(signer);
    }
}
