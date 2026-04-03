using WellTool.Core.Bean;
using Xunit;

namespace WellTool.Core.Tests;

public class BeanDescLastTest
{
    [Fact]
    public void GetTest()
    {
        var desc = BeanDesc.Get(typeof(TestBean));
        Assert.NotNull(desc);
    }

    [Fact]
    public void GetPropTest()
    {
        var desc = BeanDesc.Get(typeof(TestBean));
        var prop = desc.GetProp("Name");
        Assert.NotNull(prop);
    }

    [Fact]
    public void GetPropsTest()
    {
        var desc = BeanDesc.Get(typeof(TestBean));
        var props = desc.GetProps();
        Assert.NotEmpty(props);
    }

    [Fact]
    public void IsReadableTest()
    {
        var desc = BeanDesc.Get(typeof(TestBean));
        Assert.True(desc.IsReadable("Name"));
    }

    [Fact]
    public void IsWritableTest()
    {
        var desc = BeanDesc.Get(typeof(TestBean));
        Assert.True(desc.IsWritable("Name"));
    }

    private class TestBean
    {
        public string Name { get; set; } = "";
    }
}
