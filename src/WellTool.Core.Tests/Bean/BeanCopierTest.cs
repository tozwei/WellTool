using WellTool.Core.Bean.Copier;
using Xunit;

namespace WellTool.Core.Tests;

public class BeanCopierTest
{
    [Fact]
    public void BeanToBeanCopyTest()
    {
        var source = new SourceClass { Value = "test" };
        var target = new TargetClass();

        var copier = BeanCopier<TargetClass>.Create(source, target, CopyOptions.Create());
        var result = copier.Copy();

        Assert.Equal("test", result.Value);
    }

    [Fact]
    public void BeanToMapCopyTest()
    {
        var source = new SourceClass { Value = "test" };
        var target = new System.Collections.Generic.Dictionary<object, object>();

        var copier = BeanCopier<System.Collections.Generic.Dictionary<object, object>>.Create(source, target, CopyOptions.Create());
        var result = copier.Copy();

        Assert.Equal("test", result["value"]);
    }

    private class SourceClass
    {
        public string Value { get; set; }
    }

    private class TargetClass
    {
        public string Value { get; set; }
    }
}
