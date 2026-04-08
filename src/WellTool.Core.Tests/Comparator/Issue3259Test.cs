using Xunit;
using WellTool.Core.Comparator;
using System.Collections.Generic;

namespace WellTool.Core.Tests.Comparator;

/// <summary>
/// Issue3259 测试
/// </summary>
public class Issue3259Test
{
    [Fact]
    public void CompareTest()
    {
        var list = new List<string> { "file10.txt", "file2.txt", "file1.txt" };
        var comparator = new ArrayIndexedComparator<string>(list);
        
        Assert.True(comparator.Compare(2, 0) < 0);
        Assert.True(comparator.Compare(0, 1) < 0);
    }
}
