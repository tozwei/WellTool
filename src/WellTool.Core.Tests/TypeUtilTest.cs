using Xunit;
using WellTool.Core;
using System;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Type工具单元测试
    /// </summary>
    public class TypeUtilTest
    {
        [Fact]
        public void GetTypeArgumentTest()
        {
            var type = TypeUtil.GetTypeArgument<string>();
            Assert.Equal(typeof(string), type);
        }

        [Fact]
        public void GetTypeArgumentClassTest()
        {
            var type = TypeUtil.GetTypeArgumentClass(typeof(string));
            Assert.Equal(typeof(string), type);
        }

        [Fact]
        public void IsUnknownTest()
        {
            Assert.False(TypeUtil.IsUnknown(typeof(string)));
        }

        [Fact]
        public void GetTypeNameTest()
        {
            var name = TypeUtil.GetTypeName(typeof(string));
            Assert.Equal("String", name);
        }

        [Fact]
        public void GetTypeClassTest()
        {
            var type = TypeUtil.GetTypeClass(typeof(string));
            Assert.NotNull(type);
        }

        [Fact]
        public void IsTypeMatchTest()
        {
            Assert.True(TypeUtil.IsTypeMatch(typeof(string), typeof(string)));
            Assert.False(TypeUtil.IsTypeMatch(typeof(string), typeof(int)));
        }

        [Fact]
        public void GetCollectionElementTypeTest()
        {
            var listType = typeof(System.Collections.Generic.List<string>);
            var elementType = TypeUtil.GetCollectionElementType(listType);
            Assert.Equal(typeof(string), elementType);
        }

        [Fact]
        public void GetMapKeyTypeTest()
        {
            var dictType = typeof(System.Collections.Generic.Dictionary<string, int>);
            var keyType = TypeUtil.GetMapKeyType(dictType);
            Assert.Equal(typeof(string), keyType);
        }

        [Fact]
        public void GetMapValueTypeTest()
        {
            var dictType = typeof(System.Collections.Generic.Dictionary<string, int>);
            var valueType = TypeUtil.GetMapValueType(dictType);
            Assert.Equal(typeof(int), valueType);
        }

        [Fact]
        public void IsSimpleTypeTest()
        {
            Assert.True(TypeUtil.IsSimpleType(typeof(int)));
            Assert.True(TypeUtil.IsSimpleType(typeof(string)));
            Assert.False(TypeUtil.IsSimpleType(typeof(System.Collections.Generic.List<int>)));
        }

        [Fact]
        public void IsBasicTypeTest()
        {
            Assert.True(TypeUtil.IsBasicType(typeof(int)));
            Assert.True(TypeUtil.IsBasicType(typeof(string)));
            Assert.False(TypeUtil.IsBasicType(typeof(object)));
        }

        [Fact]
        public void IsPrimitiveTypeTest()
        {
            Assert.True(TypeUtil.IsPrimitiveType(typeof(int)));
            Assert.True(TypeUtil.IsPrimitiveType(typeof(bool)));
            Assert.False(TypeUtil.IsPrimitiveType(typeof(string)));
        }
    }
}
