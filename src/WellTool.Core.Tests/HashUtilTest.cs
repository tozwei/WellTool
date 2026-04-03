using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Hash工具单元测试
    /// </summary>
    public class HashUtilTest
    {
        [Fact]
        public void Md5Test()
        {
            var hash = HashUtil.Md5("test");
            Assert.NotNull(hash);
            Assert.Equal(32, hash.Length);
        }

        [Fact]
        public void Md5HexTest()
        {
            var hash = HashUtil.Md5Hex("test");
            Assert.NotNull(hash);
            Assert.Equal(32, hash.Length);
        }

        [Fact]
        public void Sha1Test()
        {
            var hash = HashUtil.Sha1("test");
            Assert.NotNull(hash);
            Assert.Equal(40, hash.Length);
        }

        [Fact]
        public void Sha256Test()
        {
            var hash = HashUtil.Sha256("test");
            Assert.NotNull(hash);
            Assert.Equal(64, hash.Length);
        }

        [Fact]
        public void Sha512Test()
        {
            var hash = HashUtil.Sha512("test");
            Assert.NotNull(hash);
            Assert.Equal(128, hash.Length);
        }

        [Fact]
        public void Crc32Test()
        {
            var hash = HashUtil.Crc32("test");
            Assert.True(hash >= 0);
        }

        [Fact]
        public void Adler32Test()
        {
            var hash = HashUtil.Adler32("test");
            Assert.True(hash >= 0);
        }

        [Fact]
        public void MurmurHashTest()
        {
            var hash = HashUtil.MurmurHash("test");
            Assert.True(hash >= 0);
        }

        [Fact]
        public void FnvHashTest()
        {
            var hash = HashUtil.FnvHash("test");
            Assert.True(hash >= 0);
        }

        [Fact]
        public void AddTest()
        {
            var hasher = HashUtil.Add();
            hasher.Add("test");
            var hash = hasher.Hash32();
            Assert.True(hash >= 0);
        }
    }
}
