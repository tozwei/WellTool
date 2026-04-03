namespace WellTool.Core.Exceptions;

using WellTool.Core.Util;

/*
 * 未初始化异常
 */
public class NotInitedException : RuntimeException
{
    private static readonly long SerialVersionUID = 8247610319171014183L;

    public NotInitedException(System.Exception? e) : base(e)
    {
    }

    public NotInitedException(string message) : base(message)
    {
    }

    public NotInitedException(string messageTemplate, params object[] @params)
    {
    }

    public NotInitedException(string message, System.Exception? throwable) : base(message, throwable)
    {
    }

    public NotInitedException(string message, System.Exception? throwable, bool enableSuppression, bool writableStackTrace)
        : base(message, throwable, enableSuppression, writableStackTrace)
    {
    }

    public NotInitedException(System.Exception? throwable, string messageTemplate, params object[] @params)
    {
    }
}
