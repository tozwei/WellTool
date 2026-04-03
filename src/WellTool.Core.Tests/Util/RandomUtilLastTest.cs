using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class RandomUtilLastTest
{
    [Fact]
    public void RandomIntTest()
    {
        var random = RandomUtil.RandomInt(1, 100);
        Assert.InRange(random, 1, 100);
    }

    [Fact]
    public void RandomLongTest()
    {
        var random = RandomUtil.RandomLong(1, 1000);
        Assert.InRange(random, 1, 1000);
    }

    [Fact]
    public void RandomDoubleTest()
    {
        var random = RandomUtil.RandomDouble(1.0, 10.0);
        Assert.InRange(random, 1.0, 10.0);
    }

    [Fact]
    public void RandomBytesTest()
    {
        var bytes = RandomUtil.RandomBytes(16);
        Assert.Equal(16, bytes.Length);
    }

    [Fact]
    public void RandomStringTest()
    {
        var str = RandomUtil.RandomString(32);
        Assert.Equal(32, str.Length);
    }

    [Fact]
    public void RandomEleTest()
    {
        var list = new System.Collections.Generic.List<int> { 1, 2, 3, 4, 5 };
        var randomEle = RandomUtil.RandomEle(list);
        Assert.Contains(randomEle, list);
    }

    [Fact]
    public void RandomUUIDTest()
    {
        var uuid = RandomUtil.RandomUUID();
        Assert.NotNull(uuid);
    }
}
