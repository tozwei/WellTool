using Xunit;
using WellTool.Core.Util;

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
            Assert.Equal("张**", DesensitizedUtil.ChineseName("张三三"));
            Assert.Equal("张**", DesensitizedUtil.ChineseName("张阿三"));
            Assert.Equal("张", DesensitizedUtil.ChineseName("张"));
            Assert.Equal("张***", DesensitizedUtil.ChineseName("张三三三"));
        }

        [Fact]
        public void IdCardTest()
        {
            var idCard = "321283198001011234";
            Assert.Equal("3212**********1234", DesensitizedUtil.IdCard(idCard));
        }

        [Fact]
        public void MobilePhoneTest()
        {
            Assert.Equal("188****5678", DesensitizedUtil.MobilePhone("18812345678"));
        }

        [Fact]
        public void EmailTest()
        {
            Assert.Equal("t***@qq.com", DesensitizedUtil.Email("test@qq.com"));
            Assert.Equal("tes***@qq.com", DesensitizedUtil.Email("testtest@qq.com"));
        }

        [Fact]
        public void AddressTest()
        {
            Assert.Equal("北京市昌平区****", DesensitizedUtil.Address("北京市昌平区xx镇", 6));
        }

        [Fact]
        public void BankCardTest()
        {
            Assert.Equal("6222****01234", DesensitizedUtil.BankCard("62220212345678901234"));
        }

        [Fact]
        public void PasswordTest()
        {
            Assert.Equal("******", DesensitizedUtil.Password("abc123"));
            Assert.Equal("", DesensitizedUtil.Password(""));
        }

        [Fact]
        public void CarLicenseTest()
        {
            Assert.Equal("京A**888", DesensitizedUtil.CarLicense("京A123888"));
        }
    }
}
