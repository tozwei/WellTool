using WellTool.Core.Map;
using Xunit;

namespace WellTool.Core.Tests;

public class MapBuilderLastTest
{
    [Fact]
    public void CreateTest()
    {
        var builder = MapBuilder.Create<string, string>();
        Assert.NotNull(builder);
    }

    [Fact]
    public void BuildTest()
    {
        var map = MapBuilder.Create<string, string>()
            .Put("key", "value")
            .Build();
        Assert.NotNull(map);
        Assert.Equal("value", map["key"]);
    }
}
