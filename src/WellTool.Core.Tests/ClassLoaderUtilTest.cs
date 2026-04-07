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
            var clazz = WellTool.Core.Util.ClassLoaderUtil.GetClassByName("System.String");
            Assert.NotNull(clazz);
        }

        [Fact]
        public void GetDefaultClassLoaderTest()
        {
            var classLoader = WellTool.Core.Util.ClassLoaderUtil.GetDefaultClassLoader();
            Assert.NotNull(classLoader);
        }

        [Fact]
        public void SetClassLoaderTest()
        {
            var classLoader = WellTool.Core.Util.ClassLoaderUtil.GetDefaultClassLoader();
            WellTool.Core.Util.ClassLoaderUtil.SetClassLoader(classLoader);
            Assert.Equal(classLoader, WellTool.Core.Util.ClassLoaderUtil.GetDefaultClassLoader());
        }
    }
}
