using Xunit;
using WellTool.Captcha;

namespace WellTool.Captcha.Tests
{
    public class CaptchaTests
    {
        [Fact]
        public void TestLineCaptcha()
        {
            var captcha = CaptchaUtil.CreateLineCaptcha(100, 40);
            captcha.CreateCode();

            Assert.NotNull(captcha.GetCode());
            Assert.NotEmpty(captcha.GetCode());
            Assert.True(captcha.GetCode().Length >= 4);

            var imageBytes = captcha.GetImageBytes();
            Assert.NotNull(imageBytes);
            Assert.True(imageBytes.Length > 0);
        }

        [Fact]
        public void TestCircleCaptcha()
        {
            var captcha = CaptchaUtil.CreateCircleCaptcha(100, 40);
            captcha.CreateCode();

            Assert.NotNull(captcha.GetCode());
            Assert.NotEmpty(captcha.GetCode());

            var image = captcha.GetImage();
            Assert.NotNull(image);
        }

        [Fact]
        public void TestShearCaptcha()
        {
            var captcha = CaptchaUtil.CreateShearCaptcha(100, 40);
            captcha.CreateCode();

            Assert.NotNull(captcha.GetCode());
            Assert.NotEmpty(captcha.GetCode());
        }

        [Fact]
        public void TestGifCaptcha()
        {
            var captcha = CaptchaUtil.CreateGifCaptcha(100, 40);
            captcha.CreateCode();

            Assert.NotNull(captcha.GetCode());
            Assert.NotEmpty(captcha.GetCode());

            var imageBytes = captcha.GetImageBytes();
            Assert.NotNull(imageBytes);
            Assert.True(imageBytes.Length > 0);
        }

        [Fact]
        public void TestVerify()
        {
            var captcha = CaptchaUtil.CreateLineCaptcha(100, 40);
            captcha.CreateCode();

            var code = captcha.GetCode();
            Assert.True(captcha.Verify(code));
            Assert.False(captcha.Verify("wrong"));
        }

        [Fact]
        public void TestGetImageBase64()
        {
            var captcha = CaptchaUtil.CreateLineCaptcha(100, 40);
            captcha.CreateCode();

            var base64 = captcha.GetImageBase64();
            Assert.NotNull(base64);
            Assert.NotEmpty(base64);

            var base64Data = captcha.GetImageBase64Data();
            Assert.StartsWith("data:image/png;base64,", base64Data);
        }
    }
}
