using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class AssertTest
{
    [Fact]
    public void IsNullTest()
    {
        string? a = null;
        Assert.IsNull(a);
    }

    [Fact]
    public void NotNullTest()
    {
        string? a = "test";
        Assert.NotNull(a);
    }

    [Fact]
    public void NotNullFailTest()
    {
        string? a = null;
        Assert.Throws<ArgumentNullException>(() => Assert.NotNull(a));
    }

    [Fact]
    public void IsTrueTest()
    {
        int i = 1;
        Assert.IsTrue(i > 0);
    }

    [Fact]
    public void IsTrueFailTest()
    {
        int i = 0;
        Assert.Throws<IllegalArgumentException>(() => Assert.IsTrue(i > 0, IllegalArgumentException.New));
    }

    [Fact]
    public void IsFalseTest()
    {
        int i = 0;
        Assert.IsFalse(i > 0);
    }

    [Fact]
    public void IsFalseFailTest()
    {
        int i = 1;
        Assert.Throws<IllegalArgumentException>(() => Assert.IsFalse(i > 0, IllegalArgumentException.New));
    }

    [Fact]
    public void NotEmptyTest()
    {
        var list = new List<string> { "a", "b" };
        Assert.NotEmpty(list);
    }

    [Fact]
    public void NotEmptyFailTest()
    {
        var list = new List<string>();
        Assert.Throws<ArgumentException>(() => Assert.NotEmpty(list));
    }

    [Fact]
    public void EmptyTest()
    {
        var list = new List<string>();
        Assert.Empty(list);
    }

    [Fact]
    public void EmptyNullTest()
    {
        Assert.Empty(null);
    }
}
