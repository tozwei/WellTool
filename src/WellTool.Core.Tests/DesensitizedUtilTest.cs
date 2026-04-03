using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Desensitized(脱敏)工具单元测试
    /// </summary>
    public class DesensitizedUtilTest
    {
        [Fact]
        public void ChineseNameTest()
        {
            Assert.Equal("张*", DesensitizedUtil.ChineseName("张三"));
            Assert.Equal("张*三", DesensitizedUtil.ChineseName("张三三"));
            Assert.Equal("张*三", DesensitizedUtil.ChineseName("张阿三"));
            Assert.Equal("**", DesensitizedUtil.ChineseName("张"));
            Assert.Equal("***", DesensitizedUtil.ChineseName("张三三三"));
        }

        [Fact]
        public void ChineseNameTest2()
        {
            Assert.Equal("*三", DesensitizedUtil.ChineseName("李三", 1));
            Assert.Equal("张*", DesensitizedUtil.ChineseName("张三", 2));
        }

        [Fact]
        public void IdCardNumTest()
        {
            var idCard = "321283198001011234";
            Assert.Equal("32128319800101****", DesensitizedUtil.IdCardNum(idCard, 4, 4));
            Assert.Equal("321283********1234", DesensitizedUtil.IdCardNum(idCard, 8, 4));
        }

        [Fact]
        public void IdCardNumFixedTest()
        {
            var idCard = "321283198001011234";
            Assert.Equal("****1234", DesensitizedUtil.IdCardNum(idCard));
        }

        [Fact]
        public void MobilePhoneTest()
        {
            Assert.Equal("188****6789", DesensitizedUtil.MobilePhone("188123456789"));
        }

        [Fact]
        public void MobilePhoneTest2()
        {
            Assert.Equal("188****6789", DesensitizedUtil.MobilePhone("188123456789", 3, 4));
        }

        [Fact]
        public void EmailTest()
        {
            Assert.Equal("t****@qq.com", DesensitizedUtil.Email("test@qq.com"));
            Assert.Equal("tes****@qq.com", DesensitizedUtil.Email("testtest@qq.com"));
        }

        [Fact]
        public void AddressTest()
        {
            Assert.Equal("北京市昌平区****", DesensitizedUtil.Address("北京市昌平区xx镇", 6));
        }

        [Fact]
        public void BankCardTest()
        {
            Assert.Equal("622202***********1234", DesensitizedUtil.BankCard("62220212345678901234"));
        }

        [Fact]
        public void PasswordTest()
        {
            Assert.Equal("***", DesensitizedUtil.Password("abc123"));
            Assert.Equal("", DesensitizedUtil.Password(""));
        }

        [Fact]
        public void ChineseIdCardTest()
        {
            // 默认脱敏
            Assert.Equal("321283***********234", DesensitizedUtil.ChineseIdCard("321283198001011234"));
        }

        [Fact]
        public void ChineseIdCardTest2()
        {
            // 指定保留前6位和后4位
            Assert.Equal("321283***********234", DesensitizedUtil.ChineseIdCard("321283198001011234", 6, 4));
        }
    }
}
