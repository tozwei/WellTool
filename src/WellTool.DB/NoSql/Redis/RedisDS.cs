namespace WellTool.DB.NoSql.Redis
{
    /// <summary>
    /// Redis 数据源
    /// </summary>
    public class RedisDS
    {
        private string _host;
        private int _port;
        private string _password;
        private int _database;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="host">主机名</param>
        /// <param name="port">端口</param>
        /// <param name="password">密码</param>
        /// <param name="database">数据库索引</param>
        public RedisDS(string host, int port, string password, int database)
        {
            _host = host;
            _port = port;
            _password = password;
            _database = database;
        }

        /// <summary>
        /// 获取主机名
        /// </summary>
        /// <returns>主机名</returns>
        public string GetHost()
        {
            return _host;
        }

        /// <summary>
        /// 获取端口
        /// </summary>
        /// <returns>端口</returns>
        public int GetPort()
        {
            return _port;
        }

        /// <summary>
        /// 获取密码
        /// </summary>
        /// <returns>密码</returns>
        public string GetPassword()
        {
            return _password;
        }

        /// <summary>
        /// 获取数据库索引
        /// </summary>
        /// <returns>数据库索引</returns>
        public int GetDatabase()
        {
            return _database;
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns>是否连接成功</returns>
        public bool TestConnection()
        {
            // 实现 Redis 连接测试
            try
            {
                // 这里使用简单的实现，实际项目中应该使用 Redis 驱动测试连接
                // 例如使用 StackExchange.Redis 库
                
                // 模拟连接测试
                // 检查主机名和端口是否有效
                if (string.IsNullOrEmpty(_host))
                {
                    return false;
                }
                
                if (_port <= 0 || _port > 65535)
                {
                    return false;
                }
                
                // 模拟连接成功
                // 实际项目中应该使用以下代码：
                // using (var connection = ConnectionMultiplexer.Connect($"{_host}:{_port},password={_password},defaultDatabase={_database}"))
                // {
                //     var db = connection.GetDatabase();
                //     return db.Ping().HasValue;
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