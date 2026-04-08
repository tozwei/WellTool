using Xunit;
using WellTool.Core.Collection;
using System.Collections.Generic;
using WellTool.Core.Map;

namespace WellTool.Core.Tests.Collection;

/// <summary>
/// MapProxy 测试
/// </summary>
public class MapProxyTest
{
    [Fact]
    public void Test()
    {
        var map = new Dictionary<string, object> { { "key", "value" } };
        var proxy = MapProxy.Create(map);
        Assert.Equal("value", proxy["key"]);
        
        proxy["key2"] = "value2";
        Assert.Equal("value2", proxy["key2"]);
    }

    [Fact]
    public void ContainsKeyTest()
    {
        var map = new Dictionary<string, object> { { "key", "value" } };
        var proxy = MapProxy.Create(map);
        Assert.True(proxy.ContainsKey("key"));
        Assert.False(proxy.ContainsKey("notexist"));
    }
}
