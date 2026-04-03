using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class DictTest
{
    [Fact]
    public void CreateTest()
    {
        var dict = Dict.Create()
            .Set("name", "John")
            .Set("age", 25);

        Assert.Equal("John", dict["name"]);
        Assert.Equal(25, dict["age"]);
    }

    [Fact]
    public void GetStrTest()
    {
        var dict = Dict.Create().Set("name", "John");
        Assert.Equal("John", dict.GetStr("name"));
        Assert.Equal("default", dict.GetStr("notExist", "default"));
    }

    [Fact]
    public void GetIntTest()
    {
        var dict = Dict.Create().Set("age", 25);
        Assert.Equal(25, dict.GetInt("age"));
        Assert.Equal(0, dict.GetInt("notExist"));
        Assert.Equal(100, dict.GetInt("notExist", 100));
    }

    [Fact]
    public void GetLongTest()
    {
        var dict = Dict.Create().Set("num", 1000000L);
        Assert.Equal(1000000L, dict.GetLong("num"));
    }

    [Fact]
    public void GetBoolTest()
    {
        var dict = Dict.Create().Set("enabled", true);
        Assert.True(dict.GetBool("enabled"));
        Assert.False(dict.GetBool("notExist"));
    }

    [Fact]
    public void GetDoubleTest()
    {
        var dict = Dict.Create().Set("price", 99.99);
        Assert.Equal(99.99, dict.GetDouble("price"), 0.001);
    }

    [Fact]
    public void ContainsKeyTest()
    {
        var dict = Dict.Create().Set("name", "John");
        Assert.True(dict.ContainsKey("name"));
        Assert.False(dict.ContainsKey("notExist"));
    }

    [Fact]
    public void RemoveTest()
    {
        var dict = Dict.Create().Set("name", "John").Set("age", 25);
        dict.Remove("age");

        Assert.True(dict.ContainsKey("name"));
        Assert.False(dict.ContainsKey("age"));
    }

    [Fact]
    public void ToMapTest()
    {
        var dict = Dict.Create().Set("name", "John").Set("age", 25);
        var map = dict.ToMap();

        Assert.Equal(2, map.Count);
        Assert.Equal("John", map["name"]);
    }
}
