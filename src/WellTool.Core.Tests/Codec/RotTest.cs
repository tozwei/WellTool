using Xunit;
using WellTool.Core.Codec;

namespace WellTool.Core.Tests.Codec;

/// <summary>
/// Rot 测试
/// </summary>
public class RotTest
{
    [Fact]
    public void Rot13Test()
    {
        var encoded = Rot.Rot13("Hello");
        Assert.NotNull(encoded);
    }

    [Fact]
    public void Rot47Test()
    {
        var encoded = Rot.Rot47("Hello");
        Assert.NotNull(encoded);
    }
}
