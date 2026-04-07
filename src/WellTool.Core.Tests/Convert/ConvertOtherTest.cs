using Xunit;

namespace WellTool.Core.Tests.Convert;

public class ConvertOtherTest
{
    [Fact]
    public void ToCharTest()
    {
        Assert.Equal('A', WellTool.Core.Convert.Convert.ToChar("A"));
    }

    [Fact]
    public void ToDateTimeTest()
    {
        var date = WellTool.Core.Convert.Convert.ToDateTime("2021-01-01");
        Assert.NotNull(date);
        Assert.Equal(2021, date.Value.Year);
        Assert.Equal(1, date.Value.Month);
        Assert.Equal(1, date.Value.Day);
    }

    [Fact]
    public void ToShortTest()
    {
        Assert.Equal((short)123, WellTool.Core.Convert.Convert.ToShort("123"));
        Assert.Equal((short)0, WellTool.Core.Convert.Convert.ToShort(null));
    }

    [Fact]
    public void ToFloatTest()
    {
        Assert.Equal(123.45f, WellTool.Core.Convert.Convert.ToFloat("123.45"), 0.001f);
        Assert.Equal(0f, WellTool.Core.Convert.Convert.ToFloat(null));
    }

    [Fact]
    public void ToDecimalTest()
    {
        Assert.Equal(123.456m, WellTool.Core.Convert.Convert.ToDecimal("123.456"));
        Assert.Equal(0m, WellTool.Core.Convert.Convert.ToDecimal(null));
    }
}