using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Object工具单元测试
    /// </summary>
    public class ObjectUtilTest
    {
        [Fact]
        public void IsEmptyTest()
        {
            Assert.True(ObjectUtil.IsEmpty(null));
            Assert.True(ObjectUtil.IsEmpty(""));
            Assert.True(ObjectUtil.IsEmpty(Array.Empty<object>()));

            Assert.False(ObjectUtil.IsEmpty("test"));
            Assert.False(ObjectUtil.IsEmpty(new object[] { "a" }));
        }

        [Fact]
        public void IsNotEmptyTest()
        {
            Assert.False(ObjectUtil.IsNotEmpty(null));
            Assert.False(ObjectUtil.IsNotEmpty(""));
            Assert.True(ObjectUtil.IsNotEmpty("test"));
        }

        [Fact]
        public void IsNullTest()
        {
            object nullObj = null!;
            Assert.True(ObjectUtil.IsNull(nullObj));
            Assert.False(ObjectUtil.IsNull("test"));
        }

        [Fact]
        public void IsNotNullTest()
        {
            Assert.False(ObjectUtil.IsNotNull(null));
            Assert.True(ObjectUtil.IsNotNull("test"));
        }

        [Fact]
        public void DefaultIfNullTest()
        {
            Assert.Equal("default", ObjectUtil.DefaultIfNull(null!, "default"));
            Assert.Equal("test", ObjectUtil.DefaultIfNull("test", "default"));
        }

        [Fact]
        public void EqualsTest()
        {
            Assert.True(ObjectUtil.Equals("test", "test"));
            Assert.False(ObjectUtil.Equals("test", "Test"));
            Assert.False(ObjectUtil.Equals(null, "test"));
            Assert.True(ObjectUtil.Equals(null, null));
        }

        [Fact]
        public void DeepEqualsTest()
        {
            var arr1 = new[] { 1, 2, 3 };
            var arr2 = new[] { 1, 2, 3 };
            Assert.True(ObjectUtil.DeepEquals(arr1, arr2));

            var arr3 = new[] { 1, 2, 4 };
            Assert.False(ObjectUtil.DeepEquals(arr1, arr3));
        }

        [Fact]
        public void HashCodeTest()
        {
            var hash1 = ObjectUtil.HashCode("test");
            var hash2 = ObjectUtil.HashCode("test");
            Assert.Equal(hash1, hash2);

            var hash3 = ObjectUtil.HashCode("Test");
            Assert.NotEqual(hash1, hash3);
        }

        [Fact]
        public void ToStringTest()
        {
            Assert.Equal("test", ObjectUtil.ToString("test"));
            Assert.Equal("", ObjectUtil.ToString(null));
            Assert.Equal("123", ObjectUtil.ToString(123));
        }

        [Fact]
        public void ToStringOrEmptyTest()
        {
            Assert.Equal("", ObjectUtil.ToStringOrEmpty(null!));
            Assert.Equal("test", ObjectUtil.ToStringOrEmpty("test"));
        }

        [Fact]
        public void GetTypeTest()
        {
            Assert.Equal(typeof(string), ObjectUtil.GetType("test"));
            Assert.Null(ObjectUtil.GetType(null));
        }

        [Fact]
        public void CloneTest()
        {
            var original = new TestClass { Value = 100 };
            var cloned = ObjectUtil.Clone(original);
            Assert.NotSame(original, cloned);
            Assert.Equal(original.Value, cloned.Value);
        }

        [Fact]
        public void RequireNonNullTest()
        {
            var obj = new object();
            Assert.Same(obj, ObjectUtil.RequireNonNull(obj));

            Assert.Throws<ArgumentNullException>(() => ObjectUtil.RequireNonNull(null!));
        }

        private class TestClass
        {
            public int Value { get; set; }
        }
    }
}
