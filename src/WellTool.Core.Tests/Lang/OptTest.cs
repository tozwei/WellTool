using WellTool.Core.Lang;
using Xunit;


namespace WellTool.Core.Lang.Tests;

public class OptTest
{
    [Fact]
    public void OfTest()
    {
        var opt = Opt<string>.Of("value");
        Xunit.Assert.True(opt.IsPresent);
        Xunit.Assert.Equal("value", opt.Get());
    }

    [Fact]
    public void OfEmptyTest()
    {
        var opt = Opt<string>.Empty();
        Xunit.Assert.True(opt.IsEmpty);
    }

    [Fact]
    public void IfPresentTest()
    {
        var opt = Opt<string>.Of("value");
        var executed = false;
        opt.IfPresent(v => executed = true);
        Xunit.Assert.True(executed);
    }

    [Fact]
    public void OrElseTest()
    {
        var opt = Opt<string>.Of("value");
        Xunit.Assert.Equal("value", opt.OrElse("default"));

        var emptyOpt = Opt<string>.Empty();
        Xunit.Assert.Equal("default", emptyOpt.OrElse("default"));
    }

    [Fact]
    public void OrElseGetTest()
    {
        var emptyOpt = Opt<string>.Empty();
        Xunit.Assert.Equal("computed", emptyOpt.OrElseGet(() => "computed"));
    }

    [Fact]
    public void MapTest()
    {
        var opt = Opt<string>.Of("hello");
        var mapped = opt.Map(s => s.ToUpper());
        Xunit.Assert.True(mapped.IsPresent);
        Xunit.Assert.Equal("HELLO", mapped.Get());
    }

    [Fact]
    public void FilterTest()
    {
        var opt = Opt<string>.Of("hello");
        var filtered = opt.Filter(s => s.Length > 3);
        Xunit.Assert.True(filtered.IsPresent);

        var filtered2 = opt.Filter(s => s.Length > 10);
        Xunit.Assert.True(filtered2.IsEmpty);
    }

    [Fact]
    public void IsEmptyTest()
    {
        var opt = Opt<string>.Of("value");
        Xunit.Assert.False(opt.IsEmpty);

        var emptyOpt = Opt<string>.Empty();
        Xunit.Assert.True(emptyOpt.IsEmpty);
    }
}
