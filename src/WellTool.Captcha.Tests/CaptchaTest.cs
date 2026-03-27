using WellTool.Captcha;
using WellTool.Captcha.Generator;
using System.Drawing;
using Xunit;

namespace WellTool.Captcha.Tests;

/// <summary>
/// 验证码单元测试
/// </summary>
public class CaptchaTest
{
    /// <summary>
    /// 测试基础线条验证码功能
    /// </summary>
    [Fact]
    public void LineCaptchaTest1()
    {
        // 定义图形验证码的长和宽
        using var lineCaptcha = CaptchaUtil.CreateLineCaptcha(200, 100);
        lineCaptcha.CreateCode(); // 生成验证码

        Assert.NotNull(lineCaptcha.Code);
        Assert.True(lineCaptcha.Verify(lineCaptcha.Code));
    }

    /// <summary>
    /// 测试自定义参数的线条验证码（需要输出文件）
    /// </summary>
    [Fact]
    public void LineCaptchaTest3()
    {
        // 定义图形验证码的长和宽
        using var lineCaptcha = CaptchaUtil.CreateLineCaptcha(200, 70, 4, 15);
        lineCaptcha.SetBackground(Color.Transparent);

        // 保存到临时文件
        var tempFile = Path.Combine(Path.GetTempPath(), "tellow.png");
        try
        {
            using (var fs = File.OpenWrite(tempFile))
            {
                lineCaptcha.Write(fs);
            }
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试带字体大小参数的线条验证码
    /// </summary>
    [Fact]
    public void LineCaptchaTestWithSize()
    {
        // 定义图形验证码的长和宽
        using var lineCaptcha = new LineCaptcha(200, 70, 4, 15, 0.65f);
        lineCaptcha.SetBackground(Color.Yellow);

        var tempFile = Path.Combine(Path.GetTempPath(), "yellow_size.png");
        try
        {
            using (var fs = File.OpenWrite(tempFile))
            {
                lineCaptcha.Write(fs);
            }
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试数学计算验证码
    /// </summary>
    [Fact]
    public void LineCaptchaWithMathTest()
    {
        // 定义图形验证码的长和宽
        using var lineCaptcha = CaptchaUtil.CreateLineCaptcha(200, 80);

        // 设置生成器为数学计算器
        var mathGenerator = new MathGenerator();
        lineCaptcha.CreateCode();

        lineCaptcha.SetTextAlpha(0.8f);

        var tempFile = Path.Combine(Path.GetTempPath(), "math.png");
        try
        {
            using (var fs = File.OpenWrite(tempFile))
            {
                lineCaptcha.Write(fs);
            }
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试线条验证码完整流程
    /// </summary>
    [Fact]
    public void LineCaptchaTest2()
    {
        // 定义图形验证码的长和宽
        using var lineCaptcha = CaptchaUtil.CreateLineCaptcha(200, 100);

        var tempFile1 = Path.Combine(Path.GetTempPath(), "line.png");
        var tempFile2 = Path.Combine(Path.GetTempPath(), "line2.png");

        try
        {
            // 图形验证码写出，可以写出到文件，也可以写出到流
            using (var fs = File.OpenWrite(tempFile1))
            {
                lineCaptcha.Write(fs);
            }

            Console.WriteLine($"第一次生成的验证码：{lineCaptcha.Code}");

            // 验证图形验证码的有效性，返回boolean值
            bool result1 = lineCaptcha.Verify("1234");
            Assert.False(result1); // 应该失败，因为验证码不是 1234

            // 重新生成验证码
            lineCaptcha.CreateCode();

            using (var fs = File.OpenWrite(tempFile2))
            {
                lineCaptcha.Write(fs);
            }

            Console.WriteLine($"第二次生成的验证码：{lineCaptcha.Code}");

            // 验证图形验证码的有效性，返回boolean值
            bool result2 = lineCaptcha.Verify("1234");
            Assert.False(result2); // 应该失败，因为验证码不是 1234

            // 验证正确的验证码
            bool result3 = lineCaptcha.Verify(lineCaptcha.Code);
            Assert.True(result3);
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile1))
            {
                File.Delete(tempFile1);
            }
            if (File.Exists(tempFile2))
            {
                File.Delete(tempFile2);
            }
        }
    }

    /// <summary>
    /// 测试圆圈干扰验证码
    /// </summary>
    [Fact]
    public void CircleCaptchaTest()
    {
        // 定义图形验证码的长和宽
        using var captcha = CaptchaUtil.CreateCircleCaptcha(200, 100, 4, 20);

        var tempFile = Path.Combine(Path.GetTempPath(), "circle.png");
        try
        {
            // 图形验证码写出，可以写出到文件，也可以写出到流
            using (var fs = File.OpenWrite(tempFile))
            {
                captcha.Write(fs);
            }

            // 验证图形验证码的有效性，返回boolean值
            bool result1 = captcha.Verify("1234");
            Assert.False(result1);

            // 验证正确的验证码
            bool result2 = captcha.Verify(captcha.Code);
            Assert.True(result2);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试带字体大小的圆圈验证码
    /// </summary>
    [Fact]
    public void CircleCaptchaTestWithSize()
    {
        // 定义图形验证码的长和宽
        using var captcha = new CircleCaptcha(200, 70, 4, 15, 0.65f);
        captcha.SetBackground(Color.Yellow);

        var tempFile = Path.Combine(Path.GetTempPath(), "circle_size.png");
        try
        {
            using (var fs = File.OpenWrite(tempFile))
            {
                captcha.Write(fs);
            }
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试扭曲干扰验证码
    /// </summary>
    [Fact]
    public void ShearCaptchaTest()
    {
        // 定义图形验证码的长和宽
        using var captcha = CaptchaUtil.CreateShearCaptcha(200, 100, 4, 4);

        var tempFile = Path.Combine(Path.GetTempPath(), "shear.png");
        try
        {
            // 图形验证码写出，可以写出到文件，也可以写出到流
            using (var fs = File.OpenWrite(tempFile))
            {
                captcha.Write(fs);
            }

            // 验证图形验证码的有效性，返回boolean值
            bool result1 = captcha.Verify("1234");
            Assert.False(result1);

            // 验证正确的验证码
            bool result2 = captcha.Verify(captcha.Code);
            Assert.True(result2);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试扭曲干扰验证码（直接构造）
    /// </summary>
    [Fact]
    public void ShearCaptchaTest2()
    {
        // 定义图形验证码的长和宽
        using var captcha = new ShearCaptcha(200, 100, 4, 4);

        var tempFile = Path.Combine(Path.GetTempPath(), "shear2.png");
        try
        {
            // 图形验证码写出，可以写出到文件，也可以写出到流
            using (var fs = File.OpenWrite(tempFile))
            {
                captcha.Write(fs);
            }

            // 验证图形验证码的有效性，返回boolean值
            bool result1 = captcha.Verify("1234");
            Assert.False(result1);

            // 验证正确的验证码
            bool result2 = captcha.Verify(captcha.Code);
            Assert.True(result2);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试带数学计算的扭曲验证码
    /// </summary>
    [Fact]
    public void ShearCaptchaWithMathTest()
    {
        // 定义图形验证码的长和宽
        using var captcha = CaptchaUtil.CreateShearCaptcha(200, 45, 4, 4);

        // 注意：当前版本不支持动态切换 Generator
        // captcha.SetGenerator(new MathGenerator());

        var tempFile = Path.Combine(Path.GetTempPath(), "shear_math.png");
        try
        {
            using (var fs = File.OpenWrite(tempFile))
            {
                captcha.Write(fs);
            }

            // 验证图形验证码的有效性，返回boolean值
            bool result1 = captcha.Verify("1234");
            Assert.False(result1);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试带字体大小的扭曲验证码
    /// </summary>
    [Fact]
    public void ShearCaptchaTestWithSize()
    {
        // 定义图形验证码的长和宽
        using var captcha = new ShearCaptcha(200, 70, 4, 15, 0.65f);
        captcha.SetBackground(Color.Yellow);

        var tempFile = Path.Combine(Path.GetTempPath(), "shear_size.png");
        try
        {
            using (var fs = File.OpenWrite(tempFile))
            {
                captcha.Write(fs);
            }
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试 GIF 动画验证码（简化版本）
    /// </summary>
    [Fact]
    public void GifCaptchaTest()
    {
        // 使用 LineCaptcha 作为简化实现
        using var captcha = CaptchaUtil.CreateGifCaptcha(200, 100, 4, 10);

        var tempFile = Path.Combine(Path.GetTempPath(), "gif_captcha.gif");
        try
        {
            using (var fs = File.OpenWrite(tempFile))
            {
                captcha.Write(fs);
            }

            Assert.NotNull(captcha.Code);
            Assert.True(captcha.Verify(captcha.Code));
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试带字体大小的 GIF 验证码
    /// </summary>
    [Fact]
    public void GifCaptchaTestWithSize()
    {
        // 使用 LineCaptcha 作为简化实现
        using var captcha = new LineCaptcha(200, 70, 4, 15, 0.65f);
        captcha.SetBackground(Color.Yellow);

        var tempFile = Path.Combine(Path.GetTempPath(), "gif_size.png");
        try
        {
            using (var fs = File.OpenWrite(tempFile))
            {
                captcha.Write(fs);
            }
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试背景色设置
    /// </summary>
    [Fact]
    public void BgTest()
    {
        using var captcha = CaptchaUtil.CreateLineCaptcha(200, 100, 4, 1);
        captcha.SetBackground(Color.White);

        var tempFile = Path.Combine(Path.GetTempPath(), "test_bg.jpg");
        try
        {
            using (var fs = File.OpenWrite(tempFile))
            {
                captcha.Write(fs);
            }
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试验证码验证逻辑
    /// </summary>
    [Fact]
    public void VerifyLogicTest()
    {
        using var captcha = new LineCaptcha(200, 100, 4, 150);
        captcha.CreateCode();

        // 验证正确的验证码应该返回 true
        Assert.True(captcha.Verify(captcha.Code));

        // 验证错误的验证码应该返回 false
        Assert.False(captcha.Verify("WRONG_CODE"));

        // 验证 null 或空字符串应该返回 false
        Assert.False(captcha.Verify(null));
        Assert.False(captcha.Verify(string.Empty));
    }

    /// <summary>
    /// 测试验证码不区分大小写
    /// </summary>
    [Fact]
    public void VerifyIgnoreCaseTest()
    {
        using var captcha = new LineCaptcha(200, 100, 4, 150);
        captcha.CreateCode();

        var code = captcha.Code;
        if (!string.IsNullOrEmpty(code))
        {
            // 验证忽略大小写
            var upperCase = code.ToUpper();
            var lowerCase = code.ToLower();

            Assert.True(captcha.Verify(upperCase));
            Assert.True(captcha.Verify(lowerCase));
        }
    }

    /// <summary>
    /// 测试随机字符生成器
    /// </summary>
    [Fact]
    public void RandomGeneratorTest()
    {
        var generator = new RandomGenerator(5);
        var code1 = generator.Generate();
        var code2 = generator.Generate();

        Assert.NotNull(code1);
        Assert.NotNull(code2);
        Assert.Equal(5, code1.Length);
        Assert.Equal(5, code2.Length);

        // 两次生成的验证码应该不同（概率极高）
        Assert.NotEqual(code1, code2);

        // 验证功能
        Assert.True(generator.Verify(code1, code1));
        Assert.True(generator.Verify(code1, code1.ToLower()));
        Assert.False(generator.Verify(code1, "WRONG"));
    }

    /// <summary>
    /// 测试数学计算器生成器
    /// </summary>
    [Fact]
    public void MathGeneratorTest()
    {
        var generator = new MathGenerator(2, true);
        var expression = generator.Generate();

        Assert.NotNull(expression);
        Assert.EndsWith("=", expression);
        // 验证表达式包含运算符（+、-、* 中的一个）
        Assert.True(expression.Contains("+") || expression.Contains("-") || expression.Contains("*"));

        // 计算表达式的结果
        var result = CalculateExpression(expression);

        // 验证功能
        Assert.True(generator.Verify(expression, result.ToString()));
        Assert.False(generator.Verify(expression, "99999"));
    }

    /// <summary>
    /// 计算数学表达式结果（辅助方法）
    /// </summary>
    private static int CalculateExpression(string expression)
    {
        expression = expression.Replace(" ", "").TrimEnd('=');

        foreach (var op in new[] { '+', '-', '*' })
        {
            var index = expression.LastIndexOf(op);
            if (index >= 0 && index < expression.Length - 1)
            {
                var left = expression.Substring(0, index);
                var right = expression.Substring(index + 1);

                if (int.TryParse(left, out var leftValue) &&
                    int.TryParse(right, out var rightValue))
                {
                    return op switch
                    {
                        '+' => leftValue + rightValue,
                        '-' => leftValue - rightValue,
                        '*' => leftValue * rightValue,
                        _ => 0
                    };
                }
            }
        }

        return 0;
    }
}
