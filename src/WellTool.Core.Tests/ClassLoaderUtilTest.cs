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
            var classLoader = WellTool.Core.Util.ClassUtil.GetClassLoader();
            Assert.NotNull(classLoader);
        }

        [Fact]
        public void LoadClassTest()
        {
            var clazz = WellTool.Core.Util.ClassUtil.GetClassByName("System.String");
            Assert.NotNull(clazz);
        }

        [Fact]
        public void GetDefaultClassLoaderTest()
        {
            var classLoader = WellTool.Core.Util.ClassUtil.GetDefaultClassLoader();
            Assert.NotNull(classLoader);
        }

        [Fact]
        public void SetClassLoaderTest()
        {
            var classLoader = WellTool.Core.Util.ClassUtil.GetDefaultClassLoader();
            WellTool.Core.Util.ClassUtil.SetClassLoader(classLoader);
            Assert.Equal(classLoader, WellTool.Core.Util.ClassUtil.GetDefaultClassLoader());
        }
    }
}
