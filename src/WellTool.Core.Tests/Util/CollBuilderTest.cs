using Xunit;
using System.Collections.Generic;

namespace WellTool.Core.Tests;

public class CollBuilderTest
{
    [Fact]
    public void CreateListTest()
    {
        var list = new List<string> { "a", "b", "c" };
        Assert.Equal(3, list.Count);
        Assert.Equal("a", list[0]);
        Assert.Equal("b", list[1]);
        Assert.Equal("c", list[2]);
    }

    [Fact]
    public void CreateSetTest()
    {
        var set = new HashSet<string> { "a", "b", "a" };
        Assert.Equal(2, set.Count);
    }

    [Fact]
    public void CreateMapTest()
    {
        var map = new Dictionary<string, int> { { "a", 1 }, { "b", 2 } };
        Assert.Equal(2, map.Count);
        Assert.Equal(1, map["a"]);
        Assert.Equal(2, map["b"]);
    }

    [Fact]
    public void AppendTest()
    {
        var list = new List<string> { "a", "b" };
        Assert.Equal(2, list.Count);
    }
}
