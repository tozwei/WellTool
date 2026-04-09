using System.Text;
using Xunit;
using WellTool.Crypto;
using WellTool.Crypto.Symmetric;

namespace WellTool.Crypto.Tests
{
    public class KeyUtilTests
    {
        [Fact]
        public void GenerateKeyTest()
        {
            var key = KeyUtil.GenerateKey(128);
            Assert.NotNull(key);
            Assert.Equal(16, key.Length); // 128 bits = 16 bytes
        }

        [Fact]
        public void GenerateSymmetricKeyTest()
        {
            var key = KeyUtil.GenerateSymmetricKey(256);
            Assert.NotNull(key);
            Assert.Equal(32, key.Length); // 256 bits = 32 bytes
        }

        [Fact]
        public void GenerateSymmetricKeyWithAlgorithmTest()
        {
            var key = KeyUtil.GenerateSymmetricKey(SymmetricAlgorithmType.AES);
            Assert.NotNull(key);
            Assert.Equal(32, key.Length); // 256 bits = 32 bytes
        }

        [Fact]
        public void GenerateIVTest()
        {
            var iv = KeyUtil.GenerateIV(128);
            Assert.NotNull(iv);
            Assert.Equal(16, iv.Length); // 128 bits = 16 bytes
        }

        [Fact]
        public void GenerateIVWithAlgorithmTest()
        {
            var iv = KeyUtil.GenerateIV(SymmetricAlgorithmType.AES);
            Assert.NotNull(iv);
            Assert.Equal(16, iv.Length); // 128 bits = 16 bytes
        }

        [Fact]
        public void GenerateRsaKeyPairTest()
        {
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair();
            Assert.NotNull(publicKey);
            Assert.NotNull(privateKey);
            Assert.NotEmpty(publicKey);
            Assert.NotEmpty(privateKey);
        }

        [Fact]
        public void GenerateAesKeyTest()
        {
            var key = KeyUtil.GenerateAesKey();
            Assert.NotNull(key);
            Assert.Equal(32, key.Length); // 256 bits = 32 bytes
        }

        [Fact]
        public void GenerateDesKeyTest()
        {
            var key = KeyUtil.GenerateDesKey();
            Assert.NotNull(key);
            Assert.Equal(8, key.Length); // 64 bits = 8 bytes
        }
    }
}