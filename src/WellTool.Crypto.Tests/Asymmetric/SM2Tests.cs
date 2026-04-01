using WellTool.Crypto.Asymmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Asymmetric
{
    /// <summary>
    /// 国密 SM2 椭圆曲线加密测试
    /// </summary>
    public class SM2Tests
    {
        [Fact]
        public void KeyPairGenerationTest()
        {
            // 生成 SM2 密钥对
            var keyPair = SM2.GenerateKeyPair();

            Assert.NotNull(keyPair);
            Assert.NotNull(keyPair.PrivateKey);
            Assert.NotNull(keyPair.PublicKey);
        }

        [Fact]
        public void EncryptAndDecryptTest()
        {
            // SM2 加密解密
            var keyPair = SM2.GenerateKeyPair();
            var plaintext = "Hello SM2!";

            var ciphertext = SM2.Encrypt(keyPair.PublicKey, plaintext);
            var decrypted = SM2.Decrypt(keyPair.PrivateKey, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void SignAndVerifyTest()
        {
            // SM2 签名验签
            var keyPair = SM2.GenerateKeyPair();
            var data = "Data to sign";

            var signature = SM2.Sign(keyPair.PrivateKey, data);
            var isValid = SM2.Verify(keyPair.PublicKey, data, signature);

            Assert.True(isValid);
        }

        [Fact]
        public void EncryptWithDifferentModesTest()
        {
            // 测试不同的加密模式
            var keyPair = SM2.GenerateKeyPair();
            var plaintext = "Test different modes";

            // C1C3C2 模式（SM2 标准）
            var ciphertext1 = SM2.Encrypt(keyPair.PublicKey, plaintext, AsymmetricCipherMode.C1C3C2);
            var decrypted1 = SM2.Decrypt(keyPair.PrivateKey, ciphertext1);
            Assert.Equal(plaintext, decrypted1);

            // C1C2C3 模式（旧标准）
            var ciphertext2 = SM2.Encrypt(keyPair.PublicKey, plaintext, AsymmetricCipherMode.C1C2C3);
            var decrypted2 = SM2.Decrypt(keyPair.PrivateKey, ciphertext2);
            Assert.Equal(plaintext, decrypted2);
        }

        [Fact]
        public void LargeDataEncryptTest()
        {
            // 大数据加密测试
            var keyPair = SM2.GenerateKeyPair();
            var largeData = new string('A', 1000);

            var ciphertext = SM2.Encrypt(keyPair.PublicKey, largeData);
            var decrypted = SM2.Decrypt(keyPair.PrivateKey, ciphertext);

            Assert.Equal(largeData, decrypted);
        }

        [Fact]
        public void EmptyDataTest()
        {
            // 空数据测试
            var keyPair = SM2.GenerateKeyPair();
            var emptyData = "";

            var ciphertext = SM2.Encrypt(keyPair.PublicKey, emptyData);
            var decrypted = SM2.Decrypt(keyPair.PrivateKey, ciphertext);

            Assert.Equal(emptyData, decrypted);
        }

        [Fact]
        public void ChineseTextEncryptTest()
        {
            // 中文文本加密测试
            var keyPair = SM2.GenerateKeyPair();
            var chineseText = "你好，SM2 国密算法！";

            var ciphertext = SM2.Encrypt(keyPair.PublicKey, chineseText);
            var decrypted = SM2.Decrypt(keyPair.PrivateKey, ciphertext);

            Assert.Equal(chineseText, decrypted);
        }

        [Fact]
        public void VerifyFailureWithTamperedSignatureTest()
        {
            // 签名被篡改时验证失败
            var keyPair = SM2.GenerateKeyPair();
            var data = "Original data";

            var signature = SM2.Sign(keyPair.PrivateKey, data);

            // 篡改签名
            var tamperedSignature = TamperSignature(signature);

            var isValid = SM2.Verify(keyPair.PublicKey, data, tamperedSignature);
            Assert.False(isValid);
        }

        [Fact]
        public void VerifyFailureWithWrongDataTest()
        {
            // 数据不匹配时验证失败
            var keyPair = SM2.GenerateKeyPair();
            var originalData = "Original data";
            var wrongData = "Wrong data";

            var signature = SM2.Sign(keyPair.PrivateKey, originalData);
            var isValid = SM2.Verify(keyPair.PublicKey, wrongData, signature);

            Assert.False(isValid);
        }

        [Fact]
        public void VerifyFailureWithWrongKeyTest()
        {
            // 使用错误的公钥验证失败
            var keyPair1 = SM2.GenerateKeyPair();
            var keyPair2 = SM2.GenerateKeyPair();
            var data = "Test data";

            var signature = SM2.Sign(keyPair1.PrivateKey, data);

            // 使用另一个密钥对的公钥验证
            var isValid = SM2.Verify(keyPair2.PublicKey, data, signature);

            Assert.False(isValid);
        }

        private static byte[] TamperSignature(byte[] signature)
        {
            var tampered = new byte[signature.Length];
            Array.Copy(signature, tampered, signature.Length);
            if (tampered.Length > 0)
            {
                tampered[0] ^= 0xFF; // 翻转第一个字节
            }
            return tampered;
        }
    }
}
