using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Idcard(身份证)工具单元测试
    /// </summary>
    public class IdcardUtilTest
    {
        // 有效身份证号码
        private const string ValidIdCard18 = "110101199003078888";
        private const string ValidIdCard15 = "110101900307888";

        [Fact]
        public void IsValidTest()
        {
            // 测试有效身份证
            Assert.True(IdcardUtil.IsValid(ValidIdCard18));
            Assert.True(IdcardUtil.IsValid(ValidIdCard15));
            
            // 测试无效身份证
            Assert.False(IdcardUtil.IsValid(null));
            Assert.False(IdcardUtil.IsValid(""));
            Assert.False(IdcardUtil.IsValid("123456789012345678"));
            Assert.False(IdcardUtil.IsValid("ABCDEFGHIJKLMNOPQR"));
        }

        [Fact]
        public void GetBirthDayTest()
        {
            var birthDate = IdcardUtil.GetBirthDate(ValidIdCard18);
            Assert.NotNull(birthDate);
            Assert.Equal(1990, birthDate.Value.Year);
            Assert.Equal(3, birthDate.Value.Month);
            Assert.Equal(7, birthDate.Value.Day);
        }

        [Fact]
        public void GetAgeTest()
        {
            var age = IdcardUtil.GetAge(ValidIdCard18);
            Assert.NotNull(age);
            Assert.True(age >= 0);
        }

        [Fact]
        public void GetGenderTest()
        {
            var gender = IdcardUtil.GetGender(ValidIdCard18);
            Assert.NotNull(gender);
            Assert.True(gender == 1 || gender == 2);
        }

        [Fact]
        public void GetGenderStrTest()
        {
            var genderStr = IdcardUtil.GetGenderStr(ValidIdCard18);
            Assert.NotNull(genderStr);
            Assert.True(genderStr == "男" || genderStr == "女");
        }

        [Fact]
        public void GetProvinceTest()
        {
            var province = IdcardUtil.GetProvince(ValidIdCard18);
            Assert.NotNull(province);
            Assert.NotEmpty(province);
        }
    }
}
