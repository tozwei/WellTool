using WellTool.Core.Text;
using Xunit;

public class PasswdStrengthTest
{
    [Fact]
    public void StrengthEnumTest()
    {
        Assert.Equal(PasswdStrength.Weak, PasswdStrength.Weak);
        Assert.Equal(PasswdStrength.Medium, PasswdStrength.Medium);
        Assert.Equal(PasswdStrength.Strong, PasswdStrength.Strong);
        Assert.Equal(PasswdStrength.VeryStrong, PasswdStrength.VeryStrong);
    }
}
