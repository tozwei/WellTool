using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class ModifierUtilTest
{
    [Fact]
    public void IsPublicTest()
    {
        Assert.True(ModifierUtil.IsPublic(typeof(PublicClass)));
        Assert.False(ModifierUtil.IsPublic(typeof(PrivateClass)));
    }

    [Fact]
    public void IsPrivateTest()
    {
        Assert.False(ModifierUtil.IsPrivate(typeof(PublicClass)));
        Assert.True(ModifierUtil.IsPrivate(typeof(PrivateClass)));
    }

    [Fact]
    public void IsStaticTest()
    {
        Assert.True(ModifierUtil.IsStatic(typeof(StaticClass)));
        Assert.False(ModifierUtil.IsStatic(typeof(PublicClass)));
    }

    [Fact]
    public void IsAbstractTest()
    {
        Assert.True(ModifierUtil.IsAbstract(typeof(AbstractClass)));
        Assert.False(ModifierUtil.IsAbstract(typeof(PublicClass)));
    }

    [Fact]
    public void ToStringModifierTest()
    {
        var str = ModifierUtil.ToString(typeof(PublicClass));
        Assert.Contains("public", str);
    }

    [Fact]
    public void IsInternalTest()
    {
        Assert.False(ModifierUtil.IsInternal(typeof(PublicClass)));
    }

    public class PublicClass { }
    private class PrivateClass { }
    internal static class StaticClass { }
    public abstract class AbstractClass { }
}
