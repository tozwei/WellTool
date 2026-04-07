using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class CollUtilTest
{
    [Fact]
    public void TestPredicateContains()
    {
        var list = CollUtil.NewArrayList("bbbbb", "aaaaa", "ccccc");
        Assert.True(CollUtil.Contains(list, s => s.StartsWith("a")));
        Assert.False(CollUtil.Contains(list, s => s.StartsWith("d")));
    }

    [Fact]
    public void TestRemoveWithAddIf()
    {
        var list = CollUtil.NewArrayList(1, 2, 3);
        var exceptRemovedList = CollUtil.NewArrayList(2, 3);
        var exceptResultList = CollUtil.NewArrayList(1);

        var resultList = CollUtil.RemoveWithAddIf(list, ele => 1 == ele);
        Assert.Equal(exceptRemovedList, list);
        Assert.Equal(exceptResultList, resultList);

        list = CollUtil.NewArrayList(1, 2, 3);
        resultList = new List<int>();
        CollUtil.RemoveWithAddIf(list, resultList, ele => 1 == ele);
        Assert.Equal(exceptRemovedList, list);
        Assert.Equal(exceptResultList, resultList);
    }

    [Fact]
    public void TestPadLeft()
    {
        var srcList = new List<string>();
        var answerList = CollUtil.NewArrayList("a", "b");
        CollUtil.PadLeft(srcList, 1, "b");
        CollUtil.PadLeft(srcList, 2, "a");
        Assert.Equal(answerList, srcList);

        srcList = CollUtil.NewArrayList("a", "b");
        answerList = CollUtil.NewArrayList("a", "b");
        CollUtil.PadLeft(srcList, 2, "a");
        Assert.Equal(answerList, srcList);
    }

    [Fact]
    public void TestPadRight()
    {
        var srcList = new List<string>();
        var answerList = CollUtil.NewArrayList("a", "b");
        CollUtil.PadRight(srcList, 1, "b");
        CollUtil.PadRight(srcList, 2, "a");
        Assert.Equal(answerList, srcList);
    }

    [Fact]
    public void TestIsEmpty()
    {
        Assert.True(CollUtil.IsEmpty(null));
        Assert.True(CollUtil.IsEmpty(new List<string>()));
        Assert.False(CollUtil.IsEmpty(CollUtil.NewArrayList("a")));
    }

    [Fact]
    public void TestIsNotEmpty()
    {
        Assert.False(CollUtil.IsNotEmpty(null));
        Assert.False(CollUtil.IsNotEmpty( new List<string>()));
        Assert.True(CollUtil.IsNotEmpty(CollUtil.NewArrayList("a")));
    }

    [Fact]
    public void TestNewArrayList()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.Equal(3, list.Count);

        var list2 = CollUtil.NewArrayList<string>();
        Assert.Empty(list2);
    }

    [Fact]
    public void TestNewHashMap()
    {
        var map = CollUtil.NewHashMap<string, object>();
        map["key"] = "value";
        Assert.Equal("value", map["key"]);
    }

    [Fact]
    public void TestAddIfNotNull()
    {
        var list = CollUtil.NewArrayList<string>();
        CollUtil.AddIfNotNull(list, "a");
        CollUtil.AddIfNotNull(list, null);
        Assert.Single(list);
    }

    [Fact]
    public void TestGetFirst()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.Equal("a", CollUtil.GetFirst(list));
        Assert.Null(CollUtil.GetFirst<string>(null!));
        Assert.Null(CollUtil.GetFirst(new List<string>()));
    }

    [Fact]
    public void TestGetLast()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        Assert.Equal("c", CollUtil.GetLast(list));
        Assert.Null(CollUtil.GetLast<string>(null!));
        Assert.Null(CollUtil.GetLast(new List<string>()));
    }

    [Fact]
    public void TestReverse()
    {
        var list = CollUtil.NewArrayList("a", "b", "c");
        CollUtil.Reverse(list);
        Assert.Equal("c", list[0]);
        Assert.Equal("b", list[1]);
        Assert.Equal("a", list[2]);
    }

    [Fact]
    public void TestSub()
    {
        var list = CollUtil.NewArrayList("a", "b", "c", "d");
        var sub = CollUtil.Sub(list, 1, 3);
        Assert.Equal(2, sub.Count);
        Assert.Equal("b", sub[0]);
        Assert.Equal("c", sub[1]);
    }
}
