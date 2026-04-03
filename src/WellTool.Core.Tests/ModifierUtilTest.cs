using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Modifier工具单元测试
    /// </summary>
    public class ModifierUtilTest
    {
        [Fact]
        public void IsPublicTest()
        {
            Assert.True(ModifierUtil.IsPublic(typeof(string).GetModifiers()));
        }

        [Fact]
        public void IsPrivateTest()
        {
            var privateField = typeof(TestClass).GetField("PrivateField", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.True(ModifierUtil.IsPrivate(privateField?.GetModifiers() ?? 0));
        }

        [Fact]
        public void IsProtectedTest()
        {
            var protectedField = typeof(TestClass).GetField("ProtectedField", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.True(ModifierUtil.IsProtected(protectedField?.GetModifiers() ?? 0));
        }

        [Fact]
        public void IsStaticTest()
        {
            var staticField = typeof(TestClass).GetField("StaticField", BindingFlags.Public | BindingFlags.Static);
            Assert.True(ModifierUtil.IsStatic(staticField?.GetModifiers() ?? 0));
        }

        [Fact]
        public void IsFinalTest()
        {
            var finalField = typeof(string).GetField("Empty", BindingFlags.Public | BindingFlags.Static);
            Assert.True(ModifierUtil.IsFinal(finalField?.GetModifiers() ?? 0));
        }

        [Fact]
        public void IsAbstractTest()
        {
            var abstractMethod = typeof(TestAbstractClass).GetMethod("AbstractMethod");
            Assert.True(ModifierUtil.IsAbstract(abstractMethod?.GetModifiers() ?? 0));
        }

        [Fact]
        public void IsSynchronizedTest()
        {
            var syncMethod = typeof(TestClass).GetMethod("SynchronizedMethod");
            Assert.False(ModifierUtil.IsSynchronized(syncMethod?.GetModifiers() ?? 0));
        }

        [Fact]
        public void ToStringTest()
        {
            var modifiers = ModifierUtil.ToString(typeof(string).GetModifiers());
            Assert.Contains("public", modifiers);
        }

        private class TestClass
        {
            private string PrivateField = "";
            protected string ProtectedField = "";
            public static string StaticField = "";
            public void SynchronizedMethod() { }
        }

        private abstract class TestAbstractClass
        {
            public abstract void AbstractMethod();
        }
    }
}

namespace System.Reflection
{
    public partial class MemberInfo
    {
        public virtual int GetModifiers() => 0;
    }

    public partial class TypeInfo
    {
    }
}
