using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class URLUtilLastTest
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
    public void IsHttpTest()
    {
        Assert.True(URLUtil.IsHttp("http://example.com"));
        Assert.False(URLUtil.IsHttp("ftp://example.com"));
    }
}
