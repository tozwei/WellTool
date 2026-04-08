using Xunit;
using WellTool.Core.Clone;

namespace WellTool.Core.Tests.Clone;

/// <summary>
/// Clone 测试
/// </summary>
public class CloneTest
{
    [Fact]
    public void CloneableTest()
    {
        var original = new CloneableClass { Value = "test" };
        var cloned = CloneUtil.Clone(original);
        Assert.NotNull(cloned);
        Assert.Equal("test", cloned.Value);
        Assert.NotSame(original, cloned);
    }

    public class CloneableClass : ICloneable<CloneableClass>
    {
        public string Value { get; set; } = "";

        public CloneableClass Clone()
        {
            return new CloneableClass { Value = this.Value };
        }
    }
}
