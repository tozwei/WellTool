using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class CharFinderTest
{
    [Fact]
    public void FindTest()
    {
        var finder = CharFinder.Find('a');
        Assert.True(finder.Find("abc"));
    }

    [Fact]
    public void FindFirstTest()
    {
        var finder = CharFinder.FindFirst('b');
        Assert.Equal(1, finder.FindFirst("abc"));
        Assert.Equal(-1, finder.FindFirst("aaa"));
    }

    [Fact]
    public void FindLastTest()
    {
        var finder = CharFinder.FindLast('b');
        Assert.Equal(3, finder.FindLast("abb"));
    }

    [Fact]
    public void ContainsAnyTest()
    {
        Assert.True(CharFinder.ContainsAny("abc", 'a', 'x'));
        Assert.False(CharFinder.ContainsAny("abc", 'x', 'y'));
    }

    [Fact]
    public void ContainsTest()
    {
        Assert.True(CharFinder.Contains("abc", 'b'));
        Assert.False(CharFinder.Contains("abc", 'x'));
    }

    [Fact]
    public void IndexOfTest()
    {
        Assert.Equal(1, CharFinder.IndexOf("abc", 'b'));
        Assert.Equal(-1, CharFinder.IndexOf("abc", 'x'));
    }

    [Fact]
    public void LastIndexOfTest()
    {
        Assert.Equal(3, CharFinder.LastIndexOf("abb", 'b'));
    }
}
