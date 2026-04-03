namespace WellTool.Core.Exceptions;

using WellTool.Core.Util;

/*
 * 依赖异常
 * @author xiaoleilu
 * @since 4.0.10
 */
public class DependencyException : RuntimeException
{
    private static readonly long SerialVersionUID = 8247610319171014183L;

    public DependencyException(Exception? e)
    {
        ExceptionUtil.GetMessage(e);
    }

    public DependencyException(string message) : base(message)
    {
    }

    public DependencyException(string messageTemplate, params object[] @params)
    {
    }

    public DependencyException(string message, Exception? throwable) : base(message, throwable)
    {
    }

    public DependencyException(string message, Exception? throwable, bool enableSuppression, bool writableStackTrace)
        : base(message, throwable, enableSuppression, writableStackTrace)
    {
    }

    public DependencyException(Exception? throwable, string messageTemplate, params object[] @params)
    {
    }
}
