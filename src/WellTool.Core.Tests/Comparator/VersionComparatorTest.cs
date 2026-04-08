using Xunit;
using WellTool.Core.Comparator;

namespace WellTool.Core.Tests.Comparator;

/// <summary>
/// VersionComparator 测试
/// </summary>
public class VersionComparatorTest
{
    [Fact]
    public void CompareTest()
    {
        var comparator = new VersionComparator();
        Assert.True(comparator.Compare("1.0.0", "2.0.0") < 0);
        Assert.True(comparator.Compare("2.0.0", "1.0.0") > 0);
        Assert.Equal(0, comparator.Compare("1.0.0", "1.0.0"));
    }

    [Fact]
    public void CompareWithDifferentLengthTest()
    {
        var comparator = new VersionComparator();
        Assert.True(comparator.Compare("1.0", "1.0.0") < 0);
        Assert.True(comparator.Compare("1.0.0.0", "1.0.0") > 0);
    }
}
