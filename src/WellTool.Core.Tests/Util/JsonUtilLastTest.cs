using WellTool.Core.Json;
using Xunit;

namespace WellTool.Core.Tests;

public class JsonUtilLastTest
{
    [Fact]
    public void ToJsonTest()
    {
        var obj = new { Name = "John", Age = 25 };
        var json = JsonUtil.ToJson(obj);
        Assert.Contains("John", json);
    }

    [Fact]
    public void ParseObjectTest()
    {
        var json = "{\"name\":\"John\",\"age\":25}";
        var obj = JsonUtil.ParseObject<User>(json);
        Assert.Equal("John", obj.Name);
    }

    [Fact]
    public void ParseArrayTest()
    {
        var json = "[1,2,3,4,5]";
        var list = JsonUtil.ParseArray<int>(json);
        Assert.Equal(5, list.Count);
    }

    [Fact]
    public void GetValueTest()
    {
        var json = "{\"name\":\"John\"}";
        var name = JsonUtil.GetValue<string>(json, "name");
        Assert.Equal("John", name);
    }

    private class User
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
