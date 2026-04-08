using Xunit;
using WellTool.Core.Collection;

namespace WellTool.Core.Tests.Collection;

/// <summary>
/// RingIndexUtil 测试
/// </summary>
public class RingIndexUtilTest
{
    [Fact]
    public void NextTest()
    {
        Assert.Equal(1, RingIndexUtil.Next(0, 3));
        Assert.Equal(2, RingIndexUtil.Next(1, 3));
        Assert.Equal(0, RingIndexUtil.Next(2, 3));
    }

    [Fact]
    public void PrevTest()
    {
        Assert.Equal(2, RingIndexUtil.Prev(0, 3));
        Assert.Equal(0, RingIndexUtil.Prev(1, 3));
        Assert.Equal(1, RingIndexUtil.Prev(2, 3));
    }

    [Fact]
    public void OffsetTest()
    {
        Assert.Equal(2, RingIndexUtil.Offset(0, 2, 5));
        Assert.Equal(0, RingIndexUtil.Offset(3, 2, 5));
    }
}
