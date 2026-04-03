namespace WellTool.Setting;

/// <summary>
/// Setting相关运行时异常
/// </summary>
public class SettingRuntimeException : Exception
{
    public SettingRuntimeException()
    {
    }

    public SettingRuntimeException(string message) : base(message)
    {
    }

    public SettingRuntimeException(string messageFormat, params object[] args) : base(string.Format(messageFormat, args))
    {
    }

    public SettingRuntimeException(Exception innerException, string messageFormat, params object[] args) 
        : base(string.Format(messageFormat, args), innerException)
    {
    }

    public SettingRuntimeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public SettingRuntimeException(Exception innerException) : base(innerException.Message, innerException)
    {
    }
}
