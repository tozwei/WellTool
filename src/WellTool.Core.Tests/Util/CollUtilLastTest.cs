using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class CollUtilLastTest
{
    [Fact]
    public void NewArrayListTest()
    {
        var list = CollUtil.NewArrayList<string>();
        Assert.NotNull(list);
        Assert.Empty(list);
    }

    [Fact]
    public void NewArrayListWithElementsTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void IsEmptyTest()
    {
        Assert.True(CollUtil.IsEmpty(null));
        Assert.True(CollUtil.IsEmpty(new List<string>()));
        Assert.False(CollUtil.IsEmpty(CollUtil.NewArrayList("a")));
    }

    [Fact]
    public void IsNotEmptyTest()
    {
        Assert.False(CollUtil.IsNotEmpty(null));
        Assert.False(CollUtil.IsNotEmpty(new List<string>()));
        Assert.True(CollUtil.IsNotEmpty(CollUtil.NewArrayList("a")));
    }

    [Fact]
    public void ToArrayTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        var array = CollUtil.ToArray(list);
        Assert.Equal(3, array.Length);
    }

    [Fact]
    public void IsEqualTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2, 3);
        var list2 = CollUtil.NewArrayList(1, 2, 3);
        Assert.True(CollUtil.IsEqual(list1, list2));
    }

    [Fact]
    public void IsSubTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2);
        var list2 = CollUtil.NewArrayList(1, 2, 3);
        Assert.True(CollUtil.IsSub(list1, list2));
    }

    [Fact]
    public void PartitionTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var partitions = CollUtil.Partition(list, 2);
        Assert.Equal(3, partitions.Count);
    }

    [Fact]
    public void PageTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        var page = CollUtil.Page(list, 1, 3);
        Assert.Equal(3, page.Count);
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
    public void SortByFieldTest()
    {
        var list = new List<TestItem>
        {
            new() { Name = "B", Value = 1 },
            new() { Name = "A", Value = 2 }
        };
        var sorted = CollUtil.SortByField(list, "Name");
        Assert.Equal("A", sorted[0].Name);
    }

    private class TestItem
    {
        public string Name { get; set; } = "";
        public int Value { get; set; }
    }
}
