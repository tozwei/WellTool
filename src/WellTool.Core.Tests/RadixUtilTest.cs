using Xunit;
using WellTool.Core;

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
            Assert.Equal("100", RadixUtil.Convert(4, 2, "100"));
            Assert.Equal("11", RadixUtil.Convert(10, 2, "3"));
        }

        [Fact]
        public void ConvertToBigIntegerTest()
        {
            Assert.Equal("100", RadixUtil.ConvertToBigInteger(4, 2, "100"));
        }

        [Fact]
        public void SignIntTest()
        {
            Assert.Equal("100", RadixUtil.SignInt(4, 2, "100"));
        }

        [Fact]
        public void SignIntWithArgsTest()
        {
            var result = RadixUtil.SignInt(16, 10, 2, 0, "100");
            Assert.NotNull(result);
        }

        [Fact]
        public void ToPlainBinaryTest()
        {
            var result = RadixUtil.ToPlainBinary("ABC");
            Assert.NotNull(result);
        }

        [Fact]
        public void ToPlainHexTest()
        {
            var result = RadixUtil.ToPlainHex("FF");
            Assert.NotNull(result);
        }

        [Fact]
        public void IsLowerCaseTest()
        {
            Assert.True(RadixUtil.IsLowerCase('a'));
            Assert.False(RadixUtil.IsLowerCase('A'));
            Assert.False(RadixUtil.IsLowerCase('1'));
        }

        [Fact]
        public void IsUpperCaseTest()
        {
            Assert.True(RadixUtil.IsUpperCase('A'));
            Assert.False(RadixUtil.IsUpperCase('a'));
            Assert.False(RadixUtil.IsUpperCase('1'));
        }
    }
}
