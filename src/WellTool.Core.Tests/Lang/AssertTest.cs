using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

// 使用别名解决命名冲突
using XAssert = Xunit.Assert;

public class AssertTest
{
    [Fact]
    public void IsNullTest()
    {
        string? a = null;
        XAssert.True(Assert.IsNull(a));
    }

    [Fact]
    public void NotNullTest()
    {
        string? a = "test";
        XAssert.True(Assert.NotNull(a));
    }

    [Fact]
    public void NotNullFailTest()
    {
        string? a = null;
        XAssert.Throws<ArgumentNullException>(() => Assert.NotNull(a));
    }

    [Fact]
    public void IsTrueTest()
    {
        int i = 1;
        XAssert.True(Assert.IsTrue(i > 0));
    }

    [Fact]
    public void IsTrueFailTest()
    {
        int i = 0;
        XAssert.Throws<IllegalArgumentException>(() => Assert.IsTrue(i > 0, IllegalArgumentException.New));
    }

    [Fact]
    public void IsFalseTest()
    {
        int i = 0;
        XAssert.True(Assert.IsFalse(i > 0));
    }

    [Fact]
    public void IsFalseFailTest()
    {
        int i = 1;
        XAssert.Throws<IllegalArgumentException>(() => Assert.IsFalse(i > 0, IllegalArgumentException.New));
    }

    [Fact]
    public void NotEmptyTest()
    {
        var list = new List<string> { "a", "b" };
        XAssert.True(Assert.NotEmpty(list));
    }

    [Fact]
    public void NotEmptyFailTest()
    {
        var list = new List<string>();
        XAssert.Throws<ArgumentException>(() => Assert.NotEmpty(list));
    }

    [Fact]
    public void EmptyTest()
    {
        var list = new List<string>();
        XAssert.True(Assert.Empty(list));
    }

    [Fact]
    public void EmptyNullTest()
    {
        XAssert.True(Assert.Empty(null));
    }
}
