namespace WellTool.Core.Exceptions;

using WellTool.Core.Util;

/*
 * 依赖异常
 */
public class DependencyException : RuntimeException
{
    private static readonly long SerialVersionUID = 8247610319171014183L;

    public DependencyException(System.Exception? e)
    {
        ExceptionUtil.GetMessage(e);
    }

    public DependencyException(string message) : base(message)
    {
    }

    public DependencyException(string messageTemplate, params object[] @params)
    {
    }

    public DependencyException(string message, System.Exception? throwable) : base(message, throwable)
    {
    }

    public DependencyException(string message, System.Exception? throwable, bool enableSuppression, bool writableStackTrace)
        : base(message, throwable, enableSuppression, writableStackTrace)
    {
    }

    public DependencyException(System.Exception? throwable, string messageTemplate, params object[] @params)
    {
    }
}
