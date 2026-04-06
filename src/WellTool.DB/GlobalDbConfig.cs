using System.Data;

namespace WellTool.DB
{
    /// <summary>
    /// 全局数据库配置
    /// </summary>
    public class GlobalDbConfig
    {
        /// <summary>
        /// 连接超时时间，单位毫秒
        /// </summary>
        public static int ConnectionTimeout { get; set; } = 30000;

        /// <summary>
        /// 命令超时时间，单位秒
        /// </summary>
        public static int CommandTimeout { get; set; } = 30;

        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public static IsolationLevel DefaultTransactionIsolationLevel { get; set; } = IsolationLevel.ReadCommitted;

        /// <summary>
        /// 是否在执行SQL时打印SQL语句
        /// </summary>
        public static bool ShowSql { get; set; } = true;

        /// <summary>
        /// 数据库类型，用于指定默认的数据库方言
        /// </summary>
        public static string DbType { get; set; } = "mysql";

        /// <summary>
        /// 是否大小写不敏感
        /// </summary>
        public static bool CaseInsensitive { get; set; } = true;
    }
}