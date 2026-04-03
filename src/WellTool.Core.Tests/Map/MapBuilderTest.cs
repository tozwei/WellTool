using WellTool.Core.Map;
using Xunit;

namespace WellTool.Core.Tests;

public class MapBuilderTest
{
    [Fact]
    public void ConditionPutTest()
    {
        var map = MapBuilder.Create<string, string>()
            .Put(true, "a", "1")
            .Put(false, "b", "2")
            .Put(true, "c", () => GetValue(3))
            .Put(false, "d", () => GetValue(4))
            .Build();

        Assert.Equal("1", map["a"]);
        Assert.False(map.ContainsKey("b"));
        Assert.Equal("3", map["c"]);
        Assert.False(map.ContainsKey("d"));
    }

    [Fact]
    public void PutTest()
    {
        var map = MapBuilder.Create<string, string>()
            .Put("a", "1")
            .Put("b", "2")
            .Put("c", "3")
            .Build();

        Assert.Equal("1", map["a"]);
        Assert.Equal("2", map["b"]);
        Assert.Equal("3", map["c"]);
    }

    [Fact]
    public void PutAllTest()
    {
        var source = new Dictionary<string, string>
        {
            { "a", "1" },
            { "b", "2" }
        };

        var map = MapBuilder.Create<string, string>()
            .PutAll(source)
            .Build();

        Assert.Equal(2, map.Count);
        Assert.Equal("1", map["a"]);
        Assert.Equal("2", map["b"]);
    }

    private string GetValue(int value)
    {
        return value.ToString();
    }
}
