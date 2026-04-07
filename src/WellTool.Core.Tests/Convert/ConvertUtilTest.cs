using Xunit;
using WellTool.Core.Convert;

namespace WellTool.Core.Tests.Convert;

public class ConvertUtilTest
{
    [Fact]
    public void ToStrTest()
    {
        Assert.Equal("123", ConvertUtil.ToStr(123));
        Assert.Equal("abc", ConvertUtil.ToStr("abc"));
        Assert.Equal("", ConvertUtil.ToStr(null));
    }
}
