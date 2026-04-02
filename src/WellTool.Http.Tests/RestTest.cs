using WellTool.Http;
using Xunit;
using System.Text.Json;

namespace WellTool.Http.Tests;

/// <summary>
/// REST 类型请求单元测试
/// </summary>
public class RestTest
{
    [Fact]
    public void ContentTypeTest()
    {
        // 测试 JSON 内容类型设置
        var jsonContent = JsonSerializer.Serialize(new { aaa = "aaaValue", 键2 = "值2" });
        var request = HttpRequest.Post("http://localhost:8090/rest/restTest/")
            .Body(jsonContent);
        
        // 验证 Content-Type 头是否正确设置
        var contentType = request.GetHeader("Content-Type");
        Assert.Equal("application/json;charset=UTF-8", contentType);
    }

    [Fact]
    public void PostWithJsonBodyTest()
    {
        // 测试带 JSON 体的 POST 请求
        var jsonContent = JsonSerializer.Serialize(new { name = "test", value = 123 });
        var request = HttpRequest.Post("http://example.com/api")
            .Body(jsonContent);
        
        // 验证请求方法和内容类型
        Assert.Equal(Method.POST, request.GetMethod());
        var contentType = request.GetHeader("Content-Type");
        Assert.Equal("application/json;charset=UTF-8", contentType);
    }

    [Fact]
    public void GetWithBodyTest()
    {
        // 测试带 body 的 GET 请求
        var jsonContent = JsonSerializer.Serialize(new { id = 1, name = "test" });
        var request = HttpRequest.Get("http://example.com/api")
            .SetHeader("Content-Type", "application/json")
            .Body(jsonContent);
        
        // 验证请求方法和内容类型
        Assert.Equal(Method.GET, request.GetMethod());
        var contentType = request.GetHeader("Content-Type");
        Assert.Equal("application/json", contentType);
    }

    [Fact]
    public void RestfulRequestTest()
    {
        // 测试 RESTful 风格的请求
        // 模拟 GET 请求
        var getRequest = HttpRequest.Get("http://example.com/api/users/1");
        Assert.Equal(Method.GET, getRequest.GetMethod());
        
        // 模拟 POST 请求
        var postRequest = HttpRequest.Post("http://example.com/api/users")
            .Body(JsonSerializer.Serialize(new { name = "test", email = "test@example.com" }));
        Assert.Equal(Method.POST, postRequest.GetMethod());
        
        // 模拟 PUT 请求
        var putRequest = HttpRequest.Put("http://example.com/api/users/1")
            .Body(JsonSerializer.Serialize(new { name = "updated", email = "updated@example.com" }));
        Assert.Equal(Method.PUT, putRequest.GetMethod());
        
        // 模拟 DELETE 请求
        var deleteRequest = HttpRequest.Delete("http://example.com/api/users/1");
        Assert.Equal(Method.DELETE, deleteRequest.GetMethod());
    }
}
