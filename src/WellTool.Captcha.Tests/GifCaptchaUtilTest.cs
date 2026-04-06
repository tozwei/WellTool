namespace WellTool.Captcha.Tests;

using WellTool.Captcha;
using Xunit;

public class GifCaptchaUtilTest
{
    [Fact]
    public void CreateTest()
    {
        var captcha = CaptchaUtil.CreateGifCaptcha(200, 100, 4, 10);
        Assert.NotNull(captcha);
        Assert.Equal(200, captcha.Width);
        Assert.Equal(100, captcha.Height);
    }

    [Fact]
    public void CreateAndVerifyTest()
    {
        // GifCaptcha 实际使用 LineCaptcha 实现
        var captcha = CaptchaUtil.CreateGifCaptcha(200, 100, 4, 10);
        captcha.CreateCode();
        var code = captcha.Code;
        Assert.NotNull(code);
        Assert.Equal(4, code.Length);

        var verified = captcha.Verify(code);
        Assert.True(verified);
    }

    [Fact]
    public void ToBase64Test()
    {
        // GifCaptcha 实际使用 LineCaptcha 实现，返回 PNG 格式
        var captcha = CaptchaUtil.CreateGifCaptcha(200, 100, 4, 10);
        captcha.CreateCode();
        var base64 = captcha.ToBase64();
        Assert.NotNull(base64);
        Assert.StartsWith("data:image/", base64);
    }
}