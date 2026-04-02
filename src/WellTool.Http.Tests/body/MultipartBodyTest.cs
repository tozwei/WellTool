using WellTool.Http;
using WellTool.Http.Body;
using Xunit;
using System.Collections.Generic;
using System.Text;

namespace WellTool.Http.Tests.Body;

/// <summary>
/// MultipartBody 测试类
/// </summary>
public class MultipartBodyTest
{
    [Fact]
    public void BuildTest()
    {
        // 测试构建 MultipartBody
        var form = new Dictionary<string, object?>
        {
            { "pic1", "pic1 content" },
            { "pic2", "pic2 content" },
            { "pic3", "pic3 content" }
        };

        var body = MultipartBody.Create(form, Encoding.UTF8);

        // 验证 body 不为空
        Assert.NotNull(body);
        
        // 验证 body 转换为字符串不为空
        var bodyString = body.ToString();
        Assert.NotNull(bodyString);
        Assert.False(string.IsNullOrEmpty(bodyString));
        
        // 验证 body 包含表单字段
        Assert.Contains("pic1", bodyString);
        Assert.Contains("pic2", bodyString);
        Assert.Contains("pic3", bodyString);
        
        // 验证 body 包含内容
        Assert.Contains("pic1 content", bodyString);
        Assert.Contains("pic2 content", bodyString);
        Assert.Contains("pic3 content", bodyString);
    }

    [Fact]
    public void BuildWithEmptyFormTest()
    {
        // 测试构建空的 MultipartBody
        var form = new Dictionary<string, object?>();
        var body = MultipartBody.Create(form, Encoding.UTF8);
        
        // 验证 body 不为空
        Assert.NotNull(body);
        
        // 验证 body 转换为字符串不为空
        var bodyString = body.ToString();
        Assert.NotNull(bodyString);
    }

    [Fact]
    public void BuildWithNullFormTest()
    {
        // 测试使用 null 表单构建 MultipartBody
        var body = MultipartBody.Create(null, Encoding.UTF8);
        
        // 验证 body 不为空
        Assert.NotNull(body);
        
        // 验证 body 转换为字符串不为空
        var bodyString = body.ToString();
        Assert.NotNull(bodyString);
    }
}
