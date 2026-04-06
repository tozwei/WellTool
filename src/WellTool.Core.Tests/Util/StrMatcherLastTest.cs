using WellTool.Core.Text;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class StrMatcherLastTest
{
    [Fact]
    public void MatchTest()
    {
        var matcher = StrMatcher.Of("test");
        var result = matcher.Match("test string", 0);
        Assert.Equal(0, result);
    }

    [Fact]
    public void MatchCharTest()
    {
        var matcher = StrMatcher.Of('t');
        var result = matcher.Match("test", 0);
        Assert.Equal(0, result);
    }

    [Fact]
    public void MatchNotFoundTest()
    {
        var matcher = StrMatcher.Of("not found");
        var result = matcher.Match("test string", 0);
        Assert.Equal(-1, result);
    }

    [Fact]
    public void LengthTest()
    {
        var matcher = StrMatcher.Of("test");
        Assert.Equal(4, matcher.Length);
    }

    [Fact]
    public void MatcherTest()
    {
        var matcher = StrMatcher.Of("test");
        Assert.Equal("test", matcher.Matcher);
    }
}

