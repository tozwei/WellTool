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
        Assert.Equal(100, map.GetInt("count"));
        Assert.Equal(0, map.GetInt("notExist"));
        Assert.Equal(50, map.GetInt("notExist", 50));
    }

    [Fact]
    public void GetStrTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["name"] = "John";
        Assert.Equal("John", map.GetStr("name"));
        Assert.Equal("", map.GetStr("notExist"));
        Assert.Equal("default", map.GetStr("notExist", "default"));
    }

    [Fact]
    public void GetBoolTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["enabled"] = true;
        Assert.True(map.GetBool("enabled"));
        Assert.False(map.GetBool("notExist"));
    }

    [Fact]
    public void IsEmptyTest()
    {
        Dictionary<string, object>? nullMap = null;
        Assert.True(MapUtil.IsEmpty(nullMap));
        Assert.True(MapUtil.IsEmpty(new Dictionary<string, object>()));
        Assert.False(MapUtil.IsEmpty(MapUtil.NewHashMap<string, object>().Set("k", "v")));
    }

    [Fact]
    public void IsNotEmptyTest()
    {
        Assert.False(MapUtil.IsNotEmpty(null));
        Assert.False(MapUtil.IsNotEmpty(new Dictionary<string, object>()));
        Assert.True(MapUtil.IsNotEmpty(MapUtil.NewHashMap<string, object>().Set("k", "v")));
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
