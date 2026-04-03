using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class SimHashUtilTest
{
    [Fact]
    public void GetSimHashTest()
    {
        var hash = SimHashUtil.GetSimHash("Hello World");
        Assert.NotEqual(0, hash);
    }

    [Fact]
    public void GetHammingDistanceTest()
    {
        var hash1 = SimHashUtil.GetSimHash("Hello");
        var hash2 = SimHashUtil.GetSimHash("World");
        var distance = SimHashUtil.GetHammingDistance(hash1, hash2);
        Assert.True(distance >= 0);
    }

    [Fact]
    public void IsEqualTest()
    {
        var hash1 = SimHashUtil.GetSimHash("Hello");
        var hash2 = SimHashUtil.GetSimHash("Hello");
        Assert.True(SimHashUtil.IsEqual(hash1, hash2, 3));
    }
}
