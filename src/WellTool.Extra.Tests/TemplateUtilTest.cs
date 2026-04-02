namespace WellTool.Extra.Tests;

/// <summary>
/// TemplateUtil 测试类
/// </summary>
public class TemplateUtilTest
{
    private readonly TemplateUtil _templateUtil;

    public TemplateUtilTest()
    {
        _templateUtil = new TemplateUtil();
    }

    [Fact]
    public void TestRender_SimpleTemplate_ReturnsRenderedText()
    {
        // 测试简单模板渲染
        var template = "Hello {{name}}!";
        var parameters = new Dictionary<string, object>
        {
            { "name", "World" }
        };
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Equal("Hello World!", result);
    }

    [Fact]
    public void TestRender_MultiplePlaceholders_ReturnsAllRendered()
    {
        // 测试多个占位符
        var template = "{{greeting}} {{name}}! Today is {{day}}.";
        var parameters = new Dictionary<string, object>
        {
            { "greeting", "Hello" },
            { "name", "John" },
            { "day", "Monday" }
        };
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Equal("Hello John! Today is Monday.", result);
    }

    [Fact]
    public void TestRender_NoPlaceholders_ReturnsOriginalTemplate()
    {
        // 测试无占位符
        var template = "Hello World!";
        var parameters = new Dictionary<string, object>();
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Equal(template, result);
    }

    [Fact]
    public void TestRender_EmptyParameters_ReturnsOriginalTemplate()
    {
        // 测试空参数
        var template = "Hello {{name}}!";
        var parameters = new Dictionary<string, object>();
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Equal(template, result);
    }

    [Fact]
    public void TestRender_NullValue_ReplacesWithEmptyString()
    {
        // 测试空值
        var template = "Hello {{name}}!";
        var parameters = new Dictionary<string, object>
        {
            { "name", null }
        };
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Equal("Hello !", result);
    }

    [Fact]
    public void TestRender_NumericValue_ConvertsToString()
    {
        // 测试数字值
        var template = "Count: {{count}}";
        var parameters = new Dictionary<string, object>
        {
            { "count", 42 }
        };
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Equal("Count: 42", result);
    }

    [Fact]
    public void TestRender_MissingPlaceholder_KeepsPlaceholder()
    {
        // 测试缺失的占位符
        var template = "Hello {{name}}, your age is {{age}}!";
        var parameters = new Dictionary<string, object>
        {
            { "name", "John" }
        };
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Contains("{{age}}", result);
        Assert.Contains("John", result);
    }

    [Fact]
    public void TestRender_ConsecutivePlaceholders_HandlesCorrectly()
    {
        // 测试连续的占位符
        var template = "{{first}}{{second}}{{third}}";
        var parameters = new Dictionary<string, object>
        {
            { "first", "A" },
            { "second", "B" },
            { "third", "C" }
        };
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Equal("ABC", result);
    }

    [Fact]
    public void TestRender_EmptyTemplate_ReturnsEmptyString()
    {
        // 测试空模板
        var template = "";
        var parameters = new Dictionary<string, object>
        {
            { "name", "World" }
        };
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Empty(result);
    }

    [Fact]
    public void TestRender_SpecialCharacters_HandlesCorrectly()
    {
        // 测试特殊字符
        var template = "Path: {{path}}";
        var parameters = new Dictionary<string, object>
        {
            { "path", "C:\\Users\\Test" }
        };
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Equal("Path: C:\\Users\\Test", result);
    }

    [Fact]
    public void TestRender_UnicodeCharacters_HandlesCorrectly()
    {
        // 测试 Unicode 字符
        var template = "Name: {{name}}";
        var parameters = new Dictionary<string, object>
        {
            { "name", "张三" }
        };
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Equal("Name: 张三", result);
    }

    [Fact]
    public void TestRenderFromFile_ValidFile_ReturnsRenderedContent()
    {
        // 测试从文件渲染（需要临时文件）
        var tempFile = Path.GetTempFileName();
        try
        {
            File.WriteAllText(tempFile, "Hello {{name}}!");
            var parameters = new Dictionary<string, object>
            {
                { "name", "World" }
            };
            
            var result = _templateUtil.RenderFromFile(tempFile, parameters);
            
            Assert.Equal("Hello World!", result);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public void TestRenderFromFile_NonExistentFile_ThrowsException()
    {
        // 测试不存在的文件
        var nonExistentFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");
        var parameters = new Dictionary<string, object>();
        
        Assert.Throws<TemplateException>(() => _templateUtil.RenderFromFile(nonExistentFile, parameters));
    }

    [Fact]
    public void TestRender_WhitespaceInPlaceholder_HandlesCorrectly()
    {
        // 测试占位符中的空格
        var template = "Hello {{ name }}!";
        var parameters = new Dictionary<string, object>
        {
            { " name ", "World" }
        };
        
        var result = _templateUtil.Render(template, parameters);
        
        Assert.Contains("World", result);
    }
}
