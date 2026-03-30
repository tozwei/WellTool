using WellTool.Http;
using Xunit;
using System.Text;

namespace WellTool.Http.Tests;

/// <summary>
/// ContentType 测试类
/// </summary>
public class ContentTypeTests
{
    [Fact]
    public void BuildTest()
    {
        var result = ContentType.Build(ContentType.JSON, Encoding.UTF8);
        Assert.Equal("application/json;charset=UTF-8", result);
    }

    [Fact]
    public void GetWithLeadingSpaceTest()
    {
        var json = " {\n" +
                   "     \"name\": \"welltool\"\n" +
                   " }";
        var contentType = ContentType.Get(json);
        Assert.Equal(ContentType.JSON, contentType);
    }

    [Fact]
    public void IsDefaultTest()
    {
        // 测试默认 Content-Type
        Assert.True(ContentType.IsDefault(null));
        Assert.True(ContentType.IsDefault(""));

        // 测试非默认 Content-Type
        Assert.False(ContentType.IsDefault("application/json"));
    }

    [Fact]
    public void GetContentTypeTest()
    {
        // 测试 JSON 检测
        var json = "{\"name\":\"welltool\"}";
        Assert.Equal(ContentType.JSON, ContentType.Get(json));

        // 测试 XML 检测
        var xml = "<?xml version=\"1.0\"?><root></root>";
        Assert.Equal(ContentType.XML, ContentType.Get(xml));

        // 测试 HTML 检测
        var html = "<html><body></body></html>";
        Assert.Equal(ContentType.HTML, ContentType.Get(html));

        // 测试空字符串
        Assert.Null(ContentType.Get(""));
    }
}
