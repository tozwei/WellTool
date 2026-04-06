using WellTool.Core.Collection;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class IterUtilLastTest
{
    [Fact]
    public void ToArrayTest()
    {
        var list = new System.Collections.Generic.List<int> { 1, 2, 3 };
        using var iterator = list.GetEnumerator();
        var result = IterUtil.ToArray(iterator);
        Assert.Equal(3, result.Length);
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
        Assert.Equal(3, IterUtil.Count(iterator, x => true));
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
