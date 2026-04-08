using Xunit;
using WellTool.Core.Text;

namespace WellTool.Core.Tests.Text;

/// <summary>
/// CsvWriter 测试
/// </summary>
public class CsvWriterLastTest
{
    [Fact]
    public void WriteTest()
    {
        var csv = CsvUtil.Writer()
            .WriteHeader<WriterTestClass>()
            .WriteRow(new WriterTestClass { Name = "test", Age = 20 })
            .ToString();
        Assert.Contains("Name", csv);
        Assert.Contains("test", csv);
    }

    public class WriterTestClass
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
