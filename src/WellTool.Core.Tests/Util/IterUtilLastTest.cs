using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class IterUtilLastTest
{
    [Fact]
    public void HasNextTest()
    {
        var list = new System.Collections.Generic.List<int> { 1, 2, 3 };
        using var iterator = list.GetEnumerator();
        Assert.True(IterUtil.HasNext(iterator));
    }

    [Fact]
    public void NextTest()
    {
        var list = new System.Collections.Generic.List<int> { 1, 2, 3 };
        using var iterator = list.GetEnumerator();
        Assert.Equal(1, IterUtil.Next(iterator));
    }

    [Fact]
    public void ToListTest()
    {
        var list = new System.Collections.Generic.List<int> { 1, 2, 3 };
        using var iterator = list.GetEnumerator();
        var result = IterUtil.ToList(iterator);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void CountTest()
    {
        var list = new System.Collections.Generic.List<int> { 1, 2, 3 };
        using var iterator = list.GetEnumerator();
        Assert.Equal(3, IterUtil.Count(iterator));
    }

    [Fact]
    public void ForEachTest()
    {
        var list = new System.Collections.Generic.List<int> { 1, 2, 3 };
        using var iterator = list.GetEnumerator();
        var sum = 0;
        IterUtil.ForEach(iterator, x => sum += x);
        Assert.Equal(6, sum);
    }
}
