namespace WellTool.Core.Tests;


public class ArrayUtilTest
{
    [Fact]
    public void IsEmptyTest()
    {
        int[] a = Array.Empty<int>();
        Assert.True(ArrayUtil.IsEmpty(a));
        Assert.True(ArrayUtil.IsEmpty((object)a));
        int[] b = null;
        Assert.True(ArrayUtil.IsEmpty(b));
        object c = null;
        Assert.True(ArrayUtil.IsEmpty(c));

        object d = new object[] { "1", "2", 3, 4.0 };
        bool isEmpty = ArrayUtil.IsEmpty(d);
        Assert.False(isEmpty);
        d = new object[0];
        isEmpty = ArrayUtil.IsEmpty(d);
        Assert.True(isEmpty);
        d = null;
        isEmpty = ArrayUtil.IsEmpty(d);
        Assert.True(isEmpty);

        object[] e = new object[] { "1", "2", 3, 4.0 };
        bool empty = ArrayUtil.IsEmpty(e);
        Assert.False(empty);
    }

    [Fact]
    public void IsNotEmptyTest()
    {
        int[] a = { 1, 2 };
        Assert.True(ArrayUtil.IsNotEmpty(a));

        string[] b = { "a", "b", "c" };
        Assert.True(ArrayUtil.IsNotEmpty(b));

        object c = new object[] { "1", "2", 3, 4.0 };
        Assert.True(ArrayUtil.IsNotEmpty(c));
    }

    [Fact]
    public void NewArrayTest()
    {
        string[] newArray = ArrayUtil.NewArray<string>(3);
        Assert.Equal(3, newArray.Length);
    }

    [Fact]
    public void CloneTest()
    {
        Integer[] b = { 1, 2, 3 };
        Integer[] cloneB = ArrayUtil.Clone(b);
        Assert.Equal(b, cloneB);

        int[] a = { 1, 2, 3 };
        int[] clone = ArrayUtil.Clone(a);
        Assert.Equal(a, clone);
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
    public void ToStringTest()
    {
        int[] a = { 1, 2, 3 };
        Assert.Equal("[1, 2, 3]", ArrayUtil.ToString(a));

        string[] b = { "a", "b", "c" };
        Assert.Equal("[a, b, c]", ArrayUtil.ToString(b));
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
    public void IsEmptyPrimitiveTest()
    {
        int[] empty = Array.Empty<int>();
        Assert.True(ArrayUtil.IsEmpty(empty));

        int[] notEmpty = { 1 };
        Assert.False(ArrayUtil.IsEmpty(notEmpty));
    }

    [Fact]
    public void EmptyTest()
    {
        int[] emptyInt = ArrayUtil.Empty<int>();
        Assert.Empty(emptyInt);

        string[] emptyString = ArrayUtil.Empty<string>();
        Assert.Empty(emptyString);
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
        int[] sub = ArrayUtil.Sub(a, 1, 3);
        Assert.Equal(new[] { 2, 3 }, sub);
    }

    [Fact]
    public void WrapTest()
    {
        int[] a = 1;
        int[] wrapped = ArrayUtil.Wrap(a);
        Assert.Single(wrapped);
        Assert.Equal(1, wrapped[0]);

        int[] b = { 1, 2 };
        int[] wrappedB = ArrayUtil.Wrap(b);
        Assert.Equal(2, wrappedB.Length);
    }
}
