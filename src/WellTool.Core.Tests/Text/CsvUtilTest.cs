using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class CsvUtilTest
{
    [Fact]
    public void GetReaderTest()
    {
        var reader = CsvUtil.GetReader();
        Assert.NotNull(reader);
    }

    [Fact]
    public void ReadFromStrTest()
    {
        var csvStr = "a,b,c\nd,e,f";
        var reader = CsvUtil.GetReader();
        var data = reader.ReadFromStr(csvStr);

        Assert.NotNull(data);
        Assert.Equal(2, data.RowCount);

        var row0 = data.GetRow(0);
        Assert.Equal("a", row0.Get(0));
        Assert.Equal("b", row0.Get(1));
        Assert.Equal("c", row0.Get(2));
    }

    [Fact]
    public void ReadFromStrWithQuotedTest()
    {
        var csvStr = "\"a,b\",c,d";
        var reader = CsvUtil.GetReader();
        var data = reader.ReadFromStr(csvStr);

        var row0 = data.GetRow(0);
        Assert.Equal("a,b", row0.Get(0));
        Assert.Equal("c", row0.Get(1));
        Assert.Equal("d", row0.Get(2));
    }
}
