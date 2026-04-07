using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class IterUtilTest
{
    [Fact]
    public void ToListTest()
    {
        var list = new List<int> { 1, 2, 3 };
        using var iterator = list.GetEnumerator();
        var result = IterUtil.ToList(iterator);
        Assert.Equal(3, result.Count);
        Assert.Contains(1, result);
        Assert.Contains(2, result);
        Assert.Contains(3, result);
    }

    [Fact]
    public void CountTest()
    {
        var list = new List<int> { 1, 2, 3 };
        using var iterator = list.GetEnumerator();
        // IterUtil.Count 需要 predicate 参数
        Assert.Equal(3, IterUtil.Count(iterator, x => true));
    }

    [Fact]
    public void ForEachTest()
    {
        var list = new List<int> { 1, 2, 3 };
        using var iterator = list.GetEnumerator();
        var sum = 0;
        IterUtil.ForEach(iterator, x => sum += x);
        Assert.Equal(6, sum);
    }
}
