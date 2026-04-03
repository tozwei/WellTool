using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class ConvertToCollectionTest
{
    [Fact]
    public void ToListTest()
    {
        var result = Convert.ToList<string>("a,b,c", ',');
        Assert.Equal(3, result.Count);
        Assert.Equal("a", result[0]);
    }

    [Fact]
    public void ToSetTest()
    {
        var result = Convert.ToSet<int>("1,2,2,3");
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void ToArrayTest()
    {
        var result = Convert.ToArray<string>("a,b,c", ',');
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void ToMapTest()
    {
        var result = Convert.ToMap<string, int>("a:1,b:2");
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result["a"]);
    }

    [Fact]
    public void ToLinkedListTest()
    {
        var result = Convert.ToLinkedList<string>("a,b,c");
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void ToHashSetTest()
    {
        var result = Convert.ToHashSet<string>("a,a,b,c");
        Assert.Equal(3, result.Count);
    }
}
