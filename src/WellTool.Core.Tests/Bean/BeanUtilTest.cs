using WellTool.Core.Bean;
using Xunit;

namespace WellTool.Core.Tests;

public class BeanUtilTest
{
    public class SourceBean
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string? Email { get; set; }
    }

    public class TargetBean
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string? Email { get; set; }
    }

    [Fact]
    public void CopyPropertiesTest()
    {
        var source = new SourceBean { Name = "John", Age = 25, Email = "john@test.com" };
        var target = new TargetBean();

        BeanUtil.CopyProperties(source, target);

        Assert.Equal(source.Name, target.Name);
        Assert.Equal(source.Age, target.Age);
        Assert.Equal(source.Email, target.Email);
    }

    [Fact]
    public void CopyPropertiesWithIgnoreTest()
    {
        var source = new SourceBean { Name = "John", Age = 25, Email = "john@test.com" };
        var target = new TargetBean();

        BeanUtil.CopyProperties(source, target, "Email");

        Assert.Equal(source.Name, target.Name);
        Assert.Equal(source.Age, target.Age);
        Assert.Null(target.Email);
    }

    [Fact]
    public void ToBeanTest()
    {
        var map = new Dictionary<string, object>
        {
            { "name", "John" },
            { "age", 25 },
            { "email", "john@test.com" }
        };

        var bean = BeanUtil.ToBean<TargetBean>(map);

        Assert.Equal("John", bean.Name);
        Assert.Equal(25, bean.Age);
        Assert.Equal("john@test.com", bean.Email);
    }

    [Fact]
    public void IsEmptyBeanTest()
    {
        var emptyBean = new TargetBean();
        var nonEmptyBean = new TargetBean { Name = "John" };

        Assert.True(BeanUtil.IsEmpty(emptyBean));
        Assert.False(BeanUtil.IsEmpty(nonEmptyBean));
    }

    [Fact]
    public void DescribeTest()
    {
        var bean = new TargetBean { Name = "John", Age = 25 };
        var desc = BeanUtil.Describe(bean);

        Assert.Equal("John", desc["Name"]);
        Assert.Equal(25, desc["Age"]);
    }

    [Fact]
    public void FillBeanTest()
    {
        var map = new Dictionary<string, object>
        {
            { "name", "Jane" },
            { "age", 30 }
        };

        var bean = new TargetBean();
        BeanUtil.FillBean(bean, map);

        Assert.Equal("Jane", bean.Name);
        Assert.Equal(30, bean.Age);
    }
}
