using WellTool.Core.Bean;
using Xunit;
using System.Collections.Generic;

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
    public void MapToBeanTest()
    {
        var map = new Dictionary<string, object>
        {
            { "Name", "John" },
            { "Age", 25 },
            { "Email", "john@test.com" }
        };

        var bean = BeanUtil.MapToBean<TargetBean>(map);

        Assert.Equal("John", bean.Name);
        Assert.Equal(25, bean.Age);
        Assert.Equal("john@test.com", bean.Email);
    }

    [Fact]
    public void BeanToMapTest()
    {
        var source = new TargetBean { Name = "John", Age = 25 };
        var map = BeanUtil.BeanToMap(source);

        // BeanToMap 默认将字段名转换为小写
        Assert.Equal("John", map["name"]);
        Assert.Equal(25, map["age"]);
    }

    [Fact]
    public void FillBeanTest()
    {
        var map = new Dictionary<string, object>
        {
            { "Name", "Jane" },
            { "Age", 30 }
        };

        var bean = new TargetBean();
        BeanUtil.FillBean(bean, map);

        Assert.Equal("Jane", bean.Name);
        Assert.Equal(30, bean.Age);
    }

    [Fact]
    public void ToBeanTest()
    {
        var map = new Dictionary<string, object>
        {
            { "Name", "John" },
            { "Age", 25 }
        };

        var bean = BeanUtil.ToBean<TargetBean>(map);
        Assert.Equal("John", bean.Name);
        Assert.Equal(25, bean.Age);
    }
}
