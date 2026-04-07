using Xunit;
using System.Collections.Generic;

namespace WellTool.Core.Tests;

public class ListUtilLastTest
{
    [Fact]
    public void NewArrayListTest()
    {
        var list = new List<string>();
        Assert.NotNull(list);
    }

    [Fact]
    public void NewArrayListWithElementsTest()
    {
        var list = new List<string> { "a", "b", "c" };
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void SubTest()
    {
        var list = new List<string> { "a", "b", "c", "d", "e" };
        Assert.Equal(5, list.Count);
    }

    [Fact]
    public void PartitionTest()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        Assert.Equal(7, list.Count);
    }
}

