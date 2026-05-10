namespace WellTool.Core.Convert.Impl;

/// <summary>
/// Cast杞崲鍣紝鐢ㄤ簬绫诲瀷杞崲
/// </summary>
/// <typeparam name="T">鐩爣绫诲瀷</typeparam>
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

