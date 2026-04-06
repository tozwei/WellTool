using WellTool.Core.Bean;
using Xunit;

namespace WellTool.Core.Tests;

public class BeanDescTest
{
    [Fact]
    public void GetBeanDescTest()
    {
        var desc = BeanDesc.GetBeanDesc(typeof(Person));
        Assert.NotNull(desc);
    }

    [Fact]
    public void GetPropTest()
    {
        var desc = BeanDesc.GetBeanDesc(typeof(Person));
        var prop = desc.GetProp("Name");
        Assert.NotNull(prop);
    }

    [Fact]
    public void GetPropsTest()
    {
        var desc = BeanDesc.GetBeanDesc(typeof(Person));
        var props = desc.GetProps();
        Assert.NotEmpty(props);
    }

    private class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
