using System.Collections.Generic;
using System.Text;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class IssueIBRVE4Test
{
    [Fact]
    public void HttpRequestChainingWithAllMethodsTest()
    {
        // 测试 HttpRequest 所有方法的链式调用
        var request = HttpRequest.Get("http://example.com")
            .SetUrl("http://example.org")
            .SetMethod(Method.POST)
            .Timeout(5000)
            .SetConnectionTimeout(3000)
            .SetReadTimeout(4000)
            .SetFollowRedirects(true)
            .SetMaxRedirectCount(3)
            .DisableCache()
            .SetHttpProxy("127.0.0.1", 8080)
            .SetChunkedStreamingMode(4096)
            .SetHostnameVerifier((hostname, issuer) => true)
            .SetHeader("User-Agent", "WellTool/1.0")
            .SetHeader("Authorization", "Bearer token123")
            .Form("name", "test")
            .Form("age", 18)
            .Body("{\"name\": \"value\"}");
        
        // 验证链式调用是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpUtilToParamsWithSpecialCharsTest()
    {
        // 测试 ToParams 方法处理特殊字符
        var paramMap = new Dictionary<string, object?>()
        {
            { "name", "test name" },
            { "age", 18 },
            { "active", true }
        };
        var paramsString = HttpUtil.ToParams(paramMap);
        Assert.Contains("name=test%20name", paramsString);
        Assert.Contains("age=18", paramsString);
        Assert.Contains("active=True", paramsString);
    }

    [Fact]
    public void HttpUtilDecodeParamMapWithSpecialCharsTest()
    {
        // 测试 DecodeParamMap 方法处理特殊字符
        var paramsString = "name=test%20name&age=18&active=True";
        var paramMap = HttpUtil.DecodeParamMap(paramsString);
        Assert.Equal(3, paramMap.Count);
        Assert.Equal("test name", paramMap["name"]);
        Assert.Equal("18", paramMap["age"]);
        Assert.Equal("True", paramMap["active"]);
    }

    [Fact]
    public void HttpRequestFormWithComplexTypesTest()
    {
        // 测试设置复杂类型的表单参数
        var request = HttpRequest.Post("http://example.com");
        
        // 设置整数类型
        request.Form("age", 18);
        
        // 设置布尔类型
        request.Form("active", true);
        
        // 设置浮点数类型
        request.Form("score", 95.5);
        
        // 验证表单数据是否设置成功
        var form = request.Form();
        Assert.NotNull(form);
        Assert.Contains("age", form.Keys);
        Assert.Contains("active", form.Keys);
        Assert.Contains("score", form.Keys);
        Assert.Equal("18", form["age"]);
        Assert.Equal("True", form["active"]);
        Assert.Equal("95.5", form["score"]);
    }

    [Fact]
    public void HttpBaseGetAllHeadersTest()
    {
        // 测试获取所有头部
        var request = HttpRequest.Get("http://example.com")
            .SetHeader("User-Agent", "WellTool/1.0")
            .SetHeader("Authorization", "Bearer token123")
            .SetHeader("Accept", "application/json");
        
        // 获取所有头部
        var headers = request.GetAllHeaders();
        
        // 验证头部是否获取成功
        Assert.NotNull(headers);
        Assert.Equal(3, headers.Count);
        Assert.Contains("User-Agent", headers.Keys);
        Assert.Contains("Authorization", headers.Keys);
        Assert.Contains("Accept", headers.Keys);
    }

    [Fact]
    public void HttpBaseHeaderAggregationTest()
    {
        // 测试头部聚合设置
        var request = HttpRequest.Get("http://example.com");
        
        // 获取默认的头部聚合状态
        var defaultAggregation = request.GetIsHeaderAggregated();
        
        // 设置头部聚合
        request.HeaderAggregation(true);
        
        // 验证头部聚合设置是否成功
        var aggregationAfterSet = request.GetIsHeaderAggregated();
        Assert.True(aggregationAfterSet);
        
        // 取消头部聚合
        request.HeaderAggregation(false);
        
        // 验证头部聚合设置是否成功
        var aggregationAfterUnset = request.GetIsHeaderAggregated();
        Assert.False(aggregationAfterUnset);
    }

    [Fact]
    public void HttpUtilBuildBasicAuthWithEmptyCredentialsTest()
    {
        // 测试使用空凭据构建 Basic 认证
        // 注意：这里应该抛出异常，因为用户名和密码不能为空
        Assert.Throws<ArgumentNullException>(() => HttpUtil.BuildBasicAuth(string.Empty, "password"));
        Assert.Throws<ArgumentNullException>(() => HttpUtil.BuildBasicAuth("username", string.Empty));
        Assert.Throws<ArgumentNullException>(() => HttpUtil.BuildBasicAuth(string.Empty, string.Empty));
    }

    [Fact]
    public void HttpRequestBasicAuthWithEmptyCredentialsTest()
    {
        // 测试使用空凭据设置 Basic 认证
        var request = HttpRequest.Get("http://example.com");
        
        // 注意：这里应该抛出异常，因为用户名和密码不能为空
        Assert.Throws<ArgumentNullException>(() => request.BasicAuth(string.Empty, "password"));
        Assert.Throws<ArgumentNullException>(() => request.BasicAuth("username", string.Empty));
        Assert.Throws<ArgumentNullException>(() => request.BasicAuth(string.Empty, string.Empty));
    }

    [Fact]
    public void HttpRequestBearerAuthWithEmptyTokenTest()
    {
        // 测试使用空令牌设置 Bearer 认证
        var request = HttpRequest.Get("http://example.com");
        
        // 设置空令牌
        request.BearerAuth(string.Empty);
        
        // 验证认证是否设置成功
        var authHeader = request.GetHeader(Header.AUTHORIZATION);
        Assert.Equal("Bearer ", authHeader);
    }

    [Fact]
    public void HttpRequestBearerAuthWithNullTokenTest()
    {
        // 测试使用 null 令牌设置 Bearer 认证
        var request = HttpRequest.Get("http://example.com");
        
        // 设置 null 令牌
        request.BearerAuth(null);
        
        // 验证认证是否设置成功
        var authHeader = request.GetHeader(Header.AUTHORIZATION);
        Assert.Equal("Bearer ", authHeader);
    }

    [Fact]
    public void HttpUtilGetCharsetWithVariousFormatsTest()
    {
        // 测试从各种格式的 Content-Type 中获取字符集
        var contentType1 = "application/json;charset=utf-8";
        var charset1 = HttpUtil.GetCharset(contentType1);
        Assert.Equal("utf-8", charset1);

        var contentType2 = "application/json; charset=utf-8";
        var charset2 = HttpUtil.GetCharset(contentType2);
        Assert.Equal("utf-8", charset2);

        var contentType3 = "application/json;charset=UTF-8";
        var charset3 = HttpUtil.GetCharset(contentType3);
        Assert.Equal("UTF-8", charset3);

        var contentType4 = "application/json;charset=utf-16";
        var charset4 = HttpUtil.GetCharset(contentType4);
        Assert.Equal("utf-16", charset4);

        var contentType5 = "application/json";
        var charset5 = HttpUtil.GetCharset(contentType5);
        Assert.Null(charset5);
    }

    [Fact]
    public void HttpUtilGetCharsetWithMultipleParamsTest()
    {
        // 测试从带有多个参数的 Content-Type 中获取字符集
        var contentType = "application/json;charset=utf-8;other=value";
        var charset = HttpUtil.GetCharset(contentType);
        Assert.Equal("utf-8", charset);
    }

    [Fact]
    public void HttpBaseSetContentTypeWithEmptyStringTest()
    {
        // 测试设置空字符串 Content-Type
        var request = HttpRequest.Get("http://example.com");
        
        // 设置空字符串 Content-Type
        request.SetContentType(string.Empty);
        
        // 验证 Content-Type 是否设置成功
        var contentType = request.GetHeader(Header.CONTENT_TYPE);
        Assert.Equal(string.Empty, contentType);
    }

    [Fact]
    public void HttpBaseSetContentTypeWithNullTest()
    {
        // 测试设置 null Content-Type
        var request = HttpRequest.Get("http://example.com");
        
        // 设置 null Content-Type
        request.SetContentType(null);
        
        // 验证 Content-Type 是否设置成功
        var contentType = request.GetHeader(Header.CONTENT_TYPE);
        Assert.Null(contentType);
    }
}
