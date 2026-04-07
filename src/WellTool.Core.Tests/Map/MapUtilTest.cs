using WellTool.Core.Map;
using Xunit;

namespace WellTool.Core.Tests;

public class MapUtilTest
{
    [Fact]
    public void NewHashMapTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["key"] = "value";
        Assert.Equal("value", map["key"]);
    }

    [Fact]
    public void GetIntTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["count"] = 100;
        Assert.Equal(100, MapUtil.GetInt(map, "count"));
        Assert.Equal(0, MapUtil.GetInt(map, "notExist"));
        Assert.Equal(50, MapUtil.GetInt(map, "notExist", 50));
    }

    [Fact]
    public void GetStrTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["name"] = "John";
        Assert.Equal("John", MapUtil.GetStr(map, "name"));
        Assert.Equal("", MapUtil.GetStr(map, "notExist"));
        Assert.Equal("default", MapUtil.GetStr(map, "notExist", "default"));
    }

    [Fact]
    public void GetBoolTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["enabled"] = true;
        Assert.True(MapUtil.GetBool(map, "enabled"));
        Assert.False(MapUtil.GetBool(map, "notExist"));
    }

    [Fact]
    public void IsEmptyTest()
    {
        Dictionary<string, object>? nullMap = null;
        Assert.True(MapUtil.IsEmpty<string, object>(nullMap));
        Assert.True(MapUtil.IsEmpty<string, object>(new Dictionary<string, object>()));
        var map = MapUtil.NewHashMap<string, object>();
        map["k"] = "v";
        Assert.False(MapUtil.IsEmpty<string, object>(map));
    }

    [Fact]
    public void IsNotEmptyTest()
    {
        Assert.False(MapUtil.IsNotEmpty<string, object>(null));
        Assert.False(MapUtil.IsNotEmpty<string, object>(new Dictionary<string, object>()));
        var map = MapUtil.NewHashMap<string, object>();
        map["k"] = "v";
        Assert.True(MapUtil.IsNotEmpty<string, object>(map));
    }

    [Fact]
    public void GetAnyTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["a"] = 1;
        map["b"] = 2;
        Assert.Equal(1, map.GetAny<int>("a", "c"));
        Assert.Equal(2, map.GetAny<int>("c", "b"));
        Assert.Equal(0, map.GetAny<int>("c", "d"));
    }

    [Fact]
    public void BuildMapTest()
    {
        var map = MapUtil.Builder<string, int>()
            .Put("a", 1)
            .Put("b", 2)
            .Build();

        Assert.Equal(2, map.Count);
        Assert.Equal(1, map["a"]);
        Assert.Equal(2, map["b"]);
    }
}
