using WellTool.Core.Lang;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class WeightRandomLastTest
{
    [Fact]
    public void BasicTest()
    {
        var random = new WeightRandom<string>();
        random.Add("A", 10);
        random.Add("B", 20);
        
        var result = random.Next();
        Assert.NotNull(result);
    }
}
