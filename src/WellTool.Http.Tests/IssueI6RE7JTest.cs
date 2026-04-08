using System.Collections.Generic;
using System.Text;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class IssueI6RE7JTest
{
    [Fact]
    public void HttpUtilToParamsTest()
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
    public void HttpUtilDecodeParamMapTest()
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
    public void HttpUtilEncodeParamsTest()
    {
        // 测试 EncodeParams 方法
        var urlWithParams = "http://example.com?name=test&age=18";
        var encodedUrl = HttpUtil.EncodeParams(urlWithParams, Encoding.UTF8);
        Assert.Contains("http://example.com", encodedUrl);
        Assert.Contains("name=test", encodedUrl);
        Assert.Contains("age=18", encodedUrl);
    }

    [Fact]
    public void HttpUtilGetCharsetTest()
    {
        // 测试 GetCharset 方法
        var contentType1 = "application/json;charset=utf-8";
        var charset1 = HttpUtil.GetCharset(contentType1);
        Assert.Equal("utf-8", charset1);

        var contentType2 = "text/plain";
        var charset2 = HttpUtil.GetCharset(contentType2);
        Assert.Null(charset2);

        var contentType3 = string.Empty;
        var charset3 = HttpUtil.GetCharset(contentType3);
        Assert.Null(charset3);

        var contentType4 = null as string;
        var charset4 = HttpUtil.GetCharset(contentType4);
        Assert.Null(charset4);
    }

    [Fact]
    public void HttpUtilGetMimeTypeTest()
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
    public void HttpUtilBuildBasicAuthTest()
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
    public void HttpUtilUrlWithFormTest()
    {
        // 测试 UrlWithForm 方法
        var url = "http://example.com";
        var form = new Dictionary<string, object?>()
        {
            { "name", "test" },
            { "age", 18 }
        };

        var urlWithForm = HttpUtil.UrlWithForm(url, form, Encoding.UTF8, true);
        Assert.Contains("http://example.com", urlWithForm);
        Assert.Contains("name=test", urlWithForm);
        Assert.Contains("age=18", urlWithForm);
    }

    [Fact]
    public void HttpUtilDECODE_PARAMSTest()
    {
        // 测试 DECODE_PARAMS 方法
        var paramsString = "name=test&age=18&active=True";
        var paramMap = HttpUtil.DECODE_PARAMS(paramsString, Encoding.UTF8);

        Assert.Equal(3, paramMap.Count);
        Assert.Contains("name", paramMap.Keys);
        Assert.Contains("age", paramMap.Keys);
        Assert.Contains("active", paramMap.Keys);
        Assert.Equal("test", paramMap["name"][0]);
        Assert.Equal("18", paramMap["age"][0]);
        Assert.Equal("True", paramMap["active"][0]);
    }

    [Fact]
    public void HttpUtilDECODE_PARAM_MAPTest()
    {
        // 测试 DECODE_PARAM_MAP 方法
        var paramsString = "name=test&age=18&active=True";
        var paramMap = HttpUtil.DECODE_PARAM_MAP(paramsString, Encoding.UTF8);

        Assert.Equal(3, paramMap.Count);
        Assert.Contains("name", paramMap.Keys);
        Assert.Contains("age", paramMap.Keys);
        Assert.Contains("active", paramMap.Keys);
        Assert.Equal("test", paramMap["name"][0]);
        Assert.Equal("18", paramMap["age"][0]);
        Assert.Equal("True", paramMap["active"][0]);
    }
}
