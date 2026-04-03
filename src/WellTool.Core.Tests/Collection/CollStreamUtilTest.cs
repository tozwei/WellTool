using WellTool.Core.Collection;
using Xunit;
using System.Linq;

namespace WellTool.Core.Tests;

public class CollStreamUtilTest
{
    [Fact]
    public void FilterTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var filtered = CollStreamUtil.Filter(list, x => x > 2);
        Assert.Equal(3, filtered.Count);
        Assert.Contains(3, filtered);
        Assert.Contains(4, filtered);
        Assert.Contains(5, filtered);
    }

    [Fact]
    public void MapTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        var mapped = CollStreamUtil.Map(list, x => x * 2);
        Assert.Equal(3, mapped.Count);
        Assert.Equal(2, mapped[0]);
        Assert.Equal(4, mapped[1]);
        Assert.Equal(6, mapped[2]);
    }

    [Fact]
    public void FlatMapTest()
    {
        var list = CollUtil.NewArrayList(new[] { 1, 2 }, new[] { 3, 4 });
        var flat = CollStreamUtil.FlatMap(list, x => x);
        Assert.Equal(4, flat.Count);
    }

    [Fact]
    public void DistinctTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 2, 3, 3, 3);
        var distinct = CollStreamUtil.Distinct(list);
        Assert.Equal(3, distinct.Count);
    }

    [Fact]
    public void SortTest()
    {
        var list = CollUtil.NewArrayList(3, 1, 2);
        var sorted = CollStreamUtil.Sort(list);
        Assert.Equal(1, sorted[0]);
        Assert.Equal(2, sorted[1]);
        Assert.Equal(3, sorted[2]);
    }

    [Fact]
    public void ReverseSortTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        var sorted = CollStreamUtil.Sort(list, (a, b) => b.CompareTo(a));
        Assert.Equal(3, sorted[0]);
        Assert.Equal(2, sorted[1]);
        Assert.Equal(1, sorted[2]);
    }

    [Fact]
    public void LimitTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var limited = CollStreamUtil.Limit(list, 3);
        Assert.Equal(3, limited.Count);
    }

    [Fact]
    public void SkipTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var skipped = CollStreamUtil.Skip(list, 2);
        Assert.Equal(3, skipped.Count);
        Assert.Equal(3, skipped[0]);
    }

    [Fact]
    public void CountTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var count = CollStreamUtil.Count(list, x => x > 2);
        Assert.Equal(3, count);
    }

    [Fact]
    public void AnyMatchTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        Assert.True(CollStreamUtil.AnyMatch(list, x => x == 2));
        Assert.False(CollStreamUtil.AnyMatch(list, x => x == 10));
    }

    [Fact]
    public void AllMatchTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        Assert.True(CollStreamUtil.AllMatch(list, x => x > 0));
        Assert.False(CollStreamUtil.AllMatch(list, x => x > 2));
    }

    [Fact]
    public void NoneMatchTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        Assert.True(CollStreamUtil.NoneMatch(list, x => x > 10));
        Assert.False(CollStreamUtil.NoneMatch(list, x => x == 2));
    }
}
