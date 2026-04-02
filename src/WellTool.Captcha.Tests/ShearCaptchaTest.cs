using System;
using System.Drawing;
using Xunit;

namespace WellTool.Captcha.Tests
{
    /// <summary>
    /// ShearCaptcha 测试
    /// </summary>
    public class ShearCaptchaTest
    {
        private ShearCaptcha _captcha;

        public ShearCaptchaTest()
        {
            // 初始化 ShearCaptcha 实例
            _captcha = new ShearCaptcha(200, 100);
        }

        [Fact]
        public void TestConstructor()
        {
            Assert.NotNull(_captcha);
        }

        [Fact]
        public void TestCreateImage()
        {
            string code = "ABCD";
            Image image = _captcha.CreateImage(code);
            
            Assert.NotNull(image);
        }

        [Fact]
        public void TestCaptchaSize()
        {
            ShearCaptcha captcha = new ShearCaptcha(300, 150);
            
            Assert.NotNull(captcha);
        }

        [Fact]
        public void TestLineCaptcha()
        {
            LineCaptcha lineCaptcha = new LineCaptcha(200, 100, 4, 10);
            lineCaptcha.CreateCode();
            Image image = lineCaptcha.CreateImage("ABCD");
            
            Assert.NotNull(image);
        }

        [Fact]
        public void TestCircleCaptcha()
        {
            CircleCaptcha circleCaptcha = new CircleCaptcha(200, 100, 4, 10);
            circleCaptcha.CreateCode();
            Image image = circleCaptcha.CreateImage("ABCD");
            
            Assert.NotNull(image);
        }

        [Fact]
        public void TestShearCaptchaWithCodeCount()
        {
            ShearCaptcha captcha = new ShearCaptcha(200, 100, 6);
            Assert.NotNull(captcha);
        }

        [Fact]
        public void TestShearCaptchaWithThickness()
        {
            ShearCaptcha captcha = new ShearCaptcha(200, 100, 4, 6);
            Assert.NotNull(captcha);
        }
    }
}
