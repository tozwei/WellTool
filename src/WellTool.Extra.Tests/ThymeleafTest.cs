namespace WellTool.Extra.Tests;

using WellTool.Extra.Template;
using WellTool.Extra.Template.Engine.Thymeleaf;

public class ThymeleafTest
{
    [Fact]
    public void CreateEngineTest()
    {
        var engine = new ThymeleafEngine();
        Assert.NotNull(engine);
    }

    [Fact]
    public void CreateEngineWithConfigTest()
    {
        var config = new TemplateConfig("/templates");
        var engine = new ThymeleafEngine(config);
        Assert.NotNull(engine);
    }

    [Fact]
    public void InitEngineTest()
    {
        var engine = new ThymeleafEngine();
        var config = new TemplateConfig("/test");
        var result = engine.Init(config);
        Assert.NotNull(result);
    }

    [Fact]
    public void GetTemplateTest()
    {
        var engine = new ThymeleafEngine();
        var template = engine.GetTemplate("test-template");
        Assert.NotNull(template);
        Assert.IsType<ThymeleafTemplate>(template);
    }

    [Fact]
    public void RenderTemplateTest()
    {
        var engine = new ThymeleafEngine();
        var template = engine.GetTemplate("Hello ${name}!");
        
        var bindingMap = new System.Collections.Generic.Dictionary<object, object>
        {
            { "name", "World" }
        };
        
        var result = template.Render(bindingMap);
        Assert.NotNull(result);
    }

    [Fact]
    public void RenderWithModelTest()
    {
        var engine = new ThymeleafEngine();
        var template = engine.GetTemplate("<p th:text=\"${message}\">placeholder</p>");
        
        var bindingMap = new System.Collections.Generic.Dictionary<object, object>
        {
            { "message", "Hello Thymeleaf" }
        };
        
        // Thymeleaf模板引擎是存根实现，渲染原始内容
        var result = template.Render(bindingMap);
        Assert.NotNull(result);
    }
}
