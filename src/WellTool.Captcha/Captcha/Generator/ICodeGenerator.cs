namespace WellTool.Captcha.Generator;

/// <summary>
/// 验证码文字生成器接口
/// </summary>
public interface ICodeGenerator
{
    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <returns>验证码</returns>
    string Generate();

    /// <summary>
    /// 验证用户输入的字符串是否与生成的验证码匹配
    /// </summary>
    /// <param name="code">生成的随机验证码</param>
    /// <param name="userInputCode">用户输入的验证码</param>
    /// <returns>是否验证通过</returns>
    bool Verify(string code, string? userInputCode);
}
