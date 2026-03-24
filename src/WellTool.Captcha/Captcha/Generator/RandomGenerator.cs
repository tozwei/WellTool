namespace WellTool.Captcha.Generator;

/// <summary>
/// 随机字符验证码生成器
/// 可以通过传入的基础集合和长度随机生成验证码字符
/// </summary>
public class RandomGenerator : AbstractGenerator
{
    private static readonly Random Random = new();

    /// <summary>
    /// 构造，使用字母 + 数字做为基础
    /// </summary>
    /// <param name="count">生成验证码长度</param>
    public RandomGenerator(int count) : base(count)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="baseStr">基础字符集合，用于随机获取字符串的字符集合</param>
    /// <param name="length">生成验证码长度</param>
    public RandomGenerator(string baseStr, int length) : base(baseStr, length)
    {
    }

    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <returns>验证码</returns>
    public override string Generate()
    {
        var chars = BaseStr.ToCharArray();
        var result = new char[Length];

        for (int i = 0; i < Length; i++)
        {
            result[i] = chars[Random.Next(chars.Length)];
        }

        return new string(result);
    }

    /// <summary>
    /// 验证用户输入的字符串是否与生成的验证码匹配（忽略大小写）
    /// </summary>
    /// <param name="code">生成的随机验证码</param>
    /// <param name="userInputCode">用户输入的验证码</param>
    /// <returns>是否验证通过</returns>
    public override bool Verify(string code, string? userInputCode)
    {
        if (string.IsNullOrWhiteSpace(userInputCode))
        {
            return false;
        }

        return string.Equals(code, userInputCode, StringComparison.OrdinalIgnoreCase);
    }
}
