using Xunit;
using WellTool.Core.Text;

namespace WellTool.Core.Tests.Text;

/// <summary>
/// CsvReader 测试
/// </summary>
public class CsvReaderTest
{
    [Fact]
    public void ReadTest()
    {
        var csv = "name,age\ntest,20";
        using var reader = CsvUtil.Reader(csv);
        var rows = reader.Read();
        Assert.NotNull(rows);
    }
}
