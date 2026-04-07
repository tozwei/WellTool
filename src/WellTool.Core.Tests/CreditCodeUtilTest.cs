using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// CreditCode(统一社会信用代码)工具单元测试
    /// </summary>
    public class CreditCodeUtilTest
    {
        // 有效统一社会信用代码
        private const string ValidCode = "91110000100000000X";
        private const string InvalidCode = "911100001000000000";

        [Fact]
        public void IsValidTest()
        {
            // 测试有效信用代码
            Assert.True(CreditCodeUtil.IsValid(ValidCode));
            
            // 测试无效信用代码
            Assert.False(CreditCodeUtil.IsValid(null));
            Assert.False(CreditCodeUtil.IsValid(""));
            Assert.False(CreditCodeUtil.IsValid("123456789012345678"));
            Assert.False(CreditCodeUtil.IsValid(InvalidCode));
        }

        [Fact]
        public void GenerateTest()
        {
            var code = CreditCodeUtil.Generate("10000000");
            Assert.NotNull(code);
            Assert.Equal(18, code.Length);
            Assert.True(code.StartsWith("9"));
        }
    }
}
