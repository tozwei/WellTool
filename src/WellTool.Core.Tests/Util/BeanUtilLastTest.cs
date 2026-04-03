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
    public void CopyPropertiesWithIgnoreTest()
    {
        var source = new TestBean { Name = "John", Age = 25 };
        var target = new TestBean();
        BeanUtil.CopyProperties(source, target, "Age");
        Assert.Equal(source.Name, target.Name);
        Assert.NotEqual(source.Age, target.Age);
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
    public void IsEmptyBeanTest()
    {
        var emptyBean = new TestBean();
        var nonEmptyBean = new TestBean { Name = "John" };
        Assert.True(BeanUtil.IsEmpty(emptyBean));
        Assert.False(BeanUtil.IsEmpty(nonEmptyBean));
    }

    [Fact]
    public void DescribeTest()
    {
        var bean = new TestBean { Name = "John", Age = 25 };
        var desc = BeanUtil.Describe(bean);
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
        var bean = new TestBean();
        BeanUtil.FillBean(bean, map);
        Assert.Equal("Jane", bean.Name);
        Assert.Equal(30, bean.Age);
    }

    private class TestBean
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
