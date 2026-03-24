using WellTool.Captcha.Generator;

namespace WellTool.Captcha.Examples;

/// <summary>
/// 验证码使用示例
/// </summary>
public class CaptchaExamples
{
    /// <summary>
    /// 创建线条干扰验证码
    /// </summary>
    public void CreateLineCaptchaExample()
    {
        // 创建验证码，130x48 像素，5 位字符，150 条干扰线
        using var captcha = new LineCaptcha(130, 48, 5, 150);

        // 生成验证码
        captcha.CreateCode();

        // 获取验证码文本
        string? code = captcha.Code;
        Console.WriteLine($"验证码：{code}");

        // 保存到文件
        using (var fs = File.OpenWrite("line_captcha.png"))
        {
            captcha.Write(fs);
        }

        // 验证用户输入
        bool isValid = captcha.Verify(code);
        Console.WriteLine($"验证结果：{isValid}");
    }

    /// <summary>
    /// 创建圆圈干扰验证码
    /// </summary>
    public void CreateCircleCaptchaExample()
    {
        // 创建验证码，130x48 像素，5 位字符，15 个干扰圈
        using var captcha = new CircleCaptcha(130, 48, 5, 15);

        // 生成验证码
        captcha.CreateCode();

        // 获取验证码文本
        string? code = captcha.Code;
        Console.WriteLine($"验证码：{code}");

        // 保存到内存流
        using var ms = new MemoryStream();
        captcha.Write(ms);

        // 验证用户输入
        bool isValid = captcha.Verify(code);
        Console.WriteLine($"验证结果：{isValid}");
    }

    /// <summary>
    /// 创建扭曲干扰验证码
    /// </summary>
    public void CreateShearCaptchaExample()
    {
        // 创建验证码，130x48 像素，5 位字符
        using var captcha = new ShearCaptcha(130, 48, 5, 4);

        // 生成验证码
        captcha.CreateCode();

        // 获取验证码文本
        string? code = captcha.Code;
        Console.WriteLine($"验证码：{code}");

        // 保存到字节数组
        byte[]? imageBytes = captcha.GetImageBytes();

        // 验证用户输入
        bool isValid = captcha.Verify(code);
        Console.WriteLine($"验证结果：{isValid}");
    }

    /// <summary>
    /// 使用数学计算验证码
    /// </summary>
    public void CreateMathCaptchaExample()
    {
        // 使用数学计算器生成器
        var generator = new MathGenerator(2, true); // 2 位数，允许负数

        // 创建验证码
        using var captcha = new LineCaptcha(130, 48, generator, 150);

        // 生成验证码
        captcha.CreateCode();

        // 获取验证码表达式（如："12+34="）
        string? expression = captcha.Code;
        Console.WriteLine($"计算题：{expression}");

        // 计算正确答案
        int answer = Calculate(expression!);
        Console.WriteLine($"正确答案：{answer}");

        // 验证用户输入
        bool isValid = captcha.Verify(answer.ToString());
        Console.WriteLine($"验证结果：{isValid}");
    }

    /// <summary>
    /// 使用工具类创建验证码
    /// </summary>
    public void UseCaptchaUtilExample()
    {
        // 使用工具类快速创建验证码
        using var captcha = CaptchaUtil.CreateLineCaptcha(130, 48);

        // 生成并保存
        captcha.CreateCode();

        using var fs = File.OpenWrite("captcha.png");
        captcha.Write(fs);

        Console.WriteLine($"验证码：{captcha.Code}");
    }

    /// <summary>
    /// 自定义背景色和透明度
    /// </summary>
    public void CustomStyleExample()
    {
        using var captcha = new LineCaptcha(130, 48, 5, 150);

        // 设置背景色为浅灰色
        captcha.SetBackground(System.Drawing.Color.LightGray);

        // 设置文字透明度
        captcha.SetTextAlpha(0.8f);

        // 生成验证码
        captcha.CreateCode();

        using var fs = File.OpenWrite("custom_captcha.png");
        captcha.Write(fs);

        Console.WriteLine($"验证码：{captcha.Code}");
    }

    /// <summary>
    /// Web API 中使用验证码（ASP.NET Core 示例）
    /// </summary>
    public async Task WebApiExample(/*IHttpContextAccessor httpContext*/)
    {
        // 在 Web API 中，可以这样使用：
        /*
        [HttpGet("captcha")]
        public IActionResult GetCaptcha()
        {
            using var captcha = CaptchaUtil.CreateLineCaptcha(130, 48);
            captcha.CreateCode();
            
            // 将验证码文本存入 Session
            HttpContext.Session.SetString("CaptchaCode", captcha.Code ?? "");
            
            // 返回图片
            var bytes = captcha.GetImageBytes();
            return File(bytes, "image/png");
        }
        
        [HttpPost("verify")]
        public IActionResult Verify(string code)
        {
            var storedCode = HttpContext.Session.GetString("CaptchaCode");
            using var captcha = CaptchaUtil.CreateLineCaptcha(130, 48);
            
            // 这里应该从存储中获取之前的验证码进行验证
            // 简化示例：
            bool isValid = captcha.Verify(code);
            
            return Ok(new { Success = isValid });
        }
        */
    }

    /// <summary>
    /// 计算数学表达式结果
    /// </summary>
    private static int Calculate(string expression)
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
