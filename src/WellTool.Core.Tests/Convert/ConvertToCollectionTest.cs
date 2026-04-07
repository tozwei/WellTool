using System.Collections.Generic;
using WellTool.Core.Convert;
using Xunit;

public class ConvertToCollectionTest
{
    [Fact]
    public void ToListTest()
    {
        object[] array = new object[] { 1, 2, 3 };
        var list = WellTool.Core.Convert.Convert.ToList<int>(array);
        Assert.NotNull(list);
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void ToSetTest()
    {
        object[] array = new object[] { 1, 2, 3 };
        var hashSet = WellTool.Core.Convert.Convert.ToHashSet<int>(array);
        Assert.NotNull(hashSet);
        Assert.Equal(3, hashSet.Count);
    }

    [Fact]
    public void ToArrayTest()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        var array = WellTool.Core.Convert.Convert.ToArray<int>(list);
        Assert.NotNull(array);
        Assert.Equal(3, array.Length);
    }

    [Fact]
    public void ToMapTest()
    {
        var dict = new Dictionary<string, int> { { "a", 1 }, { "b", 2 } };
        var map = WellTool.Core.Convert.Convert.ToDict<string, int>(dict);
        Assert.NotNull(map);
        Assert.Equal(2, map.Count);
    }

    [Fact]
    public void ToLinkedListTest()
    {
        object[] array = new object[] { 1, 2, 3 };
        var linkedList = WellTool.Core.Convert.Convert.ToLinkedList<int>(array);
        Assert.NotNull(linkedList);
        Assert.Equal(3, linkedList.Count);
    }

    [Fact]
    public void ToHashSetTest()
    {
        object[] array = new object[] { 1, 2, 3 };
        var hashSet = WellTool.Core.Convert.Convert.ToHashSet<int>(array);
        Assert.NotNull(hashSet);
        Assert.Equal(3, hashSet.Count);
    }
}
