using WellTool.Core.Text;
using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class ReUtilLastTest
{
    [Fact]
    public void IsMatchTest()
    {
        Assert.True(ReUtil.IsMatch("\\d+", "123"));
        Assert.False(ReUtil.IsMatch("\\d+", "abc"));
    }

    [Fact]
    public void MatchTest()
    {
        var match = ReUtil.Match("\\d+", "abc123def");
        Assert.NotNull(match);
        Assert.Equal("123", match.Value);
    }

    [Fact]
    public void GetMatchsTest()
    {
        var matchs = ReUtil.GetMatchs("\\d+", "a1b22c333");
        Assert.Equal(3, matchs.Count);
    }

    [Fact]
    public void ReplaceAllTest()
    {
        var result = ReUtil.ReplaceAll("a1b2c3", "\\d", "_");
        Assert.Equal("a_b_c_", result);
    }

    [Fact]
    public void ReplaceFirstTest()
    {
        var result = ReUtil.ReplaceFirst("a1b2c3", "\\d", "_");
        Assert.Equal("a_b2c3", result);
    }

    [Fact]
    public void DelFirstTest()
    {
        var result = ReUtil.DelFirst("\\d+", "a1b2c3");
        Assert.Equal("abc3", result);
    }

    [Fact]
    public void DelAllTest()
    {
        var result = ReUtil.DelAll("\\d+", "a1b22c333");
        Assert.Equal("abc", result);
    }

    [Fact]
    public void ContainsTest()
    {
        Assert.True(ReUtil.Contains("abc123", "\\d"));
        Assert.False(ReUtil.Contains("abc", "\\d"));
    }

    [Fact]
    public void EscapeTest()
    {
        var escaped = ReUtil.Escape("a.b*c?d");
        Assert.NotEqual("a.b*c?d", escaped);
    }
}
