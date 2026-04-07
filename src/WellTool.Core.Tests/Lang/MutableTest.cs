using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class MutableTest
{
    [Fact]
    public void OfTest()
    {
        var mutable = Mutable.Of(100);
        Xunit.Assert.Equal(100, mutable.Value);
    }

    [Fact]
    public void SetValueTest()
    {
        var mutable = Mutable.Of(100);
        mutable.SetValue(200);
        Xunit.Assert.Equal(200, mutable.Value);
    }

    [Fact]
    public void EqualsTest()
    {
        var m1 = Mutable.Of(100);
        var m2 = Mutable.Of(100);
        Xunit.Assert.True(m1.Equals(m2));
        Xunit.Assert.True(m1.Equals(m1));
    }

    [Fact]
    public void GetHashCodeTest()
    {
        var mutable = Mutable.Of(100);
        Xunit.Assert.NotEqual(0, mutable.GetHashCode());
    }

    [Fact]
    public void ToStringTest()
    {
        var mutable = Mutable.Of(100);
        Xunit.Assert.Contains("100", mutable.ToString());
    }
}
