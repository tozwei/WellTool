using Xunit;
using WellTool.Core.Text;

namespace WellTool.Core.Tests.Text;

/// <summary>
/// CsvUtil 测试
/// </summary>
public class CsvUtilTest
{
    [Fact]
    public void ReadTest()
    {
        var csv = "name,age\ntest,20";
        var reader = CsvUtil.Reader(csv);
        Assert.NotNull(reader);
    }

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
