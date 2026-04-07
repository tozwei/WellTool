using WellTool.Core.Collection;
using WellTool.Core.Util;
using Xunit;
using System.Linq;

namespace WellTool.Core.Tests;

public class CollStreamUtilTest
{
    [Fact]
    public void FilterTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var filtered = CollStreamUtil.Filter(list, x => x > 2).ToList();
        Assert.Equal(3, filtered.Count);
        Assert.Contains(3, filtered);
        Assert.Contains(4, filtered);
        Assert.Contains(5, filtered);
    }

    [Fact]
    public void MapTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        var mapped = CollStreamUtil.Map(list, x => x * 2).ToList();
        Assert.Equal(3, mapped.Count);
        Assert.Equal(2, mapped[0]);
        Assert.Equal(4, mapped[1]);
        Assert.Equal(6, mapped[2]);
    }

    [Fact]
    public void FlatMapTest()
    {
        var list = CollUtil.NewArrayList(new[] { 1, 2 }, new[] { 3, 4 });
        var flat = CollStreamUtil.Filter(list, x => true).SelectMany(x => x).ToList();
        Assert.Equal(4, flat.Count);
    }

    [Fact]
    public void DistinctTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 2, 3, 3, 3);
        var distinct = CollStreamUtil.Filter(list, x => true).Distinct().ToList();
        Assert.Equal(3, distinct.Count);
    }

    [Fact]
    public void SortTest()
    {
        var list = CollUtil.NewArrayList(3, 1, 2);
        var sorted = CollStreamUtil.Filter(list, x => true).OrderBy(x => x).ToList();
        Assert.Equal(1, sorted[0]);
        Assert.Equal(2, sorted[1]);
        Assert.Equal(3, sorted[2]);
    }

    [Fact]
    public void ReverseSortTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        var sorted = CollStreamUtil.Filter(list, x => true).OrderByDescending(x => x).ToList();
        Assert.Equal(3, sorted[0]);
        Assert.Equal(2, sorted[1]);
        Assert.Equal(1, sorted[2]);
    }

    [Fact]
    public void LimitTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var limited = CollStreamUtil.Filter(list, x => true).Take(3).ToList();
        Assert.Equal(3, limited.Count);
    }

    [Fact]
    public void SkipTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var skipped = CollStreamUtil.Filter(list, x => true).Skip(2).ToList();
        Assert.Equal(3, skipped.Count);
        Assert.Equal(3, skipped[0]);
    }

    [Fact]
    public void CountTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var count = CollStreamUtil.Filter(list, x => x > 2).Count();
        Assert.Equal(3, count);
    }

    [Fact]
    public void AnyMatchTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        Assert.True(list.Any(x => x == 2));
        Assert.False(list.Any(x => x == 10));
    }

    [Fact]
    public void AllMatchTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        Assert.True(list.All(x => x > 0));
        Assert.False(list.All(x => x > 2));
    }

    [Fact]
    public void NoneMatchTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        Assert.True(!list.Any(x => x > 10));
        Assert.False(!list.Any(x => x == 2));
    }
}
