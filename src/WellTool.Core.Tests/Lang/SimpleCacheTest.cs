using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class SimpleCacheTest
{
    [Fact]
    public void GetTest()
    {
        var cache = new SimpleCache<string, string>();
        cache.Put("key1", "value1");
        cache.Put("key2", "value2");
        cache.Put("key3", "value3");

        Assert.Equal("value1", cache.Get("key1"));
        Assert.Equal("value2", cache.Get("key2"));
        Assert.Equal("value3", cache.Get("key3"));
    }

    [Fact]
    public void GetWithFactoryTest()
    {
        var cache = new SimpleCache<string, string>();
        cache.Put("key1", "value1");

        Assert.Equal("value1", cache.Get("key1"));
        Assert.Equal("value2", cache.Get("key2", () => "value2"));
        Assert.Equal("value2", cache.Get("key2")); // Now cached
    }

    [Fact]
    public void RemoveTest()
    {
        var cache = new SimpleCache<string, string>();
        cache.Put("key1", "value1");
        cache.Put("key2", "value2");

        Assert.Equal("value1", cache.Get("key1"));
        cache.Remove("key1");
        Assert.Null(cache.Get("key1"));
        Assert.Equal("value2", cache.Get("key2"));
    }

    [Fact]
    public void ClearTest()
    {
        var cache = new SimpleCache<string, string>();
        cache.Put("key1", "value1");
        cache.Put("key2", "value2");

        cache.Clear();

        Assert.Null(cache.Get("key1"));
        Assert.Null(cache.Get("key2"));
    }

    [Fact]
    public void ContainsTest()
    {
        var cache = new SimpleCache<string, string>();
        cache.Put("key1", "value1");

        Assert.True(cache.ContainsKey("key1"));
        Assert.False(cache.ContainsKey("key2"));
    }
}
