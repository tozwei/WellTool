using WellTool.Core.Lang;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class RangeTest
{
    [Fact]
    public void IntegerRangeTest()
    {
        var range = new Range<int>(1, 10, (a, b) => a - b);
        Assert.Equal(1, range.Start);
        Assert.Equal(10, range.End);
    }

    [Fact]
    public void ContainsTest()
    {
        var range = new Range<int>(1, 10, (a, b) => a - b);
        Assert.True(range.Contains(5));
        Assert.False(range.Contains(15));
    }

    [Fact]
    public void ElementsTest()
    {
        var range = new Range<int>(1, 5, (a, b) => a - b);
        var elements = range.Elements();
        Assert.Equal(5, range.Size());
    }

    [Fact]
    public void SizeTest()
    {
        var range = new Range<int>(1, 10, (a, b) => a - b);
        Assert.Equal(10, range.Size());
    }
}
