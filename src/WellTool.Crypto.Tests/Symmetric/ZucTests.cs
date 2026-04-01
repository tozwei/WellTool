using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// 祖冲之算法测试（中国商用密码流加密算法）
    /// </summary>
    public class ZucTests
    {
        [Fact]
        public void ZUC128Test()
        {
            // ZUC-128 算法测试
            var secretKey = ZUC.GenerateKey(ZUC.ZUCAlgorithm.ZUC_128);
            var iv = RandomUtil.RandomBytes(16);

            var zuc = new ZUC(ZUC.ZUCAlgorithm.ZUC_128, secretKey, iv);

            var msg = RandomUtil.RandomString(500);
            var encrypted = zuc.Encrypt(msg);
            var decrypted = zuc.DecryptStr(encrypted, System.Text.Encoding.UTF8);

            Assert.Equal(msg, decrypted);
        }

        [Fact]
        public void ZUC256Test()
        {
            // ZUC-256 算法测试
            var secretKey = ZUC.GenerateKey(ZUC.ZUCAlgorithm.ZUC_256);
            var iv = RandomUtil.RandomBytes(25);

            var zuc = new ZUC(ZUC.ZUCAlgorithm.ZUC_256, secretKey, iv);

            var msg = RandomUtil.RandomString(500);
            var encrypted = zuc.Encrypt(msg);
            var decrypted = zuc.DecryptStr(encrypted, System.Text.Encoding.UTF8);

            Assert.Equal(msg, decrypted);
        }

        [Fact]
        public void KeySizeTest()
        {
            // ZUC 密钥大小测试
            var key128 = ZUC.GenerateKey(ZUC.ZUCAlgorithm.ZUC_128);
            Assert.NotNull(key128);
            Assert.Equal(16, key128.Length); // 128 位=16 字节

            var key256 = ZUC.GenerateKey(ZUC.ZUCAlgorithm.ZUC_256);
            Assert.NotNull(key256);
            Assert.Equal(32, key256.Length); // 256 位=32 字节
        }

        [Fact]
        public void IVSizeTest()
        {
            // ZUC IV 大小测试
            var key128 = ZUC.GenerateKey(ZUC.ZUCAlgorithm.ZUC_128);
            var iv128 = RandomUtil.RandomBytes(16);
            var zuc128 = new ZUC(ZUC.ZUCAlgorithm.ZUC_128, key128, iv128);
            Assert.NotNull(zuc128);

            var key256 = ZUC.GenerateKey(ZUC.ZUCAlgorithm.ZUC_256);
            var iv256 = RandomUtil.RandomBytes(25);
            var zuc256 = new ZUC(ZUC.ZUCAlgorithm.ZUC_256, key256, iv256);
            Assert.NotNull(zuc256);
        }

        [Fact]
        public void StreamCipherPropertyTest()
        {
            // ZUC 流加密特性测试
            var key = ZUC.GenerateKey(ZUC.ZUCAlgorithm.ZUC_128);
            var iv = RandomUtil.RandomBytes(16);
            var zuc = new ZUC(ZUC.ZUCAlgorithm.ZUC_128, key, iv);

            // 流加密可以逐字节处理
            var byteData = new byte[] { 0x41, 0x42, 0x43 };
            var encrypted = zuc.EncryptBytes(byteData);
            var decrypted = zuc.DecryptBytes(encrypted);

            Assert.Equal(byteData, decrypted);
        }

        [Fact]
        public void ChineseTextTest()
        {
            // ZUC 中文文本加密测试
            var key = ZUC.GenerateKey(ZUC.ZUCAlgorithm.ZUC_128);
            var iv = RandomUtil.RandomBytes(16);
            var zuc = new ZUC(ZUC.ZUCAlgorithm.ZUC_128, key, iv);

            var chineseText = "你好，祖冲之算法！";
            var encrypted = zuc.Encrypt(chineseText);
            var decrypted = zuc.DecryptStr(encrypted, System.Text.Encoding.UTF8);

            Assert.Equal(chineseText, decrypted);
        }

        [Fact]
        public void EmptyDataTest()
        {
            // ZUC 空数据测试
            var key = ZUC.GenerateKey(ZUC.ZUCAlgorithm.ZUC_128);
            var iv = RandomUtil.RandomBytes(16);
            var zuc = new ZUC(ZUC.ZUCAlgorithm.ZUC_128, key, iv);

            var emptyData = "";
            var encrypted = zuc.Encrypt(emptyData);
            var decrypted = zuc.DecryptStr(encrypted, System.Text.Encoding.UTF8);

            Assert.Equal(emptyData, decrypted);
        }

        [Fact]
        public void LargeDataTest()
        {
            // ZUC 大数据测试
            var key = ZUC.GenerateKey(ZUC.ZUCAlgorithm.ZUC_128);
            var iv = RandomUtil.RandomBytes(16);
            var zuc = new ZUC(ZUC.ZUCAlgorithm.ZUC_128, key, iv);

            var largeData = new string('A', 10000);
            var encrypted = zuc.Encrypt(largeData);
            var decrypted = zuc.DecryptStr(encrypted, System.Text.Encoding.UTF8);

            Assert.Equal(largeData, decrypted);
        }
    }
}
