using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class ZoneUtilLastTest
{
    [Fact]
    public void GetDefaultTest()
    {
        var zone = ZoneUtil.GetDefault();
        Assert.NotNull(zone);
    }

    [Fact]
    public void GetAvailableZonesTest()
    {
        var zones = ZoneUtil.GetAvailableZones();
        Assert.NotNull(zones);
        Assert.NotEmpty(zones);
    }

    [Fact]
    public void GetTest()
    {
        var zone = ZoneUtil.Get("UTC");
        Assert.NotNull(zone);
    }

    [Fact]
    public void GetOffsetTest()
    {
        var offset = ZoneUtil.GetOffset("UTC");
        Assert.Equal(TimeSpan.Zero, offset);
    }
}
