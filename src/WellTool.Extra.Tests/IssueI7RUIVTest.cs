namespace WellTool.Extra.Tests;

using Xunit;
using WellTool.Extra.Qrcode;
using System.Drawing;

public class IssueI7RUIVTest
{
    [Fact]
    public void QrCodeTest()
    {
        // 测试二维码生成功能
        var config = new QrConfig();
        var image = QrCodeUtil.Generate("Hello, World!", config);
        Assert.NotNull(image);
        Assert.IsType<Bitmap>(image);
    }
}
