using System.Text;
using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// RC4 流加密测试
    /// </summary>
    public class RC4Tests
    {
        [Fact]
        public void EncryptDecryptTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890");
            var rc4 = new RC4(key);

            var plaintext = "Hello, RC4!";
            var encrypted = rc4.EncryptHex(plaintext);
            var decrypted = rc4.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void EncryptDecryptStringTest()
        {
            var key = "1234567890";
            var rc4 = new RC4(key);

            var plaintext = "Hello, RC4!";
            var encrypted = rc4.EncryptString(plaintext);
            var decrypted = rc4.DecryptString(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void ResetTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890");
            var rc4 = new RC4(key);

            var plaintext = "Hello, RC4!";
            var encrypted1 = rc4.EncryptHex(plaintext);
            rc4.Reset();
            var encrypted2 = rc4.EncryptHex(plaintext);

            Assert.Equal(encrypted1, encrypted2);
        }
    }
}
