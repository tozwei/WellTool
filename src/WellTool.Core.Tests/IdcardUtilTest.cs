using Xunit;
using WellTool.Core;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Idcard(身份证)工具单元测试
    /// </summary>
    public class IdcardUtilTest
    {
        [Fact]
        public void IsValidTest()
        {
            // 有效身份证号（18位）
            Assert.True(IdcardUtil.IsValid("321283199001011234"));

            // 无效长度
            Assert.False(IdcardUtil.IsValid("32128319900101"));
            Assert.False(IdcardUtil.IsValid(""));

            // 无效日期
            Assert.False(IdcardUtil.IsValid("321283199001329999")); // 日期32日无效
        }

        [Fact]
        public void GetBirthDayTest()
        {
            var birthDay = IdcardUtil.GetBirthDay("321283199001011234");
            Assert.Equal("19900101", birthDay);
        }

        [Fact]
        public void GetAgeTest()
        {
            var age = IdcardUtil.GetAge("321283199001011234");
            Assert.True(age >= 30); // 根据当前年份动态计算
        }

        [Fact]
        public void GetYearTest()
        {
            var year = IdcardUtil.GetYear("321283199001011234");
            Assert.Equal(1990, year);
        }

        [Fact]
        public void GetMonthTest()
        {
            var month = IdcardUtil.GetMonth("321283199001011234");
            Assert.Equal(1, month);
        }

        [Fact]
        public void GetDayTest()
        {
            var day = IdcardUtil.GetDay("321283199001011234");
            Assert.Equal(1, day);
        }

        [Fact]
        public void GetGenderTest()
        {
            // 15位身份证：最后一位奇数为男，偶数为女
            Assert.Equal(1, IdcardUtil.GetGender("321283900101231"));
            Assert.Equal(0, IdcardUtil.GetGender("321283900101232"));
        }

        [Fact]
        public void GetGenderTest2()
        {
            // 18位身份证：第17位
            Assert.Equal(1, IdcardUtil.GetGender("321283199001011234")); // 第17位是3，奇数
            Assert.Equal(0, IdcardUtil.GetGender("321283199001021234")); // 第17位是2，偶数
        }

        [Fact]
        public void GetProvinceCodeTest()
        {
            var provinceCode = IdcardUtil.GetProvinceCode("321283199001011234");
            Assert.Equal("32", provinceCode);
        }

        [Fact]
        public void GetCityCodeTest()
        {
            var cityCode = IdcardUtil.GetCityCode("321283199001011234");
            Assert.Equal("321", cityCode);
        }

        [Fact]
        public void GetCountyCodeTest()
        {
            var countyCode = IdcardUtil.GetCountyCode("321283199001011234");
            Assert.Equal("321283", countyCode);
        }

        [Fact]
        public void GetProvinceTest()
        {
            var province = IdcardUtil.GetProvince("321283199001011234");
            Assert.Equal("江苏省", province);
        }

        [Fact]
        public void GetCityTest()
        {
            var city = IdcardUtil.GetCity("321283199001011234");
            Assert.Equal("泰州市", city);
        }

        [Fact]
        public void GetCountyTest()
        {
            var county = IdcardUtil.GetCounty("321283199001011234");
            Assert.Equal("靖江市", county);
        }

        [Fact]
        public void IsValidDateTest()
        {
            Assert.True(IdcardUtil.IsValidDate("19900101"));
            Assert.False(IdcardUtil.IsValidDate("19900132"));
            Assert.False(IdcardUtil.IsValidDate("19901301"));
        }

        [Fact]
        public void Convert15To18Test()
        {
            var id18 = IdcardUtil.Convert15To18("321283900101231");
            Assert.Equal(18, id18.Length);
            Assert.StartsWith("321283", id18);
        }
    }
}
