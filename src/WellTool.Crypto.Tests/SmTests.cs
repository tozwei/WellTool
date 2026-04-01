using WellTool.Crypto;
using Xunit;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// 国密综合测试（SM1/SM2/SM3/SM4）
    /// </summary>
    public class SmTests
    {
        [Fact]
        public void SM2KeyPairGenerationTest()
        {
            // SM2 密钥对生成
            var keyPair = SM2.GenerateKeyPair();

            Assert.NotNull(keyPair);
            Assert.NotNull(keyPair.PrivateKey);
            Assert.NotNull(keyPair.PublicKey);
        }

        [Fact]
        public void SM2EncryptAndDecryptTest()
        {
            // SM2 加密解密
            var keyPair = SM2.GenerateKeyPair();
            var plaintext = "Hello SM2!";

            var ciphertext = SM2.Encrypt(keyPair.PublicKey, plaintext);
            var decrypted = SM2.Decrypt(keyPair.PrivateKey, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void SM3HashTest()
        {
            // SM3 哈希算法
            var input = "Hello SM3!";
            var hash = SM3.Hash(input);

            Assert.NotNull(hash);
            Assert.Equal(64, hash.Length); // SM3 输出 256 位（64 字符十六进制）
        }

        [Fact]
        public void SM3DeterministicTest()
        {
            // SM3 确定性测试
            var input = "test input";
            var hash1 = SM3.Hash(input);
            var hash2 = SM3.Hash(input);

            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void SM3ChineseTextTest()
        {
            // SM3 中文文本测试
            var chinese = "你好，SM3 国密哈希！";
            var hash = SM3.Hash(chinese);

            Assert.NotNull(hash);
            Assert.Equal(64, hash.Length);
        }

        [Fact]
        public void SM4KeyGenerationTest()
        {
            // SM4 密钥生成
            var key = SM4.GenerateKey();

            Assert.NotNull(key);
            Assert.Equal(16, key.Length); // SM4 密钥 128 位（16 字节）
        }

        [Fact]
        public void SM4EncryptAndDecryptTest()
        {
            // SM4 加密解密
            var key = SM4.GenerateKey();
            var plaintext = "Hello SM4!";

            var ciphertext = SM4.Encrypt(key, plaintext);
            var decrypted = SM4.Decrypt(key, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void SM4CBCModeTest()
        {
            // SM4 CBC 模式
            var key = SM4.GenerateKey();
            var iv = SM4.GenerateIV();
            var plaintext = "CBC mode test";

            var ciphertext = SM4.EncryptCBC(key, iv, plaintext);
            var decrypted = SM4.DecryptCBC(key, iv, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void SMComprehensiveTest()
        {
            // 国密综合测试 - 完整流程
            // 1. SM2 生成密钥对
            var sm2KeyPair = SM2.GenerateKeyPair();

            // 2. SM3 计算数据哈希
            var data = "Sensitive data to protect";
            var hash = SM3.Hash(data);

            // 3. SM2 签名哈希
            var signature = SM2.Sign(sm2KeyPair.PrivateKey, hash);

            // 4. SM4 加密原始数据
            var sm4Key = SM4.GenerateKey();
            var encryptedData = SM4.Encrypt(sm4Key, data);

            // 验证
            Assert.NotNull(sm2KeyPair);
            Assert.NotNull(hash);
            Assert.NotNull(signature);
            Assert.NotNull(encryptedData);

            // SM2 验签
            var isValid = SM2.Verify(sm2KeyPair.PublicKey, hash, signature);
            Assert.True(isValid);

            // SM4 解密
            var decryptedData = SM4.Decrypt(sm4Key, encryptedData);
            Assert.Equal(data, decryptedData);
        }
    }
}
