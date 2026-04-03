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

        var pattern = "/**/*.xml*";
        var path = "/WEB-INF/web.xml";
        Assert.True(antPathMatcher.Match(pattern, path));

        pattern = "org/codelabor/*/**/*Service";
        path = "org/codelabor/example/HelloWorldService";
        Assert.True(antPathMatcher.Match(pattern, path));

        pattern = "org/codelabor/*/**/*Service?";
        path = "org/codelabor/example/HelloWorldServices";
        Assert.False(antPathMatcher.Match(pattern, path));
    }

    [Fact]
    public void MatchesTest3()
    {
        var pathMatcher = new AntPathMatcher();
        pathMatcher.CachePatterns = true;
        pathMatcher.CaseSensitive = true;
        pathMatcher.PathSeparator = "/";
        pathMatcher.TrimTokens = true;

        Assert.True(pathMatcher.Match("a", "a"));
        Assert.True(pathMatcher.Match("a*", "ab"));
        Assert.True(pathMatcher.Match("a*/**/a", "ab/asdsa/a"));
        Assert.True(pathMatcher.Match("a*/**/a", "ab/asdsa/asdasd/a"));

        Assert.True(pathMatcher.Match("*", "a"));
        Assert.True(pathMatcher.Match("*/*", "a/a"));
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

    [Fact]
    public void ExtractUriTemplateVariablesTest()
    {
        var antPathMatcher = new AntPathMatcher();
        var map = antPathMatcher.ExtractUriTemplateVariables(
            "/api/org/organization/{orgId}",
            "/api/org/organization/999");

        Assert.Single(map);
        Assert.Equal("999", map["orgId"]);
    }
}
