using Xunit;
using WellTool.Core.Comparator;
using WellTool.Core.Util;

namespace WellTool.Core.Tests.Comparator;

/// <summary>
/// CompareUtil 测试
/// </summary>
public class CompareUtilTest
{
    [Fact]
    public void CompareTest()
    {
        Assert.True(CompareUtil.Compare(1, 2) < 0);
        Assert.True(CompareUtil.Compare(2, 1) > 0);
        Assert.Equal(0, CompareUtil.Compare(1, 1));
    }

    [Fact]
    public void CompareWithNullTest()
    {
        Assert.True(CompareUtil.Compare(null, 1) < 0);
        Assert.True(CompareUtil.Compare(1, null) > 0);
        Assert.Equal(0, CompareUtil.Compare(null, null));
    }
}
