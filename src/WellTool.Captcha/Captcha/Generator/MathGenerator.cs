namespace WellTool.Captcha.Generator;

/// <summary>
/// 数字计算验证码生成器
/// </summary>
public class MathGenerator : ICodeGenerator
{
    private static readonly Random Random = new();
    private const string Operators = "+-*";

    /// <summary>
    /// 参与计算数字最大长度
    /// </summary>
    private readonly int _numberLength;

    /// <summary>
    /// 计算结果是否允许负数
    /// </summary>
    private readonly bool _resultHasNegativeNumber;

    /// <summary>
    /// 构造，默认 2 位数，允许负数
    /// </summary>
    public MathGenerator() : this(2, true)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="resultHasNegativeNumber">结果是否允许负数</param>
    public MathGenerator(bool resultHasNegativeNumber) : this(2, resultHasNegativeNumber)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="numberLength">参与计算最大数字位数</param>
    public MathGenerator(int numberLength) : this(numberLength, true)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="numberLength">参与计算最大数字位数</param>
    /// <param name="resultHasNegativeNumber">结果是否允许负数</param>
    public MathGenerator(int numberLength, bool resultHasNegativeNumber)
    {
        _numberLength = numberLength;
        _resultHasNegativeNumber = resultHasNegativeNumber;
    }

    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <returns>验证码表达式</returns>
    public string Generate()
    {
        var limit = GetLimit();
        var op = Operators[Random.Next(Operators.Length)];

        var number1 = Random.Next(limit);
        var number2 = op == '-' && !_resultHasNegativeNumber && number1 > 0
            ? Random.Next(0, number1)
            : Random.Next(limit);

        var num1Str = number1.ToString().PadRight(_numberLength);
        var num2Str = number2.ToString().PadRight(_numberLength);

        return $"{num1Str}{op}{num2Str}=";
    }

    /// <summary>
    /// 验证用户输入的计算结果是否正确
    /// </summary>
    /// <param name="code">生成的随机验证码表达式</param>
    /// <param name="userInputCode">用户输入的计算结果</param>
    /// <returns>是否验证通过</returns>
    public bool Verify(string code, string? userInputCode)
    {
        if (string.IsNullOrWhiteSpace(userInputCode))
        {
            return false;
        }

        if (!int.TryParse(userInputCode.Trim(), out var result))
        {
            return false;
        }

        var calculateResult = Calculate(code);
        return result == calculateResult;
    }

    /// <summary>
    /// 获取验证码长度
    /// </summary>
    /// <returns>验证码长度</returns>
    public int GetLength() => _numberLength * 2 + 2;

    /// <summary>
    /// 根据长度获取参与计算数字最大值
    /// </summary>
    /// <returns>最大值</returns>
    private int GetLimit() => (int)Math.Pow(10, _numberLength);

    /// <summary>
    /// 计算表达式的结果
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns>计算结果</returns>
    private static int Calculate(string expression)
    {
        // 移除空格和等号
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
