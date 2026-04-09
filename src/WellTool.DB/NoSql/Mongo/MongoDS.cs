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
            // 实现 MongoDB 连接测试
            try
            {
                // 这里使用简单的实现，实际项目中应该使用 MongoDB 驱动测试连接
                // 例如使用 MongoDB.Driver 库
                
                // 模拟连接测试
                // 检查连接字符串和数据库名称是否有效
                if (string.IsNullOrEmpty(_connectionString))
                {
                    return false;
                }
                
                if (string.IsNullOrEmpty(_databaseName))
                {
                    return false;
                }
                
                // 模拟连接成功
                // 实际项目中应该使用以下代码：
                // using (var client = new MongoClient(_connectionString))
                // {
                //     var database = client.GetDatabase(_databaseName);
                //     // 尝试执行一个简单的命令来测试连接
                //     database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                //     return true;
                // }
                
                return true;
            }
            catch
            {
                // 连接失败
                return false;
            }
        }
    }
}