using WellTool.Core.Codec;
using Xunit;

namespace WellTool.Core.Tests;

public class HashUtilTest
{
    [Fact]
    public void Md5Test()
    {
        var hash = HashUtil.Md5("Hello");
        Assert.NotNull(hash);
        Assert.Equal(32, hash.Length);
    }

    [Fact]
    public void Sha1Test()
    {
        var hash = HashUtil.Sha1("Hello");
        Assert.NotNull(hash);
        Assert.Equal(40, hash.Length);
    }

    [Fact]
    public void Sha256Test()
    {
        var hash = HashUtil.Sha256("Hello");
        Assert.NotNull(hash);
        Assert.Equal(64, hash.Length);
    }

    [Fact]
    public void Sha512Test()
    {
        var hash = HashUtil.Sha512("Hello");
        Assert.NotNull(hash);
        Assert.Equal(128, hash.Length);
    }

    [Fact]
    public void Crc32Test()
    {
        var hash = HashUtil.Crc32("Hello");
        Assert.True(hash >= 0);
    }

    [Fact]
    public void Adler32Test()
    {
        var hash = HashUtil.Adler32("Hello");
        Assert.True(hash >= 0);
    }
}
