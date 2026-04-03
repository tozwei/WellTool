using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class RangeTest
{
    [Fact]
    public void IntegerRangeTest()
    {
        var range = Range.Of(1, 10);
        Assert.Equal(1, range.Start);
        Assert.Equal(10, range.End);
    }

    [Fact]
    public void ContainsTest()
    {
        var range = Range.Of(1, 10);
        Assert.True(range.Contains(5));
        Assert.False(range.Contains(15));
    }

    [Fact]
    public void ContainsRangeTest()
    {
        var range1 = Range.Of(1, 10);
        var range2 = Range.Of(3, 7);
        var range3 = Range.Of(5, 15);

        Assert.True(range1.Contains(range2));
        Assert.False(range1.Contains(range3));
    }

    [Fact]
    public void IntersectionTest()
    {
        var range1 = Range.Of(1, 10);
        var range2 = Range.Of(5, 15);
        var intersection = range1.Intersection(range2);

        Assert.NotNull(intersection);
        Assert.Equal(5, intersection.Start);
        Assert.Equal(10, intersection.End);
    }

    [Fact]
    public void OverlapsTest()
    {
        var range1 = Range.Of(1, 10);
        var range2 = Range.Of(5, 15);
        var range3 = Range.Of(15, 20);

        Assert.True(range1.Overlaps(range2));
        Assert.False(range1.Overlaps(range3));
    }

    [Fact]
    public void CompareToTest()
    {
        var range1 = Range.Of(1, 10);
        var range2 = Range.Of(5, 15);
        var range3 = Range.Of(1, 10);

        Assert.True(range1.CompareTo(range2) < 0);
        Assert.True(range2.CompareTo(range1) > 0);
        Assert.Equal(0, range1.CompareTo(range3));
    }
}
