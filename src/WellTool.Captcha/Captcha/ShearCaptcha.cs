using System.Drawing;
using System.Drawing.Imaging;
using WellTool.Captcha.Generator;

namespace WellTool.Captcha;

/// <summary>
/// 扭曲干扰验证码
/// </summary>
public class ShearCaptcha : AbstractCaptcha
{
    private static readonly Random Random = new();

    /// <summary>
    /// 构造，默认 5 位验证码
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    public ShearCaptcha(int width, int height) : base(width, height, 5, 4)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="codeCount">字符个数</param>
    public ShearCaptcha(int width, int height, int codeCount) : base(width, height, codeCount, 4)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="codeCount">字符个数</param>
    /// <param name="thickness">干扰线宽度</param>
    public ShearCaptcha(int width, int height, int codeCount, int thickness)
        : base(width, height, codeCount, thickness)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="generator">验证码生成器</param>
    /// <param name="interfereCount">验证码干扰元素个数</param>
    public ShearCaptcha(int width, int height, ICodeGenerator generator, int interfereCount)
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
    public ShearCaptcha(int width, int height, int codeCount, int interfereCount, float fontSizeFactor)
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

            // 画字符串
            DrawString(g, code);

            // 扭曲
            Shear(g, Width, Height, Background);

            // 画干扰线
            DrawInterfere(g, 0, Random.Next(Height) + 1, Width, Random.Next(Height) + 1, InterfereCount);
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
    /// 扭曲图像
    /// </summary>
    /// <param name="g">Graphics 对象</param>
    /// <param name="w1">宽度</param>
    /// <param name="h1">高度</param>
    /// <param name="color">背景颜色</param>
    private void Shear(Graphics g, int w1, int h1, Color color)
    {
        ShearX(g, w1, h1, color);
        ShearY(g, w1, h1, color);
    }

    /// <summary>
    /// X 坐标扭曲
    /// </summary>
    private void ShearX(Graphics g, int w1, int h1, Color color)
    {
        var period = Random.Next(w1);
        var phase = Random.Next(2);

        for (int i = 0; i < h1; i++)
        {
            var d = (period >> 1) * Math.Sin(i / (double)period + (2 * Math.PI * phase) / 2);
            g.CopyArea(0, i, w1, 1, (int)d, 0, color);
        }
    }

    /// <summary>
    /// Y 坐标扭曲
    /// </summary>
    private void ShearY(Graphics g, int w1, int h1, Color color)
    {
        var period = Random.Next(h1 / 2);
        var phase = Random.Next(2);

        for (int i = 0; i < w1; i++)
        {
            var d = (period >> 1) * Math.Sin(i / (double)period + (2 * Math.PI * phase) / 2);
            g.CopyArea(i, 0, 1, h1, 0, (int)d, color);
        }
    }

    /// <summary>
    /// 绘制干扰线
    /// </summary>
    /// <param name="g">Graphics 对象</param>
    /// <param name="x1">起点 X</param>
    /// <param name="y1">起点 Y</param>
    /// <param name="x2">终点 X</param>
    /// <param name="y2">终点 Y</param>
    /// <param name="count">线条数量</param>
    private void DrawInterfere(Graphics g, int x1, int y1, int x2, int y2, int count)
    {
        for (int i = 0; i < count; i++)
        {
            using var pen = new Pen(RandomColor(), Random.Next(1, 3));

            var curX1 = Random.Next(Width);
            var curY1 = Random.Next(Height);
            var curX2 = Random.Next(Width);
            var curY2 = Random.Next(Height);

            g.DrawLine(pen, curX1, curY1, curX2, curY2);
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

// Graphics 扩展方法
internal static class GraphicsExtensions
{
    public static void CopyArea(this Graphics g, int x, int y, int width, int height, int dx, int dy, Color bgColor)
    {
        try
        {
            using var bitmap = new Bitmap(width, height);
            using var tempG = Graphics.FromImage(bitmap);
            tempG.Clear(bgColor);

            g.DrawImage(bitmap, x + dx, y + dy);
        }
        catch
        {
            // 忽略异常
        }
    }
}
