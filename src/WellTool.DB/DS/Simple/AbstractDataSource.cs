using System.Data;

namespace WellTool.DB.DS.Simple
{
    public abstract class AbstractDataSource
    {
        public abstract IDbConnection CreateConnection();
    }
}