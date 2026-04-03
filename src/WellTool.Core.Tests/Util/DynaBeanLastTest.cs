using WellTool.Core.Bean;
using Xunit;

namespace WellTool.Core.Tests;

public class DynaBeanLastTest
{
    [Fact]
    public void ConstructorTest()
    {
        var bean = new DynaBean("name", "John");
        Assert.Equal("John", bean.Get("name"));
    }

    [Fact]
    public void SetGetTest()
    {
        var bean = new DynaBean();
        bean.Set("name", "John");
        Assert.Equal("John", bean.Get("name"));
    }

    [Fact]
    public void ContainsTest()
    {
        var bean = new DynaBean();
        bean.Set("name", "John");
        Assert.True(bean.Contains("name"));
        Assert.False(bean.Contains("age"));
    }

    [Fact]
    public void RemoveTest()
    {
        var bean = new DynaBean();
        bean.Set("name", "John");
        bean.Remove("name");
        Assert.False(bean.Contains("name"));
    }
}
