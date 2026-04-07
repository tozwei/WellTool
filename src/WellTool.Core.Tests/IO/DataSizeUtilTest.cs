using WellTool.Core.IO.Unit;
using Xunit;

namespace WellTool.Core.Tests;

public class DataSizeUtilTest
{
    [Fact]
    public void ParseTest()
    {
        long parse = DataSizeUtil.Parse("3M");
        Assert.Equal(3145728, parse);

        parse = DataSizeUtil.Parse("3m");
        Assert.Equal(3145728, parse);

        parse = DataSizeUtil.Parse("3MB");
        Assert.Equal(3145728, parse);

        parse = DataSizeUtil.Parse("3mb");
        Assert.Equal(3145728, parse);

        parse = DataSizeUtil.Parse("3.1M");
        Assert.Equal(3250585, parse);

        parse = DataSizeUtil.Parse("3.1m");
        Assert.Equal(3250585, parse);

        parse = DataSizeUtil.Parse("3.1MB");
        Assert.Equal(3250585, parse);

        parse = DataSizeUtil.Parse("-3.1MB");
        Assert.Equal(-3250585, parse);

        parse = DataSizeUtil.Parse("+3.1MB");
        Assert.Equal(3250585, parse);

        parse = DataSizeUtil.Parse("3.1mb");
        Assert.Equal(3250585, parse);

        parse = DataSizeUtil.Parse("3.1");
        Assert.Equal(3, parse);
    }

    [Fact]
    public void ParseInvalidTest()
    {
        Assert.Throws<ArgumentException>(() => DataSizeUtil.Parse("3.1.3"));
    }

    [Fact]
    public void FormatTest()
    {
        string format = DataSizeUtil.Format(long.MaxValue);
        Assert.Equal("8 EB", format);

        format = DataSizeUtil.Format(1024L * 1024 * 1024 * 1024 * 1024);
        Assert.Equal("1 PB", format);

        format = DataSizeUtil.Format(1024L * 1024 * 1024 * 1024);
        Assert.Equal("1 TB", format);
    }

    [Fact]
    public void FormatWithUnitTest()
    {
        // 测试带单位的格式化
        var result = DataSizeUtil.Format(1024, DataUnit.Kilobytes);
        Assert.Equal("1 KB", result);
    }
}
