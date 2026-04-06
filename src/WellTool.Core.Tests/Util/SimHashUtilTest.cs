using Xunit;

namespace WellTool.Core.Tests;

public class SimHashUtilTest
{
    [Fact]
    public void GetSimHashTest()
    {
        var hash = WellTool.Core.Util.SimHashUtil.GetSimHash("Hello World");
        Assert.NotEqual(0, hash);
    }

    [Fact]
    public void GetHammingDistanceTest()
    {
        var hash1 = WellTool.Core.Util.SimHashUtil.GetSimHash("Hello");
        var hash2 = WellTool.Core.Util.SimHashUtil.GetSimHash("World");
        var distance = WellTool.Core.Util.SimHashUtil.GetHammingDistance(hash1, hash2);
        Assert.True(distance >= 0);
    }

    [Fact]
    public void IsEqualTest()
    {
        var hash1 = WellTool.Core.Util.SimHashUtil.GetSimHash("Hello");
        var hash2 = WellTool.Core.Util.SimHashUtil.GetSimHash("Hello");
        Assert.True(WellTool.Core.Util.SimHashUtil.IsEqual(hash1, hash2, 3));
    }
}
