using WellTool.Core.Map;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class MapBuilderTest
{
    [Fact]
    public void PutTest()
    {
        var map = MapBuilder<string, string>.Create()
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

        var map = MapBuilder<string, string>.Create()
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
