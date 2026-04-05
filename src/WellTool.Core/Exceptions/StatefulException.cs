namespace WellTool.Core.Exceptions;

using WellTool.Core.Util;

/*
 * 带有状态码的异常
 */
public class StatefulException : RuntimeException
{
    private static readonly long SerialVersionUID = 6057602589533840889L;

    /// <summary>
    /// 异常状态码
    /// </summary>
    private int _status;

    public StatefulException()
    {
    }

    public StatefulException(string msg) : base(msg)
    {
    }

    public StatefulException(string messageTemplate, params object[] @params)
    {
    }

    public StatefulException(System.Exception? throwable) : base(throwable)
    {
    }

    public StatefulException(string msg, System.Exception? throwable) : base(msg, throwable)
    {
    }



    public StatefulException(int status, string msg) : base(msg)
    {
        _status = status;
    }

    public StatefulException(int status, System.Exception? throwable) : base(throwable)
    {
        _status = status;
    }

    public StatefulException(int status, string msg, System.Exception? throwable) : base(msg, throwable)
    {
        _status = status;
    }

    /// <summary>
    /// 获得异常状态码
    /// </summary>
    /// <returns></returns>
    public int GetStatus() => _status;
}
