using Xunit;
using WellTool.Core.Comparator;

namespace WellTool.Core.Tests.Comparator;

/// <summary>
/// WindowsExplorerStringComparator 测试
/// </summary>
public class WindowsExplorerStringComparatorTest
{
    [Fact]
    public void CompareTest()
    {
        var comparator = new WindowsExplorerStringComparator();
        Assert.True(comparator.Compare("a1.txt", "a2.txt") < 0);
        Assert.True(comparator.Compare("a10.txt", "a2.txt") > 0);
    }
}
