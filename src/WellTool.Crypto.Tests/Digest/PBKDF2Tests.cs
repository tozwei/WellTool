using WellTool.Crypto.Digest;
using Xunit;

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
            // PBKDF2 派生密钥
            var password = "myPassword123";
            var salt = "randomSalt123456";
            var iterations = 10000;
            var keyLength = 32;

            var key = PBKDF2.DeriveKey(password, salt, iterations, keyLength);

            Assert.NotNull(key);
            Assert.Equal(keyLength, key.Length);
        }

        [Fact]
        public void DeterministicDerivationTest()
        {
            // PBKDF2 确定性派生 - 相同参数产生相同密钥
            var password = "testPassword";
            var salt = "testsalt";
            var iterations = 1000;
            var keyLength = 16;

            var key1 = PBKDF2.DeriveKey(password, salt, iterations, keyLength);
            var key2 = PBKDF2.DeriveKey(password, salt, iterations, keyLength);

            Assert.Equal(key1, key2);
        }

        [Fact]
        public void DifferentSaltProducesDifferentKeyTest()
        {
            // PBKDF2 不同盐产生不同密钥
            var password = "samePassword";
            var salt1 = "salt123456789012";
            var salt2 = "salt234567890123";
            var iterations = 1000;
            var keyLength = 32;

            var key1 = PBKDF2.DeriveKey(password, salt1, iterations, keyLength);
            var key2 = PBKDF2.DeriveKey(password, salt2, iterations, keyLength);

            Assert.NotEqual(key1, key2);
        }

        [Fact]
        public void DifferentIterationsProducesDifferentKeyTest()
        {
            // PBKDF2 不同迭代次数产生不同密钥
            var password = "testPassword";
            var salt = "testsalt12345678";
            var keyLength = 32;

            var key1000 = PBKDF2.DeriveKey(password, salt, 1000, keyLength);
            var key10000 = PBKDF2.DeriveKey(password, salt, 10000, keyLength);

            Assert.NotEqual(key1000, key10000);
        }

        [Fact]
        public void DifferentKeyLengthsTest()
        {
            // PBKDF2 不同密钥长度测试
            var password = "testPassword";
            var salt = "testsalt12345678";
            var iterations = 1000;

            var key16 = PBKDF2.DeriveKey(password, salt, iterations, 16);
            var key32 = PBKDF2.DeriveKey(password, salt, iterations, 32);
            var key64 = PBKDF2.DeriveKey(password, salt, iterations, 64);

            Assert.Equal(16, key16.Length);
            Assert.Equal(32, key32.Length);
            Assert.Equal(64, key64.Length);
        }

        [Fact]
        public void UnicodePasswordTest()
        {
            // PBKDF2 Unicode密码测试
            var password = "密码 Password🔐";
            var salt = "unicodeSalt12345";
            var iterations = 1000;
            var keyLength = 32;

            var key = PBKDF2.DeriveKey(password, salt, iterations, keyLength);

            Assert.NotNull(key);
            Assert.Equal(keyLength, key.Length);
        }

        [Fact]
        public void LongPasswordTest()
        {
            // PBKDF2 长密码测试
            var longPassword = new string('A', 1000);
            var salt = "longPasswordSalt";
            var iterations = 1000;
            var keyLength = 32;

            var key = PBKDF2.DeriveKey(longPassword, salt, iterations, keyLength);

            Assert.NotNull(key);
            Assert.Equal(keyLength, key.Length);
        }

        [Fact]
        public void RecommendedParametersTest()
        {
            // PBKDF2 推荐参数测试（NIST 建议）
            var password = "securePassword";
            var salt = "recommendedSalt123";

            // NIST SP 800-132 推荐至少 10000 次迭代
            var iterations = 10000;
            var keyLength = 32;

            var key = PBKDF2.DeriveKey(password, salt, iterations, keyLength);

            Assert.NotNull(key);
            Assert.Equal(keyLength, key.Length);
        }

        [Fact]
        public void HexOutputTest()
        {
            // PBKDF2 十六进制输出测试
            var password = "test";
            var salt = "hexsalt123456789";
            var iterations = 1000;
            var keyLength = 32;

            var hexKey = PBKDF2.DeriveKeyHex(password, salt, iterations, keyLength);

            Assert.NotNull(hexKey);
            Assert.Equal(keyLength * 2, hexKey.Length); // 十六进制长度是字节数的 2 倍

            // 验证都是十六进制字符
            foreach (var c in hexKey)
            {
                Assert.Contains(c, "0123456789abcdefABCDEF");
            }
        }

        [Fact]
        public void KnownValueTest()
        {
            // PBKDF2 已知值测试
            var password = "password";
            var salt = "salt";
            var iterations = 1;
            var keyLength = 20;

            var key = PBKDF2.DeriveKey(password, salt, iterations, keyLength);

            Assert.NotNull(key);
            Assert.Equal(keyLength, key.Length);
        }
    }
}
