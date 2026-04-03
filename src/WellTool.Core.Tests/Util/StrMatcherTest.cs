using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class StrMatcherTest
{
    [Fact]
    public void IsMatchTest()
    {
        Assert.True(StrMatcher.IsMatch("hello", "h*"));
        Assert.True(StrMatcher.IsMatch("hello", "h?llo"));
        Assert.False(StrMatcher.IsMatch("world", "hello"));
    }

    [Fact]
    public void MatchTest()
    {
        Assert.True(StrMatcher.Match("test", "t*"));
        Assert.False(StrMatcher.Match("test", "x*"));
    }

    [Fact]
    public void LikeTest()
    {
        Assert.True(StrMatcher.Like("test", "*est"));
        Assert.True(StrMatcher.Like("test", "t??t"));
    }

    [Fact]
    public void IsRegexMatchTest()
    {
        Assert.True(StrMatcher.IsRegexMatch("hello123", "\\d+"));
    }

    [Fact]
    public void CharMatcherTest()
    {
        Assert.True(StrMatcher.CharMatcher('a').Matches('a'));
        Assert.False(StrMatcher.CharMatcher('a').Matches('b'));
    }
}
