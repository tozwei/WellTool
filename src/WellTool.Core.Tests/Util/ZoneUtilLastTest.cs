using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class ZoneUtilLastTest
{
    [Fact]
    public void ToDateTimeOffsetTest()
    {
        var offset = ZoneUtil.ToDateTimeOffset(null);
        Assert.NotNull(offset);
    }

    [Fact]
    public void GetLocalTimeZoneTest()
    {
        var zone = ZoneUtil.GetLocalTimeZone();
        Assert.NotNull(zone);
    }
}
