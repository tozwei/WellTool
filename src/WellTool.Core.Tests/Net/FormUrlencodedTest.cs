using WellTool.Core.Net;
using Xunit;

namespace WellTool.Core.Tests;

public class FormUrlencodedTest
{
    [Fact]
    public void EncodeParamTest()
    {
        var encode = FormUrlencoded.Encode("a+b", StrUtil.CHARSET_UTF_8);
        Assert.Equal("a%2Bb", encode);

        encode = FormUrlencoded.Encode("a b", StrUtil.CHARSET_UTF_8);
        Assert.Equal("a+b", encode);
    }

    [Fact]
    public void EncodeDecodeTest()
    {
        var original = "Hello World!";
        var encoded = FormUrlencoded.Encode(original, StrUtil.CHARSET_UTF_8);
        Assert.NotEmpty(encoded);
    }
}
