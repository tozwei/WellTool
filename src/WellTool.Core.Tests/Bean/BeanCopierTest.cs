using WellTool.Core.Bean;
using Xunit;

namespace WellTool.Core.Tests;

public class BeanCopierTest
{
    [Fact]
    public void BeanToMapIgnoreNullTest()
    {
        var a = new A { Value = null };
        var map = BeanCopier.Create(a, new Dictionary<object, object>(), CopyOptions.Create()).Copy();

        Assert.Single(map);
        Assert.True(map.ContainsKey("Value"));
        Assert.Null(map["Value"]);
    }

    [Fact]
    public void BeanToMapIgnoreNullValueTest()
    {
        var a = new A { Value = null };
        var map = BeanCopier.Create(a, new Dictionary<object, object>(), CopyOptions.Create().SetIgnoreNullValue(true)).Copy();

        Assert.Empty(map);
    }

    [Fact]
    public void BeanToBeanNotOverrideTest()
    {
        var a = new A { Value = "123" };
        var b = new B { Value = "abc" };

        BeanCopier.Create(a, b, CopyOptions.Create().SetOverride(false)).Copy();

        Assert.Equal("abc", b.Value);
    }

    [Fact]
    public void BeanToBeanOverrideTest()
    {
        var a = new A { Value = "123" };
        var b = new B { Value = "abc" };

        BeanCopier.Create(a, b, CopyOptions.Create()).Copy();

        Assert.Equal("123", b.Value);
    }

    [Fact]
    public void Issues2484Test()
    {
        var a = new A { Value = "abc" };
        var b = new B { Value = "123" };

        BeanCopier.Create(a, b, CopyOptions.Create().SetOverride(false)).Copy();
        Assert.Equal("123", b.Value);

        b.Value = null;
        BeanCopier.Create(a, b, CopyOptions.Create().SetOverride(false)).Copy();
        Assert.Equal("abc", b.Value);
    }

    private class A
    {
        public string? Value { get; set; }
    }

    private class B
    {
        public string? Value { get; set; }
    }
}
