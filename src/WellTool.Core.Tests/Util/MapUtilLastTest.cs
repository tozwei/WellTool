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
        Assert.Equal(100, MapUtil.GetInt(map, "count"));
    }

    [Fact]
    public void GetStrTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["name"] = "John";
        Assert.Equal("John", MapUtil.GetStr(map, "name"));
    }

    [Fact]
    public void GetBoolTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["enabled"] = true;
        Assert.True(MapUtil.GetBool(map, "enabled"));
    }

    [Fact]
    public void IsEmptyTest()
    {
        Assert.True(MapUtil.IsEmpty<string, object>(null));
        Assert.True(MapUtil.IsEmpty<string, object>(new System.Collections.Generic.Dictionary<string, object>()));
    }

    [Fact]
    public void IsNotEmptyTest()
    {
        Assert.False(MapUtil.IsNotEmpty<string, object>(null));
        var map = MapUtil.NewHashMap<string, object>();
        map["k"] = "v";
        Assert.True(MapUtil.IsNotEmpty<string, object>(map));
    }
}

