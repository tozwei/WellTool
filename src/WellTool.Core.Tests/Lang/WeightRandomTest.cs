using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class WeightRandomTest
{
    [Fact]
    public void WeightRandomTest()
    {
        var random = new WeightRandom<string>();
        random.Add("A", 10);
        random.Add("B", 50);
        random.Add("C", 100);

        var result = random.Next();
        Assert.Contains(result, new[] { "A", "B", "C" });
    }

    [Fact]
    public void WeightRandomMultipleTest()
    {
        var random = new WeightRandom<string>();
        random.Add("A", 0);
        random.Add("B", 100);
        random.Add("C", 0);

        // Should always return B since others have 0 weight
        for (int i = 0; i < 10; i++)
        {
            Assert.Equal("B", random.Next());
        }
    }

    [Fact]
    public void WeightRandomEmptyTest()
    {
        var random = new WeightRandom<string>();
        Assert.Throws<Exception>(() => random.Next());
    }
}
