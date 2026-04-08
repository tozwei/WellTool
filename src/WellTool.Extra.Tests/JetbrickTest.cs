namespace WellTool.Extra.Tests;

using WellTool.Extra.Template;
using WellTool.Extra.Template.Engine.Jetbrick;

public class JetbrickTest
{
    [Fact]
    public void CreateEngineTest()
    {
        var engine = new JetbrickEngine();
        Assert.NotNull(engine);
    }

    [Fact]
    public void CreateEngineWithConfigTest()
    {
        var config = new TemplateConfig("/templates");
        var engine = new JetbrickEngine(config);
        Assert.NotNull(engine);
    }

    [Fact]
    public void InitEngineTest()
    {
        var engine = new JetbrickEngine();
        var config = new TemplateConfig("/test");
        var result = engine.Init(config);
        Assert.NotNull(result);
    }

    [Fact]
    public void GetTemplateTest()
    {
        var engine = new JetbrickEngine();
        var template = engine.GetTemplate("test-template");
        Assert.NotNull(template);
        Assert.IsType<JetbrickTemplate>(template);
    }

    [Fact]
    public void RenderTemplateTest()
    {
        var engine = new JetbrickEngine();
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
        var engine = new JetbrickEngine();
        var template = engine.GetTemplate("#if(${show})Show#end");
        
        var bindingMap = new System.Collections.Generic.Dictionary<object, object>
        {
            { "show", true }
        };
        
        var result = template.Render(bindingMap);
        Assert.NotNull(result);
    }
}
