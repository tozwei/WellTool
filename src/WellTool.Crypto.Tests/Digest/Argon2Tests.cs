using WellTool.Crypto.Digest;
using Xunit;

namespace WellTool.Crypto.Tests.Digest
{
    /// <summary>
    /// Argon2 密码哈希测试 - 2015 年密码哈希竞赛获胜者
    /// </summary>
    public class Argon2Tests
    {
        [Fact]
        public void HashPasswordTest()
        {
            // Argon2 哈希密码
            var password = "mySecurePassword123";
            var hash = Argon2.Hash(password);

            Assert.NotNull(hash);
        }

        [Fact]
        public void VerifyPasswordTest()
        {
            // Argon2 验证密码
            var password = "mySecurePassword123";
            var hash = Argon2.Hash(password);

            var isValid = Argon2.Verify(password, hash);

            Assert.True(isValid);
        }

        [Fact]
        public void VerifyWrongPasswordTest()
        {
            // Argon2 验证错误密码
            var password = "correctPassword";
            var wrongPassword = "wrongPassword";
            var hash = Argon2.Hash(password);

            var isValid = Argon2.Verify(wrongPassword, hash);

            Assert.False(isValid);
        }

        [Fact]
        public void DifferentTypesTest()
        {
            // Argon2 不同类型测试（Argon2d、Argon2i、Argon2id）
            var password = "testPassword";

            // Argon2d - 对 GPU 攻击抵抗力强
            var hashD = Argon2.Hash(password, variant: Argon2Variant.Argon2d);
            Assert.NotNull(hashD);

            // Argon2i - 对侧信道攻击抵抗力强
            var hashI = Argon2.Hash(password, variant: Argon2Variant.Argon2i);
            Assert.NotNull(hashI);

            // Argon2id - 综合两者优点（推荐）
            var hashId = Argon2.Hash(password, variant: Argon2Variant.Argon2id);
            Assert.NotNull(hashId);

            // 都能验证通过
            Assert.True(Argon2.Verify(password, hashD));
            Assert.True(Argon2.Verify(password, hashI));
            Assert.True(Argon2.Verify(password, hashId));
        }

        [Fact]
        public void DifferentParametersTest()
        {
            // Argon2 不同参数测试
            var password = "testPassword";

            // 时间成本、空间成本、并行度
            var hash1 = Argon2.Hash(password, timeCost: 1, memoryCost: 8192, parallelism: 1);
            var hash2 = Argon2.Hash(password, timeCost: 3, memoryCost: 65536, parallelism: 4);

            Assert.NotNull(hash1);
            Assert.NotNull(hash2);
            Assert.NotEqual(hash1, hash2);

            // 都能验证
            Assert.True(Argon2.Verify(password, hash1));
            Assert.True(Argon2.Verify(password, hash2));
        }

        [Fact]
        public void UnicodePasswordTest()
        {
            // Argon2 Unicode 密码测试
            var password = "密码 Password🔐";
            var hash = Argon2.Hash(password);

            Assert.NotNull(hash);
            Assert.True(Argon2.Verify(password, hash));
        }

        [Fact]
        public void LongPasswordTest()
        {
            // Argon2 长密码测试
            var longPassword = new string('A', 1000);
            var hash = Argon2.Hash(longPassword);

            Assert.NotNull(hash);
            Assert.True(Argon2.Verify(longPassword, hash));
        }

        [Fact]
        public void EmptyPasswordTest()
        {
            // Argon2 空密码测试
            var emptyPassword = "";
            var hash = Argon2.Hash(emptyPassword);

            Assert.NotNull(hash);
            Assert.True(Argon2.Verify(emptyPassword, hash));
        }

        [Fact]
        public void SaltLengthTest()
        {
            // Argon2 盐长度测试
            var password = "testPassword";

            // 推荐盐长度至少 8 字节
            var salt8 = Argon2.GenerateSalt(8);
            var salt16 = Argon2.GenerateSalt(16);

            Assert.NotNull(salt8);
            Assert.NotNull(salt16);
            Assert.Equal(8, salt8.Length);
            Assert.Equal(16, salt16.Length);
        }
    }
}
