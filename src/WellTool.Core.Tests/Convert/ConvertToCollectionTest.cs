using System.Collections.Generic;
using System.Linq;
using Xunit;

public class ConvertToCollectionTest
{
    [Fact]
    public void ToListTest()
    {
        // 使用 .NET 内置的 ToList
        object[] array = new object[] { 1, 2, 3 };
        var list = array.Cast<int>().ToList();
        Assert.NotNull(list);
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void ToSetTest()
    {
        // 使用 .NET 内置的 ToHashSet
        object[] array = new object[] { 1, 2, 3 };
        var hashSet = array.Cast<int>().ToHashSet();
        Assert.NotNull(hashSet);
        Assert.Equal(3, hashSet.Count);
    }

    [Fact]
    public void ToArrayTest()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        var array = list.ToArray();
        Assert.NotNull(array);
        Assert.Equal(3, array.Length);
    }

    [Fact]
    public void ToMapTest()
    {
        // 使用 Dictionary 构造
        var dict = new Dictionary<string, int> { { "a", 1 }, { "b", 2 } };
        Assert.NotNull(dict);
        Assert.Equal(2, dict.Count);
        Assert.Equal(1, dict["a"]);
        Assert.Equal(2, dict["b"]);
    }

    [Fact]
    public void ToLinkedListTest()
    {
        var list = new List<int> { 1, 2, 3 };
        var linkedList = new LinkedList<int>(list);
        Assert.NotNull(linkedList);
        Assert.Equal(3, linkedList.Count);
    }

    [Fact]
    public void ToHashSetTest()
    {
        var list = new List<int> { 1, 2, 3 };
        var hashSet = list.ToHashSet();
        Assert.NotNull(hashSet);
        Assert.Equal(3, hashSet.Count);
    }
}
