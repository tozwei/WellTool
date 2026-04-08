using Xunit;
using WellTool.Core.Codec;

namespace WellTool.Core.Tests.Codec;

/// <summary>
/// Caesar 测试
/// </summary>
public class CaesarTest
{
    [Fact]
    public void EncodeTest()
    {
        var encoded = Caesar.Encode("Hello", 3);
        Assert.NotNull(encoded);
    }

    [Fact]
    public void DecodeTest()
    {
        var encoded = Caesar.Encode("Hello", 3);
        var decoded = Caesar.Decode(encoded, 3);
        Assert.Equal("Hello", decoded);
    }
}
