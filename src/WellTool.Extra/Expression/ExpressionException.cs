namespace WellTool.Extra.Expression;

/// <summary>
/// 表达式语言异常
/// </summary>
public class ExpressionException : Exception
{
    public ExpressionException()
    {
    }

    public ExpressionException(string message) : base(message)
    {
    }

    public ExpressionException(string messageFormat, params object[] args) : base(string.Format(messageFormat, args))
    {
    }

    public ExpressionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ExpressionException(Exception innerException) : base(innerException?.Message, innerException)
    {
    }

    public ExpressionException(Exception innerException, string messageFormat, params object[] args) 
        : base(string.Format(messageFormat, args), innerException)
    {
    }
}
