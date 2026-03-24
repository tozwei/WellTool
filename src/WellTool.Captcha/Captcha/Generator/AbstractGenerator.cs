namespace WellTool.Captcha.Generator;

/// <summary>
/// 抽象验证码生成器基类
/// </summary>
public abstract class AbstractGenerator : ICodeGenerator
{
    /// <summary>
    /// 基础字符集合，用于随机获取字符串的字符集合
    /// </summary>
    protected readonly string BaseStr;

    /// <summary>
    /// 验证码长度
    /// </summary>
    protected readonly int Length;

    private const string DefaultBaseStr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    /// <summary>
    /// 构造，使用字母 + 数字做为基础
    /// </summary>
    /// <param name="count">生成验证码长度</param>
    protected AbstractGenerator(int count) : this(DefaultBaseStr, count)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="baseStr">基础字符集合，用于随机获取字符串的字符集合</param>
    /// <param name="length">生成验证码长度</param>
    protected AbstractGenerator(string baseStr, int length)
    {
        BaseStr = baseStr;
        Length = length;
    }

    /// <summary>
    /// 获取验证码长度
    /// </summary>
    /// <returns>验证码长度</returns>
    public virtual int GetLength() => Length;

    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <returns>验证码</returns>
    public abstract string Generate();

    /// <summary>
    /// 验证用户输入的字符串是否与生成的验证码匹配
    /// </summary>
    /// <param name="code">生成的随机验证码</param>
    /// <param name="userInputCode">用户输入的验证码</param>
    /// <returns>是否验证通过</returns>
    public abstract bool Verify(string code, string? userInputCode);
}
