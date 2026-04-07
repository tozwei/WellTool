using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Radix工具单元测试
    /// </summary>
    public class RadixUtilTest
    {
        [Fact]
        public void ConvertTest()
        {
            Assert.Equal("100", Convert.ToString(4, 2));
            Assert.Equal("11", Convert.ToString(3, 2));
        }

        [Fact]
        public void IsLowerCaseTest()
        {
            Assert.True(char.IsLower('a'));
            Assert.False(char.IsLower('A'));
            Assert.False(char.IsLower('1'));
        }

        [Fact]
        public void IsUpperCaseTest()
        {
            Assert.True(char.IsUpper('A'));
            Assert.False(char.IsUpper('a'));
            Assert.False(char.IsUpper('1'));
        }
    }
}
