using WellTool.Core.Convert;
using WellTool.Core.Converter;
using Xunit;

namespace WellTool.Core.Tests;

public class CastUtilTest
{
    [Fact]
    public void CastTest()
    {
        var obj = "Hello" as object;
        var str = CastUtil.Cast<string>(obj);
        Assert.Equal("Hello", str);
    }

    [Fact]
    public void CastToIntTest()
    {
        Assert.Equal(123, CastUtil.CastToInt(123.45));
        Assert.Equal(123, CastUtil.CastToInt("123"));
    }

    [Fact]
    public void CastToLongTest()
    {
        Assert.Equal(123L, CastUtil.CastToLong(123));
        Assert.Equal(123L, CastUtil.CastToLong("123"));
    }

    [Fact]
    public void CastToStringTest()
    {
        Assert.Equal("123", CastUtil.CastToString(123));
        Assert.Equal("Hello", CastUtil.CastToString("Hello"));
    }

    [Fact]
    public void CastToBoolTest()
    {
        Assert.True(CastUtil.CastToBool(1));
        Assert.False(CastUtil.CastToBool(0));
    }

    [Fact]
    public void CastToDoubleTest()
    {
        Assert.Equal(123.45, CastUtil.CastToDouble("123.45"), 0.001);
    }
}
