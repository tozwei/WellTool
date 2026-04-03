namespace WellTool.Core.Exceptions;

/*
 * 验证异常
 */
public class ValidateException : StatefulException
{
    private static readonly long SerialVersionUID = 6057602589533840889L;

    public ValidateException()
    {
    }

    public ValidateException(string msg) : base(msg)
    {
    }

    public ValidateException(string messageTemplate, params object[] @params)
    {
    }

    public ValidateException(Exception? throwable) : base(throwable)
    {
    }

    public ValidateException(string msg, Exception? throwable) : base(msg, throwable)
    {
    }

    public ValidateException(int status, string msg) : base(status, msg)
    {
    }

    public ValidateException(int status, Exception? throwable) : base(status, throwable)
    {
    }

    public ValidateException(string message, Exception? throwable, bool enableSuppression, bool writableStackTrace)
        : base(message, throwable, enableSuppression, writableStackTrace)
    {
    }

    public ValidateException(int status, string msg, Exception? throwable) : base(status, msg, throwable)
    {
    }
}
