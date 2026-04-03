using WellTool.Core.IO;
using Xunit;

namespace WellTool.Core.Tests;

public class ManifestUtilTest
{
    [Fact]
    public void GetManifestTest()
    {
        var manifest = ManifestUtil.GetManifest(typeof(ManifestUtilTest));
        Assert.NotNull(manifest);
    }
}
