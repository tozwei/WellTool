namespace WellTool.Captcha.Tests;

using System.Drawing;
using WellTool.Captcha;
using Xunit;

public class CircleCaptchaTest
{
    [Fact]
    public void CreateAndVerifyTest()
    {
        var captcha = new CircleCaptcha(200, 100, 4, 15);
        captcha.CreateCode();
        var code = captcha.Code;
        Assert.NotNull(code);
        Assert.Equal(4, code.Length);

        var verified = captcha.Verify(code);
        Assert.True(verified);
    }

    [Fact]
    public void CreateImageTest()
    {
        var captcha = new CircleCaptcha(200, 100, 4, 15);
        captcha.CreateCode();
        var image = captcha.CreateImage();
        Assert.NotNull(image);
        Assert.Equal(200, image.Width);
        Assert.Equal(100, image.Height);
    }

    [Fact]
    public void ToBase64Test()
    {
        var captcha = new CircleCaptcha(200, 100, 4, 15);
        captcha.CreateCode();
        var base64 = captcha.ToBase64();
        Assert.NotNull(base64);
        Assert.StartsWith("data:image/", base64);
    }

    [Fact]
    public void SetCircleCountTest()
    {
        var captcha = new CircleCaptcha(200, 100, 4, 20);
        captcha.CreateCode();
        Assert.NotNull(captcha);
    }

    [Fact]
    public void VerifyWrongCodeTest()
    {
        var captcha = new CircleCaptcha(200, 100, 4, 15);
        captcha.CreateCode();
        var verified = captcha.Verify("wrong");
        Assert.False(verified);
    }
}
