using System;

namespace WellTool.DB.Sql
{
    /// <summary>
    /// SQL日志配置
    /// </summary>
    public class SqlLog
    {
        /// <summary>
        /// 单例实例
        /// </summary>
        public static SqlLog Instance { get; } = new SqlLog();

        private SqlLog() { }

        /// <summary>
        /// 配置文件中配置属性名：是否显示SQL
        /// </summary>
        public const string KeyShowSql = "showSql";
        
        /// <summary>
        /// 配置文件中配置属性名：是否格式化SQL
        /// </summary>
        public const string KeyFormatSql = "formatSql";
        
        /// <summary>
        /// 配置文件中配置属性名：是否显示参数
        /// </summary>
        public const string KeyShowParams = "showParams";

        /// <summary>
        /// 是否显示SQL
        /// </summary>
        public bool ShowSql { get; private set; }
        
        /// <summary>
        /// 是否格式化SQL
        /// </summary>
        public bool FormatSql { get; private set; }
        
        /// <summary>
        /// 是否显示参数
        /// </summary>
        public bool ShowParams { get; private set; }

        /// <summary>
        /// 初始化全局配置
        /// </summary>
        /// <param name="isShowSql">是否显示SQL</param>
        /// <param name="isFormatSql">是否格式化显示的SQL</param>
        /// <param name="isShowParams">是否打印参数</param>
        public void Init(bool isShowSql, bool isFormatSql, bool isShowParams)
        {
            ShowSql = isShowSql;
            FormatSql = isFormatSql;
            ShowParams = isShowParams;
        }

        /// <summary>
        /// 打印SQL日志
        /// </summary>
        /// <param name="sql">SQL语句</param>
        public void Log(string sql)
        {
            Log(sql, null);
        }

        /// <summary>
        /// 打印批量SQL日志
        /// </summary>
        /// <param name="sql">SQL语句</param>
        public void LogForBatch(string sql)
        {
            if (ShowSql)
            {
                var formattedSql = FormatSql ? SqlFormatter.Format(sql) : sql;
                Console.WriteLine($"[Batch SQL] -> {formattedSql}");
            }
        }

        /// <summary>
        /// 打印SQL日志
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="paramValues">参数</param>
        public void Log(string sql, object paramValues)
        {
            if (!ShowSql) return;

            var formattedSql = FormatSql ? SqlFormatter.Format(sql) : sql;
            
            if (paramValues != null && ShowParams)
            {
                Console.WriteLine($"[SQL] -> {formattedSql}\nParams -> {paramValues}");
            }
            else
            {
                Console.WriteLine($"[SQL] -> {formattedSql}");
            }
        }
    }
}
