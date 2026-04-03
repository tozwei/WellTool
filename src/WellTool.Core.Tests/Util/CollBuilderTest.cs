using WellTool.Core.Builder;
using Xunit;

namespace WellTool.Core.Tests;

public class CollBuilderTest
{
    [Fact]
    public void CreateListTest()
    {
        var list = CollBuilder<string>.Create().Append("a", "b", "c").Build();
        Assert.Equal(3, list.Count);
        Assert.Equal("a", list[0]);
        Assert.Equal("b", list[1]);
        Assert.Equal("c", list[2]);
    }

    [Fact]
    public void CreateSetTest()
    {
        var set = CollBuilder<string>.CreateSet().Append("a", "b", "a").Build();
        Assert.Equal(2, set.Count);
    }

    [Fact]
    public void CreateMapTest()
    {
        var map = CollBuilder<string, int>.CreateMap()
            .Append("a", 1)
            .Append("b", 2)
            .Build();
        Assert.Equal(2, map.Count);
        Assert.Equal(1, map["a"]);
        Assert.Equal(2, map["b"]);
    }

    [Fact]
    public void AppendTest()
    {
        var list = CollBuilder<string>.Create().Append("a").Append("b").Build();
        Assert.Equal(2, list.Count);
    }
}
