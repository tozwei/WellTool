namespace WellTool.Captcha.Tests;

using Well.Captcha;

public class CaptchaUtilTest
{
    [Fact]
    public void CreateLineCaptchaTest()
    {
        var captcha = CaptchaUtil.CreateLineCaptcha(200, 100);
        Assert.NotNull(captcha);
        Assert.Equal(200, captcha.Width);
        Assert.Equal(100, captcha.Height);
    }

    [Fact]
    public void CreateCircleCaptchaTest()
    {
        var captcha = CaptchaUtil.CreateCircleCaptcha(200, 100);
        Assert.NotNull(captcha);
        Assert.Equal(200, captcha.Width);
        Assert.Equal(100, captcha.Height);
    }

    [Fact]
    public void CreateShearCaptchaTest()
    {
        var captcha = CaptchaUtil.CreateShearCaptcha(200, 100);
        Assert.NotNull(captcha);
        Assert.Equal(200, captcha.Width);
        Assert.Equal(100, captcha.Height);
    }

    [Fact]
    public void CreateGifCaptchaTest()
    {
        var captcha = CaptchaUtil.CreateGifCaptcha(200, 100);
        Assert.NotNull(captcha);
        Assert.Equal(200, captcha.Width);
        Assert.Equal(100, captcha.Height);
    }

    [Fact]
    public void CreateSpecCaptchaTest()
    {
        var captcha = CaptchaUtil.CreateSpecCaptcha(200, 100);
        Assert.NotNull(captcha);
    }

    [Fact]
    public void GenerateCodeTest()
    {
        var code = CaptchaUtil.GenerateCode(4);
        Assert.NotNull(code);
        Assert.Equal(4, code.Length);
    }
}
