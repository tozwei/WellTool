using System;
using Xunit;
using WellTool.Crypto.Asymmetric;

namespace WellTool.Crypto.Tests.Asymmetric
{
    /// <summary>
    /// 国密 SM2 椭圆曲线加密测试
    /// </summary>
    public class SM2Tests
    {
        [Fact]
        public void ConstructorTest()
        {
            // 测试无参构造函数
            var sm2 = new SM2();
            Assert.NotNull(sm2);
        }

        [Fact]
        public void ConstructorWithKeysTest()
        {
            // 测试带密钥的构造函数
            var publicKey = new byte[65]; // 模拟公钥
            var privateKey = new byte[32]; // 模拟私钥
            var sm2 = new SM2(publicKey, privateKey);
            Assert.NotNull(sm2);
        }

        [Fact]
        public void ConstructorWithBase64KeysTest()
        {
            // 测试带 Base64 编码密钥的构造函数
            var publicKey = Convert.ToBase64String(new byte[65]); // 模拟公钥
            var privateKey = Convert.ToBase64String(new byte[32]); // 模拟私钥
            var sm2 = new SM2(publicKey, privateKey);
            Assert.NotNull(sm2);
        }
    }
}
