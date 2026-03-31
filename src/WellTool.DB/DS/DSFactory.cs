using System.Data;

namespace WellTool.DB.DS
{
    public interface DSFactory
    {
        IDbConnection GetConnection();
        
        IDbConnection GetConnection(string connStr);
        
        IDbDataAdapter GetAdapter();
    }
}