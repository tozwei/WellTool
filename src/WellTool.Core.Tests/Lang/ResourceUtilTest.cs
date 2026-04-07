using WellTool.Core.IO.Resource;
using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class ResourceUtilTest
{
    [Fact]
    public void GetResourceUrlTest()
    {
        var url = ResourceUtil.GetResourceUrl("WellTool.Core.xml");
        Xunit.Assert.NotNull(url);
    }

    [Fact]
    public void ReadUtf8StrTest()
    {
        var content = ResourceUtil.ReadUtf8Str("WellTool.Core.xml");
        Xunit.Assert.NotNull(content);
    }

    [Fact]
    public void GetResourcesTest()
    {
        var resources = ResourceUtil.GetResources("*.xml");
        Xunit.Assert.NotNull(resources);
    }
}
