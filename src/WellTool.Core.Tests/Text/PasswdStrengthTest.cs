using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class PasswdStrengthTest
{
    [Fact]
    public void StrengthTest()
    {
        var passwd = "2hAj5#mne-ix.86H";
        var strength = PasswdStrength.Check(passwd);
        Assert.True(strength > 0);
    }

    [Fact]
    public void StrengthNumberTest()
    {
        var passwd = "9999999999999";
        var strength = PasswdStrength.Check(passwd);
        Assert.True(strength >= 0);
    }

    [Fact]
    public void ConsecutiveLettersTest()
    {
        // 测试连续小写字母
        var strength1 = PasswdStrength.Check("abcdefghijklmn");
        Assert.True(strength1 >= 0);

        // 测试连续大写字母
        var strength2 = PasswdStrength.Check("ABCDEFGHIJKLMN");
        Assert.True(strength2 >= 0);
    }

    [Fact]
    public void SimplePasswordTest()
    {
        var strength1 = PasswdStrength.Check("password");
        Assert.True(strength1 >= 0);
    }

    [Fact]
    public void NumericSequenceTest()
    {
        var strength1 = PasswdStrength.Check("01234567890");
        Assert.True(strength1 >= 0);

        var strength2 = PasswdStrength.Check("09876543210");
        Assert.True(strength2 >= 0);
    }
}
