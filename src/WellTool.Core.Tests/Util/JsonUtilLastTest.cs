using WellTool.Json;
using Xunit;

namespace WellTool.Core.Tests;

public class JsonUtilLastTest
{
    [Fact]
    public void ToJsonTest()
    {
        var obj = new { Name = "John", Age = 25 };
        var json = JSONUtil.ToJson(obj);
        Assert.Contains("John", json);
    }

    [Fact]
    public void ParseObjectTest()
    {
        var json = "{\"name\":\"John\",\"age\":25}";
        var jsonObj = JSONUtil.ParseObj(json);
        Assert.NotNull(jsonObj);
    }

    [Fact]
    public void ParseArrayTest()
    {
        var json = "[1,2,3,4,5]";
        var jsonArray = JSONUtil.ParseArray(json);
        Assert.NotNull(jsonArray);
    }

    [Fact]
    public void GetByPathTest()
    {
        var json = "{\"name\":\"John\"}";
        var jsonObj = JSONUtil.ParseObj(json);
        var name = JSONUtil.GetByPath(jsonObj, "name");
        Assert.Equal("John", name);
    }

    private class User
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
