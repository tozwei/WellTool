using System.Collections;
using System.Collections.Generic;
using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class CollUtilTest
{
    [Fact]
    public void TestIsEmpty()
    {
        Assert.True(CollUtil.IsEmpty((ICollection)null));
        Assert.True(CollUtil.IsEmpty((ICollection)new List<string>()));
        Assert.False(CollUtil.IsEmpty((ICollection)new List<string> { "a" }));
    }

    [Fact]
    public void TestIsNotEmpty()
    {
        Assert.False(CollUtil.IsNotEmpty((ICollection)null));
        Assert.False(CollUtil.IsNotEmpty((ICollection)new List<string>()));
        Assert.True(CollUtil.IsNotEmpty((ICollection)new List<string> { "a" }));
    }

    [Fact]
    public void TestFirst()
    {
        var list = new List<string> { "a", "b", "c" };
        Assert.Equal("a", CollUtil.First(list));
        Assert.Null(CollUtil.First<string>(null!));
        Assert.Null(CollUtil.First(new List<string>()));
    }

    [Fact]
    public void TestLast()
    {
        var list = new List<string> { "a", "b", "c" };
        Assert.Equal("c", CollUtil.Last(list));
        Assert.Null(CollUtil.Last<string>(null!));
        Assert.Null(CollUtil.Last(new List<string>()));
    }

    [Fact]
    public void TestReverse()
    {
        var list = new List<string> { "a", "b", "c" };
        var reversedList = CollUtil.Reverse(list);
        Assert.Equal("c", reversedList[0]);
        Assert.Equal("b", reversedList[1]);
        Assert.Equal("a", reversedList[2]);
    }

    [Fact]
    public void TestSub()
    {
        var list = new List<string> { "a", "b", "c", "d" };
        var sub = CollUtil.Sub((IList)list, 1, 3, 1);
        Assert.Equal(2, sub.Count);
    }

    [Fact]
    public void TestContains()
    {
        var list = new List<string> { "a", "b", "c" };
        Assert.True(CollUtil.Contains(list, "a"));
        Assert.False(CollUtil.Contains(list, "d"));
    }

    [Fact]
    public void TestContainsAll()
    {
        var list = new List<string> { "a", "b", "c" };
        Assert.True(CollUtil.ContainsAll(list, "a", "b"));
        Assert.False(CollUtil.ContainsAll(list, "a", "d"));
    }

    [Fact]
    public void TestContainsAny()
    {
        var list = new List<string> { "a", "b", "c" };
        Assert.True(CollUtil.ContainsAny(list, "a", "d"));
        Assert.False(CollUtil.ContainsAny(list, "d", "e"));
    }

    [Fact]
    public void TestUnion()
    {
        var list1 = new List<string> { "a", "b" };
        var list2 = new List<string> { "b", "c" };
        var union = CollUtil.Union(list1, list2);
        Assert.Equal(3, union.Count);
    }

    [Fact]
    public void TestIntersection()
    {
        var list1 = new List<string> { "a", "b" };
        var list2 = new List<string> { "b", "c" };
        var intersection = CollUtil.Intersection(list1, list2);
        Assert.Single(intersection);
    }

    [Fact]
    public void TestSubtract()
    {
        var list1 = new List<string> { "a", "b", "c" };
        var list2 = new List<string> { "b" };
        var subtract = CollUtil.Subtract(list1, list2);
        Assert.Equal(2, subtract.Count);
    }

    [Fact]
    public void TestAddAll()
    {
        var list = new List<string> { "a" };
        var items = new List<string> { "b", "c" };
        CollUtil.AddAll(list, items);
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void TestLimit()
    {
        var list = new List<string> { "a", "b", "c", "d" };
        var limited = CollUtil.Limit(list, 2);
        Assert.Equal(2, limited.Count);
    }

    [Fact]
    public void TestSkip()
    {
        var list = new List<string> { "a", "b", "c", "d" };
        var skipped = CollUtil.Skip(list, 2);
        Assert.Equal(2, skipped.Count);
    }

    [Fact]
    public void TestSkipLimit()
    {
        var list = new List<string> { "a", "b", "c", "d" };
        var result = CollUtil.SkipLimit(list, 1, 2);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void TestSplit()
    {
        var list = new List<string> { "a", "b", "c", "d" };
        var split = CollUtil.Split(list, 2);
        Assert.Equal(2, split.Count);
    }

    [Fact]
    public void TestMerge()
    {
        var list1 = new List<string> { "a" };
        var list2 = new List<string> { "b" };
        var merged = CollUtil.Merge(list1, list2);
        Assert.Equal(2, merged.Count);
    }

    [Fact]
    public void TestCopy()
    {
        var list = new List<string> { "a", "b" };
        var copy = CollUtil.Copy(list);
        Assert.Equal(2, copy.Count);
    }

    [Fact]
    public void TestEquals()
    {
        var list1 = new List<string> { "a", "b" };
        var list2 = new List<string> { "a", "b" };
        var list3 = new List<string> { "a", "c" };
        Assert.True(CollUtil.Equals(list1, list2));
        Assert.False(CollUtil.Equals(list1, list3));
    }

    [Fact]
    public void TestToString()
    {
        var list = new List<string> { "a", "b", "c" };
        var str = CollUtil.ToString(list);
        Assert.Equal("a, b, c", str);
    }
}
