using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class CollUtilTestExtra
{
    [Fact]
    public void UnionTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2);
        var list2 = CollUtil.NewArrayList(2, 3);
        var union = CollUtil.GetUnion(list1, list2);
        Assert.Equal(3, union.Count);
    }

    [Fact]
    public void IntersectionTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2, 3);
        var list2 = CollUtil.NewArrayList(2, 3, 4);
        var intersection = CollUtil.GetIntersection(list1, list2);
        Assert.Equal(2, intersection.Count);
    }

    [Fact]
    public void DisjunctionTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2, 3);
        var list2 = CollUtil.NewArrayList(2, 3, 4);
        var disj = CollUtil.Disjunction(list1, list2);
        Assert.Equal(2, disj.Count);
        Assert.Contains(1, disj);
    }

    [Fact]
    public void NewArraySetTest()
    {
        var set = CollUtil.NewArraySet(1, 2, 2, 3);
        Assert.Equal(3, set.Count);
    }

    [Fact]
    public void IsEmptyTest()
    {
        var list = CollUtil.NewArrayList<int>();
        Assert.True(CollUtil.IsEmpty(list));
        list.Add(1);
        Assert.False(CollUtil.IsEmpty(list));
    }

    [Fact]
    public void IsNotEmptyTest()
    {
        var list = CollUtil.NewArrayList<int>();
        Assert.False(CollUtil.IsNotEmpty(list));
        list.Add(1);
        Assert.True(CollUtil.IsNotEmpty(list));
    }
}
