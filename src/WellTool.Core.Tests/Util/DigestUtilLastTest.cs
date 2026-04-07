using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class DigestUtilLastTest
{
    [Fact]
    public void Md5Test()
    {
        var hash = HashUtil.MD5("Hello");
        Assert.NotNull(hash);
    }

    [Fact]
    public void Md5BytesTest()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes("Hello");
        using var md5 = System.Security.Cryptography.MD5.Create();
        var hash = md5.ComputeHash(bytes);
        Assert.Equal(16, hash.Length);
    }

    [Fact]
    public void Sha256Test()
    {
        var hash = HashUtil.SHA256("Hello");
        Assert.NotNull(hash);
    }

    [Fact]
    public void Sha1Test()
    {
        var hash = HashUtil.SHA1("Hello");
        Assert.NotNull(hash);
    }

    [Fact]
    public void Sha512Test()
    {
        var hash = HashUtil.SHA512("Hello");
        Assert.NotNull(hash);
    }

    [Fact]
    public void HmacMd5Test()
    {
        var hash = HashUtil.HMACMD5("Hello", "key");
        Assert.NotNull(hash);
    }

    [Fact]
    public void HmacSha256Test()
    {
        var hash = HashUtil.HMACSHA256("Hello", "key");
        Assert.NotNull(hash);
    }
}
