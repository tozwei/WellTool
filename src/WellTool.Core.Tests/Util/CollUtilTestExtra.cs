using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class CollUtilTestExtra
{
    [Fact]
    public void UnionTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2);
        var list2 = CollUtil.NewArrayList(2, 3);
        var union = CollUtil.Union(list1, list2);
        Assert.Equal(3, union.Count);
    }

    [Fact]
    public void IntersectionTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2, 3);
        var list2 = CollUtil.NewArrayList(2, 3, 4);
        var intersection = CollUtil.Intersection(list1, list2);
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
        Assert.Contains(4, disj);
    }

    [Fact]
    public void SubtractTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2, 3);
        var list2 = CollUtil.NewArrayList(2, 3);
        var diff = CollUtil.Subtract(list1, list2);
        Assert.Single(diff);
        Assert.Contains(1, diff);
    }

    [Fact]
    public void AddAllIfNotContainsTest()
    {
        var list = CollUtil.NewArrayList(1, 2);
        var toAdd = CollUtil.NewArrayList(2, 3, 4);
        CollUtil.AddAllIfNotContains(list, toAdd);
        Assert.Equal(4, list.Count);
        Assert.Contains(3, list);
        Assert.Contains(4, list);
    }

    [Fact]
    public void NewLinkedListTest()
    {
        var list = CollUtil.NewLinkedList(1, 2, 3);
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void NewArraySetTest()
    {
        var set = CollUtil.NewArraySet(1, 2, 2, 3);
        Assert.Equal(3, set.Count);
    }
}
