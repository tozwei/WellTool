using WellTool.Core.Bean;
using System.Collections.Generic;
using Xunit;

namespace WellTool.Core.Tests;

public class DynaBeanLastTest
{
    [Fact]
    public void ConstructorTest()
    {
        var map = new Dictionary<string, object> { { "name", "John" } };
        var bean = new DynaBean(map);
        Assert.Equal("John", bean.Get<string>("name"));
    }

    [Fact]
    public void SetGetTest()
    {
        var map = new Dictionary<string, object>();
        var bean = new DynaBean(map);
        bean.Set("name", "John");
        Assert.Equal("John", bean.Get<string>("name"));
    }

    [Fact]
    public void ContainsTest()
    {
        var map = new Dictionary<string, object>();
        var bean = new DynaBean(map);
        bean.Set("name", "John");
        Assert.True(bean.ContainsProp("name"));
        Assert.False(bean.ContainsProp("age"));
    }

    [Fact]
    public void RemoveTest()
    {
        var map = new Dictionary<string, object>();
        var bean = new DynaBean(map);
        bean.Set("name", "John");
        map.Remove("name");
        Assert.False(bean.ContainsProp("name"));
    }
}
