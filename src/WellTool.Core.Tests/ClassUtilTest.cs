using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Class工具单元测试
    /// </summary>
    public class ClassUtilTest
    {
        [Fact]
        public void GetClassLoaderTest()
        {
            var classLoader = WellTool.Core.ClassUtil.GetClassLoader();
            Assert.NotNull(classLoader);
        }

        [Fact]
        public void GetClassByNameTest()
        {
            var clazz = WellTool.Core.ClassUtil.GetClassByName("System.String");
            Assert.NotNull(clazz);
            Assert.Equal(typeof(string), clazz);
        }

        [Fact]
        public void GetPackageNameTest()
        {
            var packageName = WellTool.Core.ClassUtil.GetPackageName(typeof(string).FullName);
            Assert.Equal("System", packageName);
        }

        [Fact]
        public void IsPresentTest()
        {
            Assert.True(WellTool.Core.ClassUtil.IsPresent("System.String"));
            Assert.False(WellTool.Core.ClassUtil.IsPresent("NotExistClass12345"));
        }

        [Fact]
        public void IsAssignableTest()
        {
            Assert.True(WellTool.Core.ClassUtil.IsAssignable(typeof(string), typeof(object)));
            Assert.True(WellTool.Core.ClassUtil.IsAssignable(typeof(IEnumerable<string>), typeof(object)));
            Assert.False(WellTool.Core.ClassUtil.IsAssignable(typeof(object), typeof(string)));
        }

        [Fact]
        public void GetSimpleNameTest()
        {
            var name = WellTool.Core.ClassUtil.GetSimpleName(typeof(string));
            Assert.Equal("String", name);
        }

        [Fact]
        public void GetTypeNameTest()
        {
            var name = WellTool.Core.ClassUtil.GetTypeName(typeof(string));
            Assert.Equal("System.String", name);
        }

        [Fact]
        public void IsPublicTest()
        {
            Assert.True(WellTool.Core.ClassUtil.IsPublic(typeof(string)));
        }

        [Fact]
        public void IsInnerClassTest()
        {
            // 内部类测试
            var isInner = WellTool.Core.ClassUtil.IsInnerClass(typeof(ClassUtilTest));
            Assert.False(isInner);
        }
    }
}
