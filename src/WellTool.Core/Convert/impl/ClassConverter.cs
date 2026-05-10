namespace WellTool.Core.Convert.Impl;

/// <summary>
/// 绫诲瀷杞崲鍣紝灏嗙被鍨嬪悕瀛楃涓茶浆鎹负Type
/// </summary>
public class ClassConverter : AbstractConverter<Type>
{
    /// <summary>
    /// 鏄惁鍒濆鍖栫被锛堣皟鐢╯tatic妯″潡鍐呭鍜屽垵濮嬪寲static灞炴€э級
    /// </summary>
    public bool IsInitialized { get; }

    /// <summary>
    /// 鏋勯€?    /// </summary>
    public ClassConverter() : this(true)
    {
    }

    /// <summary>
    /// 鏋勯€?    /// </summary>
    /// <param name="isInitialized">鏄惁鍒濆鍖栫被</param>
    public ClassConverter(bool isInitialized)
    {
        IsInitialized = isInitialized;
    }

    protected override Type ConvertInternal(object value)
    {
        var className = ConvertToStr(value);
        return Type.GetType(className, true, !IsInitialized) 
               ?? throw new InvalidOperationException($"Cannot find type: {className}");
    }
}

