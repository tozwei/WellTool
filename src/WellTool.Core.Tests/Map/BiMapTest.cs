using WellTool.Core.Map;
using Xunit;

namespace WellTool.Core.Tests;

public class BiMapTest
{
    [Fact]
    public void GetTest()
    {
        var biMap = new BiMap<string, int>(new Dictionary<string, int>());
        biMap.Put("aaa", 111);
        biMap.Put("bbb", 222);

        Assert.Equal(111, biMap.Get("aaa"));
        Assert.Equal(222, biMap.Get("bbb"));

        Assert.Equal("aaa", biMap.GetKey(111));
        Assert.Equal("bbb", biMap.GetKey(222));
    }

    [Fact]
    public void ComputeIfAbsentTest()
    {
        var biMap = new BiMap<string, int>(new Dictionary<string, int>());
        biMap.Put("aaa", 111);
        biMap.Put("bbb", 222);

        biMap.ComputeIfAbsent("ccc", s => 333);
        Assert.Equal(333, biMap.Get("ccc"));
        Assert.Equal("ccc", biMap.GetKey(333));
    }

    [Fact]
    public void PutIfAbsentTest()
    {
        var biMap = new BiMap<string, int>(new Dictionary<string, int>());
        biMap.Put("aaa", 111);
        biMap.Put("bbb", 222);

        biMap.PutIfAbsent("ccc", 333);
        Assert.Equal(333, biMap.Get("ccc"));
        Assert.Equal("ccc", biMap.GetKey(333));
    }

    [Fact]
    public void InverseTest()
    {
        var biMap = new BiMap<string, int>(new Dictionary<string, int>());
        biMap.Put("aaa", 111);
        biMap.Put("bbb", 222);

        var inverse = biMap.Inverse();
        Assert.Equal("aaa", inverse.Get(111));
        Assert.Equal("bbb", inverse.Get(222));
    }
}
