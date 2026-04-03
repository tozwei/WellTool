using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class ValidatorTest
{
    [Fact]
    public void IsNumberTest()
    {
        Assert.True(Validator.IsNumber("45345365465"));
        Assert.True(Validator.IsNumber("0004545435"));
        Assert.True(Validator.IsNumber("5.222"));
        Assert.True(Validator.IsNumber("0.33323"));
    }

    [Fact]
    public void IsNotNumberTest()
    {
        Assert.False(Validator.IsNumber("abc"));
        Assert.False(Validator.IsNumber("12abc"));
    }

    [Fact]
    public void HasNumberTest()
    {
        Assert.False(Validator.HasNumber(""));
        Assert.False(Validator.HasNumber("str"));
        Assert.True(Validator.HasNumber("180"));
        Assert.True(Validator.HasNumber("身高180体重180"));
    }

    [Fact]
    public void IsLetterTest()
    {
        Assert.True(Validator.IsLetter("asfdsdsfds"));
        Assert.True(Validator.IsLetter("asfdsdfdsfVCDFDFGdsfds"));
        Assert.True(Validator.IsLetter("asfdsdf你好dsfVCDFDFGdsfds"));
    }

    [Fact]
    public void IsUperCaseTest()
    {
        Assert.True(Validator.IsUpperCase("VCDFDFG"));
        Assert.True(Validator.IsUpperCase("ASSFD"));
        Assert.False(Validator.IsUpperCase("asfdsdsfds"));
        Assert.False(Validator.IsUpperCase("ASSFD你好"));
    }

    [Fact]
    public void IsLowerCaseTest()
    {
        Assert.True(Validator.IsLowerCase("asfdsdsfds"));
        Assert.False(Validator.IsLowerCase("aaaa你好"));
        Assert.False(Validator.IsLowerCase("VCDFDFG"));
        Assert.False(Validator.IsLowerCase("ASSFD"));
    }

    [Fact]
    public void IsBirthdayTest()
    {
        Assert.True(Validator.IsBirthday("20150101"));
        Assert.True(Validator.IsBirthday("2015-01-01"));
        Assert.True(Validator.IsBirthday("2015.01.01"));
        Assert.True(Validator.IsBirthday("2015年01月01日"));
    }

    [Fact]
    public void IsEmailTest()
    {
        Assert.True(Validator.IsEmail("abc_cde@163.com"));
        Assert.True(Validator.IsEmail("abc_%cde@aaa.c"));
        Assert.True(Validator.IsEmail("xiaolei.lu@aaa.b"));
    }

    [Fact]
    public void IsMobileTest()
    {
        Assert.True(Validator.IsMobile("13900221432"));
        Assert.True(Validator.IsMobile("015100221432"));
        Assert.True(Validator.IsMobile("+8618600221432"));
    }

    [Fact]
    public void IsChineseTest()
    {
        Assert.True(Validator.IsChinese("全都是中文"));
        Assert.False(Validator.IsChinese("not全都是中文"));
    }

    [Fact]
    public void HasChineseTest()
    {
        Assert.True(Validator.HasChinese("黄单桑米"));
        Assert.True(Validator.HasChinese("Kn 四兄弟"));
        Assert.False(Validator.HasChinese("Abc"));
    }

    [Fact]
    public void IsZipCodeTest()
    {
        Assert.True(Validator.IsZipCode("999077")); // 港
        Assert.True(Validator.IsZipCode("999078")); // 澳
        Assert.True(Validator.IsZipCode("822001")); // 台
        Assert.True(Validator.IsZipCode("016063")); // 内蒙
        Assert.True(Validator.IsZipCode("045246")); // 山西
    }

    [Fact]
    public void IsBetweenTest()
    {
        Assert.True(Validator.IsBetween(0, 0, 1));
        Assert.True(Validator.IsBetween(1L, 0L, 1L));
        Assert.True(Validator.IsBetween(0.19f, 0.1f, 0.2f));
        Assert.True(Validator.IsBetween(0.19, 0.1, 0.2));
    }

    [Fact]
    public void IsCarVinTest()
    {
        Assert.True(Validator.IsCarVin("LSJA24U62JG269225"));
        Assert.True(Validator.IsCarVin("LDC613P23A1305189"));
        Assert.False(Validator.IsCarVin("LOC613P23A1305189"));
    }

    [Fact]
    public void IsPlateNumberTest()
    {
        Assert.True(Validator.IsPlateNumber("粤BA03205"));
        Assert.True(Validator.IsPlateNumber("闽20401领"));
        Assert.True(Validator.IsPlateNumber("沪AE22075"));
    }
}
