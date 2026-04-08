using Xunit;
using WellTool.Core.Codec;

namespace WellTool.Core.Tests.Codec;

/// <summary>
/// Morse 测试
/// </summary>
public class MorseTest
{
    [Fact]
    public void EncodeTest()
    {
        var morse = Morse.Encode("SOS");
        Assert.NotNull(morse);
        Assert.Contains("...", morse);
        Assert.Contains("-", morse);
    }

    [Fact]
    public void DecodeTest()
    {
        var morse = Morse.Decode("... --- ...");
        Assert.Equal("SOS", morse);
    }
}
