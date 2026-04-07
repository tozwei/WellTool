using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class ZoneUtilTest
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
