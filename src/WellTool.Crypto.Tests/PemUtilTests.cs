using System.IO;
using System.Text;
using Xunit;
using WellTool.Crypto;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

namespace WellTool.Crypto.Tests
{
    public class PemUtilTests
    {
        [Fact]
        public void ReadWritePemTest()
        {
            // 生成 RSA 密钥对
            var generator = new RsaKeyPairGenerator();
            generator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            var keyPair = generator.GenerateKeyPair();

            // 写入私钥为 PEM 字符串
            var privateKeyPem = PemUtil.WritePem(keyPair.Private);
            Assert.NotNull(privateKeyPem);
            Assert.NotEmpty(privateKeyPem);

            // 读取私钥
            var readPrivateKey = PemUtil.ReadPem(privateKeyPem);
            Assert.NotNull(readPrivateKey);

            // 写入公钥为 PEM 字符串
            var publicKeyPem = PemUtil.WritePem(keyPair.Public);
            Assert.NotNull(publicKeyPem);
            Assert.NotEmpty(publicKeyPem);

            // 读取公钥
            var readPublicKey = PemUtil.ReadPem(publicKeyPem);
            Assert.NotNull(readPublicKey);
        }

        [Fact]
        public void ReadWritePemFileTest()
        {
            // 生成 RSA 密钥对
            var generator = new RsaKeyPairGenerator();
            generator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            var keyPair = generator.GenerateKeyPair();

            // 创建临时文件
            var privateKeyPath = Path.GetTempFileName();
            var publicKeyPath = Path.GetTempFileName();

            try
            {
                // 写入私钥为 PEM 文件
                PemUtil.WritePemFile(keyPair.Private, privateKeyPath);
                Assert.True(File.Exists(privateKeyPath));

                // 读取私钥
                var readPrivateKey = PemUtil.ReadPemFile(privateKeyPath);
                Assert.NotNull(readPrivateKey);

                // 写入公钥为 PEM 文件
                PemUtil.WritePemFile(keyPair.Public, publicKeyPath);
                Assert.True(File.Exists(publicKeyPath));

                // 读取公钥
                var readPublicKey = PemUtil.ReadPemFile(publicKeyPath);
                Assert.NotNull(readPublicKey);
            }
            finally
            {
                // 清理临时文件
                if (File.Exists(privateKeyPath))
                {
                    File.Delete(privateKeyPath);
                }
                if (File.Exists(publicKeyPath))
                {
                    File.Delete(publicKeyPath);
                }
            }
        }
    }
}