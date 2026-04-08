using Xunit;
using WellTool.Core.Comparator;
using System.Collections.Generic;

namespace WellTool.Core.Tests.Comparator;

/// <summary>
/// IndexedComparator 测试
/// </summary>
public class IndexedComparatorTest
{
    [Fact]
    public void CompareTest()
    {
        var list = new List<string> { "B", "A", "C" };
        var indices = new int[] { 0, 1, 2 };
        var comparator = new IndexedComparator<string>(list);

        Assert.True(comparator.Compare(0, 1) < 0);
        Assert.True(comparator.Compare(1, 2) < 0);
    }
}
