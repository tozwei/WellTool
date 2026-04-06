using WellTool.Core.Text;
using System;
using Xunit;

namespace WellTool.Core.Tests;

public class StrSplitterLastTest
{
    [Fact]
    public void SplitTest()
    {
        var result = StrSplitter.SplitByChar("a,b,c", ',');
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void SplitByCharTest()
    {
        var result = StrSplitter.SplitByChar("a-b-c", '-');
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void SplitIgnoreBlankTest()
    {
        var result = StrSplitter.SplitByChar("a,,b,,c", ',', StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void SplitTrimTest()
    {
        var result = StrSplitter.SplitByChar(" a , b , c ", ',', -1, true);
        Assert.Equal("a", result[0]);
    }
}

