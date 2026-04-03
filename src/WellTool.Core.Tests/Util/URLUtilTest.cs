using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class URLUtilTest
{
    [Fact]
    public void NormalizeTest()
    {
        var url = URLUtil.Normalize("http://example.com/");
        Assert.Equal("http://example.com/", url);
    }

    [Fact]
    public void EncodeTest()
    {
        var encoded = URLUtil.Encode("Hello World");
        Assert.Contains("%20", encoded);
    }

    [Fact]
    public void DecodeTest()
    {
        var decoded = URLUtil.Decode("Hello%20World");
        Assert.Equal("Hello World", decoded);
    }

    [Fact]
    public void GetPathTest()
    {
        var path = URLUtil.GetPath("http://example.com/path/to/page");
        Assert.Equal("/path/to/page", path);
    }

    [Fact]
    public void GetHostTest()
    {
        var host = URLUtil.GetHost("http://example.com/path");
        Assert.Equal("example.com", host);
    }

    [Fact]
    public void GetPortTest()
    {
        var port = URLUtil.GetPort("http://example.com:8080/path");
        Assert.Equal(8080, port);
    }

    [Fact]
    public void IsHttpTest()
    {
        Assert.True(URLUtil.IsHttp("http://example.com"));
        Assert.True(URLUtil.IsHttp("https://example.com"));
        Assert.False(URLUtil.IsHttp("ftp://example.com"));
    }

    [Fact]
    public void BuildQueryStringTest()
    {
        var query = URLUtil.BuildQueryString(new Dictionary<string, string>
        {
            { "a", "1" },
            { "b", "2" }
        });
        Assert.Contains("a=1", query);
        Assert.Contains("b=2", query);
    }
}
