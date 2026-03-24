using WellTool.Captcha.Generator;

namespace WellTool.Captcha;

/// <summary>
/// 图形验证码工具类
/// </summary>
public static class CaptchaUtil
{
    /// <summary>
    /// 创建线干扰的验证码，默认 5 位验证码，150 条干扰线
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <returns>LineCaptcha</returns>
    public static LineCaptcha CreateLineCaptcha(int width, int height)
    {
        return new LineCaptcha(width, height);
    }

    /// <summary>
    /// 创建线干扰的验证码
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="codeCount">字符个数</param>
    /// <param name="lineCount">干扰线条数</param>
    /// <returns>LineCaptcha</returns>
    public static LineCaptcha CreateLineCaptcha(int width, int height, int codeCount, int lineCount)
    {
        return new LineCaptcha(width, height, codeCount, lineCount);
    }

    /// <summary>
    /// 创建线干扰的验证码
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="generator">验证码生成器</param>
    /// <param name="lineCount">干扰线条数</param>
    /// <returns>LineCaptcha</returns>
    public static LineCaptcha CreateLineCaptcha(int width, int height, ICodeGenerator generator, int lineCount)
    {
        return new LineCaptcha(width, height, generator, lineCount);
    }

    // ------------------------- lineCaptcha end -------------------------

    /// <summary>
    /// 创建圆圈干扰的验证码，默认 5 位验证码，15 个干扰圈
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <returns>CircleCaptcha</returns>
    public static CircleCaptcha CreateCircleCaptcha(int width, int height)
    {
        return new CircleCaptcha(width, height);
    }

    /// <summary>
    /// 创建圆圈干扰的验证码
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="codeCount">字符个数</param>
    /// <param name="circleCount">干扰圆圈条数</param>
    /// <returns>CircleCaptcha</returns>
    public static CircleCaptcha CreateCircleCaptcha(int width, int height, int codeCount, int circleCount)
    {
        return new CircleCaptcha(width, height, codeCount, circleCount);
    }

    /// <summary>
    /// 创建圆圈干扰的验证码
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="generator">验证码生成器</param>
    /// <param name="circleCount">干扰圆圈条数</param>
    /// <returns>CircleCaptcha</returns>
    public static CircleCaptcha CreateCircleCaptcha(int width, int height, ICodeGenerator generator, int circleCount)
    {
        return new CircleCaptcha(width, height, generator, circleCount);
    }

    // ------------------------- circleCaptcha end -------------------------

    /// <summary>
    /// 创建扭曲干扰的验证码，默认 5 位验证码
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <returns>ShearCaptcha</returns>
    public static ShearCaptcha CreateShearCaptcha(int width, int height)
    {
        return new ShearCaptcha(width, height);
    }

    /// <summary>
    /// 创建扭曲干扰的验证码
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="codeCount">字符个数</param>
    /// <param name="thickness">干扰线宽度</param>
    /// <returns>ShearCaptcha</returns>
    public static ShearCaptcha CreateShearCaptcha(int width, int height, int codeCount, int thickness)
    {
        return new ShearCaptcha(width, height, codeCount, thickness);
    }

    /// <summary>
    /// 创建扭曲干扰的验证码
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="generator">验证码生成器</param>
    /// <param name="thickness">干扰线宽度</param>
    /// <returns>ShearCaptcha</returns>
    public static ShearCaptcha CreateShearCaptcha(int width, int height, ICodeGenerator generator, int thickness)
    {
        return new ShearCaptcha(width, height, generator, thickness);
    }

    // ------------------------- shearCaptcha end -------------------------

    /// <summary>
    /// 创建 GIF 动画验证码（简化版本，返回静态验证码）
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <returns>GifCaptcha（使用 LineCaptcha 实现）</returns>
    public static LineCaptcha CreateGifCaptcha(int width, int height)
    {
        // 简化实现，实际项目中可以实现完整的 GIF 动画验证码
        return new LineCaptcha(width, height);
    }

    /// <summary>
    /// 创建 GIF 动画验证码
    /// </summary>
    /// <param name="width">图片宽</param>
    /// <param name="height">图片高</param>
    /// <param name="codeCount">字符个数</param>
    /// <param name="frameCount">帧数</param>
    /// <returns>GifCaptcha（使用 LineCaptcha 实现）</returns>
    public static LineCaptcha CreateGifCaptcha(int width, int height, int codeCount, int frameCount)
    {
        return new LineCaptcha(width, height, codeCount, frameCount);
    }
}
