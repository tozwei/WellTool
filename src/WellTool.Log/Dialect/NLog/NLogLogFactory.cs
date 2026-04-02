using System.Reflection;
using WellTool.Log.Dialect.Console;

namespace WellTool.Log.Dialect.NLog;

/// <summary>
/// NLog日志工厂
/// </summary>
public class NLogLogFactory : LogFactory
{
    private static readonly Type LoggerType;

    static NLogLogFactory()
    {
        try
        {
            var assembly = Assembly.Load("NLog");
            if (assembly != null)
            {
                LoggerType = assembly.GetType("NLog.Logger");
            }
        }
        catch
        {
            // 忽略异常
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public NLogLogFactory() : base("NLog")
    {
    }

    /// <summary>
    /// 是否可用
    /// </summary>
    public static bool IsAvailable => LoggerType != null;

    /// <summary>
    /// 创建日志对象
    /// </summary>
    /// <param name="name">日志对象名</param>
    /// <returns>日志对象</returns>
    public override ILog CreateLog(string name)
    {
        // 暂时返回ConsoleLog，因为NLogLog类不存在
        return new ConsoleLog(name);
    }

    /// <summary>
    /// 创建日志对象
    /// </summary>
    /// <param name="clazz">日志对应类</param>
    /// <returns>日志对象</returns>
    public override ILog CreateLog(Type clazz)
    {
        // 暂时返回ConsoleLog，因为NLogLog类不存在
        return new ConsoleLog(clazz);
    }

    /// <summary>
    /// 检查日志实现是否存在
    /// </summary>
    /// <param name="logClassName">日志实现相关类</param>
    protected override void CheckLogExist(Type logClassName)
    {
        // 不做任何操作
    }
}
