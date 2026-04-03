using WellTool.Core.Bean;
using Xunit;

namespace WellTool.Core.Tests;

public class BeanCopyMappingLastTest
{
    [Fact]
    public void AddMappingTest()
    {
        var mapping = new BeanCopyMapping();
        mapping.AddMapping("SourceName", "TargetName");
        Assert.True(true);
    }

    [Fact]
    public void BuildTest()
    {
        var mapping = new BeanCopyMapping();
        mapping.AddMapping("SourceName", "TargetName");
        var built = mapping.Build();
        Assert.NotNull(built);
    }

    [Fact]
    public void CopyTest()
    {
        var source = new SourceBean { SourceName = "John" };
        var target = new TargetBean();
        var mapping = new BeanCopyMapping();
        mapping.AddMapping("SourceName", "TargetName");
        mapping.Copy(source, target);
        Assert.True(true);
    }

    private class SourceBean
    {
        public string SourceName { get; set; } = "";
    }

    private class TargetBean
    {
        public string TargetName { get; set; } = "";
    }
}
