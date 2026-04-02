using System;

namespace WellTool.Log.Dialect.Log4j;

/// <summary>
/// Apache Log4j 1.x 日志工厂
/// </summary>
public class Log4jLogFactory : LogFactory
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public Log4jLogFactory()
        : base("Log4j")
    {
        // 检查Log4j是否存在
        try
        {
            Type.GetType("org.apache.log4j.Logger");
        }
        catch
        {
            // 忽略异常，让上层处理
        }
    }

    /// <summary>
    /// 创建日志实例
    /// </summary>
    /// <param name="name">日志名称</param>
    /// <returns>日志实例</returns>
    public override ILog CreateLog(string name)
    {
        return new Log4jLog(name);
    }

    /// <summary>
    /// 创建日志实例
    /// </summary>
    /// <param name="type">日志类型</param>
    /// <returns>日志实例</returns>
    public override ILog CreateLog(Type type)
    {
        return new Log4jLog(type);
    }
}