namespace WellTool.Core.Convert.impl;

/// <summary>
/// Cast转换器，用于类型转换
/// </summary>
/// <typeparam name="T">目标类型</typeparam>
public class CastConverter<T> : AbstractConverter<T>
{
    protected override T ConvertInternal(object value)
    {
        if (value is T result)
        {
            return result;
        }
        return (T)System.Convert.ChangeType(value, typeof(T));
    }
}
