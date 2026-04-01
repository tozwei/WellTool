using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// AES 加密测试
    /// </summary>
    public class AESTests
    {
        [Fact]
        public void EncryptCBCTest()
        {
            // AES CBC 模式加密测试
            var key = "1234567890123456".GetBytes();
            var iv = "1234567890123456".GetBytes();

            var aes = new AES(Mode.CBC, Padding.PKCS7Padding, key, iv);
            var encryptHex = aes.EncryptHex("123456");

            Assert.Equal("d637735ae9e21ba50cb686b74fab8d2c", encryptHex);
        }

        [Fact]
        public void EncryptCTSTest()
        {
            // AES CTS 模式加密测试
            var content = "test 中文";
            var key = "0CoJUm6Qyw8W8jue".GetBytes();
            var iv = "0102030405060708".GetBytes();

            var aes = new AES(Mode.CTS, Padding.PKCS7Padding, key, iv);
            var encryptHex = aes.EncryptHex(content);

            Assert.Equal("8dc9de7f050e86ca2c8261dde56dfec9", encryptHex);
        }

        [Fact]
        public void EncryptPKCS7Test()
        {
            // AES PKCS7Padding 加密测试
            var key = "1234567890123456".GetBytes();
            var iv = "1234567890123456".GetBytes();

            var aes = new AES(Mode.CBC, Padding.PKCS7Padding, key, iv);
            var encryptHex = aes.EncryptHex("123456");

            Assert.Equal("d637735ae9e21ba50cb686b74fab8d2c", encryptHex);
        }

        [Fact]
        public void EncryptECBWithHexDataTest()
        {
            // AES ECB 模式加密十六进制数据测试
            var key = HexStringToBytes("0102030405060708090a0b0c0d0e0f10");

            var aes = new AES(Mode.ECB, Padding.PKCS7Padding, key);

            // 加密十六进制数据
            var encryptHex = aes.EncryptHex(HexStringToBytes("16c5"));
            Assert.Equal("25869eb3ff227d9e34b3512d3c3c92ed", encryptHex);

            // 加密 Base64 数据
            var encryptBase64 = aes.EncryptBase64(HexStringToBytes("16c5"));
            Assert.Equal("JYaes/8ifZ40s1EtPDyS7Q==", encryptBase64);

            // 解密
            Assert.Equal("16c5", BytesToHexString(aes.Decrypt("25869eb3ff227d9e34b3512d3c3c92ed")));
        }

        [Fact]
        public void EncryptWithStringDataTest()
        {
            // AES 加密字符串数据测试
            var key = HexStringToBytes("0102030405060708090a0b0c0d0e0f10");

            var aes = new AES(Mode.ECB, Padding.PKCS7Padding, key);

            // 加密 UTF-8 字符串
            var encryptHex = aes.EncryptHex("16c5");
            Assert.Equal("79c210d3e304932cf9ea6a9c887c6d7c", encryptHex);

            var encryptBase64 = aes.EncryptBase64("16c5");
            Assert.Equal("ecIQ0+MEkyz56mqciHxtfA==", encryptBase64);

            // 解密
            Assert.Equal("16c5", aes.DecryptStr("79c210d3e304932cf9ea6a9c887c6d7c"));
        }

        [Fact]
        public void KeyGenerationTest()
        {
            // AES 密钥生成测试
            var key128 = AES.GenerateKey(128);
            Assert.NotNull(key128);
            Assert.Equal(16, key128.Length);

            var key192 = AES.GenerateKey(192);
            Assert.NotNull(key192);
            Assert.Equal(24, key192.Length);

            var key256 = AES.GenerateKey(256);
            Assert.NotNull(key256);
            Assert.Equal(32, key256.Length);
        }

        [Fact]
        public void IVGenerationTest()
        {
            // AES IV 生成测试
            var iv = AES.GenerateIV();
            Assert.NotNull(iv);
            Assert.Equal(16, iv.Length);
        }

        [Fact]
        public void DifferentModesTest()
        {
            // AES 不同模式测试
            var key = AES.GenerateKey();
            var plaintext = "Test different modes";

            // ECB 模式
            var cipherECB = AES.EncryptECB(key, plaintext);
            var plainECB = AES.DecryptECB(key, cipherECB);
            Assert.Equal(plaintext, plainECB);

            // CBC 模式
            var iv = AES.GenerateIV();
            var cipherCBC = AES.EncryptCBC(key, iv, plaintext);
            var plainCBC = AES.DecryptCBC(key, iv, cipherCBC);
            Assert.Equal(plaintext, plainCBC);

            // CFB 模式
            var cipherCFB = AES.EncryptCFB(key, iv, plaintext);
            var plainCFB = AES.DecryptCFB(key, iv, cipherCFB);
            Assert.Equal(plaintext, plainCFB);
        }

        [Fact]
        public void GCMModeTest()
        {
            // AES GCM 模式测试（认证加密）
            var key = AES.GenerateKey();
            var nonce = AES.GenerateNonce(); // GCM 使用 nonce 而非 IV
            var plaintext = "GCM authenticated encryption";

            var (ciphertext, tag) = AES.EncryptGCM(key, nonce, plaintext);
            var (decrypted, isValid) = AES.DecryptGCM(key, nonce, ciphertext, tag);

            Assert.Equal(plaintext, decrypted);
            Assert.True(isValid); // GCM 提供完整性验证
        }

        [Fact]
        public void LargeDataEncryptionTest()
        {
            // AES 大数据加密测试
            var key = AES.GenerateKey();
            var iv = AES.GenerateIV();
            var largeData = new string('A', 100000);

            var ciphertext = AES.EncryptCBC(key, iv, largeData);
            var decrypted = AES.DecryptCBC(key, iv, ciphertext);

            Assert.Equal(largeData, decrypted);
        }

        private static byte[] HexStringToBytes(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        private static string BytesToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
