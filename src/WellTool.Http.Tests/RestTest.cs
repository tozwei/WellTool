using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WellTool.Http;
using Xunit;

namespace WellTool.Http.Tests;

/// <summary>
/// Rest API 测试类
/// </summary>
public class RestTest
{
    /// <summary>
    /// 测试Content-Type设置
    /// </summary>
    [Fact]
    public void ContentTypeTest()
    {
        var request = HttpRequest.Post("http://example.com/api")
            .Body("{\"key\":\"value\"}")
            .SetContentType(ContentType.JSON);

        var contentType = request.GetHeader(Header.CONTENT_TYPE);
        Assert.Contains("application/json", contentType);
    }

    /// <summary>
    /// 测试POST请求创建
    /// </summary>
    [Fact]
    public void PostRequestCreationTest()
    {
        var request = HttpRequest.Post("http://example.com/api")
            .Body("{\"name\":\"test\"}");

        Assert.NotNull(request);
        Assert.Equal(Method.POST, request.GetMethod());
    }

    /// <summary>
    /// 测试GET请求创建
    /// </summary>
    [Fact]
    public void GetRequestCreationTest()
    {
        var request = HttpRequest.Get("http://example.com/api");

        Assert.NotNull(request);
        Assert.Equal(Method.GET, request.GetMethod());
    }

    /// <summary>
    /// 测试PUT请求创建
    /// </summary>
    [Fact]
    public void PutRequestCreationTest()
    {
        var request = HttpRequest.Put("http://example.com/api")
            .Body("{\"name\":\"updated\"}");

        Assert.NotNull(request);
        Assert.Equal(Method.PUT, request.GetMethod());
    }

    /// <summary>
    /// 测试DELETE请求创建
    /// </summary>
    [Fact]
    public void DeleteRequestCreationTest()
    {
        var request = HttpRequest.Delete("http://example.com/api/123");

        Assert.NotNull(request);
        Assert.Equal(Method.DELETE, request.GetMethod());
    }

    /// <summary>
    /// 测试PATCH请求创建
    /// </summary>
    [Fact]
    public void PatchRequestCreationTest()
    {
        var request = HttpRequest.Patch("http://example.com/api/123")
            .Body("{\"field\":\"newValue\"}");

        Assert.NotNull(request);
        Assert.Equal(Method.PATCH, request.GetMethod());
    }

    /// <summary>
    /// 测试带JSON Body的GET请求
    /// </summary>
    [Fact]
    public void GetWithBodyTest()
    {
        var request = HttpRequest.Get("http://example.com/api")
            .SetHeader(Header.CONTENT_TYPE, ContentType.JSON)
            .Body("{\"query\":\"value\"}");

        Assert.NotNull(request);
        Assert.Equal(Method.GET, request.GetMethod());
    }

    /// <summary>
    /// 测试请求头设置
    /// </summary>
    [Fact]
    public void HeaderSettingTest()
    {
        var request = HttpRequest.Post("http://example.com/api")
            .SetHeader("X-Custom-Header", "custom-value")
            .SetHeader("Authorization", "Bearer token123");

        Assert.Equal("custom-value", request.GetHeader("X-Custom-Header"));
        Assert.Equal("Bearer token123", request.GetHeader("Authorization"));
    }

    /// <summary>
    /// 测试表单数据设置
    /// </summary>
    [Fact]
    public void FormDataTest()
    {
        var request = HttpRequest.Post("http://example.com/api")
            .Form("username", "testuser")
            .Form("password", "testpass");

        Assert.NotNull(request);
    }

    /// <summary>
    /// 测试字符集设置
    /// </summary>
    [Fact]
    public void CharsetSettingTest()
    {
        var request = HttpRequest.Get("http://example.com/api")
            .SetCharset(Encoding.UTF8);

        Assert.NotNull(request);
    }

    /// <summary>
    /// 测试超时设置
    /// </summary>
    [Fact]
    public void TimeoutSettingTest()
    {
        var request = HttpRequest.Get("http://example.com/api")
            .Timeout(5000);

        Assert.NotNull(request);
    }

    /// <summary>
    /// 测试链式调用
    /// </summary>
    [Fact]
    public void ChainCallTest()
    {
        var request = HttpRequest.Post("http://example.com/api")
            .SetHeader("Accept", ContentType.JSON)
            .SetHeader(Header.CONTENT_TYPE, ContentType.JSON)
            .SetCharset(Encoding.UTF8)
            .Timeout(10000)
            .Body("{\"data\":\"test\"}");

        Assert.NotNull(request);
        Assert.Equal(Method.POST, request.GetMethod());
    }

    /// <summary>
    /// 测试请求体设置
    /// </summary>
    [Fact]
    public void BodySettingTest()
    {
        var jsonBody = "{\"key1\":\"value1\",\"key2\":\"value2\"}";
        var request = HttpRequest.Post("http://example.com/api")
            .Body(jsonBody);

        Assert.NotNull(request);
    }

    /// <summary>
    /// 测试User-Agent设置
    /// </summary>
    [Fact]
    public void UserAgentTest()
    {
        var request = HttpRequest.Get("http://example.com/api")
            .SetHeader(Header.USER_AGENT, "WellTool-Http-Client/1.0");

        var userAgent = request.GetHeader(Header.USER_AGENT);
        Assert.Contains("WellTool-Http-Client", userAgent);
    }

    /// <summary>
    /// 测试Accept头设置
    /// </summary>
    [Fact]
    public void AcceptHeaderTest()
    {
        var request = HttpRequest.Get("http://example.com/api")
            .SetHeader(Header.ACCEPT, ContentType.JSON);

        var accept = request.GetHeader(Header.ACCEPT);
        Assert.Contains("application/json", accept);
    }

    /// <summary>
    /// 测试所有HTTP方法创建
    /// </summary>
    [Fact]
    public void AllHttpMethodsTest()
    {
        var get = HttpRequest.Get("http://example.com");
        var post = HttpRequest.Post("http://example.com");
        var put = HttpRequest.Put("http://example.com");
        var delete = HttpRequest.Delete("http://example.com");
        var patch = HttpRequest.Patch("http://example.com");
        var head = HttpRequest.Head("http://example.com");
        var options = HttpRequest.Options("http://example.com");

        Assert.Equal(Method.GET, get.GetMethod());
        Assert.Equal(Method.POST, post.GetMethod());
        Assert.Equal(Method.PUT, put.GetMethod());
        Assert.Equal(Method.DELETE, delete.GetMethod());
        Assert.Equal(Method.PATCH, patch.GetMethod());
        Assert.Equal(Method.HEAD, head.GetMethod());
        Assert.Equal(Method.OPTIONS, options.GetMethod());
    }

    /// <summary>
    /// 测试HTTPS URL
    /// </summary>
    [Fact]
    public void HttpsUrlTest()
    {
        var request = HttpRequest.Get("https://example.com/api");

        Assert.NotNull(request);
    }

    /// <summary>
    /// 测试带查询参数的URL
    /// </summary>
    [Fact]
    public void UrlWithQueryParamsTest()
    {
        var request = HttpRequest.Get("http://example.com/api?param1=value1&param2=value2");

        Assert.NotNull(request);
    }

    /// <summary>
    /// 测试Content-Type常量
    /// </summary>
    [Fact]
    public void ContentTypeConstantsTest()
    {
        Assert.Equal("application/json", ContentType.JSON);
        Assert.Equal("application/xml", ContentType.XML);
        Assert.Equal("text/html", ContentType.HTML);
        Assert.Equal("text/plain", ContentType.TEXT);
        Assert.Equal("application/x-www-form-urlencoded", ContentType.FORM_URLENCODED);
        Assert.Equal("multipart/form-data", ContentType.MULTIPART);
    }
}
