using System.Drawing;
using System.Drawing.Imaging;
using WellTool.Captcha.Generator;

namespace WellTool.Captcha;

/// <summary>
/// 使用干扰线方式生成的图形验证码
/// </summary>
public class LineCaptcha : AbstractCaptcha
{
    private static readonly Random Random = new();

    /// <summary>
    /// 构造，默认 5 位验证码，150 条干扰线
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    public LineCaptcha(int width, int height) : base(width, height, 5, 150)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="codeCount">字符个数</param>
    /// <param name="lineCount">干扰线条数</param>
    public LineCaptcha(int width, int height, int codeCount, int lineCount)
        : base(width, height, codeCount, lineCount)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="generator">验证码生成器</param>
    /// <param name="interfereCount">验证码干扰元素个数</param>
    public LineCaptcha(int width, int height, ICodeGenerator generator, int interfereCount)
        : base(width, height, generator, interfereCount)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="codeCount">字符个数</param>
    /// <param name="interfereCount">验证码干扰元素个数</param>
    /// <param name="fontSizeFactor">字体大小因子</param>
    public LineCaptcha(int width, int height, int codeCount, int interfereCount, float fontSizeFactor)
        : base(width, height, new RandomGenerator(codeCount), interfereCount, fontSizeFactor)
    {
    }

    /// <summary>
    /// 创建验证码图片
    /// </summary>
    /// <param name="code">验证码文本</param>
    /// <returns>图片对象</returns>
    public override Image? CreateImage(string code)
    {
        var image = new Bitmap(Width, Height, PixelFormat.Format32bppRgb);
        using var g = Graphics.FromImage(image);

        try
        {
            // 绘制背景
            g.Clear(Background);

            // 干扰线
            DrawInterfere(g);

            // 字符串
            DrawString(g, code);
        }
        finally
        {
            g.Dispose();
        }

        return image;
    }

    /// <summary>
    /// 绘制字符串
    /// </summary>
    /// <param name="g">Graphics 对象</param>
    /// <param name="code">验证码文本</param>
    private void DrawString(Graphics g, string code)
    {
        if (Font == null) return;

        var charWidth = Width / (float)code.Length;

        for (int i = 0; i < code.Length; i++)
        {
            using var brush = new SolidBrush(RandomColor());

            var x = i * charWidth + Random.Next((int)(charWidth * 0.1), (int)(charWidth * 0.3));
            var y = Random.Next((int)(Height * 0.15), (int)(Height * 0.35));

            g.DrawString(code[i].ToString(), Font, brush, x, y);
        }
    }

    /// <summary>
    /// 绘制干扰线
    /// </summary>
    /// <param name="g">Graphics 对象</param>
    private void DrawInterfere(Graphics g)
    {
        for (int i = 0; i < InterfereCount; i++)
        {
            using var pen = new Pen(RandomColor(), Random.Next(1, 3));

            var x1 = Random.Next(Width);
            var y1 = Random.Next(Height);
            var x2 = Random.Next(Width);
            var y2 = Random.Next(Height);

            g.DrawLine(pen, x1, y1, x2, y2);
        }
    }

    /// <summary>
    /// 生成随机颜色
    /// </summary>
    /// <returns>随机颜色</returns>
    private static Color RandomColor()
    {
        return Color.FromArgb(
            Random.Next(50, 200),
            Random.Next(50, 200),
            Random.Next(50, 200)
        );
    }
}
