using WellTool.Core.Map;
using Xunit;

namespace WellTool.Core.Tests;

public class MapUtilExtraTest
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
        Assert.Equal(1, map["a"]);
    }

    [Fact]
    public void GetIntTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["count"] = 100;
        Assert.Equal(100, map.GetInt("count"));
        Assert.Equal(0, map.GetInt("notExist"));
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
    public void GetLongTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["num"] = 100L;
        Assert.Equal(100L, map.GetLong("num"));
    }

    [Fact]
    public void GetDoubleTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["price"] = 99.99;
        Assert.Equal(99.99, map.GetDouble("price"), 0.001);
    }

    [Fact]
    public void IsEmptyTest()
    {
        Assert.True(MapUtil.IsEmpty(null));
        Assert.True(MapUtil.IsEmpty(new System.Collections.Generic.Dictionary<string, object>()));
        Assert.False(MapUtil.IsEmpty(MapUtil.NewHashMap<string, object>().Set("k", "v")));
    }

    [Fact]
    public void IsNotEmptyTest()
    {
        Assert.False(MapUtil.IsNotEmpty(null));
        Assert.False(MapUtil.IsNotEmpty(new System.Collections.Generic.Dictionary<string, object>()));
        Assert.True(MapUtil.IsNotEmpty(MapUtil.NewHashMap<string, object>().Set("k", "v")));
    }

    [Fact]
    public void FilterTest()
    {
        var map = MapUtil.NewHashMap<string, int>();
        map["a"] = 1;
        map["b"] = 2;
        map["c"] = 3;
        var filtered = MapUtil.Filter(map, kvp => kvp.Value > 1);
        Assert.Equal(2, filtered.Count);
    }

    [Fact]
    public void MapTest()
    {
        var map = MapUtil.NewHashMap<string, int>();
        map["a"] = 1;
        map["b"] = 2;
        var mapped = MapUtil.Map(map, kvp => kvp.Value * 2);
        Assert.Equal(2, mapped["a"]);
        Assert.Equal(4, mapped["b"]);
    }

    [Fact]
    public void InverseTest()
    {
        var map = MapUtil.NewHashMap<string, int>();
        map["a"] = 1;
        map["b"] = 2;
        var inverted = MapUtil.Inverse(map);
        Assert.Equal("a", inverted[1]);
        Assert.Equal("b", inverted[2]);
    }

    [Fact]
    public void ToListTest()
    {
        var map = MapUtil.NewHashMap<string, int>();
        map["a"] = 1;
        map["b"] = 2;
        var list = MapUtil.ToList(map);
        Assert.Equal(2, list.Count);
    }

    [Fact]
    public void UnionTest()
    {
        var map1 = MapUtil.NewHashMap<string, int>();
        map1["a"] = 1;
        map1["b"] = 2;
        var map2 = MapUtil.NewHashMap<string, int>();
        map2["b"] = 3;
        map2["c"] = 4;
        var union = MapUtil.Union(map1, map2);
        Assert.Equal(3, union.Count);
    }
}
