namespace WellTool.Captcha.Tests;

using Well.Captcha;

public class GifCaptchaUtilTest
{
    [Fact]
    public void CreateTest()
    {
        var captcha = CaptchaUtil.CreateGifCaptcha(200, 100, 4);
        Assert.NotNull(captcha);
        Assert.Equal(200, captcha.Width);
        Assert.Equal(100, captcha.Height);
    }

    [Fact]
    public void CreateAndVerifyTest()
    {
        var captcha = new GifCaptcha(200, 100, 4);
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
        var captcha = new GifCaptcha(200, 100, 4);
        captcha.CreateCode();
        var base64 = captcha.ToBase64();
        Assert.NotNull(base64);
        Assert.StartsWith("data:image/gif", base64);
    }

    [Fact]
    public void CreateFramesTest()
    {
        var captcha = new GifCaptcha(200, 100, 4);
        captcha.CreateCode();
        var frames = captcha.Frames;
        Assert.NotNull(frames);
    }
}
