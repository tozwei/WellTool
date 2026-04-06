using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class CollUtilExtraTest
{
    [Fact]
    public void GetOrDefaultTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.Equal("a", CollUtil.GetOrDefault(list, 0));
        Assert.Equal(default(string), CollUtil.GetOrDefault(list, -1));
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
    public void PageTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        var page = CollUtil.Page(list, 1, 3); // 注意：这里pageIndex从0开始
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

    [Fact]
    public void RemoveLastTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        CollUtil.RemoveLast(list);
        Assert.Equal(2, list.Count);
        Assert.Equal("b", list[1]);
    }

    [Fact]
    public void SortByFieldTest()
    {
        var list = CollUtil.NewArrayList(new { Value = 3 }, new { Value = 1 }, new { Value = 2 });
        var sorted = CollUtil.SortByField(list, "Value");
        Assert.Equal(1, sorted[0].Value);
        Assert.Equal(2, sorted[1].Value);
        Assert.Equal(3, sorted[2].Value);
    }
}
