using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class StrMatcherTest
{
    [Fact]
    public void StringMatchTest()
    {
        var matcher = StrMatcher.Of("hello");
        Assert.Equal(0, matcher.Match("hello world", 0));
        Assert.Equal(-1, matcher.Match("world hello", 0));
    }

    [Fact]
    public void CharMatchTest()
    {
        var matcher = StrMatcher.Of('h');
        Assert.Equal(0, matcher.Match("hello", 0));
        Assert.Equal(6, matcher.Match("world h", 0));
        Assert.Equal(-1, matcher.Match("world", 0));
    }

    [Fact]
    public void MatcherLengthTest()
    {
        var stringMatcher = StrMatcher.Of("hello");
        Assert.Equal(5, stringMatcher.Length);

        var charMatcher = StrMatcher.Of('h');
        Assert.Equal(1, charMatcher.Length);
    }

    [Fact]
    public void MatcherTest()
    {
        var stringMatcher = StrMatcher.Of("hello");
        Assert.Equal("hello", stringMatcher.Matcher);

        var charMatcher = StrMatcher.Of('h');
        Assert.Equal("h", charMatcher.Matcher);
    }
}
