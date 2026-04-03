using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class OptTest
{
    [Fact]
    public void OfTest()
    {
        var opt = Opt.Of("value");
        Assert.True(opt.IsPresent());
        Assert.Equal("value", opt.Get());
    }

    [Fact]
    public void OfEmptyTest()
    {
        var opt = Opt.Of<string>(null);
        Assert.False(opt.IsPresent());
    }

    [Fact]
    public void IfPresentTest()
    {
        var opt = Opt.Of("value");
        var executed = false;
        opt.IfPresent(v => executed = true);
        Assert.True(executed);
    }

    [Fact]
    public void OrElseTest()
    {
        var opt = Opt.Of<string>(null);
        Assert.Equal("default", opt.OrElse("default"));

        opt = Opt.Of("value");
        Assert.Equal("value", opt.OrElse("default"));
    }

    [Fact]
    public void OrElseGetTest()
    {
        var opt = Opt.Of<string>(null);
        Assert.Equal("computed", opt.OrElseGet(() => "computed"));

        opt = Opt.Of("value");
        Assert.Equal("value", opt.OrElseGet(() => "computed"));
    }

    [Fact]
    public void MapTest()
    {
        var opt = Opt.Of("hello");
        var mapped = opt.Map(s => s.ToUpper());
        Assert.True(mapped.IsPresent());
        Assert.Equal("HELLO", mapped.Get());
    }

    [Fact]
    public void FilterTest()
    {
        var opt = Opt.Of(10);
        var filtered = opt.Filter(v => v > 5);
        Assert.True(filtered.IsPresent());

        filtered = opt.Filter(v => v > 100);
        Assert.False(filtered.IsPresent());
    }

    [Fact]
    public void IsEmptyTest()
    {
        var opt = Opt.Of<string>(null);
        Assert.True(opt.IsEmpty());

        opt = Opt.Of("value");
        Assert.False(opt.IsEmpty());
    }
}
