using System.Text;
using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// 对称加密通用测试
    /// </summary>
    public class SymmetricTests
    {
        [Fact]
        public void AESTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var aes = new AES(key, iv);

            var plaintext = "Hello, AES!";
            var encrypted = aes.EncryptHex(plaintext);
            var decrypted = aes.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void DESTest()
        {
            var key = Encoding.UTF8.GetBytes("12345678");
            var iv = Encoding.UTF8.GetBytes("12345678");
            var des = new DES(key, iv);

            var plaintext = "Hello, DES!";
            var encrypted = des.EncryptHex(plaintext);
            var decrypted = des.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void DESedeTest()
        {
            var key = Encoding.UTF8.GetBytes("123456789012345678901234");
            var iv = Encoding.UTF8.GetBytes("12345678");
            var desede = new DESede(key, iv);

            var plaintext = "Hello, DESede!";
            var encrypted = desede.EncryptHex(plaintext);
            var decrypted = desede.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void SM4Test()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var sm4 = new SM4(key, iv);

            var plaintext = "Hello, SM4!";
            var encrypted = sm4.EncryptHex(plaintext);
            var decrypted = sm4.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void RC4Test()
        {
            var key = Encoding.UTF8.GetBytes("1234567890");
            var rc4 = new RC4(key);

            var plaintext = "Hello, RC4!";
            var encrypted = rc4.EncryptHex(plaintext);
            var decrypted = rc4.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void ChaCha20Test()
        {
            var key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            var nonce = Encoding.UTF8.GetBytes("123456789012");
            var plaintext = "Hello, ChaCha20!";

            // 加密
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            var encrypted = ChaCha20.Encrypt(plaintextBytes, key, nonce);

            // 解密
            var decrypted = ChaCha20.Decrypt(encrypted, key, nonce);
            var decryptedText = Encoding.UTF8.GetString(decrypted);

            Assert.Equal(plaintext, decryptedText);
        }

        [Fact]
        public void VigenereTest()
        {
            var key = "KEY";
            var vigenere = new Vigenere(key);

            var plaintext = "Hello, Vigenere!";
            var encrypted = vigenere.Encrypt(plaintext);
            var decrypted = vigenere.Decrypt(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void XXTEATest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var xxtea = new XXTEA(key);

            var plaintext = "Hello, XXTEA!";
            var encrypted = xxtea.EncryptHex(plaintext);
            var decrypted = xxtea.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }
    }
}
