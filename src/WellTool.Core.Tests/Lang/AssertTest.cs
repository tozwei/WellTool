using Xunit;
using System.Collections.Generic;

namespace WellTool.Core.Tests;

public class AssertTest
{
    [Fact]
    public void IsNullTest()
    {
        string? a = null;
        Xunit.Assert.Null(a);
    }

    [Fact]
    public void NotNullTest()
    {
        string? a = "test";
        Xunit.Assert.NotNull(a);
    }

    [Fact]
    public void IsTrueTest()
    {
        int i = 1;
        Xunit.Assert.True(i > 0);
    }

    [Fact]
    public void IsFalseTest()
    {
        int i = 0;
        Xunit.Assert.False(i > 0);
    }

    [Fact]
    public void NotEmptyTest()
    {
        var list = new List<string> { "a", "b" };
        Xunit.Assert.NotEmpty(list);
    }

    [Fact]
    public void EmptyTest()
    {
        var list = new List<string>();
        Xunit.Assert.Empty(list);
    }
}
