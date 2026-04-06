namespace WellTool.Core.Math;

using System;
using System.Globalization;
using System.Text;

/// <summary>
/// 单币种货币类，处理货币算术、币种和取整
/// </summary>
public class Money : IComparable<Money>
{
    private static readonly long SerialVersionUID = -1004117971993390293L;

    /// <summary>
    /// 缺省的币种代码，为 CNY（人民币）
    /// </summary>
    public const string DEFAULT_CURRENCY_CODE = "CNY";

    /// <summary>
    /// 缺省的取整模式
    /// </summary>
    public static readonly MidpointRounding DEFAULT_ROUNDING_MODE = MidpointRounding.ToEven;

    /// <summary>
    /// 一组可能的元/分换算比例
    /// </summary>
    private static readonly int[] CENT_FACTORS = new int[] { 1, 10, 100, 1000 };

    /// <summary>
    /// 金额，以分为单位
    /// </summary>
    private long _cent;

    /// <summary>
    /// 币种
    /// </summary>
    private readonly string _currencyCode;

    /// <summary>
    /// 缺省构造器，创建一个具有缺省金额（0）和缺省币种的货币对象
    /// </summary>
    public Money()
    {
        _cent = 0;
        _currencyCode = DEFAULT_CURRENCY_CODE;
    }

    /// <summary>
    /// 创建一个具有金额 yuan 元 cent 分和缺省币种的货币对象
    /// </summary>
    /// <param name="yuan">金额元数</param>
    /// <param name="cent">金额分数</param>
    public Money(long yuan, int cent)
    {
        _currencyCode = DEFAULT_CURRENCY_CODE;
        _cent = yuan == 0 ? cent : (yuan * GetCentFactor()) + (cent % GetCentFactor());
    }

    /// <summary>
    /// 创建一个具有金额 yuan 元 cent 分和指定币种的货币对象
    /// </summary>
    /// <param name="yuan">金额元数</param>
    /// <param name="cent">金额分数</param>
    /// <param name="currencyCode">货币代码</param>
    public Money(long yuan, int cent, string currencyCode)
    {
        _currencyCode = currencyCode;
        _cent = yuan == 0 ? cent : (yuan * GetCentFactor()) + (cent % GetCentFactor());
    }

    /// <summary>
    /// 创建一个具有金额 amount 元和缺省币种的货币对象
    /// </summary>
    /// <param name="amount">金额，以元为单位</param>
    public Money(decimal amount)
    {
        _currencyCode = DEFAULT_CURRENCY_CODE;
        _cent = (long)Math.Round(amount * GetCentFactor(), 0, DEFAULT_ROUNDING_MODE);
    }

    /// <summary>
    /// 创建一个具有金额 amount 元和指定币种的货币对象
    /// </summary>
    /// <param name="amount">金额，以元为单位</param>
    /// <param name="currencyCode">币种代码</param>
    public Money(decimal amount, string currencyCode)
    {
        _currencyCode = currencyCode;
        _cent = (long)Math.Round(amount * GetCentFactor(), 0, DEFAULT_ROUNDING_MODE);
    }

    /// <summary>
    /// 获取币种的最小货币单位换算比例
    /// </summary>
    private int GetCentFactor()
    {
        return CENT_FACTORS[2]; // 默认 100
    }

    /// <summary>
    /// 获取币种的最小货币单位换算比例
    /// </summary>
    /// <param name="currencyCode">币种代码</param>
    private int GetCentFactor(string currencyCode)
    {
        switch (currencyCode)
        {
            case "JPY":
                return 1;
            case "KRW":
                return 1;
            case "CNY":
            default:
                return 100;
        }
    }

    /// <summary>
    /// 获取金额元数
    /// </summary>
    public long Yuan => _cent / GetCentFactor();

    /// <summary>
    /// 获取金额分数（分）
    /// </summary>
    public int Cent => (int)(_cent % GetCentFactor());

    /// <summary>
    /// 获取币种代码
    /// </summary>
    public string CurrencyCode => _currencyCode;

    /// <summary>
    /// 获取金额
    /// </summary>
    public decimal Amount => (decimal)_cent / GetCentFactor();

    /// <summary>
    /// 加
    /// </summary>
    public Money Add(Money money)
    {
        if (!_currencyCode.Equals(money._currencyCode, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Currency code mismatch");
        }
        Money result = new Money(Amount + money.Amount, _currencyCode);
        result._cent = _cent + money._cent;
        return result;
    }

    /// <summary>
    /// 减
    /// </summary>
    public Money Subtract(Money money)
    {
        if (!_currencyCode.Equals(money._currencyCode, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Currency code mismatch");
        }
        Money result = new Money(Amount - money.Amount, _currencyCode);
        result._cent = _cent - money._cent;
        return result;
    }

    /// <summary>
    /// 乘
    /// </summary>
    public Money Multiply(decimal multiplier)
    {
        Money result = new Money(Amount * multiplier, _currencyCode);
        result._cent = (long)Math.Round(_cent * multiplier, 0, DEFAULT_ROUNDING_MODE);
        return result;
    }

    /// <summary>
    /// 除
    /// </summary>
    public Money Divide(decimal divisor)
    {
        Money result = new Money(Amount / divisor, _currencyCode);
        result._cent = (long)Math.Round(_cent / divisor, 0, DEFAULT_ROUNDING_MODE);
        return result;
    }

    /// <summary>
    /// 金额从小到大排序
    /// </summary>
    public int CompareTo(Money? other)
    {
        if (other == null) return 1;
        if (!_currencyCode.Equals(other._currencyCode, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Currency code mismatch");
        }
        return _cent.CompareTo(other._cent);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Money money)
        {
            return _cent == money._cent && 
                   _currencyCode.Equals(money._currencyCode, StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_cent, _currencyCode);
    }

    public override string ToString()
    {
        return $"{CurrencyCode} {Amount:N2}";
    }

    /// <summary>
    /// 转换为 decimal
    /// </summary>
    public decimal ToDecimal()
    {
        return Amount;
    }

    /// <summary>
    /// 转换为 long（分为单位）
    /// </summary>
    public long ToLong()
    {
        return _cent;
    }

    /// <summary>
    /// 大于运算符
    /// </summary>
    public static bool operator >(Money left, Money right)
    {
        if (left == null || right == null)
        {
            throw new ArgumentNullException();
        }
        if (!left._currencyCode.Equals(right._currencyCode, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Currency code mismatch");
        }
        return left._cent > right._cent;
    }

    /// <summary>
    /// 小于运算符
    /// </summary>
    public static bool operator <(Money left, Money right)
    {
        if (left == null || right == null)
        {
            throw new ArgumentNullException();
        }
        if (!left._currencyCode.Equals(right._currencyCode, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Currency code mismatch");
        }
        return left._cent < right._cent;
    }

    /// <summary>
    /// 大于等于运算符
    /// </summary>
    public static bool operator >=(Money left, Money right)
    {
        if (left == null || right == null)
        {
            throw new ArgumentNullException();
        }
        if (!left._currencyCode.Equals(right._currencyCode, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Currency code mismatch");
        }
        return left._cent >= right._cent;
    }

    /// <summary>
    /// 小于等于运算符
    /// </summary>
    public static bool operator <=(Money left, Money right)
    {
        if (left == null || right == null)
        {
            throw new ArgumentNullException();
        }
        if (!left._currencyCode.Equals(right._currencyCode, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Currency code mismatch");
        }
        return left._cent <= right._cent;
    }
}
