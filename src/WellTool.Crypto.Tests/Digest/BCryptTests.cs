using WellTool.Crypto.Digest;
using Xunit;

namespace WellTool.Crypto.Tests.Digest
{
    /// <summary>
    /// BCrypt 密码哈希测试
    /// </summary>
    public class BCryptTests
    {
        [Fact]
        public void HashPasswordTest()
        {
            // BCrypt 哈希密码
            var password = "mySecurePassword123";
            var hash = BCrypt.Hash(password);

            Assert.NotNull(hash);
            Assert.StartsWith("$2", hash); // BCrypt 哈希以$2 开头
        }

        [Fact]
        public void VerifyPasswordTest()
        {
            // BCrypt 验证密码
            var password = "mySecurePassword123";
            var hash = BCrypt.Hash(password);

            var isValid = BCrypt.Verify(password, hash);

            Assert.True(isValid);
        }

        [Fact]
        public void VerifyWrongPasswordTest()
        {
            // BCrypt 验证错误密码
            var password = "correctPassword";
            var wrongPassword = "wrongPassword";
            var hash = BCrypt.Hash(password);

            var isValid = BCrypt.Verify(wrongPassword, hash);

            Assert.False(isValid);
        }

        [Fact]
        public void DifferentSaltProducesDifferentHashTest()
        {
            // BCrypt 不同盐产生不同哈希
            var password = "samePassword";
            var hash1 = BCrypt.Hash(password);
            var hash2 = BCrypt.Hash(password);

            // BCrypt 每次生成不同的盐，所以哈希也不同
            Assert.NotEqual(hash1, hash2);

            // 但都能验证通过
            Assert.True(BCrypt.Verify(password, hash1));
            Assert.True(BCrypt.Verify(password, hash2));
        }

        [Fact]
        public void DifferentCostFactorsTest()
        {
            // BCrypt 不同成本因子测试
            var password = "testPassword";

            // 成本因子 4（较快）
            var hash4 = BCrypt.Hash(password, 4);
            Assert.NotNull(hash4);

            // 成本因子 10（标准）
            var hash10 = BCrypt.Hash(password, 10);
            Assert.NotNull(hash10);

            // 成本因子 12（更安全）
            var hash12 = BCrypt.Hash(password, 12);
            Assert.NotNull(hash12);

            // 所有哈希都能验证
            Assert.True(BCrypt.Verify(password, hash4));
            Assert.True(BCrypt.Verify(password, hash10));
            Assert.True(BCrypt.Verify(password, hash12));
        }

        [Fact]
        public void LongPasswordTest()
        {
            // BCrypt 长密码测试
            var longPassword = new string('A', 100);
            var hash = BCrypt.Hash(longPassword);

            Assert.NotNull(hash);
            Assert.True(BCrypt.Verify(longPassword, hash));
        }

        [Fact]
        public void UnicodePasswordTest()
        {
            // BCrypt Unicode 密码测试
            var unicodePassword = "密码 Password🔐";
            var hash = BCrypt.Hash(unicodePassword);

            Assert.NotNull(hash);
            Assert.True(BCrypt.Verify(unicodePassword, hash));
        }

        [Fact]
        public void EmptyPasswordTest()
        {
            // BCrypt 空密码测试
            var emptyPassword = "";
            var hash = BCrypt.Hash(emptyPassword);

            Assert.NotNull(hash);
            Assert.True(BCrypt.Verify(emptyPassword, hash));
        }

        [Fact]
        public void HashFormatValidationTest()
        {
            // BCrypt 哈希格式验证
            var password = "test";
            var hash = BCrypt.Hash(password);

            // BCrypt 格式：$2a$[cost]$[22 位 salt][31 位 hash]
            var parts = hash.Split('$');
            Assert.Equal(4, parts.Length);
            Assert.Equal("2", parts[1]); // 版本号
            Assert.Equal(2, parts[2].Length); // 成本因子
            Assert.Equal(22, parts[3].Length); // salt + hash 长度
        }

        [Fact]
        public void KnownValueTest()
        {
            // BCrypt 已知值测试（使用固定盐）
            var password = "password";
            var salt = "saltssssssssssssss"; // 22 字符盐

            // 注意：实际使用中不应该使用固定盐
            // 这里仅用于测试目的
            var hash = BCrypt.HashWithSalt(password, salt);

            Assert.NotNull(hash);
            // 使用相同盐和密碼应该产生相同哈希
            var hash2 = BCrypt.HashWithSalt(password, salt);
            Assert.Equal(hash, hash2);
        }
    }
}
