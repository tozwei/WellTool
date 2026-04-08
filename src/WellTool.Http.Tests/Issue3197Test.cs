using System.Collections.Generic;
using System.Text;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class Issue3197Test
{
    [Fact]
    public void IsHttpTest()
    {
        // 测试 IsHttp 方法
        Assert.True(HttpUtil.IsHttp("http://example.com"));
        Assert.True(HttpUtil.IsHttp("HTTP://example.com"));
        Assert.False(HttpUtil.IsHttp("https://example.com"));
        Assert.False(HttpUtil.IsHttp("ftp://example.com"));
    }

    [Fact]
    public void IsHttpsTest()
    {
        // 测试 IsHttps 方法
        Assert.True(HttpUtil.IsHttps("https://example.com"));
        Assert.True(HttpUtil.IsHttps("HTTPS://example.com"));
        Assert.False(HttpUtil.IsHttps("http://example.com"));
        Assert.False(HttpUtil.IsHttps("ftp://example.com"));
    }

    [Fact]
    public void ToParamsTest()
    {
        // 测试 ToParams 方法
        var paramMap = new Dictionary<string, object?>()
        {
            { "name", "test" },
            { "age", 18 },
            { "active", true }
        };

        var paramsString = HttpUtil.ToParams(paramMap);
        Assert.Contains("name=test", paramsString);
        Assert.Contains("age=18", paramsString);
        Assert.Contains("active=True", paramsString);
    }

    [Fact]
    public void DecodeParamMapTest()
    {
        // 测试 DecodeParamMap 方法
        var paramsString = "name=test&age=18&active=True";
        var paramMap = HttpUtil.DecodeParamMap(paramsString);

        Assert.Equal(3, paramMap.Count);
        Assert.Equal("test", paramMap["name"]);
        Assert.Equal("18", paramMap["age"]);
        Assert.Equal("True", paramMap["active"]);
    }

    [Fact]
    public void GetMimeTypeTest()
    {
        // 测试 GetMimeType 方法
        Assert.Equal("text/plain", HttpUtil.GetMimeType("file.txt"));
        Assert.Equal("text/html", HttpUtil.GetMimeType("file.html"));
        Assert.Equal("application/json", HttpUtil.GetMimeType("file.json"));
        Assert.Equal("image/jpeg", HttpUtil.GetMimeType("file.jpg"));
        Assert.Equal("image/png", HttpUtil.GetMimeType("file.png"));
        Assert.Null(HttpUtil.GetMimeType("file.unknown"));
        Assert.Equal("application/octet-stream", HttpUtil.GetMimeType("file.unknown", "application/octet-stream"));
    }

    [Fact]
    public void BuildBasicAuthTest()
    {
        // 测试 BuildBasicAuth 方法
        var auth = HttpUtil.BuildBasicAuth("username", "password");
        Assert.StartsWith("Basic ", auth);
        // 验证 Base64 编码的部分
        var base64Part = auth.Substring(6);
        var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(base64Part));
        Assert.Equal("username:password", decoded);
    }

    [Fact]
    public void CreateGetTest()
    {
        // 测试 CreateGet 方法
        var request = HttpUtil.CreateGet("http://localhost:8080/index");
        Assert.NotNull(request);
    }
}
