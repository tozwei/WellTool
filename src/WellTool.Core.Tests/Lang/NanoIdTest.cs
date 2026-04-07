using WellTool.Core.Lang.Id;
using Xunit;

namespace WellTool.Core.Lang.Id.Tests;

public class NanoIdTest
{
    [Fact]
    public void RandomNanoIdTest()
    {
        var id = NanoId.Generate();
        Assert.NotNull(id);
        Assert.Equal(21, id.Length);
    }

    [Fact]
    public void NanoIdWithSizeTest()
    {
        var id = NanoId.Generate(10);
        Assert.NotNull(id);
        Assert.Equal(10, id.Length);
    }

    [Fact]
    public void NanoIdWithAlphabetTest()
    {
        var id = NanoId.Generate(10, "ABCDEF");
        Assert.NotNull(id);
        Assert.Equal(10, id.Length);
        foreach (char c in id)
        {
            Assert.Contains(c, "ABCDEF");
        }
    }

    [Fact]
    public void NanoIdUniqueTest()
    {
        var id1 = NanoId.Generate();
        var id2 = NanoId.Generate();
        Assert.NotEqual(id1, id2);
    }

    [Fact]
    public void NanoIdSecureTest()
    {
        var id = NanoId.GenerateSecure();
        Assert.NotNull(id);
        Assert.Equal(21, id.Length);
    }
}
