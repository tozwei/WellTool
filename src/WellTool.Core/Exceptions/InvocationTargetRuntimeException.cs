namespace WellTool.Core.Exceptions;

/*
 * InvocationTargetException的运行时异常
 */
public class InvocationTargetRuntimeException : UtilException
{
    public InvocationTargetRuntimeException(System.Exception? e) : base(e)
    {
    }

    public InvocationTargetRuntimeException(string message) : base(message)
    {
    }

    public InvocationTargetRuntimeException(string messageTemplate, params object[] @params)
    {
    }

    public InvocationTargetRuntimeException(string message, System.Exception? throwable) : base(message, throwable)
    {
    }



    public InvocationTargetRuntimeException(System.Exception? throwable, string messageTemplate, params object[] @params)
    {
    }
}
