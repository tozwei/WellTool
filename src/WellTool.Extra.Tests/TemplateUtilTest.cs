namespace WellTool.Extra.Tests;

using WellTool.Extra.Template;
using System.Collections.Generic;
using System.IO;

public class TemplateUtilTest
{
    [Fact]
    public void GetTest()
    {
        var template = TemplateUtil.Get("beetl", "test");
        Assert.Null(template);
    }

    [Fact]
    public void GetByPathTest()
    {
        var template = TemplateUtil.GetByPath("test.ftl");
        Assert.Null(template);
    }

    [Fact]
    public void CreateEngineTest()
    {
        var engine = TemplateUtil.CreateEngine("velocity");
        Assert.Null(engine);
    }

    [Fact]
    public void GetEngineNamesTest()
    {
        var names = TemplateUtil.GetEngineNames();
        Assert.NotNull(names);
    }

    [Fact]
    public void RegisterTest()
    {
        TemplateUtil.Register("custom", new CustomTemplateEngine());
        Assert.True(TemplateUtil.GetEngineNames().Contains("custom"));
    }

    [Fact]
    public void UnregisterTest()
    {
        TemplateUtil.Register("tempEngine", new CustomTemplateEngine());
        TemplateUtil.Unregister("tempEngine");
        Assert.False(TemplateUtil.GetEngineNames().Contains("tempEngine"));
    }

    [Fact]
    public void GetWithNullEngineTest()
    {
        var template = TemplateUtil.Get(null, "test");
        Assert.Null(template);
    }

    [Fact]
    public void GetWithEmptyEngineTest()
    {
        var template = TemplateUtil.Get("", "test");
        Assert.Null(template);
    }
}

internal class CustomTemplateEngine : Template
{
    public void Render(IDictionary<object, object> bindingMap, TextWriter writer)
    {
    }

    public void Render(IDictionary<object, object> bindingMap, Stream output)
    {
    }

    public void Render(IDictionary<object, object> bindingMap, FileInfo file)
    {
    }

    public string Render(IDictionary<object, object> bindingMap)
    {
        return string.Empty;
    }
}
