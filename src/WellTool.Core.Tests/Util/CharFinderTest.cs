using Xunit;

namespace WellTool.Core.Tests;

public class CharFinderTest
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
        Assert.Equal(-1, "aaa".IndexOf('b'));
    }

    [Fact]
    public void FindLastTest()
    {
        Assert.Equal(2, "abb".LastIndexOf('b'));
    }

    [Fact]
    public void ContainsAnyTest()
    {
        Assert.True("abc".Contains('a') || "abc".Contains('x'));
        Assert.False("abc".Contains('x') || "abc".Contains('y'));
    }

    [Fact]
    public void ContainsTest()
    {
        Assert.True("abc".Contains('b'));
        Assert.False("abc".Contains('x'));
    }

    [Fact]
    public void IndexOfTest()
    {
        Assert.Equal(1, "abc".IndexOf('b'));
        Assert.Equal(-1, "abc".IndexOf('x'));
    }

    [Fact]
    public void LastIndexOfTest()
    {
        Assert.Equal(2, "abb".LastIndexOf('b'));
    }
}
