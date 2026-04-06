using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class ResourceUtilTest
{
    [Fact]
    public void GetResourceUrlTest()
    {
        var url = ResourceUtil.GetResourceUrl("WellTool.Core.xml");
        Assert.NotNull(url);
    }

    [Fact]
    public void ReadUtf8StrTest()
    {
        var content = ResourceUtil.ReadUtf8Str("WellTool.Core.xml");
        Assert.NotNull(content);
    }

    [Fact]
    public void GetResourcesTest()
    {
        var resources = ResourceUtil.GetResources("*.xml");
        Assert.NotNull(resources);
    }
}
