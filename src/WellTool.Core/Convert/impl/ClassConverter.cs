namespace WellTool.Core.Convert.impl;

/// <summary>
/// 类型转换器，将类型名字符串转换为Type
/// </summary>
public class ClassConverter : AbstractConverter<Type>
{
    /// <summary>
    /// 是否初始化类（调用static模块内容和初始化static属性）
    /// </summary>
    public bool IsInitialized { get; }

    /// <summary>
    /// 构造
    /// </summary>
    public ClassConverter() : this(true)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="isInitialized">是否初始化类</param>
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
