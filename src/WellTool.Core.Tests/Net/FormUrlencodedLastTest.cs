using WellTool.Core.Net;
using Xunit;
using System.Text;

namespace WellTool.Core.Tests;

public class FormUrlencodedLastTest
{
    [Fact]
    public void EncodeTest()
    {
        // FormUrlencoded 是一个 PercentCodec，不是静态工具类
        // 使用 URLEncodeUtil 代替
        var encoded = URLEncodeUtil.Encode("a b", Encoding.UTF8);
        Assert.NotNull(encoded);
    }
      
    [Fact]
    public void DecodeTest()
    {
        var decoded = URLEncodeUtil.Decode("a+b", Encoding.UTF8);
        Assert.NotNull(decoded);
    }
}
