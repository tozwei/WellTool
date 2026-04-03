using WellTool.Core.Bean;
using Xunit;

namespace WellTool.Core.Tests;

public class DynaBeanTest
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
        bean.Set("age", 25);
        Assert.Equal("John", bean.Get("name"));
        Assert.Equal(25, bean.Get("age"));
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

    [Fact]
    public void ToBeanTest()
    {
        var bean = new DynaBean();
        bean.Set("name", "John");
        bean.Set("age", 25);
        var obj = bean.ToBean<Person>();
        Assert.Equal("John", obj.Name);
        Assert.Equal(25, obj.Age);
    }

    [Fact]
    public void ToMapTest()
    {
        var bean = new DynaBean();
        bean.Set("name", "John");
        bean.Set("age", 25);
        var map = bean.ToMap();
        Assert.Equal(2, map.Count);
        Assert.Equal("John", map["name"]);
    }

    [Fact]
    public void CloneTest()
    {
        var bean = new DynaBean();
        bean.Set("name", "John");
        var cloned = bean.Clone();
        Assert.Equal("John", cloned.Get("name"));
    }

    private class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
