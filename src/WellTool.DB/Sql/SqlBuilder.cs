using System.Text;

namespace WellTool.DB.Sql
{
    public class SqlBuilder
    {
        private StringBuilder _sql;
        
        public SqlBuilder()
        {
            _sql = new StringBuilder();
        }
        
        public SqlBuilder Append(string sql)
        {
            _sql.Append(sql);
            return this;
        }
        
        public SqlBuilder AppendLine(string sql)
        {
            _sql.AppendLine(sql);
            return this;
        }
        
        public SqlBuilder AppendFormat(string format, params object[] args)
        {
            _sql.AppendFormat(format, args);
            return this;
        }
        
        public override string ToString()
        {
            return _sql.ToString();
        }
    }
}