using System.Text;
using Xunit;
using WellTool.Crypto.Asymmetric;

namespace WellTool.Crypto.Tests.Asymmetric
{
    public class SignTests
    {
        [Fact]
        public void CreateTest()
        {
            // 测试创建 Sign 对象
            var sign = Sign.Create(SignAlgorithm.RSA_SHA256);
            Assert.NotNull(sign);
        }

        [Fact]
        public void CreateWithAsymmetricAlgorithmTest()
        {
            // 测试使用 AsymmetricAlgorithm 创建 Sign 对象
            var sign = Sign.Create(AsymmetricAlgorithm.RSA);
            Assert.NotNull(sign);
        }
    }
}