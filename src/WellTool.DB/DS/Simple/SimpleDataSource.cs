using System.Data;

namespace WellTool.DB.DS.Simple
{
    public class SimpleDataSource : AbstractDataSource
    {
        private string _connectionString;
        private string _driverClass;
        
        public SimpleDataSource(string connectionString, string driverClass)
        {
            _connectionString = connectionString;
            _driverClass = driverClass;
        }
        
        public override IDbConnection CreateConnection()
        {
            var connection = (IDbConnection)System.Activator.CreateInstance(System.Type.GetType(_driverClass));
            connection.ConnectionString = _connectionString;
            return connection;
        }
    }
}