using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class CsvReaderTest
{
    [Fact]
    public void ReadTest()
    {
        var csvStr = "a,b,c\nd,e,f";
        var reader = new CsvReader();
        var data = reader.Read(csvStr);

        Assert.Equal(2, data.RowCount);
        Assert.Equal("a", data.GetRow(0).Get(0));
        Assert.Equal("b", data.GetRow(0).Get(1));
        Assert.Equal("c", data.GetRow(0).Get(2));
    }

    [Fact]
    public void ReadMapListTest()
    {
        var csvStr = "name,age,city\n张三,18,北京\n李四,20,上海";
        var reader = CsvUtil.GetReader();
        reader.SetConfig(CsvReadConfig.DefaultConfig().SetContainsHeader(true));
        var result = reader.ReadMapList(csvStr);

        Assert.Equal(2, result.Count);
        Assert.Equal("张三", result[0]["name"]);
        Assert.Equal("18", result[0]["age"]);
        Assert.Equal("北京", result[0]["city"]);
    }

    [Fact]
    public void CustomConfigTest()
    {
        var reader = CsvUtil.GetReader(
            CsvReadConfig.DefaultConfig()
                .SetTextDelimiter('\'')
                .SetFieldSeparator(';'));

        var csvStr = "123;456;'789;0'abc;";
        var csvData = reader.ReadFromStr(csvStr);
        var row = csvData.GetRow(0);

        Assert.Equal("123", row.Get(0));
        Assert.Equal("456", row.Get(1));
        Assert.Equal("'789;0'abc", row.Get(2));
    }
}
