namespace WellTool.DB.NoSql.Mongo
{
    /// <summary>
    /// MongoDB 数据源工厂
    /// </summary>
    public class MongoFactory
    {
        /// <summary>
        /// 创建 MongoDB 数据源
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseName">数据库名称</param>
        /// <returns>MongoDB 数据源</returns>
        public static MongoDS Create(string connectionString, string databaseName)
        {
            return new MongoDS(connectionString, databaseName);
        }

        /// <summary>
        /// 创建 MongoDB 数据源
        /// </summary>
        /// <param name="host">主机名</param>
        /// <param name="port">端口</param>
        /// <param name="databaseName">数据库名称</param>
        /// <returns>MongoDB 数据源</returns>
        public static MongoDS Create(string host, int port, string databaseName)
        {
            string connectionString = string.Format("mongodb://{0}:{1}", host, port);
            return Create(connectionString, databaseName);
        }

        /// <summary>
        /// 创建 MongoDB 数据源
        /// </summary>
        /// <param name="host">主机名</param>
        /// <param name="port">端口</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="databaseName">数据库名称</param>
        /// <returns>MongoDB 数据源</returns>
        public static MongoDS Create(string host, int port, string username, string password, string databaseName)
        {
            string connectionString = string.Format("mongodb://{0}:{1}@{2}:{3}/{4}", username, password, host, port, databaseName);
            return Create(connectionString, databaseName);
        }
    }
}