using WellTool.Core.Net;
using Xunit;
using System.Text;

namespace WellTool.Core.Tests;

public class FormUrlencodedLastTest
{
    [Fact]
    public void EncodeTest()
    {
        var encoded = FormUrlencoded.Encode("a b", Encoding.UTF8);
        Assert.NotNull(encoded);
    }
      
    [Fact]
    public void DecodeTest()
    {
        var decoded = FormUrlencoded.Decode("a+b", Encoding.UTF8);
        Assert.Equal("a b", decoded);
    }
}
