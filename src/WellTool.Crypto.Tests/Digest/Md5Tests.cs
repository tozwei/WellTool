using WellTool.Crypto.Digest;
using Xunit;

namespace WellTool.Crypto.Tests.Digest
{
    /// <summary>
    /// MD5 哈希算法测试
    /// </summary>
    public class Md5Tests
    {
        [Fact]
        public void Md5HashTest()
        {
            // MD5 哈希
            var input = "Hello MD5!";
            var hash = MD5.Hash(input);

            Assert.NotNull(hash);
            Assert.Equal(32, hash.Length); // MD5 输出 32 位十六进制字符串
        }

        [Fact]
        public void Md5WithSaltTest()
        {
            // 加盐 MD5
            var input = "password";
            var salt = "randomsalt";
            var hash = MD5.HashWithSalt(input, salt);

            Assert.NotNull(hash);
            Assert.NotEqual(MD5.Hash(input), hash);
        }

        [Fact]
        public void Md5DeterministicTest()
        {
            // MD5 确定性测试 - 相同输入产生相同输出
            var input = "test input";
            var hash1 = MD5.Hash(input);
            var hash2 = MD5.Hash(input);

            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void Md5DifferentInputTest()
        {
            // 不同输入产生不同输出
            var input1 = "input1";
            var input2 = "input2";

            var hash1 = MD5.Hash(input1);
            var hash2 = MD5.Hash(input2);

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void Md5EmptyStringTest()
        {
            // 空字符串 MD5
            var empty = "";
            var hash = MD5.Hash(empty);

            Assert.NotNull(hash);
            // MD5("") = d41d8cd98f00b204e9800998ecf8427e
            Assert.Equal("d41d8cd98f00b204e9800998ecf8427e", hash);
        }

        [Fact]
        public void Md5ChineseTextTest()
        {
            // 中文文本 MD5
            var chinese = "你好，世界！";
            var hash = MD5.Hash(chinese);

            Assert.NotNull(hash);
            Assert.Equal(32, hash.Length);
        }

        [Fact]
        public void Md5LargeDataTest()
        {
            // 大数据 MD5
            var largeData = new string('A', 10000);
            var hash = MD5.Hash(largeData);

            Assert.NotNull(hash);
            Assert.Equal(32, hash.Length);
        }

        [Fact]
        public void Md5HexFormatTest()
        {
            // MD5 十六进制格式测试
            var input = "test";
            var hashHex = MD5.HashHex(input);

            Assert.NotNull(hashHex);
            Assert.Equal(32, hashHex.Length);
            // 验证都是十六进制字符
            foreach (var c in hashHex)
            {
                Assert.Contains(c, "0123456789abcdef");
            }
        }

        [Fact]
        public void Md5BytesFormatTest()
        {
            // MD5 字节数组格式测试
            var input = "test";
            var hashBytes = MD5.HashBytes(input);

            Assert.NotNull(hashBytes);
            Assert.Equal(16, hashBytes.Length); // MD5 输出 16 字节
        }

        [Fact]
        public void Md5KnownValueTest()
        {
            // 已知值测试
            var input = "hello world";
            var hash = MD5.Hash(input);

            // MD5("hello world") = 5eb63bbbe01eeed093cb22bb8f5acdc3
            Assert.Equal("5eb63bbbe01eeed093cb22bb8f5acdc3", hash);
        }
    }
}
