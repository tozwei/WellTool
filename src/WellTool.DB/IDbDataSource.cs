using System.Data;

namespace WellTool.DB
{
    /// <summary>
    /// 数据库数据源接口
    /// </summary>
    public interface IDbDataSource
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns>数据库连接</returns>
        IDbConnection GetConnection();
        
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>数据库连接</returns>
        IDbConnection GetConnection(string connectionString);
    }
}