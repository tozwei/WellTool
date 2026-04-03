using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class ConvertToBeanTest
{
    [Fact]
    public void ToBeanTest()
    {
        var map = new System.Collections.Generic.Dictionary<string, object>
        {
            { "name", "John" },
            { "age", 25 }
        };
        var bean = Convert.ToBean<User>(map);
        Assert.Equal("John", bean.Name);
        Assert.Equal(25, bean.Age);
    }

    [Fact]
    public void ToBeanIgnoreCaseTest()
    {
        var map = new System.Collections.Generic.Dictionary<string, object>
        {
            { "NAME", "John" },
            { "AGE", 25 }
        };
        var bean = Convert.ToBean<User>(map, true);
        Assert.Equal("John", bean.Name);
    }

    [Fact]
    public void ToBeanFromJsonTest()
    {
        var json = "{\"name\":\"Jane\",\"age\":30}";
        var bean = Convert.ToBean<User>(json);
        Assert.Equal("Jane", bean.Name);
        Assert.Equal(30, bean.Age);
    }

    [Fact]
    public void BeanToMapTest()
    {
        var bean = new User { Name = "John", Age = 25 };
        var map = Convert.ToMap(bean);
        Assert.Equal(2, map.Count);
        Assert.Equal("John", map["Name"]);
        Assert.Equal(25, map["Age"]);
    }

    private class User
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
