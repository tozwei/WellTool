using System.Drawing;

namespace WellTool.Captcha;

/// <summary>
/// 验证码接口，提供验证码对象接口定义
/// </summary>
public interface ICaptcha
{
    /// <summary>
    /// 创建验证码，实现类需同时生成随机验证码字符串和验证码图片
    /// </summary>
    void CreateCode();

    /// <summary>
    /// 获取验证码的文字内容
    /// </summary>
    /// <returns>验证码文字内容</returns>
    string? Code { get; }

    /// <summary>
    /// 验证验证码是否正确，建议忽略大小写
    /// </summary>
    /// <param name="userInputCode">用户输入的验证码</param>
    /// <returns>是否与生成的一致</returns>
    bool Verify(string? userInputCode);

    /// <summary>
    /// 将验证码写出到目标流中
    /// </summary>
    /// <param name="outStream">目标流</param>
    void Write(Stream outStream);

    /// <summary>
    /// 创建验证码图片（无参数版本）
    /// </summary>
    /// <returns>图片对象</returns>
    Image? CreateImage();

    /// <summary>
    /// 转换为 Base64 字符串
    /// </summary>
    /// <returns>Base64 字符串</returns>
    string ToBase64();
}
