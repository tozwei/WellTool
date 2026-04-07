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
        // 使用完全限定名来避免二义性
        var result = cache.Get("nonexistent", new WellTool.Core.Lang.Func<string>(() => "default"));
        Xunit.Assert.Equal("default", result);
    }
}
