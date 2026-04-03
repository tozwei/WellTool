using WellTool.Core.Id;
using Xunit;

namespace WellTool.Core.Tests;

public class NanoIdTest
{
    [Fact]
    public void RandomNanoIdTest()
    {
        var id1 = NanoIdUtil.RandomNanoId();
        var id2 = NanoIdUtil.RandomNanoId();

        Assert.NotNull(id1);
        Assert.NotNull(id2);
        Assert.NotEqual(id1, id2);
        Assert.Equal(21, id1.Length);
        Assert.Equal(21, id2.Length);
    }

    [Fact]
    public void NanoIdWithSizeTest()
    {
        var id = NanoIdUtil.RandomNanoId(10);
        Assert.Equal(10, id.Length);

        var id2 = NanoIdUtil.RandomNanoId(32);
        Assert.Equal(32, id2.Length);
    }

    [Fact]
    public void NanoIdWithAlphabetTest()
    {
        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var id = NanoIdUtil.RandomNanoId(10, alphabet);

        Assert.Equal(10, id.Length);
        foreach (var c in id)
        {
            Assert.Contains(c.ToString(), alphabet);
        }
    }

    [Fact]
    public void NanoIdUniqueTest()
    {
        var set = new HashSet<string>();
        for (int i = 0; i < 10000; i++)
        {
            var id = NanoIdUtil.RandomNanoId();
            Assert.True(set.Add(id), $"Non-unique ID generated: {id}");
        }
    }

    [Fact]
    public void NanoIdEmptyAlphabetTest()
    {
        Assert.Throws<ArgumentException>(() => NanoIdUtil.RandomNanoId(10, ""));
    }

    [Fact]
    public void NanoIdNegativeSizeTest()
    {
        Assert.Throws<ArgumentException>(() => NanoIdUtil.RandomNanoId(-1));
    }

    [Fact]
    public void NanoIdZeroSizeTest()
    {
        var id = NanoIdUtil.RandomNanoId(0);
        Assert.Equal("", id);
    }
}
