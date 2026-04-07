using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// ClassLoader工具单元测试
    /// </summary>
    public class ClassLoaderUtilTest
    {
        [Fact]
        public void GetClassLoaderTest()
        {
            var classLoader = WellTool.Core.Util.ClassLoaderUtil.GetClassLoader();
            Assert.NotNull(classLoader);
        }

        [Fact]
        public void LoadClassTest()
        {
            var clazz = WellTool.Core.Util.ClassLoaderUtil.LoadClass("System.String");
            Assert.NotNull(clazz);
        }

        [Fact]
        public void GetSystemClassLoaderTest()
        {
            var classLoader = WellTool.Core.Util.ClassLoaderUtil.GetSystemClassLoader();
            Assert.NotNull(classLoader);
        }

        [Fact]
        public void GetCallerClassLoaderTest()
        {
            var classLoader = WellTool.Core.Util.ClassLoaderUtil.GetCallerClassLoader();
            Assert.NotNull(classLoader);
        }

        [Fact]
        public void GetAssemblyTest()
        {
            var assembly = WellTool.Core.Util.ClassLoaderUtil.GetAssembly("System.String");
            Assert.NotNull(assembly);
        }

        [Fact]
        public void IsClassExistTest()
        {
            Assert.True(WellTool.Core.Util.ClassLoaderUtil.IsClassExist("System.String"));
            Assert.False(WellTool.Core.Util.ClassLoaderUtil.IsClassExist("Non.Existent.Class"));
        }
    }
}
