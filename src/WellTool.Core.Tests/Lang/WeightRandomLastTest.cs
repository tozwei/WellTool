using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class WeightRandomLastTest
{
    [Fact]
    public void BasicTest()
    {
        var random = WeightRandom<string>.Create();
        random.Add("A", 10);
        random.Add("B", 20);
        
        var result = random.Next();
        Assert.NotNull(result);
    }
}
