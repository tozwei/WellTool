using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Hex工具单元测试
    /// </summary>
    public class HexUtilTest
    {
        [Fact]
        public void EncodeHexStrTest()
        {
            var bytes = new byte[] { 0x01, 0xAB, 0xFF };
            var result = HexUtil.EncodeHexStr(bytes);
            Assert.Equal("01ABFF", result);
        }

        [Fact]
        public void EncodeHexStrLowerCaseTest()
        {
            var bytes = new byte[] { 0x01, 0xAB, 0xFF };
            var result = HexUtil.EncodeHexStr(bytes, true);
            Assert.Equal("01abff", result);
        }

        [Fact]
        public void DecodeHexStrTest()
        {
            var result = HexUtil.DecodeHexStr("01ABFF");
            Assert.Equal(new byte[] { 0x01, 0xAB, 0xFF }, result);
        }

        [Fact]
        public void EncodeHexTest()
        {
            var bytes = new byte[] { 0x01, 0xAB, 0xFF };
            var result = HexUtil.EncodeHex(bytes);
            Assert.Equal("01ABFF", HexUtil.EncodeHexStr(result));
        }

        [Fact]
        public void DecodeHexTest()
        {
            var hexStr = "01ABFF";
            var result = HexUtil.DecodeHex(hexStr);
            Assert.Equal(new byte[] { 0x01, 0xAB, 0xFF }, result);
        }

        [Fact]
        public void IsHexadecimalTest()
        {
            Assert.True(HexUtil.IsHexadecimal("0123456789ABCDEF"));
            Assert.True(HexUtil.IsHexadecimal("abcdef"));
            Assert.False(HexUtil.IsHexadecimal("GHIJ"));
            Assert.False(HexUtil.IsHexadecimal("123G"));
        }

        [Fact]
        public void HasHexPrefixTest()
        {
            Assert.True(HexUtil.HasHexPrefix("0x01AB"));
            Assert.False(HexUtil.HasHexPrefix("01AB"));
        }

        [Fact]
        public void CleanHexPrefixTest()
        {
            Assert.Equal("01AB", HexUtil.CleanHexPrefix("0x01AB"));
            Assert.Equal("01AB", HexUtil.CleanHexPrefix("01AB"));
        }

        [Fact]
        public void AddHexPrefixTest()
        {
            Assert.Equal("0x01AB", HexUtil.AddHexPrefix("01AB"));
            Assert.Equal("0x01AB", HexUtil.AddHexPrefix("0x01AB"));
        }

        [Fact]
        public void RoundTripTest()
        {
            var original = "Hello, World!";
            var bytes = System.Text.Encoding.UTF8.GetBytes(original);
            var hex = HexUtil.EncodeHexStr(bytes);
            var decoded = HexUtil.DecodeHexStr(hex);
            var result = System.Text.Encoding.UTF8.GetString(decoded);
            Assert.Equal(original, result);
        }
    }
}
