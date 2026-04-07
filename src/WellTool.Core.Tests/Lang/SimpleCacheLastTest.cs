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
        Xunit.Assert.Equal("value", cache.Get("key"));
    }

    [Fact]
    public void GetWithDefaultTest()
    {
        var cache = new SimpleCache<string, string>();
        var result = cache.Get("nonexistent", () => "default");
        Xunit.Assert.Equal("default", result);
    }
}
