using Xunit;
using System;
using System.Collections.Generic;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Class工具单元测试
    /// </summary>
    public class ClassUtilTest
    {
        [Fact]
        public void GetSimpleNameTest()
        {
            var name = WellTool.Core.Util.ClassUtil.GetSimpleName(typeof(string));
            Assert.Equal("String", name);
        }

        [Fact]
        public void GetNameTest()
        {
            var name = WellTool.Core.Util.ClassUtil.GetName(typeof(string));
            Assert.Equal("String", name);

            var genericName = WellTool.Core.Util.ClassUtil.GetName(typeof(List<string>));
            Assert.Contains("List", genericName);
            Assert.Contains("String", genericName);
        }

        [Fact]
        public void GetPackageTest()
        {
            var packageName = WellTool.Core.Util.ClassUtil.GetPackage(typeof(string));
            Assert.Equal("System", packageName);
        }

        [Fact]
        public void GetClassLoaderNameTest()
        {
            var className = WellTool.Core.Util.ClassUtil.GetClassLoaderName(typeof(string));
            Assert.NotNull(className);
        }

        [Fact]
        public void GetComponentTypeTest()
        {
            var componentType = WellTool.Core.Util.ClassUtil.GetComponentType(typeof(int[]));
            Assert.Equal(typeof(int), componentType);

            var nonComponentType = WellTool.Core.Util.ClassUtil.GetComponentType(typeof(string));
            Assert.Null(nonComponentType);
        }

        [Fact]
        public void IsPrimitiveWrapperTest()
        {
            Assert.True(WellTool.Core.Util.ClassUtil.IsPrimitiveWrapper(typeof(Int32)));
            Assert.False(WellTool.Core.Util.ClassUtil.IsPrimitiveWrapper(typeof(int)));
        }

        [Fact]
        public void IsPrimitiveTest()
        {
            Assert.True(WellTool.Core.Util.ClassUtil.IsPrimitive(typeof(int)));
            Assert.True(WellTool.Core.Util.ClassUtil.IsPrimitive(typeof(string)));
            Assert.False(WellTool.Core.Util.ClassUtil.IsPrimitive(typeof(Int32)));
        }

        [Fact]
        public void LoadClassTest()
        {
            var clazz = WellTool.Core.Util.ClassUtil.LoadClass("System.String");
            Assert.NotNull(clazz);
            Assert.Equal(typeof(string), clazz);
        }

        [Fact]
        public void GetClassNameTest()
        {
            var nameFromType = WellTool.Core.Util.ClassUtil.GetClassName(typeof(string));
            Assert.Equal("String", nameFromType);

            var nameFromObject = WellTool.Core.Util.ClassUtil.GetClassName("test");
            Assert.Equal("String", nameFromObject);

            var nameFromNull = WellTool.Core.Util.ClassUtil.GetClassName(null);
            Assert.Null(nameFromNull);
        }
    }
}
