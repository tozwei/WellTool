using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests
{
    public class ByteUtilTest
    {
        [Fact]
        public void ToLongTest()
        {
            var bytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x2B };
            var result = ByteUtil.ToLong(bytes);
            Assert.Equal(299L, result);
        }

        [Fact]
        public void ToIntTest()
        {
            var bytes = new byte[] { 0x00, 0x00, 0x01, 0x2B };
            var result = ByteUtil.ToInt(bytes);
            Assert.Equal(299, result);
        }

        [Fact]
        public void ToShortTest()
        {
            var bytes = new byte[] { 0x01, 0x2B };
            var result = ByteUtil.ToShort(bytes);
            Assert.Equal((short)299, result);
        }

        [Fact]
        public void ToHexStringTest()
        {
            var bytes = new byte[] { 0x01, 0x2B };
            var result = ByteUtil.ToHexString(bytes);
            Assert.Equal("012B", result);
        }

        [Fact]
        public void FromHexStringTest()
        {
            var result = ByteUtil.FromHexString("012B");
            Assert.Equal(new byte[] { 0x01, 0x2B }, result);
        }

        [Fact]
        public void ToBytesIntTest()
        {
            var bytes = ByteUtil.ToBytes(299);
            Assert.Equal(4, bytes.Length);
        }

        [Fact]
        public void ToBytesLongTest()
        {
            var bytes = ByteUtil.ToBytes(299L);
            Assert.Equal(8, bytes.Length);
        }
    }
}
