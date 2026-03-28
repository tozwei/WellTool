using System;
using System.IO;
using Xunit;
using XAssert = Xunit.Assert;
using WellTool.Core.Img;

namespace WellTool.Core.Tests
{
    public class ImgTests
    {
        [Fact]
        public void ColorUtilTest()
        {
            // 测试颜色转换功能
            var hexColor = ColorUtil.ToHex(255, 0, 0);
            XAssert.Equal("#FF0000", hexColor);

            var rgb = ColorUtil.FromHex("#FF0000");
            XAssert.Equal(255, rgb.r);
            XAssert.Equal(0, rgb.g);
            XAssert.Equal(0, rgb.b);

            var hexArgb = ColorUtil.ToHexArgb(255, 255, 0, 0);
            XAssert.Equal("#FFFF0000", hexArgb);

            var argb = ColorUtil.FromHexArgb("#FFFF0000");
            XAssert.Equal(255, argb.a);
            XAssert.Equal(255, argb.r);
            XAssert.Equal(0, argb.g);
            XAssert.Equal(0, argb.b);
        }

#if WINDOWS
        [Fact]
        public void FontUtilTest()
        {
            // 测试字体工具功能
            var font = FontUtil.CreateFont("Arial", 12);
            XAssert.NotNull(font);
            XAssert.Equal("Arial", font.FontFamily.Name);
            XAssert.Equal(12, font.Size);
        }

        
        [Fact]
        public void ImgUtilScaleTest()
        {
            // 创建一个简单的测试图像
            using (var originalImage = new System.Drawing.Bitmap(100, 100))
            {
                // 测试缩放功能
                var scaledImage = ImgUtil.Scale(originalImage, 50, 50);
                XAssert.NotNull(scaledImage);
                XAssert.Equal(50, scaledImage.Width);
                XAssert.Equal(50, scaledImage.Height);
                scaledImage.Dispose();
            }
        }

        [Fact]
        public void ImgUtilRotateTest()
        {
            // 创建一个简单的测试图像
            using (var originalImage = new System.Drawing.Bitmap(100, 100))
            {
                // 测试旋转功能
                var rotatedImage = ImgUtil.Rotate(originalImage, 90);
                XAssert.NotNull(rotatedImage);
                rotatedImage.Dispose();
            }
        }

        [Fact]
        public void ImgUtilFlipTest()
        {
            // 创建一个简单的测试图像
            using (var originalImage = new System.Drawing.Bitmap(100, 100))
            {
                // 测试翻转功能
                var flippedImage = ImgUtil.Flip(originalImage, ImgUtil.FlipType.Horizontal);
                XAssert.NotNull(flippedImage);
                flippedImage.Dispose();
            }
        }
#endif
    }
}