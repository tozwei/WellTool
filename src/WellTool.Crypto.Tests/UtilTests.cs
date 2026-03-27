using System;
using System.IO;
using System.Text;
using Xunit;
using WellTool.Crypto;
using WellTool.Crypto.Symmetric;
using Org.BouncyCastle.Security;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// 工具类测试
    /// </summary>
    public class UtilTests
    {
        // 测试数据
        private const string TestData = "Hello, World!";

        /// <summary>
        /// 测试 BCUtil.CreateSecureRandom
        /// </summary>
        [Fact]
        public void TestBCUtilCreateSecureRandom()
        {
            // 测试创建安全随机数生成器
            var random = BCUtil.CreateSecureRandom();
            Assert.NotNull(random);

            // 测试生成随机数
            var randomBytes = new byte[16];
            random.NextBytes(randomBytes);
            Assert.NotEmpty(randomBytes);
        }

        /// <summary>
        /// 测试 SecureUtil 方法
        /// </summary>
        [Fact]
        public void TestSecureUtilMethods()
        {
            // 测试 Md5 方法
            var md5Bytes = SecureUtil.Md5(Encoding.UTF8.GetBytes(TestData));
            Assert.NotNull(md5Bytes);
            Assert.Equal(16, md5Bytes.Length);

            var md5BytesFromString = SecureUtil.Md5(TestData);
            Assert.NotNull(md5BytesFromString);
            Assert.Equal(16, md5BytesFromString.Length);

            // 测试 Sha1 方法
            var sha1Bytes = SecureUtil.Sha1(Encoding.UTF8.GetBytes(TestData));
            Assert.NotNull(sha1Bytes);
            Assert.Equal(20, sha1Bytes.Length);

            // 测试 Sha256 方法
            var sha256Bytes = SecureUtil.Sha256(Encoding.UTF8.GetBytes(TestData));
            Assert.NotNull(sha256Bytes);
            Assert.Equal(32, sha256Bytes.Length);

            // 测试 Aes 方法
            var aesKey = KeyUtil.GenerateAesKey();
            var aes = SecureUtil.Aes(aesKey);
            Assert.NotNull(aes);

            // 测试 Rsa 方法
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair();
            var rsa = SecureUtil.Rsa(privateKey);
            Assert.NotNull(rsa);
        }

        /// <summary>
        /// 测试 KeyUtil 方法
        /// </summary>
        [Fact]
        public void TestKeyUtilMethods()
        {
            // 测试 GenerateSymmetricKey 方法
            var aesKey = KeyUtil.GenerateSymmetricKey(SymmetricAlgorithmType.AES);
            Assert.NotNull(aesKey);
            Assert.Equal(32, aesKey.Length); // 256位密钥

            var desKey = KeyUtil.GenerateSymmetricKey(SymmetricAlgorithmType.DES);
            Assert.NotNull(desKey);
            Assert.Equal(8, desKey.Length); // 64位密钥

            var desEdeKey = KeyUtil.GenerateSymmetricKey(SymmetricAlgorithmType.DESede);
            Assert.NotNull(desEdeKey);
            Assert.Equal(24, desEdeKey.Length); // 192位密钥

            // 测试 GenerateIV 方法
            var aesIv = KeyUtil.GenerateIV(SymmetricAlgorithmType.AES);
            Assert.NotNull(aesIv);
            Assert.Equal(16, aesIv.Length); // 128位IV

            var desIv = KeyUtil.GenerateIV(SymmetricAlgorithmType.DES);
            Assert.NotNull(desIv);
            Assert.Equal(8, desIv.Length); // 64位IV

            var desEdeIv = KeyUtil.GenerateIV(SymmetricAlgorithmType.DESede);
            Assert.NotNull(desEdeIv);
            Assert.Equal(8, desEdeIv.Length); // 64位IV
        }

        /// <summary>
        /// 测试 PemUtil 方法
        /// </summary>
        [Fact]
        public void TestPemUtilMethods()
        {
            // 生成RSA密钥对
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair();

            // 测试 WritePem 和 ReadPem 方法
            var rsa = new Asymmetric.RSA(publicKey, privateKey);
            var pemStr = PemUtil.WritePem(rsa);
            Assert.NotNull(pemStr);
            Assert.NotEmpty(pemStr);

            var obj = PemUtil.ReadPem(pemStr);
            Assert.NotNull(obj);

            // 测试 WritePemFile 和 ReadPemFile 方法
            var tempFile = Path.GetTempFileName() + ".pem";
            try
            {
                PemUtil.WritePemFile(rsa, tempFile);
                Assert.True(File.Exists(tempFile));

                var objFromFile = PemUtil.ReadPemFile(tempFile);
                Assert.NotNull(objFromFile);
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }
    }
}