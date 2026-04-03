using WellTool.Core.Map;
using Xunit;

namespace WellTool.Core.Tests;

public class MapProxyTest
{
    [Fact]
    public void GetTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["key"] = "value";
        var proxy = MapProxy.Create(map);
        Assert.Equal("value", proxy.Get("key"));
    }

    [Fact]
    public void SetTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        var proxy = MapProxy.Create(map);
        proxy.Set("key", "value");
        Assert.Equal("value", map["key"]);
    }

    [Fact]
    public void ContainsKeyTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["key"] = "value";
        var proxy = MapProxy.Create(map);
        Assert.True(proxy.ContainsKey("key"));
        Assert.False(proxy.ContainsKey("notExist"));
    }

    [Fact]
    public void RemoveTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["key"] = "value";
        var proxy = MapProxy.Create(map);
        proxy.Remove("key");
        Assert.False(map.ContainsKey("key"));
    }

    [Fact]
    public void ClearTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["a"] = 1;
        map["b"] = 2;
        var proxy = MapProxy.Create(map);
        proxy.Clear();
        Assert.Empty(map);
    }

    [Fact]
    public void SizeTest()
    {
        var map = MapUtil.NewHashMap<string, object>();
        map["a"] = 1;
        var proxy = MapProxy.Create(map);
        Assert.Equal(1, proxy.Size());
    }
}
