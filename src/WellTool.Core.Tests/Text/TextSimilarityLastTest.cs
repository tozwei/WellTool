using Xunit;
using WellTool.Core.Text;

namespace WellTool.Core.Tests.Text;

/// <summary>
/// TextSimilarity 测试
/// </summary>
public class TextSimilarityLastTest
{
    [Fact]
    public void SimilarTest()
    {
        var similarity = TextSimilarity.Similar("Hello World", "Hello");
        Assert.True(similarity >= 0 && similarity <= 1);
    }

    [Fact]
    public void LikeTest()
    {
        var similarity = TextSimilarity.Like("Hello World", "Hello");
        Assert.True(similarity >= 0 && similarity <= 1);
    }

    [Fact]
    public void TotalSimilarityTest()
    {
        var similarity = TextSimilarity.TotalSimilarity("Hello World", "Hello");
        Assert.True(similarity >= 0 && similarity <= 1);
    }
}
