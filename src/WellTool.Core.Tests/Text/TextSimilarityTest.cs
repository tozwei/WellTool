using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class TextSimilarityTest
{
    [Fact]
    public void SimilarDegreeTest()
    {
        var a = "我是一个文本，独一无二的文本";
        var b = "一个文本，独一无二的文本";

        var degree = TextSimilarityUtil.Similar(a, b);
        Assert.True(Math.Abs(degree - 0.8461538462D) < 0.01);

        var similarPercent = TextSimilarityUtil.Similar(a, b, 2);
        Assert.Equal("84.62%", similarPercent);
    }

    [Fact]
    public void SimilarDegreeTest2()
    {
        var a = "我是一个文本，独一无二的文本";
        var b = "一个文本，独一无二的文本,#,>>?#$%^%$&^&^%";

        var degree = TextSimilarityUtil.Similar(a, b);
        Assert.True(Math.Abs(degree - 0.8461538462D) < 0.01);
    }

    [Fact]
    public void SimilarTest()
    {
        var abd = TextSimilarityUtil.Similar("abd", "1111");
        Assert.Equal(0, abd, 0);
    }

    [Fact]
    public void SimilarEmptyTest()
    {
        var abd = TextSimilarityUtil.Similar("", "");
        Assert.Equal(1.0, abd, 0);

        var abd2 = TextSimilarityUtil.Similar("abc", "");
        Assert.Equal(0, abd2, 0);
    }
}
