using WellTool.Core.Codec;
using Xunit;

namespace WellTool.Core.Tests;

public class DigestUtilLastTest
{
    [Fact]
    public void Md5Test()
    {
        var hash = DigestUtil.Md5("Hello");
        Assert.NotNull(hash);
    }

    [Fact]
    public void Md5BytesTest()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes("Hello");
        var hash = DigestUtil.Md5Bytes(bytes);
        Assert.Equal(16, hash.Length);
    }

    [Fact]
    public void Sha256Test()
    {
        var hash = DigestUtil.Sha256("Hello");
        Assert.NotNull(hash);
    }

    [Fact]
    public void Sha1Test()
    {
        var hash = DigestUtil.Sha1("Hello");
        Assert.NotNull(hash);
    }

    [Fact]
    public void Sha512Test()
    {
        var hash = DigestUtil.Sha512("Hello");
        Assert.NotNull(hash);
    }

    [Fact]
    public void HmacMd5Test()
    {
        var hash = DigestUtil.HmacMd5("Hello", "key");
        Assert.NotNull(hash);
    }

    [Fact]
    public void HmacSha256Test()
    {
        var hash = DigestUtil.HmacSha256("Hello", "key");
        Assert.NotNull(hash);
    }
}
