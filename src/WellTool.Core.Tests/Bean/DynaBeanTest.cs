using WellTool.Core.Bean;
using Xunit;

namespace WellTool.Core.Tests;

public class DynaBeanTest
{
    [Fact]
    public void CreateAndGetTest()
    {
        var bean = DynaBean.Create(typeof(Person));
        bean.Set("Name", "John");
        bean.Set("Age", 25);
        Assert.Equal("John", bean.Get<string>("Name"));
        Assert.Equal(25, bean.Get<int>("Age"));
    }

    [Fact]
    public void ContainsPropTest()
    {
        var bean = DynaBean.Create(typeof(Person));
        bean.Set("Name", "John");
        Assert.True(bean.ContainsProp("Name"));
        Assert.False(bean.ContainsProp("Age"));
    }

    [Fact]
    public void SetGetTest()
    {
        var bean = DynaBean.Create(typeof(Person));
        bean.Set("Name", "John");
        bean.Set("Age", 25);
        Assert.Equal("John", bean.Get<string>("Name"));
        Assert.Equal(25, bean.Get<int>("Age"));
    }

    [Fact]
    public void GetBeanTest()
    {
        var person = new Person { Name = "John", Age = 25 };
        var bean = DynaBean.Create(person);
        var result = bean.GetBean<Person>();
        Assert.Equal("John", result.Name);
        Assert.Equal(25, result.Age);
    }

    [Fact]
    public void GetBeanClassTest()
    {
        var bean = DynaBean.Create(typeof(Person));
        Assert.Equal(typeof(Person), bean.GetBeanClass());
    }

    private class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
