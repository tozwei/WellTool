using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class ArrayUtilTest2
{
    [Fact]
    public void NewArrayTest()
    {
        var array = ArrayUtil.NewArray<string>(5);
        Assert.Equal(5, array.Length);
    }

    [Fact]
    public void IsEmptyTest()
    {
        Assert.True(ArrayUtil.IsEmpty(null));
        Assert.True(ArrayUtil.IsEmpty(new string[0]));
        Assert.False(ArrayUtil.IsEmpty(new[] { "a" }));
    }

    [Fact]
    public void GetTest()
    {
        var array = new[] { "a", "b", "c" };
        Assert.Equal("a", ArrayUtil.Get(array, 0));
        Assert.Equal("c", ArrayUtil.Get(array, -1));
    }

    [Fact]
    public void SetTest()
    {
        var array = new[] { "a", "b", "c" };
        ArrayUtil.Set(array, 0, "x");
        Assert.Equal("x", array[0]);
    }

    [Fact]
    public void AddTest()
    {
        var array = new[] { "a", "b" };
        var newArray = ArrayUtil.Add(array, "c");
        Assert.Equal(3, newArray.Length);
        Assert.Equal("c", newArray[2]);
    }

    [Fact]
    public void RemoveTest()
    {
        var array = new[] { "a", "b", "c" };
        var newArray = ArrayUtil.Remove(array, 1);
        Assert.Equal(2, newArray.Length);
        Assert.Equal("a", newArray[0]);
        Assert.Equal("c", newArray[1]);
    }

    [Fact]
    public void ContainsTest()
    {
        var array = new[] { "a", "b", "c" };
        Assert.True(ArrayUtil.Contains(array, "b"));
        Assert.False(ArrayUtil.Contains(array, "d"));
    }

    [Fact]
    public void IndexOfTest()
    {
        var array = new[] { "a", "b", "c" };
        Assert.Equal(1, ArrayUtil.IndexOf(array, "b"));
        Assert.Equal(-1, ArrayUtil.IndexOf(array, "d"));
    }

    [Fact]
    public void ToStringArrayTest()
    {
        var list = new[] { "a", "b", "c" };
        var array = ArrayUtil.ToArray(list);
        Assert.Equal(3, array.Length);
    }
}
