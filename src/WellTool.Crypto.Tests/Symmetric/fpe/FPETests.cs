using Org.BouncyCastle.Crypto.Utilities;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric.FPE
{
    /// <summary>
    /// 格式保留加密测试
    /// </summary>
    public class FPETests
    {
        [Fact]
        public void FF1Test()
        {
            // FPE FF1 模式测试
            var numberMapper = new BasicAlphabetMapper("A0123456789");
            var keyBytes = RandomUtil.RandomBytes(16);

            var fpe = new FPE(FPE.FPEMode.FF1, keyBytes, numberMapper, null);

            // 原始数据
            var phone = RandomUtil.RandomString("A0123456789", 13);
            var encrypted = fpe.Encrypt(phone);

            // 加密后与原密文长度一致
            Assert.Equal(phone.Length, encrypted.Length);

            var decrypted = fpe.Decrypt(encrypted);
            Assert.Equal(phone, decrypted);
        }

        [Fact]
        public void FF3Test()
        {
            // FPE FF3_1 模式测试
            var numberMapper = new BasicAlphabetMapper("A0123456789");
            var keyBytes = RandomUtil.RandomBytes(16);

            var fpe = new FPE(FPE.FPEMode.FF3_1, keyBytes, numberMapper, null);

            // 原始数据
            var phone = RandomUtil.RandomString("A0123456789", 13);
            var encrypted = fpe.Encrypt(phone);

            // 加密后与原密文长度一致
            Assert.Equal(phone.Length, encrypted.Length);

            var decrypted = fpe.Decrypt(encrypted);
            Assert.Equal(phone, decrypted);
        }

        [Fact]
        public void DifferentKeySizesTest()
        {
            // FPE 不同密钥长度测试
            var numberMapper = new BasicAlphabetMapper("0123456789");

            // 16 字节密钥（128 位）
            var key16 = RandomUtil.RandomBytes(16);
            var fpe16 = new FPE(FPE.FPEMode.FF1, key16, numberMapper, null);
            var data = "1234567890";
            var encrypted16 = fpe16.Encrypt(data);
            Assert.Equal(data.Length, encrypted16.Length);
            Assert.Equal(data, fpe16.Decrypt(encrypted16));

            // 24 字节密钥（192 位）
            var key24 = RandomUtil.RandomBytes(24);
            var fpe24 = new FPE(FPE.FPEMode.FF1, key24, numberMapper, null);
            var encrypted24 = fpe24.Encrypt(data);
            Assert.Equal(data.Length, encrypted24.Length);
            Assert.Equal(data, fpe24.Decrypt(encrypted24));

            // 32 字节密钥（256 位）
            var key32 = RandomUtil.RandomBytes(32);
            var fpe32 = new FPE(FPE.FPEMode.FF1, key32, numberMapper, null);
            var encrypted32 = fpe32.Encrypt(data);
            Assert.Equal(data.Length, encrypted32.Length);
            Assert.Equal(data, fpe32.Decrypt(encrypted32));
        }

        [Fact]
        public void DifferentAlphabetsTest()
        {
            // FPE 不同字符集测试
            var key = RandomUtil.RandomBytes(16);

            // 数字字符集
            var numberMapper = new BasicAlphabetMapper("0123456789");
            var fpeNumber = new FPE(FPE.FPEMode.FF1, key, numberMapper, null);
            var numberData = "1234567890";
            var encryptedNumber = fpeNumber.Encrypt(numberData);
            Assert.Equal(numberData.Length, encryptedNumber.Length);
            Assert.Equal(numberData, fpeNumber.Decrypt(encryptedNumber));

            // 十六进制字符集
            var hexMapper = new BasicAlphabetMapper("0123456789ABCDEF");
            var fpeHex = new FPE(FPE.FPEMode.FF1, key, hexMapper, null);
            var hexData = "ABCD1234";
            var encryptedHex = fpeHex.Encrypt(hexData);
            Assert.Equal(hexData.Length, encryptedHex.Length);
            Assert.Equal(hexData, fpeHex.Decrypt(encryptedHex));

            // 大写字母 + 数字
            var upperMapper = new BasicAlphabetMapper("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
            var fpeUpper = new FPE(FPE.FPEMode.FF1, key, upperMapper, null);
            var upperData = "ABC123";
            var encryptedUpper = fpeUpper.Encrypt(upperData);
            Assert.Equal(upperData.Length, encryptedUpper.Length);
            Assert.Equal(upperData, fpeUpper.Decrypt(encryptedUpper));
        }

        [Fact]
        public void ShortDataTest()
        {
            // FPE 短数据测试
            var numberMapper = new BasicAlphabetMapper("0123456789");
            var key = RandomUtil.RandomBytes(16);
            var fpe = new FPE(FPE.FPEMode.FF1, key, numberMapper, null);

            // 单个字符
            var single = "1";
            var encryptedSingle = fpe.Encrypt(single);
            Assert.Equal(1, encryptedSingle.Length);
            Assert.Equal(single, fpe.Decrypt(encryptedSingle));

            // 两个字符
            var two = "12";
            var encryptedTwo = fpe.Encrypt(two);
            Assert.Equal(2, encryptedTwo.Length);
            Assert.Equal(two, fpe.Decrypt(encryptedTwo));
        }

        [Fact]
        public void LongDataTest()
        {
            // FPE 长数据测试
            var numberMapper = new BasicAlphabetMapper("0123456789");
            var key = RandomUtil.RandomBytes(16);
            var fpe = new FPE(FPE.FPEMode.FF1, key, numberMapper, null);

            var longData = RandomUtil.RandomString("0123456789", 100);
            var encrypted = fpe.Encrypt(longData);

            Assert.Equal(longData.Length, encrypted.Length);
            Assert.Equal(longData, fpe.Decrypt(encrypted));
        }

        [Fact]
        public void DeterministicEncryptionTest()
        {
            // FPE 确定性加密测试
            var numberMapper = new BasicAlphabetMapper("0123456789");
            var key = RandomUtil.RandomBytes(16);
            var fpe = new FPE(FPE.FPEMode.FF1, key, numberMapper, null);

            var data = "1234567890";
            var encrypted1 = fpe.Encrypt(data);
            var encrypted2 = fpe.Encrypt(data);

            // 相同的密钥和数据应该产生相同的密文
            Assert.Equal(encrypted1, encrypted2);
        }

        [Fact]
        public void DifferentKeysProduceDifferentCiphertextTest()
        {
            // FPE 不同密钥产生不同密文测试
            var numberMapper = new BasicAlphabetMapper("0123456789");
            var data = "1234567890";

            var key1 = RandomUtil.RandomBytes(16);
            var fpe1 = new FPE(FPE.FPEMode.FF1, key1, numberMapper, null);
            var encrypted1 = fpe1.Encrypt(data);

            var key2 = RandomUtil.RandomBytes(16);
            var fpe2 = new FPE(FPE.FPEMode.FF1, key2, numberMapper, null);
            var encrypted2 = fpe2.Encrypt(data);

            // 不同的密钥应该产生不同的密文
            Assert.NotEqual(encrypted1, encrypted2);
        }
    }
}
