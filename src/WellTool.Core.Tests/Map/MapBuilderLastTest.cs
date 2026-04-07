using WellTool.Core.Map;
using Xunit;

namespace WellTool.Core.Tests;

public class MapBuilderLastTest
{
    [Fact]
    public void CreateTest()
    {
        var builder = MapBuilder<string, string>.Create();
        Assert.NotNull(builder);
    }

    [Fact]
    public void BuildTest()
    {
        var map = MapBuilder<string, string>.Create()
            .Put("key", "value")
            .Build();
        Assert.NotNull(map);
        Assert.Equal("value", map["key"]);
    }
}
