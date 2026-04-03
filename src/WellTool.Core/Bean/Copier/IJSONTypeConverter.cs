namespace WellTool.Core.Bean.Copier;

/// <summary>
/// JSON自定义转换扩展接口
/// </summary>
public interface IJSONTypeConverter
{
    /// <summary>
    /// 转为实体类对象
    /// </summary>
    /// <typeparam name="T">Bean类型</typeparam>
    /// <param name="type">Type</param>
    /// <returns>实体类对象</returns>
    T? ToBean<T>(Type type) where T : class;
}
