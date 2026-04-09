using System.Text;
using Xunit;
using WellTool.Crypto.Digest;

namespace WellTool.Crypto.Tests.Digest
{
    /// <summary>
    /// PBKDF2 密钥派生函数测试
    /// </summary>
    public class PBKDF2Tests
    {
        [Fact]
        public void DeriveKeyTest()
        {
            var password = "password";
            var salt = "salt";
            var iterations = 1000;
            var keyLength = 32;

            var key = PBKDF2.DeriveKey(password, salt, iterations, keyLength);
            Assert.NotNull(key);
            Assert.Equal(keyLength, key.Length);
        }

        [Fact]
        public void DeriveKeyHexTest()
        {
            var password = "password";
            var salt = "salt";
            var iterations = 1000;
            var keyLength = 32;

            var keyHex = PBKDF2.DeriveKeyHex(password, salt, iterations, keyLength);
            Assert.NotNull(keyHex);
            Assert.Equal(keyLength * 2, keyHex.Length); // 每个字节两个十六进制字符
        }

        [Fact]
        public void DeriveKeyWithByteArrayTest()
        {
            var password = Encoding.UTF8.GetBytes("password");
            var salt = Encoding.UTF8.GetBytes("salt");
            var iterations = 1000;
            var keyLength = 32;

            var pbkdf2 = new PBKDF2();
            var key = pbkdf2.DeriveKey(password, salt, iterations, keyLength);
            Assert.NotNull(key);
            Assert.Equal(keyLength, key.Length);
        }

        [Fact]
        public void ConsistencyTest()
        {
            var password = "password";
            var salt = "salt";
            var iterations = 1000;
            var keyLength = 32;

            var key1 = PBKDF2.DeriveKey(password, salt, iterations, keyLength);
            var key2 = PBKDF2.DeriveKey(password, salt, iterations, keyLength);

            Assert.Equal(key1, key2);
        }
    }
}
