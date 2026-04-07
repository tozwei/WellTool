using WellTool.Core.Lang;
using Xunit;

public class OptTest
{
    [Fact]
    public void OfTest()
    {
        var opt = WellTool.Core.Lang.Opt.Of("value");
        Assert.True(opt.IsPresent());
        Assert.Equal("value", opt.Get());
    }

    [Fact]
    public void OfEmptyTest()
    {
        var opt = WellTool.Core.Lang.Opt.Empty<string>();
        Assert.True(opt.IsEmpty());
    }

    [Fact]
    public void IfPresentTest()
    {
        var opt = WellTool.Core.Lang.Opt.Of("value");
        var executed = false;
        opt.IfPresent(v => executed = true);
        Assert.True(executed);
    }

    [Fact]
    public void OrElseTest()
    {
        var opt = WellTool.Core.Lang.Opt.Of("value");
        Assert.Equal("value", opt.OrElse("default"));

        var emptyOpt = WellTool.Core.Lang.Opt.Empty<string>();
        Assert.Equal("default", emptyOpt.OrElse("default"));
    }

    [Fact]
    public void OrElseGetTest()
    {
        var emptyOpt = WellTool.Core.Lang.Opt.Empty<string>();
        Assert.Equal("computed", emptyOpt.OrElseGet(() => "computed"));
    }

    [Fact]
    public void MapTest()
    {
        var opt = WellTool.Core.Lang.Opt.Of("hello");
        var mapped = opt.Map(s => s.ToUpper());
        Assert.True(mapped.IsPresent());
        Assert.Equal("HELLO", mapped.Get());
    }

    [Fact]
    public void FilterTest()
    {
        var opt = WellTool.Core.Lang.Opt.Of("hello");
        var filtered = opt.Filter(s => s.Length > 3);
        Assert.True(filtered.IsPresent());

        var filtered2 = opt.Filter(s => s.Length > 10);
        Assert.True(filtered2.IsEmpty());
    }

    [Fact]
    public void IsEmptyTest()
    {
        var opt = WellTool.Core.Lang.Opt.Of("value");
        Assert.False(opt.IsEmpty());

        var emptyOpt = WellTool.Core.Lang.Opt.Empty<string>();
        Assert.True(emptyOpt.IsEmpty());
    }
}
