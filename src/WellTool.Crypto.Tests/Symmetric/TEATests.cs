using System.Text;
using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// TEA 加密算法测试
    /// </summary>
    public class TEATests
    {
        [Fact]
        public void EncryptDecryptTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes key
            var tea = new TEA(key);

            var plaintext = "Hello, TEA!";
            var encrypted = tea.EncryptHex(plaintext);
            var decrypted = tea.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void StaticEncryptDecryptTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes key
            var plaintext = Encoding.UTF8.GetBytes("Hello, TEA!");

            var encrypted = TEA.Encrypt(plaintext, key);
            var decrypted = TEA.Decrypt(encrypted, key);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void EncryptDecryptWithPaddingTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes key
            var tea = new TEA(key);

            // Test with different lengths to ensure padding works
            string[] testStrings = { "", "a", "ab", "abc", "abcd", "abcde", "abcdef", "abcdefg", "abcdefgh" };

            foreach (var testString in testStrings)
            {
                var encrypted = tea.EncryptHex(testString);
                var decrypted = tea.DecryptStr(encrypted);
                Assert.Equal(testString, decrypted);
            }
        }
    }
}
