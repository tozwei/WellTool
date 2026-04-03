using WellTool.Core.Map;
using Xunit;

namespace WellTool.Core.Tests;

public class MapUtilLastTest
{
    [Fact]
    public void NewHashMapTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["key"] = "value";
        Assert.Equal("value", map["key"]);
    }

    [Fact]
    public void BuilderTest()
    {
        var map = MapUtil.Builder<string, int>()
            .Put("a", 1)
            .Put("b", 2)
            .Build();
        Assert.Equal(2, map.Count);
    }

    [Fact]
    public void GetIntTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["count"] = 100;
        Assert.Equal(100, map.GetInt("count"));
    }

    [Fact]
    public void GetStrTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["name"] = "John";
        Assert.Equal("John", map.GetStr("name"));
    }

    [Fact]
    public void GetBoolTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["enabled"] = true;
        Assert.True(map.GetBool("enabled"));
    }

    [Fact]
    public void IsEmptyTest()
    {
        Assert.True(MapUtil.IsEmpty(null));
        Assert.True(MapUtil.IsEmpty(new System.Collections.Generic.Dictionary<string, object>()));
    }

    [Fact]
    public void IsNotEmptyTest()
    {
        Assert.False(MapUtil.IsNotEmpty(null));
        var map = MapUtil.NewHashMap<string, object>();
        map["k"] = "v";
        Assert.True(MapUtil.IsNotEmpty(map));
    }
}
