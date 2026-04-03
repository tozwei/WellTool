using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class SimpleCacheLastTest
{
    [Fact]
    public void BasicPutGetTest()
    {
        var cache = new SimpleCache<string, string>();
        cache.Put("key", "value");
        Assert.Equal("value", cache.Get("key"));
    }

    [Fact]
    public void GetWithDefaultTest()
    {
        var cache = new SimpleCache<string, string>();
        var result = cache.Get("nonexistent", () => "default");
        Assert.Equal("default", result);
    }
}
