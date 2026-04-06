using WellTool.Core.Util;
using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class ListUtilLastTest
{
    [Fact]
    public void NewArrayListTest()
    {
        var list = CollUtil.NewArrayList<string>();
        Assert.NotNull(list);
    }

    [Fact]
    public void NewArrayListWithElementsTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void ToListTest()
    {
        var array = new[] { 1, 2, 3 };
        var list = ListUtil.Of(array);
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void SubTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c", "d", "e");
        var sub = ListUtil.Sub(list, 1, 3); // 注意：Sub方法的参数是startIndex和count
        Assert.Equal(3, sub.Count);
    }

    [Fact]
    public void PartitionTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5, 6, 7);
        var partitions = CollUtil.Partition(list, 3);
        Assert.Equal(3, partitions.Count);
    }
}

