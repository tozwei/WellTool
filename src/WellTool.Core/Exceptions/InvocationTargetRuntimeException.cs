namespace WellTool.Core.Exceptions;

/*
 * InvocationTargetException的运行时异常
 * @author looly
 * @since 5.8.1
 */
public class InvocationTargetRuntimeException : UtilException
{
    public InvocationTargetRuntimeException(Exception? e) : base(e)
    {
    }

    public InvocationTargetRuntimeException(string message) : base(message)
    {
    }

    public InvocationTargetRuntimeException(string messageTemplate, params object[] @params)
    {
    }

    public InvocationTargetRuntimeException(string message, Exception? throwable) : base(message, throwable)
    {
    }

    public InvocationTargetRuntimeException(string message, Exception? throwable, bool enableSuppression, bool writableStackTrace)
        : base(message, throwable, enableSuppression, writableStackTrace)
    {
    }

    public InvocationTargetRuntimeException(Exception? throwable, string messageTemplate, params object[] @params)
    {
    }
}
