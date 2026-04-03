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
    public void IsProtectedTest()
    {
        Assert.False(ModifierUtil.IsProtected(typeof(PublicClass)));
        Assert.True(ModifierUtil.IsProtected(typeof(ProtectedClass)));
    }

    [Fact]
    public void IsStaticTest()
    {
        Assert.True(ModifierUtil.IsStatic(typeof(StaticClass)));
        Assert.False(ModifierUtil.IsStatic(typeof(PublicClass)));
    }

    [Fact]
    public void IsFinalTest()
    {
        Assert.True(ModifierUtil.IsFinal(typeof(FinalClass)));
        Assert.False(ModifierUtil.IsFinal(typeof(PublicClass)));
    }

    [Fact]
    public void IsAbstractTest()
    {
        Assert.True(ModifierUtil.IsAbstract(typeof(AbstractClass)));
        Assert.False(ModifierUtil.IsAbstract(typeof(PublicClass)));
    }

    [Fact]
    public void GetModifiersTest()
    {
        var modifiers = ModifierUtil.GetModifiers(typeof(PublicClass));
        Assert.NotEmpty(modifiers);
    }

    [Fact]
    public void ToStringModifierTest()
    {
        var str = ModifierUtil.ToString(typeof(PublicClass));
        Assert.Contains("public", str);
    }

    public class PublicClass { }
    private class PrivateClass { }
    protected class ProtectedClass { }
    internal static class StaticClass { }
    public sealed class FinalClass { }
    public abstract class AbstractClass { }
}
