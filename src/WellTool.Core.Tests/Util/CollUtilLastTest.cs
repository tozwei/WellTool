using WellTool.Core.Collection;
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
    public void NewLinkedListTest()
    {
        var list = CollUtil.NewLinkedList<string>();
        Assert.NotNull(list);
    }

    [Fact]
    public void NewHashSetTest()
    {
        var set = CollUtil.NewHashSet<string>();
        Assert.NotNull(set);
    }

    [Fact]
    public void NewLinkedHashSetTest()
    {
        var set = CollUtil.NewLinkedHashSet<string>();
        Assert.NotNull(set);
    }

    [Fact]
    public void NewTreeSetTest()
    {
        var set = CollUtil.NewTreeSet<string>();
        Assert.NotNull(set);
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
    public void GetFirstTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.Equal("a", CollUtil.GetFirst(list));
    }

    [Fact]
    public void GetLastTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.Equal("c", CollUtil.GetLast(list));
    }

    [Fact]
    public void AddIfNotNullTest()
    {
        var list = CollUtil.NewArrayList<string>();
        CollUtil.AddIfNotNull(list, "a");
        CollUtil.AddIfNotNull(list, null);
        Assert.Single(list);
    }

    [Fact]
    public void AddAllIfNotNullTest()
    {
        var list = CollUtil.NewArrayList<string>();
        var toAdd = new[] { "a", "b" };
        CollUtil.AddAllIfNotNull(list, toAdd);
        Assert.Equal(2, list.Count);
    }

    [Fact]
    public void ReverseTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        CollUtil.Reverse(list);
        Assert.Equal("c", list[0]);
        Assert.Equal("a", list[2]);
    }

    [Fact]
    public void SubTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c", "d");
        var sub = CollUtil.Sub(list, 1, 3);
        Assert.Equal(2, sub.Count);
        Assert.Equal("b", sub[0]);
    }

    [Fact]
    public void ToListTest()
    {
        var array = new[] { 1, 2, 3 };
        var list = CollUtil.ToList(array);
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void ToArrayTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        var array = CollUtil.ToArray(list);
        Assert.Equal(3, array.Length);
    }

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
        var inter = CollUtil.Intersection(list1, list2);
        Assert.Equal(2, inter.Count);
    }

    [Fact]
    public void DisjunctionTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2);
        var list2 = CollUtil.NewArrayList(2, 3);
        var disj = CollUtil.Disjunction(list1, list2);
        Assert.Equal(2, disj.Count);
    }

    [Fact]
    public void SubtractTest()
    {
        var list1 = CollUtil.NewArrayList(1, 2, 3);
        var list2 = CollUtil.NewArrayList(2, 3);
        var diff = CollUtil.Subtract(list1, list2);
        Assert.Single(diff);
    }

    [Fact]
    public void ContainsAnyTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.True(CollUtil.ContainsAny(list, "a", "x"));
        Assert.False(CollUtil.ContainsAny(list, "x", "y"));
    }

    [Fact]
    public void ContainsAllTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.True(CollUtil.ContainsAll(list, "a", "b"));
        Assert.False(CollUtil.ContainsAll(list, "a", "d"));
    }

    [Fact]
    public void IndexOfTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.Equal(1, CollUtil.IndexOf(list, "b"));
        Assert.Equal(-1, CollUtil.IndexOf(list, "x"));
    }

    [Fact]
    public void LastIndexOfTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "b", "c");
        Assert.Equal(2, CollUtil.LastIndexOf(list, "b"));
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
    public void DistinctTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 2, 3);
        var distinct = CollUtil.Distinct(list);
        Assert.Equal(3, distinct.Count);
    }

    [Fact]
    public void SortTest()
    {
        var list = CollUtil.NewArrayList(3, 1, 2);
        var sorted = CollUtil.Sort(list);
        Assert.Equal(1, sorted[0]);
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
