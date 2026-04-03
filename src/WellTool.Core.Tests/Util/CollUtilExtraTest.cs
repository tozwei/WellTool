using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class CollUtilExtraTest
{
    [Fact]
    public void GetTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.Equal("a", CollUtil.Get(list, 0));
        Assert.Equal("c", CollUtil.Get(list, -1));
    }

    [Fact]
    public void SetTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        CollUtil.Set(list, 0, "x");
        Assert.Equal("x", list[0]);
    }

    [Fact]
    public void AddTest()
    {
        var list = CollUtil.NewArrayList("a", "b");
        CollUtil.Add(list, "c");
        Assert.Equal(3, list.Count);
        Assert.Equal("c", list[2]);
    }

    [Fact]
    public void RemoveTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        CollUtil.Remove(list, 1);
        Assert.Equal(2, list.Count);
        Assert.Equal("a", list[0]);
        Assert.Equal("c", list[1]);
    }

    [Fact]
    public void RemoveFirstTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        CollUtil.RemoveFirst(list, x => x == "b");
        Assert.Equal(2, list.Count);
        Assert.Equal("a", list[0]);
        Assert.Equal("c", list[1]);
    }

    [Fact]
    public void RemoveLastTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c", "b");
        CollUtil.RemoveLast(list, x => x == "b");
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void GetIntersectionTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2, 3);
        var list2 = CollUtil.NewArrayList(2, 3, 4);
        var intersection = CollUtil.GetIntersection(list1, list2);
        Assert.Equal(2, intersection.Count);
        Assert.Contains(2, intersection);
        Assert.Contains(3, intersection);
    }

    [Fact]
    public void GetUnionTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2);
        var list2 = CollUtil.NewArrayList(2, 3);
        var union = CollUtil.GetUnion(list1, list2);
        Assert.Equal(3, union.Count);
    }

    [Fact]
    public void GetDisjunctionTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2, 3);
        var list2 = CollUtil.NewArrayList(2, 3, 4);
        var disjunction = CollUtil.GetDisjunction(list1, list2);
        Assert.Equal(2, disjunction.Count);
        Assert.Contains(1, disjunction);
        Assert.Contains(4, disjunction);
    }

    [Fact]
    public void IsSubTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2);
        var list2 = CollUtil.NewArrayList(1, 2, 3);
        Assert.True(CollUtil.IsSub(list1, list2));
        Assert.False(CollUtil.IsSub(list2, list1));
    }

    [Fact]
    public void IsEqualTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2, 3);
        var list2 = CollUtil.NewArrayList(1, 2, 3);
        Assert.True(CollUtil.IsEqual(list1, list2));
    }

    [Fact]
    public void FilterTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var filtered = CollUtil.Filter(list, x => x > 2);
        Assert.Equal(3, filtered.Count);
    }

    [Fact]
    public void MapTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        var mapped = CollUtil.Map(list, x => x * 2);
        Assert.Equal(6, mapped[2]);
    }

    [Fact]
    public void FlatMapTest()
    {
        var list = CollUtil.NewArrayList(new[] { 1, 2 }, new[] { 3, 4 });
        var flat = CollUtil.FlatMap(list, x => x.ToList());
        Assert.Equal(4, flat.Count);
    }

    [Fact]
    public void DistinctTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 2, 3, 3, 3);
        var distinct = CollUtil.Distinct(list);
        Assert.Equal(3, distinct.Count);
    }

    [Fact]
    public void SortTest()
    {
        var list = CollUtil.NewArrayList(3, 1, 2);
        var sorted = CollUtil.Sort(list);
        Assert.Equal(1, sorted[0]);
        Assert.Equal(2, sorted[1]);
        Assert.Equal(3, sorted[2]);
    }

    [Fact]
    public void PageTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        var page = CollUtil.Page(list, 2, 3);
        Assert.Equal(3, page.Count);
        Assert.Equal(4, page[0]);
        Assert.Equal(5, page[1]);
        Assert.Equal(6, page[2]);
    }

    [Fact]
    public void PartitionTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var partitions = CollUtil.Partition(list, 2);
        Assert.Equal(3, partitions.Count);
    }
}
