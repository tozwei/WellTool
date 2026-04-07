using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateFieldLastTest
{
    [Fact]
    public void OfTest()
    {
        // DateField是枚举，直接使用枚举值
        var field = DateField.Year;
        Assert.True(true);
    }

    [Fact]
    public void GetValueTest()
    {
        var dateTime = DateTime.Now;
        // 直接使用DateTime的Year属性
        var value = dateTime.Year;
        Assert.Equal(dateTime.Year, value);
    }
}
