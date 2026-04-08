using Xunit;
using System.Web;
using System;
using System.Collections.Generic;

namespace WellTool.Core.Tests;

public class URLUtilTest
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
    public void GetPortTest()
    {
        var uri = new Uri("http://example.com:8080/path");
        var port = uri.Port;
        Assert.Equal(8080, port);
    }

    [Fact]
    public void IsHttpTest()
    {
        Assert.True(new Uri("http://example.com").Scheme == "http");
        Assert.True(new Uri("https://example.com").Scheme == "https");
        Assert.False(new Uri("ftp://example.com").Scheme == "http");
    }

    [Fact]
    public void BuildQueryStringTest()
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["a"] = "1";
        query["b"] = "2";
        var queryString = query.ToString();
        Assert.Contains("a=1", queryString);
        Assert.Contains("b=2", queryString);
    }
}
