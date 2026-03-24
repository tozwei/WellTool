using System.Drawing;
using System.Drawing.Imaging;
using WellTool.Captcha.Generator;

namespace WellTool.Captcha;

/// <summary>
/// 抽象验证码<br/>
/// 抽象验证码实现了验证码字符串的生成、验证，验证码图片的写出<br/>
/// 实现类通过实现 CreateImage(string) 方法生成图片对象
/// </summary>
public abstract class AbstractCaptcha : ICaptcha, IDisposable
{
    /// <summary>
    /// 图片的宽度
    /// </summary>
    protected int Width;

    /// <summary>
    /// 图片的高度
    /// </summary>
    protected int Height;

    /// <summary>
    /// 验证码干扰元素个数
    /// </summary>
    protected int InterfereCount;

    /// <summary>
    /// 字体
    /// </summary>
    protected Font? Font;

    /// <summary>
    /// 验证码
    /// </summary>
    protected string? _code;

    /// <summary>
    /// 验证码图片字节
    /// </summary>
    protected byte[]? _imageBytes;

    /// <summary>
    /// 验证码生成器
    /// </summary>
    protected ICodeGenerator? Generator;

    /// <summary>
    /// 背景色
    /// </summary>
    protected Color Background = Color.White;

    /// <summary>
    /// 文字透明度
    /// </summary>
    protected float TextAlpha = 1.0f;

    private bool _disposed;

    /// <summary>
    /// 构造，使用随机验证码生成器生成验证码
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="codeCount">字符个数</param>
    /// <param name="interfereCount">验证码干扰元素个数</param>
    protected AbstractCaptcha(int width, int height, int codeCount, int interfereCount)
        : this(width, height, new RandomGenerator(codeCount), interfereCount)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="generator">验证码生成器</param>
    /// <param name="interfereCount">验证码干扰元素个数</param>
    protected AbstractCaptcha(int width, int height, ICodeGenerator generator, int interfereCount)
    {
        Width = width;
        Height = height;
        Generator = generator;
        InterfereCount = interfereCount;
        // 字体高度设为验证码高度 -2，留边距
        Font = new Font(FontFamily.GenericSansSerif, (float)(Height * 0.75), FontStyle.Regular);
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="generator">验证码生成器</param>
    /// <param name="interfereCount">验证码干扰元素个数</param>
    /// <param name="fontSizeFactor">字体大小因子（相对于高度的倍数）</param>
    protected AbstractCaptcha(int width, int height, ICodeGenerator generator, int interfereCount, float fontSizeFactor)
    {
        Width = width;
        Height = height;
        Generator = generator;
        InterfereCount = interfereCount;
        Font = new Font(FontFamily.GenericSansSerif, Height * fontSizeFactor, FontStyle.Regular);
    }

    /// <summary>
    /// 获取验证码的文字内容
    /// </summary>
    /// <returns>验证码文字内容</returns>
    public string? Code => _code;

    /// <summary>
    /// 创建验证码，实现类需同时生成随机验证码字符串和验证码图片
    /// </summary>
    public virtual void CreateCode()
    {
        if (Generator == null)
        {
            throw new InvalidOperationException("Generator is not set");
        }

        _code = Generator.Generate();
        _imageBytes = GenerateImageBytes(_code);
    }

    /// <summary>
    /// 验证验证码是否正确，忽略大小写
    /// </summary>
    /// <param name="userInputCode">用户输入的验证码</param>
    /// <returns>是否与生成的一致</returns>
    public virtual bool Verify(string? userInputCode)
    {
        if (string.IsNullOrWhiteSpace(userInputCode) || string.IsNullOrEmpty(Code))
        {
            return false;
        }

        if (Generator != null)
        {
            return Generator.Verify(Code, userInputCode);
        }

        return string.Equals(Code, userInputCode, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 将验证码写出到目标流中
    /// </summary>
    /// <param name="outStream">目标流</param>
    public virtual void Write(Stream outStream)
    {
        if (_imageBytes == null)
        {
            CreateCode();
        }

        if (_imageBytes != null)
        {
            outStream.Write(_imageBytes, 0, _imageBytes.Length);
        }
    }

    /// <summary>
    /// 获取验证码图片字节数组
    /// </summary>
    /// <returns>图片字节数组</returns>
    public byte[]? GetImageBytes() => _imageBytes;

    /// <summary>
    /// 设置背景颜色
    /// </summary>
    /// <param name="color">背景颜色</param>
    /// <returns>this</returns>
    public AbstractCaptcha SetBackground(Color color)
    {
        Background = color;
        return this;
    }

    /// <summary>
    /// 设置文字透明度
    /// </summary>
    /// <param name="alpha">透明度（0.0-1.0）</param>
    /// <returns>this</returns>
    public AbstractCaptcha SetTextAlpha(float alpha)
    {
        TextAlpha = Math.Max(0, Math.Min(1, alpha));
        return this;
    }

    /// <summary>
    /// 创建验证码图片
    /// </summary>
    /// <param name="code">验证码文本</param>
    /// <returns>图片对象</returns>
    public abstract Image? CreateImage(string code);

    /// <summary>
    /// 生成图片字节数组
    /// </summary>
    /// <param name="code">验证码文本</param>
    /// <returns>图片字节数组</returns>
    protected virtual byte[] GenerateImageBytes(string code)
    {
        using var image = CreateImage(code);
        if (image == null)
        {
            return Array.Empty<byte>();
        }

        using var ms = new MemoryStream();
        image.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Font?.Dispose();
            }

            _disposed = true;
        }
    }
}
