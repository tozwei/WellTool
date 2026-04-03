using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Phone工具单元测试
    /// </summary>
    public class PhoneUtilTest
    {
        [Fact]
        public void IsValidTest()
        {
            // 有效手机号
            Assert.True(PhoneUtil.IsValid("13812345678"));
            Assert.True(PhoneUtil.IsValid("+8613912345678"));

            // 无效手机号
            Assert.False(PhoneUtil.IsValid("12345678901")); // 长度不对
            Assert.False(PhoneUtil.IsValid("1381234567")); // 长度不对
            Assert.False(PhoneUtil.IsValid(""));
            Assert.False(PhoneUtil.IsValid(null));
        }

        [Fact]
        public void IsValidSimpleTest()
        {
            // 简单验证，只检查11位数字
            Assert.True(PhoneUtil.IsValidSimple("13812345678"));
            Assert.False(PhoneUtil.IsValidSimple("123"));
        }

        [Fact]
        public void IsMobileTest()
        {
            // 中国移动
            Assert.True(PhoneUtil.IsMobile("13812345678"));
            Assert.True(PhoneUtil.IsMobile("15012345678"));
            Assert.True(PhoneUtil.IsMobile("15212345678"));

            // 中国联通
            Assert.True(PhoneUtil.IsMobile("18612345678"));
            Assert.True(PhoneUtil.IsMobile("15612345678"));

            // 中国电信
            Assert.True(PhoneUtil.IsMobile("19912345678"));
            Assert.True(PhoneUtil.IsMobile("18012345678"));

            // 无效号码
            Assert.False(PhoneUtil.IsMobile("10012345678"));
        }

        [Fact]
        public void HideTest()
        {
            Assert.Equal("138****5678", PhoneUtil.Hide("13812345678"));
            Assert.Equal("****5678", PhoneUtil.Hide("13812345678", 0, 3));
            Assert.Equal("138****", PhoneUtil.Hide("13812345678", 7, 4));
        }

        [Fact]
        public void GetTypeTest()
        {
            Assert.NotEqual(PhoneUtil.PhoneType.Unknown, PhoneUtil.GetType("13812345678"));
            Assert.Equal(PhoneUtil.PhoneType.Unknown, PhoneUtil.GetType("10012345678"));
        }

        [Fact]
        public void CleanPhoneTest()
        {
            Assert.Equal("13812345678", PhoneUtil.CleanPhone("+86-138-1234-5678"));
            Assert.Equal("13812345678", PhoneUtil.CleanPhone("138-1234-5678"));
        }
    }
}
