using System;
using Xunit;
using WellTool.Crypto;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

namespace WellTool.Crypto.Tests
{
    public class OpensslKeyUtilTests
    {
        [Fact]
        public void ReadWritePrivateKeyTest()
        {
            // 生成 RSA 密钥对
            var keyPair = GenerateRsaKeyPair();

            // 写入私钥为 PEM 格式
            var privateKeyPem = OpensslKeyUtil.WritePrivateKey(keyPair);
            Assert.NotNull(privateKeyPem);
            Assert.NotEmpty(privateKeyPem);

            // 读取私钥
            var readKeyPair = OpensslKeyUtil.ReadPrivateKey(privateKeyPem);
            Assert.NotNull(readKeyPair);
            Assert.NotNull(readKeyPair.Private);
            Assert.NotNull(readKeyPair.Public);
        }

        [Fact]
        public void ReadWritePublicKeyTest()
        {
            // 生成 RSA 密钥对
            var keyPair = GenerateRsaKeyPair();

            // 写入公钥为 PEM 格式
            var publicKeyPem = OpensslKeyUtil.WritePublicKey(keyPair.Public);
            Assert.NotNull(publicKeyPem);
            Assert.NotEmpty(publicKeyPem);

            // 读取公钥
            var readPublicKey = OpensslKeyUtil.ReadPublicKey(publicKeyPem);
            Assert.NotNull(readPublicKey);
        }

        private AsymmetricCipherKeyPair GenerateRsaKeyPair()
        {
            var generator = new RsaKeyPairGenerator();
            generator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            return generator.GenerateKeyPair();
        }
    }
}