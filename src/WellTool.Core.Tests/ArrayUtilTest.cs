using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;


public class ArrayUtilTest
{
    [Fact]
    public void IsEmptyTest()
    {
        int[] a = Array.Empty<int>();
        Assert.True(ArrayUtil.IsEmpty(a));
        int[] b = null;
        Assert.True(ArrayUtil.IsEmpty(b));

        int[] c = { 1, 2, 3 };
        Assert.False(ArrayUtil.IsEmpty(c));
    }

    [Fact]
    public void IsNotEmptyTest()
    {
        int[] a = { 1, 2 };
        Assert.True(ArrayUtil.IsNotEmpty(a));

        string[] b = { "a", "b", "c" };
        Assert.True(ArrayUtil.IsNotEmpty(b));
    }

    [Fact]
    public void AddAllTest()
    {
        var a = new[] { 1, 2, 3 };
        var b = new[] { 4, 5, 6 };
        var result = ArrayUtil.AddAll(a, b);
        Assert.Equal(6, result.Length);
    }

    [Fact]
    public void ContainsTest()
    {
        int[] a = { 1, 2, 3 };
        Assert.True(ArrayUtil.Contains(a, 2));
        Assert.False(ArrayUtil.Contains(a, 4));

        string[] b = { "a", "b", "c" };
        Assert.True(ArrayUtil.Contains(b, "b"));
        Assert.False(ArrayUtil.Contains(b, "d"));
    }

    [Fact]
    public void IndexOfTest()
    {
        int[] a = { 1, 2, 3 };
        Assert.Equal(1, ArrayUtil.IndexOf(a, 2));
        Assert.Equal(-1, ArrayUtil.IndexOf(a, 4));
    }

    [Fact]
    public void LengthTest()
    {
        int[] a = { 1, 2, 3 };
        Assert.Equal(3, ArrayUtil.Length(a));

        string[] b = { "a", "b" };
        Assert.Equal(2, ArrayUtil.Length(b));
    }

    [Fact]
    public void ToListTest()
    {
        int[] a = { 1, 2, 3 };
        var list = ArrayUtil.ToList(a);
        Assert.Equal(3, list.Count);
        Assert.Equal(1, list[0]);
    }

    [Fact]
    public void ReverseTest()
    {
        int[] a = { 1, 2, 3, 4, 5 };
        ArrayUtil.Reverse(a);
        Assert.Equal(new[] { 5, 4, 3, 2, 1 }, a);
    }

    [Fact]
    public void SubTest()
    {
        int[] a = { 1, 2, 3, 4, 5 };
        var sub = ArrayUtil.Sub(a, 1, 3, 1);
        Assert.Equal(3, sub.Length);
    }

    [Fact]
    public void CopyOfTest()
    {
        int[] a = { 1, 2, 3 };
        var copy = ArrayUtil.CopyOf(a);
        Assert.Equal(3, copy.Length);
        Assert.Equal(1, copy[0]);
    }

    [Fact]
    public void FillTest()
    {
        var a = new int[3];
        ArrayUtil.Fill(a, 5);
        Assert.All(a, x => Assert.Equal(5, x));
    }
}
