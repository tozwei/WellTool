using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class ListUtilLastTest
{
    [Fact]
    public void NewArrayListTest()
    {
        var list = ListUtil.NewArrayList<string>();
        Assert.NotNull(list);
    }

    [Fact]
    public void NewArrayListWithElementsTest()
    {
        var list = ListUtil.NewArrayList("a", "b", "c");
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void NewLinkedListTest()
    {
        var list = ListUtil.NewLinkedList<string>();
        Assert.NotNull(list);
    }

    [Fact]
    public void ToListTest()
    {
        var array = new[] { 1, 2, 3 };
        var list = ListUtil.ToList(array);
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void SubTest()
    {
        var list = ListUtil.NewArrayList("a", "b", "c", "d", "e");
        var sub = ListUtil.Sub(list, 1, 4);
        Assert.Equal(3, sub.Count);
    }

    [Fact]
    public void PartitionTest()
    {
        var list = ListUtil.NewArrayList(1, 2, 3, 4, 5, 6, 7);
        var partitions = ListUtil.Partition(list, 3);
        Assert.Equal(3, partitions.Count);
    }
}
