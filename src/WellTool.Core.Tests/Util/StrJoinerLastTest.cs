using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class StrJoinerLastTest
{
    [Fact]
    public void AddTest()
    {
        var joiner = new StrJoiner(",");
        joiner.Add("a").Add("b").Add("c");
        Assert.Equal("a,b,c", joiner.ToString());
    }

    [Fact]
    public void MergeTest()
    {
        var joiner1 = new StrJoiner(",");
        joiner1.Add("a").Add("b");
        var joiner2 = new StrJoiner(",");
        joiner2.Add("c").Add("d");
        var merged = joiner1.Merge(joiner2);
        Assert.Equal("a,b,c,d", merged.ToString());
    }

    [Fact]
    public void LengthTest()
    {
        var joiner = new StrJoiner(",");
        joiner.Add("a").Add("b");
        Assert.Equal(3, joiner.Length());
    }

    [Fact]
    public void SetEmptyValueTest()
    {
        var joiner = new StrJoiner(",");
        joiner.SetEmptyValue("empty");
        Assert.Equal("empty", joiner.ToString());
    }
}
