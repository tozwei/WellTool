using WellTool.Core.Bean;
using Xunit;

namespace WellTool.Core.Tests;

public class BeanPathTest
{
    [Fact]
    public void GetTest()
    {
        var bean = new TestBean { Name = "John", Inner = new InnerBean { Value = "test" } };
        var path = BeanPath.Create("Inner.Value");
        var value = path.Get(bean);
        Assert.Equal("test", value);
    }

    [Fact]
    public void SetTest()
    {
        var bean = new TestBean { Name = "John" };
        var path = BeanPath.Create("Inner.Value");
        path.Set(bean, "newValue");
        Assert.Equal("newValue", bean.Inner?.Value);
    }

    [Fact]
    public void CreateTest()
    {
        var path = BeanPath.Create("a.b.c");
        Assert.NotNull(path);
    }

    private class TestBean
    {
        public string Name { get; set; } = "";
        public InnerBean? Inner { get; set; }
    }

    private class InnerBean
    {
        public string Value { get; set; } = "";
    }
}
