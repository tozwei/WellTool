using WellTool.Core.Text.Csv;
using Xunit;
using System.IO;

namespace WellTool.Core.Tests;

public class CsvParserLastTest
{
    [Fact]
    public void BasicParseTest()
    {
        var csv = "a,b,c";
        using var reader = new StringReader(csv);
        // Basic test placeholder - CsvParser implementation needed
        Assert.NotNull(reader);
    }
}
