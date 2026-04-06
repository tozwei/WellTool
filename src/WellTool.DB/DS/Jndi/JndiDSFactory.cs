using System.Data;

namespace WellTool.DB.DS.Jndi
{
    /// <summary>
    /// JNDI数据源工厂
    /// 
    /// 需要在容器环境中使用
    /// </summary>
    public class JndiDSFactory : AbstractDSFactory
    {
        /// <summary>
        /// 构造
        /// </summary>
        public JndiDSFactory()
        {
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public override IDbConnection GetConnection()
        {
            // TODO: 需要在容器环境中实现
            return null;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <returns>数据库连接</returns>
        public override IDbConnection GetConnection(string connStr)
        {
            // TODO: 需要在容器环境中实现
            return null;
        }

        /// <summary>
        /// 获取数据适配器
        /// </summary>
        /// <returns>数据适配器</returns>
        public override IDbDataAdapter GetAdapter()
        {
            // TODO: 需要在容器环境中实现
            return null;
        }

        /// <summary>
        /// 创建数据源
        /// </summary>
        /// <param name="url">连接URL</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>数据库连接</returns>
        public override IDbConnection CreateDataSource(string url, string username, string password)
        {
            // TODO: 需要在容器环境中实现
            return null;
        }
    }
}
