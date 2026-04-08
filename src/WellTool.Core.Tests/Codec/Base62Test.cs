using Xunit;
using WellTool.Core.Codec;

namespace WellTool.Core.Tests.Codec;

/// <summary>
/// Base62 测试
/// </summary>
public class Base62Test
{
    [Fact]
    public void EncodeTest()
    {
        var encoded = Base62.Encode("Hello");
        Assert.NotNull(encoded);
    }

    [Fact]
    public void DecodeTest()
    {
        var encoded = Base62.Encode("Hello");
        var decoded = Base62.Decode(encoded);
        Assert.Equal("Hello", decoded);
    }
}
