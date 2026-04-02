using System.Data;

namespace WellTool.DB.DS.Simple
{
    public class SimpleDSFactory : AbstractDSFactory
    {
        private SimpleDataSource _dataSource;
        
        public SimpleDSFactory()
        {
        }
        
        public SimpleDSFactory(string connectionString, string driverClass)
        {
            _dataSource = new SimpleDataSource(connectionString, driverClass);
        }
        
        public override IDbConnection GetConnection()
        {
            return _dataSource.CreateConnection();
        }
        
        public override IDbConnection GetConnection(string connStr)
        {
            var dataSource = new SimpleDataSource(connStr, string.Empty);
            return dataSource.CreateConnection();
        }
        
        public override IDbDataAdapter GetAdapter()
        {
            // DbDataAdapter是抽象类，不能直接实例化
            // 这里返回null，实际使用时需要根据具体数据库类型创建相应的适配器
            return null;
        }
        
        public override IDbConnection CreateDataSource(string url, string username, string password)
        {
            var connectionString = url;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // 根据不同数据库类型构建连接字符串
                if (url.Contains("mysql"))
                {
                    connectionString = $"{url};User ID={username};Password={password}";
                }
                else if (url.Contains("sqlserver"))
                {
                    connectionString = $"{url};User ID={username};Password={password}";
                }
                else if (url.Contains("postgresql"))
                {
                    connectionString = $"{url};Username={username};Password={password}";
                }
            }
            _dataSource = new SimpleDataSource(connectionString, string.Empty);
            return _dataSource.CreateConnection();
        }
    }
}