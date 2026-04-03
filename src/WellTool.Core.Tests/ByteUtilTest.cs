using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Byte工具单元测试
    /// </summary>
    public class ByteUtilTest
    {
        [Fact]
        public void ToLongTest()
        {
            var bytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x2B };
            var result = WellTool.Core.ByteUtil.ToLong(bytes);
            Assert.Equal(299L, result);
        }

        [Fact]
        public void ToIntTest()
        {
            var bytes = new byte[] { 0x00, 0x00, 0x01, 0x2B };
            var result = WellTool.Core.ByteUtil.ToInt(bytes);
            Assert.Equal(299, result);
        }

        [Fact]
        public void ToShortTest()
        {
            var bytes = new byte[] { 0x01, 0x2B };
            var result = WellTool.Core.ByteUtil.ToShort(bytes);
            Assert.Equal((short)299, result);
        }

        [Fact]
        public void ToHexStringTest()
        {
            var bytes = new byte[] { 0x01, 0x2B };
            var result = WellTool.Core.ByteUtil.ToHexString(bytes);
            Assert.Equal("012B", result);
        }

        [Fact]
        public void FromHexStringTest()
        {
            var result = WellTool.Core.ByteUtil.FromHexString("012B");
            Assert.Equal(new byte[] { 0x01, 0x2B }, result);
        }

        [Fact]
        public void IsEmptyTest()
        {
            Assert.True(WellTool.Core.ByteUtil.IsEmpty(null));
            Assert.True(WellTool.Core.ByteUtil.IsEmpty(new byte[0]));
            Assert.False(WellTool.Core.ByteUtil.IsEmpty(new byte[] { 0x01 }));
        }

        [Fact]
        public void ToBinaryStringTest()
        {
            var b = (byte)0x3A;
            var result = WellTool.Core.ByteUtil.ToBinaryString(b);
            Assert.Equal("00111010", result);
        }
    }
}
