using Xunit;
using WellTool.Core.Util;
using System;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Type工具单元测试
    /// </summary>
    public class TypeUtilTest
    {
        [Fact]
        public void GetNameTest()
        {
            var name = TypeUtil.GetName(typeof(string));
            Assert.Equal("String", name);
        }

        [Fact]
        public void GetFullNameTest()
        {
            var fullName = TypeUtil.GetFullName(typeof(string));
            Assert.Equal("System.String", fullName);
        }

        [Fact]
        public void IsBasicTypeTest()
        {
            Assert.True(TypeUtil.IsBasicType(typeof(int)));
            Assert.True(TypeUtil.IsBasicType(typeof(string)));
            Assert.False(TypeUtil.IsBasicType(typeof(object)));
        }

        [Fact]
        public void IsValueTypeTest()
        {
            Assert.True(TypeUtil.IsValueType(typeof(int)));
            Assert.True(TypeUtil.IsValueType(typeof(bool)));
            Assert.False(TypeUtil.IsValueType(typeof(string)));
        }

        [Fact]
        public void IsEnumTest()
        {
            Assert.True(TypeUtil.IsEnum(typeof(DayOfWeek)));
            Assert.False(TypeUtil.IsEnum(typeof(string)));
        }

        [Fact]
        public void IsArrayTest()
        {
            Assert.True(TypeUtil.IsArray(typeof(int[])));
            Assert.False(TypeUtil.IsArray(typeof(string)));
        }

        [Fact]
        public void GetElementTypeTest()
        {
            var elementType = TypeUtil.GetElementType(typeof(int[]));
            Assert.Equal(typeof(int), elementType);
        }

        [Fact]
        public void GetGenericArgumentsTest()
        {
            var listType = typeof(System.Collections.Generic.List<string>);
            var arguments = TypeUtil.GetGenericArguments(listType);
            Assert.Equal(1, arguments.Length);
            Assert.Equal(typeof(string), arguments[0]);
        }

        [Fact]
        public void GetBaseTypeTest()
        {
            var baseType = TypeUtil.GetBaseType(typeof(string));
            Assert.Equal(typeof(object), baseType);
        }

        [Fact]
        public void IsAssignableFromTest()
        {
            Assert.True(TypeUtil.IsAssignableFrom(typeof(string), typeof(object)));
            Assert.False(TypeUtil.IsAssignableFrom(typeof(int), typeof(string)));
        }

        [Fact]
        public void GetNamespaceTest()
        {
            var ns = TypeUtil.GetNamespace(typeof(string));
            Assert.Equal("System", ns);
        }

        [Fact]
        public void GetAssemblyNameTest()
        {
            var assemblyName = TypeUtil.GetAssemblyName(typeof(string));
            Assert.NotNull(assemblyName);
        }
    }
}
