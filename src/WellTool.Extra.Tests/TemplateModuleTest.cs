using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WellTool.Extra.Template;

namespace WellTool.Extra.Tests;

/// <summary>
/// TemplateEngine 接口测试类
/// </summary>
public class TemplateEngineTest
{
    /// <summary>
    /// 测试使用默认配置创建引擎
    /// </summary>
    [Fact]
    public void TestCreate_WithDefaultConfig()
    {
        var engine = TemplateFactory.Create();
        Assert.NotNull(engine);
    }

    /// <summary>
    /// 测试使用自定义配置创建引擎
    /// </summary>
    [Fact]
    public void TestCreate_WithCustomConfig()
    {
        var config = new TemplateConfig("test/path");
        var engine = TemplateFactory.Create(config);
        Assert.NotNull(engine);
    }

    /// <summary>
    /// 测试引擎初始化
    /// </summary>
    [Fact]
    public void TestInit()
    {
        var engine = TemplateFactory.Create();
        var config = new TemplateConfig("new/path");
        
        var result = engine.Init(config);
        Assert.Same(engine, result);
    }

    /// <summary>
    /// 测试获取模板
    /// </summary>
    [Fact]
    public void TestGetTemplate()
    {
        var engine = TemplateFactory.Create();
        var template = engine.GetTemplate("test-resource");
        
        Assert.NotNull(template);
    }
}

/// <summary>
/// TemplateFactory 工厂类测试
/// </summary>
public class TemplateFactoryTest
{
    /// <summary>
    /// 测试创建默认引擎
    /// </summary>
    [Fact]
    public void TestCreate_Default()
    {
        var engine = TemplateFactory.Create();
        Assert.NotNull(engine);
    }

    /// <summary>
    /// 测试创建带配置的引擎
    /// </summary>
    [Fact]
    public void TestCreate_WithConfig()
    {
        var config = new TemplateConfig();
        var engine = TemplateFactory.Create(config);
        Assert.NotNull(engine);
    }
}

/// <summary>
/// TemplateConfig 配置类测试
/// </summary>
public class TemplateConfigTest
{
    /// <summary>
    /// 测试默认配置
    /// </summary>
    [Fact]
    public void TestDefaultConfig()
    {
        var config = TemplateConfig.Default;
        Assert.NotNull(config);
    }

    /// <summary>
    /// 测试无参构造
    /// </summary>
    [Fact]
    public void TestConstructor_NoParams()
    {
        var config = new TemplateConfig();
        Assert.NotNull(config);
        Assert.Equal(Encoding.UTF8, config.Charset);
    }

    /// <summary>
    /// 测试带路径构造
    /// </summary>
    [Fact]
    public void TestConstructor_WithPath()
    {
        var config = new TemplateConfig("/templates");
        Assert.Equal("/templates", config.Path);
        Assert.Equal(Encoding.UTF8, config.Charset);
    }

    /// <summary>
    /// 测试带路径和资源模式构造
    /// </summary>
    [Fact]
    public void TestConstructor_WithPathAndResourceMode()
    {
        var config = new TemplateConfig("/templates", TemplateConfig.ResourceModeType.File);
        Assert.Equal("/templates", config.Path);
        Assert.Equal(TemplateConfig.ResourceModeType.File, config.ResourceMode);
    }

    /// <summary>
    /// 测试带编码、路径和资源模式构造
    /// </summary>
    [Fact]
    public void TestConstructor_Full()
    {
        var config = new TemplateConfig(Encoding.UTF8, "/templates", TemplateConfig.ResourceModeType.ClassPath);
        Assert.Equal(Encoding.UTF8, config.Charset);
        Assert.Equal("/templates", config.Path);
        Assert.Equal(TemplateConfig.ResourceModeType.ClassPath, config.ResourceMode);
    }

    /// <summary>
    /// 测试获取编码字符串
    /// </summary>
    [Fact]
    public void TestGetCharsetStr()
    {
        var config = new TemplateConfig(Encoding.UTF8, "/templates", TemplateConfig.ResourceModeType.String);
        var charsetStr = config.GetCharsetStr();
        Assert.Contains("UTF-8", charsetStr);
    }

    /// <summary>
    /// 测试设置自定义引擎
    /// </summary>
    [Fact]
    public void TestSetCustomEngine()
    {
        var config = new TemplateConfig();
        var result = config.SetCustomEngine(typeof(string));
        Assert.Same(config, result);
        Assert.Equal(typeof(string), config.CustomEngine);
    }

    /// <summary>
    /// 测试设置是否使用缓存
    /// </summary>
    [Fact]
    public void TestSetUseCache()
    {
        var config = new TemplateConfig();
        Assert.True(config.UseCache);
        
        var result = config.SetUseCache(false);
        Assert.Same(config, result);
        Assert.False(config.UseCache);
    }

    /// <summary>
    /// 测试资源模式枚举值
    /// </summary>
    [Theory]
    [InlineData(TemplateConfig.ResourceModeType.ClassPath)]
    [InlineData(TemplateConfig.ResourceModeType.File)]
    [InlineData(TemplateConfig.ResourceModeType.WebRoot)]
    [InlineData(TemplateConfig.ResourceModeType.String)]
    [InlineData(TemplateConfig.ResourceModeType.Composite)]
    public void TestResourceModeType(TemplateConfig.ResourceModeType mode)
    {
        var config = new TemplateConfig("/test", mode);
        Assert.Equal(mode, config.ResourceMode);
    }
}

/// <summary>
/// AbstractTemplate 抽象模板测试
/// </summary>
public class AbstractTemplateTest
{
    /// <summary>
    /// 测试渲染为字符串
    /// </summary>
    [Fact]
    public void TestRender_ToString()
    {
        var engine = TemplateFactory.Create();
        var template = engine.GetTemplate("test-content");
        
        var bindingMap = new Dictionary<object, object>();
        var result = template.Render(bindingMap);
        
        Assert.NotNull(result);
    }

    /// <summary>
    /// 测试渲染到TextWriter
    /// </summary>
    [Fact]
    public void TestRender_ToTextWriter()
    {
        var engine = TemplateFactory.Create();
        var template = engine.GetTemplate("test-content");
        
        var bindingMap = new Dictionary<object, object>();
        using var writer = new StringWriter();
        
        template.Render(bindingMap, writer);
        
        var result = writer.ToString();
        Assert.NotNull(result);
    }

    /// <summary>
    /// 测试渲染到Stream
    /// </summary>
    [Fact]
    public void TestRender_ToStream()
    {
        var engine = TemplateFactory.Create();
        var template = engine.GetTemplate("test-content");
        
        var bindingMap = new Dictionary<object, object>();
        using var stream = new MemoryStream();
        
        template.Render(bindingMap, stream);
        
        Assert.True(stream.Length > 0);
    }

    /// <summary>
    /// 测试渲染到FileInfo
    /// </summary>
    [Fact]
    public void TestRender_ToFileInfo()
    {
        var engine = TemplateFactory.Create();
        var template = engine.GetTemplate("test-content");
        
        var bindingMap = new Dictionary<object, object>();
        var tempFile = new FileInfo(Path.GetTempFileName());
        
        try
        {
            template.Render(bindingMap, tempFile);
            Assert.True(tempFile.Exists);
            Assert.True(tempFile.Length > 0);
        }
        finally
        {
            if (tempFile.Exists)
            {
                tempFile.Delete();
            }
        }
    }

    /// <summary>
    /// 测试渲染空绑定参数
    /// </summary>
    [Fact]
    public void TestRender_EmptyBindingMap()
    {
        var engine = TemplateFactory.Create();
        var template = engine.GetTemplate("content");
        
        var result = template.Render(new Dictionary<object, object>());
        Assert.NotNull(result);
    }

    /// <summary>
    /// 测试渲染带参数的绑定
    /// </summary>
    [Fact]
    public void TestRender_WithBindingParams()
    {
        var engine = TemplateFactory.Create();
        var template = engine.GetTemplate("content");
        
        var bindingMap = new Dictionary<object, object>
        {
            { "name", "test" },
            { "value", 123 }
        };
        
        var result = template.Render(bindingMap);
        Assert.NotNull(result);
    }
}

/// <summary>
/// Template 接口测试
/// </summary>
public class TemplateInterfaceTest
{
    /// <summary>
    /// 测试模板渲染方法存在
    /// </summary>
    [Fact]
    public void TestTemplate_HasRenderMethods()
    {
        var engine = TemplateFactory.Create();
        var template = engine.GetTemplate("test");
        
        // 验证模板实现了所有必需的方法
        Assert.NotNull(template);
        
        var bindingMap = new Dictionary<object, object>();
        var result = template.Render(bindingMap);
        Assert.NotNull(result);
    }
}
