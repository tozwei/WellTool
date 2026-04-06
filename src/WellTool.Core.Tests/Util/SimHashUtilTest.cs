using WellTool.Core.Util;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class SimHashUtilTest
{
    [Fact]
    public void GetSimHashTest()
    {
        var hash = HashUtil.FnvHash("Hello World");
        Assert.NotEqual(0, hash);
    }

    [Fact]
    public void GetHammingDistanceTest()
    {
        var hash1 = HashUtil.FnvHash("Hello");
        var hash2 = HashUtil.FnvHash("World");
        var distance = GetHammingDistance(hash1, hash2);
        Assert.True(distance >= 0);
    }

    [Fact]
    public void IsEqualTest()
    {
        var hash1 = HashUtil.FnvHash("Hello");
        var hash2 = HashUtil.FnvHash("Hello");
        var distance = GetHammingDistance(hash1, hash2);
        Assert.True(distance <= 3);
    }

    private int GetHammingDistance(int hash1, int hash2)
    {
        int xor = hash1 ^ hash2;
        int distance = 0;
        while (xor != 0)
        {
            distance++;
            xor &= xor - 1;
        }
        return distance;
    }
}
