using WellTool.Core.Bean;
using Xunit;

namespace WellTool.Core.Tests;

public class BeanUtilLastTest
{
    [Fact]
    public void CopyPropertiesTest()
    {
        var source = new TestBean { Name = "John", Age = 25 };
        var target = new TestBean();
        BeanUtil.CopyProperties(source, target);
        Assert.Equal(source.Name, target.Name);
        Assert.Equal(source.Age, target.Age);
    }

    [Fact]
    public void ToBeanTest()
    {
        var map = new System.Collections.Generic.Dictionary<string, object>
        {
            { "Name", "John" },
            { "Age", 25 }
        };
        var bean = BeanUtil.ToBean<TestBean>(map);
        Assert.Equal("John", bean.Name);
        Assert.Equal(25, bean.Age);
    }

    [Fact]
    public void DescribeTest()
    {
        var bean = new TestBean { Name = "John", Age = 25 };
        var desc = BeanUtil.BeanToMap(bean);
        Assert.Equal("John", desc["Name"]);
        Assert.Equal(25, desc["Age"]);
    }

    [Fact]
    public void FillBeanTest()
    {
        var map = new System.Collections.Generic.Dictionary<string, object>
        {
            { "Name", "Jane" },
            { "Age", 30 }
        };
        var bean = BeanUtil.MapToBean<TestBean>(map);
        Assert.Equal("Jane", bean.Name);
        Assert.Equal(30, bean.Age);
    }

    private class TestBean
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
