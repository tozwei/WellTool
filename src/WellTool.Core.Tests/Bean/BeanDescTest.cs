using WellTool.Core.Bean;
using Xunit;

namespace WellTool.Core.Tests;

public class BeanDescTest
{
    [Fact]
    public void GetTest()
    {
        var desc = BeanDesc.Get(typeof(Person));
        Assert.NotNull(desc);
    }

    [Fact]
    public void GetPropTest()
    {
        var desc = BeanDesc.Get(typeof(Person));
        var prop = desc.GetProp("Name");
        Assert.NotNull(prop);
        Assert.Equal("Name", prop.Name);
    }

    [Fact]
    public void GetPropsTest()
    {
        var desc = BeanDesc.Get(typeof(Person));
        var props = desc.GetProps();
        Assert.NotEmpty(props);
    }

    [Fact]
    public void GetSetterTest()
    {
        var desc = BeanDesc.Get(typeof(Person));
        var setter = desc.GetSetter("Name");
        Assert.NotNull(setter);
    }

    [Fact]
    public void GetGetterTest()
    {
        var desc = BeanDesc.Get(typeof(Person));
        var getter = desc.GetGetter("Name");
        Assert.NotNull(getter);
    }

    [Fact]
    public void IsReadableTest()
    {
        var desc = BeanDesc.Get(typeof(Person));
        Assert.True(desc.IsReadable("Name"));
        Assert.True(desc.IsReadable("Age"));
    }

    [Fact]
    public void IsWritableTest()
    {
        var desc = BeanDesc.Get(typeof(Person));
        Assert.True(desc.IsWritable("Name"));
        Assert.True(desc.IsWritable("Age"));
    }

    private class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
