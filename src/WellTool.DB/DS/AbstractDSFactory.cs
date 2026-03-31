using System.Data;

namespace WellTool.DB.DS
{
    public abstract class AbstractDSFactory
    {
        public abstract IDbConnection GetConnection();
        
        public abstract IDbConnection GetConnection(string connStr);
        
        public abstract IDbDataAdapter GetAdapter();
    }
}