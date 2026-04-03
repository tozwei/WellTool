using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class TextSimilarityLastTest
{
    [Fact]
    public void SimilarTest()
    {
        var a = "这是一个测试文本";
        var b = "这是一个测试";
        var result = TextSimilarity.Similar(a, b);
        Assert.True(result > 0);
    }

    [Fact]
    public void SimilarPercentTest()
    {
        var a = "这是一个测试";
        var b = "这是一个测试";
        var result = TextSimilarity.Similar(a, b, 2);
        Assert.Contains("%", result);
    }
}
