using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class StringConvertLastTest
{
    [Fact]
    public void ToStrTest()
    {
        var result = Convert.ToStr(123);
        Assert.Equal("123", result);
    }

    [Fact]
    public void ToStrWithNullTest()
    {
        var result = Convert.ToStr(null);
        Assert.Null(result);
    }
}
