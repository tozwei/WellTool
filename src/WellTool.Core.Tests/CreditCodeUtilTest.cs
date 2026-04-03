using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// CreditCode(统一社会信用代码)工具单元测试
    /// </summary>
    public class CreditCodeUtilTest
    {
        [Fact]
        public void IsValidTest()
        {
            // 有效的统一社会信用代码
            var validCode = "91110000MA01XXXXXX";
            Assert.True(CreditCodeUtil.IsValid(validCode));

            // 无效代码
            Assert.False(CreditCodeUtil.IsValid(null));
            Assert.False(CreditCodeUtil.IsValid(""));
            Assert.False(CreditCodeUtil.IsValid("123"));
            Assert.False(CreditCodeUtil.IsValid("91110000MA01XXXXX")); // 长度不对
        }

        [Fact]
        public void IsChineseTest()
        {
            // 18位代码
            var code = "91110000MA01XXXXXX";
            Assert.True(CreditCodeUtil.IsChinese(code));

            // 较短代码
            var shortCode = "MA01XXXXXX";
            Assert.False(CreditCodeUtil.IsChinese(shortCode));
        }

        [Fact]
        public void GetAddressCodeTest()
        {
            var code = "91110000MA01XXXXXX";
            var addressCode = CreditCodeUtil.GetAddressCode(code);
            Assert.Equal("911100", addressCode);
        }

        [Fact]
        public void GetOrgCodeTest()
        {
            var code = "91110000MA01XXXXXX";
            var orgCode = CreditCodeUtil.GetOrgCode(code);
            Assert.Equal("91110000", orgCode);
        }
    }
}
