using Xunit;
using WellTool.Core.Util;

namespace WellTool.Core.Tests;

/// <summary>
/// PhoneUtil 测试
/// </summary>
public class PhoneUtilTest
{
    [Fact]
    public void IsValidTest()
    {
        Assert.True(PhoneUtil.IsValid("13812345678"));
        Assert.True(PhoneUtil.IsValid("+8613812345678"));
        Assert.False(PhoneUtil.IsValid("12345"));
    }

    [Fact]
    public void HideTest()
    {
        var hidden = PhoneUtil.Hide("13812345678");
        Assert.Contains("****", hidden);
    }
}
