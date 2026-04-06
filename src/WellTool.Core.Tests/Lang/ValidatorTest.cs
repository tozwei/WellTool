using WellTool.Core.Lang;
using Xunit;
using Assert = Xunit.Assert;

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
    public void IsEmailTest()
    {
        Assert.True(Validator.IsEmail("abc_cde@163.com"));
        Assert.True(Validator.IsEmail("abc_%cde@aaa.c"));
        Assert.True(Validator.IsEmail("xiaolei.lu@aaa.b"));
    }

    [Fact]
    public void IsPhoneTest()
    {
        Assert.True(Validator.IsPhone("13900221432"));
        Assert.True(Validator.IsPhone("18600221432"));
        Assert.False(Validator.IsPhone("1234567890"));
    }

    [Fact]
    public void IsIdCardTest()
    {
        Assert.True(Validator.IsIdCard("110101199001011234"));
        Assert.True(Validator.IsIdCard("11010119900101123X"));
        Assert.False(Validator.IsIdCard("11010119900101123"));
    }

    [Fact]
    public void IsUrlTest()
    {
        Assert.True(Validator.IsUrl("https://www.example.com"));
        Assert.True(Validator.IsUrl("http://example.com"));
        Assert.False(Validator.IsUrl("example.com"));
    }

    [Fact]
    public void IsIpTest()
    {
        Assert.True(Validator.IsIp("192.168.1.1"));
        Assert.True(Validator.IsIp("127.0.0.1"));
        Assert.False(Validator.IsIp("256.256.256.256"));
    }

    [Fact]
    public void IsDateTest()
    {
        Assert.True(Validator.IsDate("2023-12-25"));
        Assert.True(Validator.IsDate("2023/12/25"));
        Assert.False(Validator.IsDate("2023-13-25"));
    }

    [Fact]
    public void IsIntegerTest()
    {
        Assert.True(Validator.IsInteger("123"));
        Assert.True(Validator.IsInteger("-123"));
        Assert.False(Validator.IsInteger("123.45"));
    }

    [Fact]
    public void IsDecimalTest()
    {
        Assert.True(Validator.IsDecimal("123.45"));
        Assert.True(Validator.IsDecimal("-123.45"));
        Assert.False(Validator.IsDecimal("123"));
    }

    [Fact]
    public void IsValidPasswordTest()
    {
        Assert.True(Validator.IsValidPassword("Password123"));
        Assert.False(Validator.IsValidPassword("password123"));
        Assert.False(Validator.IsValidPassword("PASSWORD123"));
        Assert.False(Validator.IsValidPassword("Password"));
    }
}
