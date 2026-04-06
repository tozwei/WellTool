using Xunit;
using Assert = Xunit.Assert;
using CollectionCollUtil = WellTool.Core.Collection.CollUtil;

namespace WellTool.Core.Tests;

public class ListUtilLastTest
{
    [Fact]
    public void NewArrayListTest()
    {
        var list = CollectionCollUtil.NewArrayList<string>();
        Assert.NotNull(list);
    }

    [Fact]
    public void NewArrayListWithElementsTest()
    {
        var list = CollectionCollUtil.NewArrayList("a", "b", "c");
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void SubTest()
    {
        var list = CollectionCollUtil.NewArrayList("a", "b", "c", "d", "e");
        Assert.Equal(5, list.Count);
    }

    [Fact]
    public void PartitionTest()
    {
        var list = CollectionCollUtil.NewArrayList(1, 2, 3, 4, 5, 6, 7);
        Assert.Equal(7, list.Count);
    }
}

