namespace WellTool.Setting.Dialect;

/// <summary>
/// Properties 工具类
/// </summary>
public static class PropsUtil
{
    /// <summary>
    /// 从 Classpath 加载 Properties 文件
    /// </summary>
    /// <param name="resource">资源路径</param>
    /// <returns>Props 对象</returns>
    public static Props GetProp(string resource)
    {
        return new Props(resource);
    }

    /// <summary>
    /// 从多个位置查找第一个存在的 Properties 文件
    /// </summary>
    /// <param name="resources">资源路径数组</param>
    /// <returns>Props 对象，如果未找到则返回 null</returns>
    public static Props? GetFirstFoundProp(params string[] resources)
    {
        foreach (var resource in resources)
        {
            try
            {
                return new Props(resource);
            }
            catch (FileNotFoundException)
            {
                // 忽略异常，继续查找下一个
            }
        }
        return null;
    }
}
