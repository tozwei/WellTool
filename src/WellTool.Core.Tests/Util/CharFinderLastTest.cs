using Xunit;

namespace WellTool.Core.Tests;

public class CharFinderLastTest
{
    [Fact]
    public void FindTest()
    {
        Assert.True("abc".Contains('a'));
    }

    [Fact]
    public void FindFirstTest()
    {
        Assert.Equal(1, "abc".IndexOf('b'));
    }

    [Fact]
    public void ContainsTest()
    {
        Assert.True("abc".Contains('b'));
        Assert.False("abc".Contains('d'));
    }
}
