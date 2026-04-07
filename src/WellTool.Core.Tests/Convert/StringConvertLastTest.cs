using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class StringConvertLastTest
{
    [Fact]
    public void ToStrTest()
    {
        var result = WellTool.Core.Convert.Convert.ToStr(123);
        Assert.Equal("123", result);
    }

    [Fact]
    public void ToStrWithNullTest()
    {
        var result = WellTool.Core.Convert.Convert.ToStr(null);
        Assert.Null(result);
    }
}
