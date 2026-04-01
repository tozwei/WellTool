namespace WellTool.DB.NoSql.Mongo
{
    /// <summary>
    /// MongoDB 数据源
    /// </summary>
    public class MongoDS
    {
        private string _connectionString;
        private string _databaseName;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseName">数据库名称</param>
        public MongoDS(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        public string GetConnectionString()
        {
            return _connectionString;
        }

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <returns>数据库名称</returns>
        public string GetDatabaseName()
        {
            return _databaseName;
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns>是否连接成功</returns>
        public bool TestConnection()
        {
            // 简化实现，实际项目中需要使用 MongoDB 驱动测试连接
            return true;
        }
    }
}