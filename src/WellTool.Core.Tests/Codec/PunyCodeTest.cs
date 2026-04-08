using Xunit;
using WellTool.Core.Codec;

namespace WellTool.Core.Tests.Codec;

/// <summary>
/// PunyCode 测试
/// </summary>
public class PunyCodeTest
{
    [Fact]
    public void EncodeTest()
    {
        var punycode = PunyCode.Encode("例子");
        Assert.NotNull(punycode);
        Assert.Contains("xn--", punycode);
    }

    [Fact]
    public void DecodeTest()
    {
        var punycode = PunyCode.Encode("例子");
        var decoded = PunyCode.Decode(punycode);
        Assert.Equal("例子", decoded);
    }
}
