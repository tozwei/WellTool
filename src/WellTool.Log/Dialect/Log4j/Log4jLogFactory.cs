using System;

namespace WellTool.Log.Dialect.Log4j;

/// <summary>
/// Apache Log4j 1.x 日志工厂
/// </summary>
public class Log4jLogFactory : LogFactory
{
    private readonly bool _log4jExists;

    /// <summary>
    /// 构造函数
    /// </summary>
    public Log4jLogFactory()
        : base("Log4j")
    {
        // 检查Log4j是否存在
        try
        {
            _log4jExists = Type.GetType("org.apache.log4j.Logger") != null;
            if (!_log4jExists)
            {
                throw new Exception("Log4j not found, please add log4j dependency");
            }
        }
        catch
        {
            _log4jExists = false;
            throw;
        }
    }

    /// <summary>
    /// 创建日志实例
    /// </summary>
    /// <param name="name">日志名称</param>
    /// <returns>日志实例</returns>
    public override ILog CreateLog(string name)
    {
        if (!_log4jExists)
        {
            throw new Exception("Log4j not found, please add log4j dependency");
        }
        return new Log4jLog(name);
    }

    /// <summary>
    /// 创建日志实例
    /// </summary>
    /// <param name="type">日志类型</param>
    /// <returns>日志实例</returns>
    public override ILog CreateLog(Type type)
    {
        if (!_log4jExists)
        {
            throw new Exception("Log4j not found, please add log4j dependency");
        }
        return new Log4jLog(type);
    }
}