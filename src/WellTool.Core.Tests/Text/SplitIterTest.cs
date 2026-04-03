using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class SplitIterTest
{
    [Fact]
    public void SplitIterTest()
    {
        var text = "a,b,c,d";
        var splitIter = new SplitIter(text, ',');

        var result = new List<string>();
        while (splitIter.MoveNext())
        {
            result.Add(splitIter.Current);
        }

        Assert.Equal(4, result.Count);
        Assert.Equal("a", result[0]);
        Assert.Equal("b", result[1]);
        Assert.Equal("c", result[2]);
        Assert.Equal("d", result[3]);
    }

    [Fact]
    public void SplitIterWithLimitTest()
    {
        var text = "a,b,c,d,e";
        var splitIter = new SplitIter(text, ',', 3);

        var result = new List<string>();
        while (splitIter.MoveNext())
        {
            result.Add(splitIter.Current);
        }

        Assert.Equal(3, result.Count);
        Assert.Equal("a", result[0]);
        Assert.Equal("b", result[1]);
        Assert.Equal("c,d,e", result[2]); // remaining part
    }
}
