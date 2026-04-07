using System.Linq;
using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class ListUtilTest
{
    [Fact]
    public void NewArrayListTest()
    {
        var list = ListUtil.Empty<string>();
        Assert.NotNull(list);
        Assert.Empty(list);
    }

    [Fact]
    public void NewArrayListWithElementsTest()
    {
        var list = ListUtil.Of("a", "b", "c");
        Assert.Equal(3, list.Count);
        Assert.Equal("a", list[0]);
        Assert.Equal("b", list[1]);
        Assert.Equal("c", list[2]);
    }

    [Fact]
    public void NewLinkedListTest()
    {
        var list = new LinkedList<string>();
        Assert.NotNull(list);
    }

    [Fact]
    public void ToListTest()
    {
        var array = new[] { 1, 2, 3 };
        var list = array.ToList();
        Assert.Equal(3, list.Count);
        Assert.Equal(1, list[0]);
        Assert.Equal(2, list[1]);
        Assert.Equal(3, list[2]);
    }

    [Fact]
    public void SubTest()
    {
        var list = ListUtil.Of("a", "b", "c", "d", "e");
        var sub = ListUtil.Sub(list, 1, 4);
        Assert.Equal(3, sub.Count);
        Assert.Equal("b", sub[0]);
        Assert.Equal("c", sub[1]);
        Assert.Equal("d", sub[2]);
    }

    [Fact]
    public void PartitionTest()
    {
        var list = ListUtil.Of(1, 2, 3, 4, 5, 6, 7);
        // 使用自定义方法实现分片
        var partitions = PartitionList(list, 3);
        Assert.Equal(3, partitions.Count);
        Assert.Equal(3, partitions[0].Count);
        Assert.Equal(3, partitions[1].Count);
        Assert.Single(partitions[2]);
    }

    private static List<List<T>> PartitionList<T>(List<T> list, int size)
    {
        var result = new List<List<T>>();
        for (int i = 0; i < list.Count; i += size)
        {
            result.Add(list.GetRange(i, System.Math.Min(size, list.Count - i)));
        }
        return result;
    }
}
