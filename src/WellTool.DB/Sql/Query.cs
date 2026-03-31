using System.Collections.Generic;

namespace WellTool.DB.Sql
{
    public class Query
    {
        private string _tableName;
        private List<Condition> _conditions = new List<Condition>();
        private List<Order> _orders = new List<Order>();
        
        public Query(string tableName)
        {
            _tableName = tableName;
        }
        
        public Query Where(Condition condition)
        {
            _conditions.Add(condition);
            return this;
        }
        
        public Query OrderBy(Order order)
        {
            _orders.Add(order);
            return this;
        }
        
        public string TableName => _tableName;
        public List<Condition> Conditions => _conditions;
        public List<Order> Orders => _orders;
    }
}