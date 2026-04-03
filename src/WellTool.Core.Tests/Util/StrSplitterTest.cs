using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class StrSplitterTest
{
    [Fact]
    public void SplitTest()
    {
        var result = StrSplitter.Split("a,b,c", ',');
        Assert.Equal(3, result.Length);
        Assert.Equal("a", result[0]);
        Assert.Equal("b", result[1]);
        Assert.Equal("c", result[2]);
    }

    [Fact]
    public void SplitByCharTest()
    {
        var result = StrSplitter.SplitByChar("a-b-c", '-');
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void SplitByStringTest()
    {
        var result = StrSplitter.SplitByString("a||b||c", "||");
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void SplitIgnoreBlankTest()
    {
        var result = StrSplitter.Split("a,,b,,c", ',', true);
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void SplitTrimTest()
    {
        var result = StrSplitter.Split(" a , b , c ", ',', -1, true);
        Assert.Equal("a", result[0]);
        Assert.Equal("b", result[1]);
        Assert.Equal("c", result[2]);
    }

    [Fact]
    public void SplitByRegexTest()
    {
        var result = StrSplitter.SplitByRegex("a1b2c", "\\d");
        Assert.Equal(3, result.Length);
    }
}
