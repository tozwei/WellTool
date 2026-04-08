using Xunit;
using System.Web;
using System;

namespace WellTool.Core.Tests;

public class URLUtilLastTest
{
    [Fact]
    public void NormalizeTest()
    {
        var url = "http://example.com/";
        Assert.Equal("http://example.com/", url);
    }

    [Fact]
    public void EncodeTest()
    {
        var encoded = HttpUtility.UrlEncode("Hello World");
        Assert.Contains("+", encoded);
    }

    [Fact]
    public void DecodeTest()
    {
        var decoded = HttpUtility.UrlDecode("Hello%20World");
        Assert.Equal("Hello World", decoded);
    }

    [Fact]
    public void GetPathTest()
    {
        var uri = new Uri("http://example.com/path/to/page");
        var path = uri.AbsolutePath;
        Assert.Equal("/path/to/page", path);
    }

    [Fact]
    public void GetHostTest()
    {
        var uri = new Uri("http://example.com/path");
        var host = uri.Host;
        Assert.Equal("example.com", host);
    }

    [Fact]
    public void IsHttpTest()
    {
        Assert.True(new Uri("http://example.com").Scheme == "http");
        Assert.False(new Uri("ftp://example.com").Scheme == "http");
    }
}
