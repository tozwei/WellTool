using WellTool.Core.Map;
using Xunit;

namespace WellTool.Core.Tests;

public class MapProxyLastTest
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
    }
}
