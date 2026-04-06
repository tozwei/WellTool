using WellTool.Core.Streams;
using Xunit;
using System.IO;
using System.Text;

namespace WellTool.Core.Tests;

public class StreamUtilFinalTest
{
    [Fact]
    public void OfArrayTest()
    {
        var result = StreamUtil.OfArray(new[] { "a", "b", "c" });
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void OfListTest()
    {
        var list = new System.Collections.Generic.List<string> { "a", "b", "c" };
        var result = StreamUtil.OfList(list);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void OfParamsTest()
    {
        var result = StreamUtil.Of("a", "b", "c");
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void JoinTest()
    {
        var items = new[] { "a", "b", "c" };
        var result = StreamUtil.Join(items, ",");
        Assert.Equal("a,b,c", result);
    }

    [Fact]
    public void EmptyTest()
    {
        var empty = StreamUtil.Of<string>();
        Assert.Empty(empty);
    }
}
