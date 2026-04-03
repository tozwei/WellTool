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
    }
}