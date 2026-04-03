namespace WellTool.Captcha.Tests;

using Well.Captcha;
using Xunit;

public class ShearCaptchaTest
{
    [Fact]
    public void CreateAndVerifyTest()
    {
        var captcha = new ShearCaptcha(200, 100, 4, 30);
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
        var captcha = new ShearCaptcha(200, 100, 4, 30);
        captcha.CreateCode();
        var image = captcha.CreateImage();
        Assert.NotNull(image);
        Assert.Equal(200, image.Width);
        Assert.Equal(100, image.Height);
    }

    [Fact]
    public void ToBase64Test()
    {
        var captcha = new ShearCaptcha(200, 100, 4, 30);
        captcha.CreateCode();
        var base64 = captcha.ToBase64();
        Assert.NotNull(base64);
        Assert.StartsWith("data:image/", base64);
    }

    [Fact]
    public void SetThicknessTest()
    {
        var captcha = new ShearCaptcha(200, 100, 4, 50);
        captcha.CreateCode();
        Assert.NotNull(captcha);
    }

    [Fact]
    public void VerifyWrongCodeTest()
    {
        var captcha = new ShearCaptcha(200, 100, 4, 30);
        captcha.CreateCode();
        var verified = captcha.Verify("wrong");
        Assert.False(verified);
    }

    [Fact]
    public void SetBackgroundTest()
    {
        var captcha = new ShearCaptcha(200, 100, 4, 30);
        captcha.SetBackground(Color.White);
        captcha.CreateCode();
        Assert.NotNull(captcha);
    }
}
