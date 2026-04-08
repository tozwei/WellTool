using Xunit;
using WellTool.Core.Bean;
using System.Collections.Generic;

namespace WellTool.Core.Tests;

/// <summary>
/// Bean 测试
/// </summary>
public class BeanTests
{
    [Fact]
    public void CopyPropertiesTest()
    {
        var source = new SourceBean { Name = "test", Age = 25 };
        var target = new TargetBean();
        BeanUtil.CopyProperties(source, target);
        Assert.Equal("test", target.Name);
        Assert.Equal(25, target.Age);
    }

    [Fact]
    public void MapToBeanTest()
    {
        var map = new Dictionary<string, object>
        {
            { "name", "test" },
            { "age", 25 }
        };
        var bean = BeanUtil.MapToBean<SourceBean>(map);
        Assert.Equal("test", bean.Name);
        Assert.Equal(25, bean.Age);
    }

    [Fact]
    public void BeanToMapTest()
    {
        var bean = new SourceBean { Name = "test", Age = 25 };
        var map = BeanUtil.BeanToMap(bean);
        Assert.Equal("test", map["Name"]);
        Assert.Equal(25, map["Age"]);
    }

    public class SourceBean
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }

    public class TargetBean
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
