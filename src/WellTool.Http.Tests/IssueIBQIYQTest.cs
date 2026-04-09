using System.Collections.Generic;
using System.Text;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class IssueIBQIYQTest
{
    [Fact]
    public void HttpUtilEncodeParamsWithNoParamsTest()
    {
        // 测试编码没有参数的 URL
        var url = "http://example.com";
        var encodedUrl = HttpUtil.EncodeParams(url, Encoding.UTF8);
        Assert.Equal(url, encodedUrl);
    }

    [Fact]
    public void HttpUtilEncodeParamsWithParamsTest()
    {
        // 测试编码带参数的 URL
        var url = "http://example.com?name=test&age=18";
        var encodedUrl = HttpUtil.EncodeParams(url, Encoding.UTF8);
        Assert.Contains("http://example.com", encodedUrl);
        Assert.Contains("name=test", encodedUrl);
        Assert.Contains("age=18", encodedUrl);
    }

    [Fact]
    public void HttpUtilEncodeParamsWithSpecialCharsTest()
    {
        // 测试编码带特殊字符的 URL
        var url = "http://example.com?name=test+name&age=18";
        var encodedUrl = HttpUtil.EncodeParams(url, Encoding.UTF8);
        Assert.Contains("http://example.com", encodedUrl);
        Assert.Contains("name=test%2Bname", encodedUrl);
        Assert.Contains("age=18", encodedUrl);
    }

    [Fact]
    public void HttpUtilNormalizeParamsWithEmptyStringTest()
    {
        // 测试标准化空参数字符串
        var paramsString = string.Empty;
        var normalizedParams = HttpUtil.NormalizeParams(paramsString, Encoding.UTF8);
        Assert.Equal(string.Empty, normalizedParams);
    }

    [Fact]
    public void HttpUtilNormalizeParamsWithNoEqualsTest()
    {
        // 测试标准化没有等号的参数字符串
        var paramsString = "name&age&active";
        var normalizedParams = HttpUtil.NormalizeParams(paramsString, Encoding.UTF8);
        Assert.Contains("name=", normalizedParams);
        Assert.Contains("age=", normalizedParams);
        Assert.Contains("active=", normalizedParams);
    }

    [Fact]
    public void HttpUtilNormalizeParamsWithTrailingAmpersandTest()
    {
        // 测试标准化末尾带有 & 的参数字符串
        var paramsString = "name=test&age=18&";
        var normalizedParams = HttpUtil.NormalizeParams(paramsString, Encoding.UTF8);
        Assert.Equal("name=test&age=18", normalizedParams);
    }

    [Fact]
    public void HttpUtilNormalizeParamsWithLeadingQuestionMarkTest()
    {
        // 测试标准化开头带有 ? 的参数字符串
        var paramsString = "?name=test&age=18";
        var normalizedParams = HttpUtil.NormalizeParams(paramsString, Encoding.UTF8);
        Assert.Equal("name=test&age=18", normalizedParams);
    }

    [Fact]
    public void HttpUtilUrlWithFormWithEmptyUrlTest()
    {
        // 测试将表单数据添加到空 URL
        var url = string.Empty;
        var form = new Dictionary<string, object?> { { "name", "test" } };
        var urlWithForm = HttpUtil.UrlWithForm(url, form, Encoding.UTF8, true);
        Assert.Contains("name=test", urlWithForm);
    }

    [Fact]
    public void HttpUtilUrlWithFormWithNoParamsTest()
    {
        // 测试将空表单数据添加到 URL
        var url = "http://example.com";
        var form = new Dictionary<string, object?>();
        var urlWithForm = HttpUtil.UrlWithForm(url, form, Encoding.UTF8, true);
        Assert.Equal(url, urlWithForm);
    }

    [Fact]
    public void HttpUtilUrlWithFormWithExistingParamsTest()
    {
        // 测试将表单数据添加到已有参数的 URL
        var url = "http://example.com?existing=value";
        var form = new Dictionary<string, object?> { { "name", "test" } };
        var urlWithForm = HttpUtil.UrlWithForm(url, form, Encoding.UTF8, true);
        Assert.Contains("http://example.com", urlWithForm);
        Assert.Contains("existing=value", urlWithForm);
        Assert.Contains("name=test", urlWithForm);
    }

    [Fact]
    public void HttpUtilUrlWithFormWithQueryStringTest()
    {
        // 测试将查询字符串添加到 URL
        var url = "http://example.com";
        var queryString = "name=test&age=18";
        var urlWithForm = HttpUtil.UrlWithForm(url, queryString, Encoding.UTF8, true);
        Assert.Contains("http://example.com", urlWithForm);
        Assert.Contains("name=test", urlWithForm);
        Assert.Contains("age=18", urlWithForm);
    }

    [Fact]
    public void HttpUtilUrlWithFormWithEmptyQueryStringTest()
    {
        // 测试将空查询字符串添加到 URL
        var url = "http://example.com";
        var queryString = string.Empty;
        var urlWithForm = HttpUtil.UrlWithForm(url, queryString, Encoding.UTF8, true);
        Assert.Equal(url, urlWithForm);
    }

    [Fact]
    public void HttpUtilUrlWithFormWithExistingQueryStringTest()
    {
        // 测试将查询字符串添加到已有参数的 URL
        var url = "http://example.com?existing=value";
        var queryString = "name=test&age=18";
        var urlWithForm = HttpUtil.UrlWithForm(url, queryString, Encoding.UTF8, true);
        Assert.Contains("http://example.com", urlWithForm);
        Assert.Contains("existing=value", urlWithForm);
        Assert.Contains("name=test", urlWithForm);
        Assert.Contains("age=18", urlWithForm);
    }

    [Fact]
    public void HttpUtilDECODE_PARAMSWithEmptyStringTest()
    {
        // 测试 DECODE_PARAMS 方法处理空字符串
        var paramsString = string.Empty;
        var paramMap = HttpUtil.DECODE_PARAMS(paramsString, Encoding.UTF8);
        Assert.NotNull(paramMap);
        Assert.Empty(paramMap);
    }

    [Fact]
    public void HttpUtilDECODE_PARAMSWithNullTest()
    {
        // 测试 DECODE_PARAMS 方法处理 null
        var paramMap = HttpUtil.DECODE_PARAMS(null, Encoding.UTF8);
        Assert.NotNull(paramMap);
        Assert.Empty(paramMap);
    }

    [Fact]
    public void HttpUtilDECODE_PARAM_MAPWithEmptyStringTest()
    {
        // 测试 DECODE_PARAM_MAP 方法处理空字符串
        var paramsString = string.Empty;
        var paramMap = HttpUtil.DECODE_PARAM_MAP(paramsString, Encoding.UTF8);
        Assert.NotNull(paramMap);
        Assert.Empty(paramMap);
    }

    [Fact]
    public void HttpUtilDECODE_PARAM_MAPWithNullTest()
    {
        // 测试 DECODE_PARAM_MAP 方法处理 null
        var paramMap = HttpUtil.DECODE_PARAM_MAP(null, Encoding.UTF8);
        Assert.NotNull(paramMap);
        Assert.Empty(paramMap);
    }

    [Fact]
    public void HttpUtilGetMimeTypeWithVariousExtensionsTest()
    {
        // 测试 GetMimeType 方法处理各种文件扩展名
        Assert.Equal("text/plain", HttpUtil.GetMimeType("file.txt"));
        Assert.Equal("text/html", HttpUtil.GetMimeType("file.html"));
        Assert.Equal("text/css", HttpUtil.GetMimeType("file.css"));
        Assert.Equal("text/javascript", HttpUtil.GetMimeType("file.js"));
        Assert.Equal("application/json", HttpUtil.GetMimeType("file.json"));
        Assert.Equal("application/xml", HttpUtil.GetMimeType("file.xml"));
        Assert.Equal("image/jpeg", HttpUtil.GetMimeType("file.jpg"));
        Assert.Equal("image/jpeg", HttpUtil.GetMimeType("file.jpeg"));
        Assert.Equal("image/png", HttpUtil.GetMimeType("file.png"));
        Assert.Equal("image/gif", HttpUtil.GetMimeType("file.gif"));
        Assert.Equal("application/pdf", HttpUtil.GetMimeType("file.pdf"));
        Assert.Equal("application/zip", HttpUtil.GetMimeType("file.zip"));
        Assert.Equal("application/x-rar-compressed", HttpUtil.GetMimeType("file.rar"));
        Assert.Equal("application/msword", HttpUtil.GetMimeType("file.doc"));
        Assert.Equal("application/vnd.openxmlformats-officedocument.wordprocessingml.document", HttpUtil.GetMimeType("file.docx"));
        Assert.Equal("application/vnd.ms-excel", HttpUtil.GetMimeType("file.xls"));
        Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", HttpUtil.GetMimeType("file.xlsx"));
        Assert.Equal("application/vnd.ms-powerpoint", HttpUtil.GetMimeType("file.ppt"));
        Assert.Equal("application/vnd.openxmlformats-officedocument.presentationml.presentation", HttpUtil.GetMimeType("file.pptx"));
    }

    [Fact]
    public void HttpUtilGetMimeTypeWithUnknownExtensionTest()
    {
        // 测试 GetMimeType 方法处理未知文件扩展名
        var mimeType = HttpUtil.GetMimeType("file.unknown");
        Assert.Null(mimeType);
    }

    [Fact]
    public void HttpUtilGetMimeTypeWithDefaultValueTest()
    {
        // 测试 GetMimeType 方法处理未知文件扩展名并提供默认值
        var mimeType = HttpUtil.GetMimeType("file.unknown", "application/octet-stream");
        Assert.Equal("application/octet-stream", mimeType);
    }
}
