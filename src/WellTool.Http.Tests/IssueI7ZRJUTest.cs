using System.Collections.Generic;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class IssueI7ZRJUTest
{
    [Fact]
    public void HttpRequestFormWithDictionaryTest()
    {
        // 测试使用字典设置表单数据
        var request = HttpRequest.Post("http://example.com");
        var formData = new Dictionary<string, object?>
        {
            { "name", "test" },
            { "age", 18 },
            { "active", true }
        };
        
        // 设置表单数据
        request.Form(formData);
        
        // 验证表单数据是否设置成功
        var form = request.Form();
        Assert.NotNull(form);
        Assert.Equal(3, form.Count);
        Assert.Equal("test", form["name"]);
        Assert.Equal("18", form["age"]);
        Assert.Equal("True", form["active"]);
    }

    [Fact]
    public void HttpRequestFormWithListTest()
    {
        // 测试使用列表设置表单数据
        var request = HttpRequest.Post("http://example.com");
        var list = new List<string> { "value1", "value2", "value3" };
        
        // 设置表单数据
        request.Form("list", list);
        
        // 验证表单数据是否设置成功
        var form = request.Form();
        Assert.NotNull(form);
        Assert.Contains("list", form.Keys);
        Assert.Equal("value1,value2,value3", form["list"]);
    }

    [Fact]
    public void HttpRequestFormWithArrayTest()
    {
        // 测试使用数组设置表单数据
        var request = HttpRequest.Post("http://example.com");
        var array = new string[] { "value1", "value2", "value3" };
        
        // 设置表单数据
        request.Form("array", array);
        
        // 验证表单数据是否设置成功
        var form = request.Form();
        Assert.NotNull(form);
        Assert.Contains("array", form.Keys);
        Assert.Equal("value1,value2,value3", form["array"]);
    }

    [Fact]
    public void HttpRequestBodyWithStringTest()
    {
        // 测试设置字符串请求体
        var request = HttpRequest.Post("http://example.com");
        var body = "{\"name\": \"value\"}";
        
        // 设置请求体
        request.Body(body);
        
        // 验证请求体是否设置成功
        var bodyBytes = request.BodyBytes();
        Assert.NotNull(bodyBytes);
        Assert.NotEmpty(bodyBytes);
    }

    [Fact]
    public void HttpRequestBodyWithBytesTest()
    {
        // 测试设置字节数组请求体
        var request = HttpRequest.Post("http://example.com");
        var body = "{\"name\": \"value\"}";
        var bodyBytes = System.Text.Encoding.UTF8.GetBytes(body);
        
        // 设置请求体
        request.Body(bodyBytes);
        
        // 验证请求体是否设置成功
        var resultBytes = request.BodyBytes();
        Assert.NotNull(resultBytes);
        Assert.NotEmpty(resultBytes);
    }

    [Fact]
    public void HttpRequestBodyWithContentTypeTest()
    {
        // 测试设置请求体并指定内容类型
        var request = HttpRequest.Post("http://example.com");
        var body = "<root><child>value</child></root>";
        
        // 设置请求体并指定内容类型
        request.Body(body, ContentType.XML);
        
        // 验证请求体和内容类型是否设置成功
        var bodyBytes = request.BodyBytes();
        Assert.NotNull(bodyBytes);
        Assert.NotEmpty(bodyBytes);
        var contentType = request.GetHeader(Header.CONTENT_TYPE);
        Assert.Equal(ContentType.XML, contentType);
    }

    [Fact]
    public void HttpRequestFormAndBodyConflictTest()
    {
        // 测试表单数据和请求体的冲突
        var request = HttpRequest.Post("http://example.com");
        
        // 先设置表单数据
        request.Form("name", "test");
        
        // 验证表单数据是否设置成功
        var form1 = request.Form();
        Assert.NotNull(form1);
        Assert.Contains("name", form1.Keys);
        
        // 再设置请求体，应该清除表单数据
        request.Body("{\"name\": \"value\"}");
        
        // 验证表单数据是否被清除
        var form2 = request.Form();
        Assert.Null(form2);
    }

    [Fact]
    public void HttpRequestBodyAndFormConflictTest()
    {
        // 测试请求体和表单数据的冲突
        var request = HttpRequest.Post("http://example.com");
        
        // 先设置请求体
        request.Body("{\"name\": \"value\"}");
        
        // 验证请求体是否设置成功
        var bodyBytes1 = request.BodyBytes();
        Assert.NotNull(bodyBytes1);
        Assert.NotEmpty(bodyBytes1);
        
        // 再设置表单数据，应该清除请求体
        request.Form("name", "test");
        
        // 验证请求体是否被清除
        var bodyBytes2 = request.BodyBytes();
        Assert.Null(bodyBytes2);
        
        // 验证表单数据是否设置成功
        var form = request.Form();
        Assert.NotNull(form);
        Assert.Contains("name", form.Keys);
    }
}
