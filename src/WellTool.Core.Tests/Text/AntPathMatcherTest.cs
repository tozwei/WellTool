using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class AntPathMatcherTest
{
    [Fact]
    public void MatchesTest()
    {
        var antPathMatcher = new AntPathMatcher();
        var matched = antPathMatcher.Match("/api/org/organization/{orgId}", "/api/org/organization/999");
        Assert.True(matched);
    }

    [Fact]
    public void MatchesTest2()
    {
        var antPathMatcher = new AntPathMatcher();

        // 测试 /** 模式
        var pattern = "/**/*.xml";
        var path = "/WEB-INF/web.xml";
        Assert.True(antPathMatcher.Match(pattern, path));

        // 测试通配符
        pattern = "org/*/example/*Service";
        path = "org/codelabor/example/HelloWorldService";
        Assert.True(antPathMatcher.Match(pattern, path));
    }

    [Fact]
    public void MatchesTest3()
    {
        var pathMatcher = new AntPathMatcher();
        pathMatcher.CachePatterns = true;
        pathMatcher.CaseSensitive = true;
        pathMatcher.TrimTokens = true;

        // 简单匹配
        Assert.True(pathMatcher.Match("a", "a"));
        Assert.True(pathMatcher.Match("a*", "ab"));
        
        // 测试通配符?
        Assert.True(pathMatcher.Match("t?st", "test"));
        Assert.True(pathMatcher.Match("te??", "test"));
        
        // 测试单个 * (不匹配斜杠)
        Assert.True(pathMatcher.Match("*.txt", "test.txt"));
        Assert.False(pathMatcher.Match("*.txt", "test/test.txt"));
    }

    [Fact]
    public void MatchesTest4()
    {
        var pathMatcher = new AntPathMatcher();

        // 精确匹配
        Assert.True(pathMatcher.Match("/test", "/test"));
        Assert.False(pathMatcher.Match("test", "/test"));

        // 测试通配符?
        Assert.True(pathMatcher.Match("t?st", "test"));
        Assert.True(pathMatcher.Match("te??", "test"));
        Assert.False(pathMatcher.Match("tes?", "tes"));
        Assert.False(pathMatcher.Match("tes?", "testt"));

        // 测试通配符*
        Assert.True(pathMatcher.Match("*", "test"));
        Assert.True(pathMatcher.Match("test*", "test"));
        Assert.True(pathMatcher.Match("test/*", "test/Test"));
        Assert.True(pathMatcher.Match("*.*", "test."));
        Assert.True(pathMatcher.Match("*.*", "test.test.test"));
    }

}
