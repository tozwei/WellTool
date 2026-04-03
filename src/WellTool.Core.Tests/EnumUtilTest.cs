using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Enum工具单元测试
    /// </summary>
    public class EnumUtilTest
    {
        private enum TestEnum
        {
            A = 1,
            B = 2,
            C = 3
        }

        [Fact]
        public void GetNamesTest()
        {
            var names = EnumUtil.GetNames<TestEnum>();
            Assert.Equal(3, names.Count);
            Assert.Contains("A", names);
            Assert.Contains("B", names);
            Assert.Contains("C", names);
        }

        [Fact]
        public void GetValuesTest()
        {
            var values = EnumUtil.GetValues<TestEnum>();
            Assert.Equal(3, values.Count);
            Assert.Contains(TestEnum.A, values);
            Assert.Contains(TestEnum.B, values);
            Assert.Contains(TestEnum.C, values);
        }

        [Fact]
        public void GetEnumMapTest()
        {
            var map = EnumUtil.GetEnumMap<TestEnum>();
            Assert.Equal(3, map.Count);
            Assert.Equal(TestEnum.A, map["A"]);
            Assert.Equal(TestEnum.B, map["B"]);
            Assert.Equal(TestEnum.C, map["C"]);
        }

        [Fact]
        public void GetNameTest()
        {
            Assert.Equal("A", EnumUtil.GetName(TestEnum.A));
            Assert.Equal("B", EnumUtil.GetName(TestEnum.B));
        }

        [Fact]
        public void GetEnumTest()
        {
            Assert.Equal(TestEnum.A, EnumUtil.GetEnum<TestEnum>("A"));
            Assert.Equal(TestEnum.B, EnumUtil.GetEnum<TestEnum>("B"));
        }

        [Fact]
        public void GetEnumOrNullTest()
        {
            Assert.Equal(TestEnum.A, EnumUtil.GetEnumOrNull<TestEnum>("A"));
            Assert.Null(EnumUtil.GetEnumOrNull<TestEnum>("D"));
        }

        [Fact]
        public void IsValidTest()
        {
            Assert.True(EnumUtil.IsValid<TestEnum>("A"));
            Assert.True(EnumUtil.IsValid<TestEnum>("B"));
            Assert.False(EnumUtil.IsValid<TestEnum>("D"));
            Assert.False(EnumUtil.IsValid<TestEnum>(0));
            Assert.True(EnumUtil.IsValid<TestEnum>(1));
        }

        [Fact]
        public void ContainsTest()
        {
            Assert.True(EnumUtil.Contains<TestEnum>(TestEnum.A));
            Assert.False(EnumUtil.Contains<TestEnum>((TestEnum)99));
        }

        [Fact]
        public void ToEnumTest()
        {
            Assert.Equal(TestEnum.A, EnumUtil.ToEnum<TestEnum>("A"));
            Assert.Equal(TestEnum.B, EnumUtil.ToEnum<TestEnum>("B"));
        }
    }
}
