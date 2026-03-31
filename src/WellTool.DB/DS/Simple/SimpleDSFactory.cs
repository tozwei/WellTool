using System.Data;

namespace WellTool.DB.DS.Simple
{
    public class SimpleDSFactory : AbstractDSFactory
    {
        private SimpleDataSource _dataSource;
        
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
            var dataSource = new SimpleDataSource(connStr, _dataSource.GetType().FullName);
            return dataSource.CreateConnection();
        }
        
        public override IDbDataAdapter GetAdapter()
        {
            // DbDataAdapter是抽象类，不能直接实例化
            // 这里返回null，实际使用时需要根据具体数据库类型创建相应的适配器
            return null;
        }
    }
}