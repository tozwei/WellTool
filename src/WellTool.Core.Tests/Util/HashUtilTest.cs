using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class HashUtilTest
{
    [Fact]
    public void Md5Test()
    {
        var hash = HashUtil.MD5("Hello");
        Assert.NotNull(hash);
        Assert.Equal(32, hash.Length);
    }

    [Fact]
    public void Sha1Test()
    {
        var hash = HashUtil.SHA1("Hello");
        Assert.NotNull(hash);
        Assert.Equal(40, hash.Length);
    }

    [Fact]
    public void Sha256Test()
    {
        var hash = HashUtil.SHA256("Hello");
        Assert.NotNull(hash);
        Assert.Equal(64, hash.Length);
    }

    [Fact]
    public void Sha512Test()
    {
        var hash = HashUtil.SHA512("Hello");
        Assert.NotNull(hash);
        Assert.Equal(128, hash.Length);
    }

    [Fact]
    public void FnvHashTest()
    {
        var hash = HashUtil.FnvHash("Hello");
        Assert.True(hash != 0);
    }
}
