using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class CharFinderLastTest
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
    }

    [Fact]
    public void ContainsTest()
    {
        Assert.True(CharFinder.Contains("abc", 'b'));
        Assert.False(CharFinder.Contains("abc", 'd'));
    }
}
