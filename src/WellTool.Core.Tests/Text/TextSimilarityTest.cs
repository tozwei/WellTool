using WellTool.Core.Text;
using Xunit;


namespace WellTool.Core.Text.Tests;

public class TextSimilarityTest
{
    [Fact]
    public void SimilarDegreeTest()
    {
        int distance = TextSimilarity.EditDistance("hello", "hallo");
        Assert.Equal(1, distance);
    }

    [Fact]
    public void SimilarDegreeTest2()
    {
        int distance = TextSimilarity.EditDistance("kitten", "sitting");
        Assert.Equal(3, distance);
    }

    [Fact]
    public void SimilarTest()
    {
        double similarity = TextSimilarity.Similarity("hello", "hello");
        Assert.Equal(1.0, similarity, 2);
    }

    [Fact]
    public void SimilarEmptyTest()
    {
        double similarity = TextSimilarity.Similarity("hello", "");
        Assert.Equal(0.0, similarity, 2);
    }
}
