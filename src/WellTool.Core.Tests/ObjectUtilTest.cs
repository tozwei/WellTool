using Xunit;
using WellTool.Core.Lang;

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
        public void IsEqualTest()
        {
            Assert.True(ObjectUtil.IsEqual("test", "test"));
            Assert.False(ObjectUtil.IsEqual("test", "Test"));
            Assert.False(ObjectUtil.IsEqual(null, "test"));
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
        public void ToStringWithDefaultTest()
        {
            Assert.Equal("", ObjectUtil.ToString(null, ""));
            Assert.Equal("test", ObjectUtil.ToString("test", ""));
        }

        private class TestClass
        {
            public int Value { get; set; }
        }
    }
}
