using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class StrMatcherLastTest
{
    [Fact]
    public void IsMatchTest()
    {
        Assert.True(StrMatcher.IsMatch("hello", "h*"));
        Assert.False(StrMatcher.IsMatch("world", "hello"));
    }

    [Fact]
    public void MatchTest()
    {
        Assert.True(StrMatcher.Match("test", "t*"));
    }

    [Fact]
    public void LikeTest()
    {
        Assert.True(StrMatcher.Like("test", "*est"));
    }
}
