using System.Collections.Generic;
using System.Text;

namespace WellTool.DB.Sql
{
    /// <summary>
    /// 查询构建器
    /// </summary>
    public class Query
    {
        private string _tableName;
        private List<string> _fields;
        private ConditionGroup _where;
        private List<Order> _orders;
        private int? _limit;
        private int? _offset;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableName">表名</param>
        public Query(string tableName)
        {
            _tableName = tableName;
            _fields = new List<string>();
            _where = new ConditionGroup();
            _orders = new List<Order>();
        }

        /// <summary>
        /// 设置查询字段
        /// </summary>
        /// <param name="fields">字段名列表</param>
        /// <returns>this</returns>
        public Query Select(params string[] fields)
        {
            _fields.AddRange(fields);
            return this;
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>this</returns>
        public Query Where(Condition condition)
        {
            _where.Add(condition);
            return this;
        }

        /// <summary>
        /// 设置查询条件组
        /// </summary>
        /// <param name="conditionGroup">条件组</param>
        /// <returns>this</returns>
        public Query Where(ConditionGroup conditionGroup)
        {
            _where.Add(conditionGroup);
            return this;
        }

        /// <summary>
        /// 添加排序条件
        /// </summary>
        /// <param name="order">排序条件</param>
        /// <returns>this</returns>
        public Query OrderBy(Order order)
        {
            _orders.Add(order);
            return this;
        }

        /// <summary>
        /// 添加排序条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="direction">排序方向</param>
        /// <returns>this</returns>
        public Query OrderBy(string field, Direction direction)
        {
            return OrderBy(new Order(field, direction));
        }

        /// <summary>
        /// 添加升序排序
        /// </summary>
        /// <param name="field">字段名</param>
        /// <returns>this</returns>
        public Query OrderByAsc(string field)
        {
            return OrderBy(field, Direction.ASC);
        }

        /// <summary>
        /// 添加降序排序
        /// </summary>
        /// <param name="field">字段名</param>
        /// <returns>this</returns>
        public Query OrderByDesc(string field)
        {
            return OrderBy(field, Direction.DESC);
        }

        /// <summary>
        /// 设置分页参数
        /// </summary>
        /// <param name="limit">限制数量</param>
        /// <param name="offset">偏移量</param>
        /// <returns>this</returns>
        public Query Limit(int limit, int offset = 0)
        {
            _limit = limit;
            _offset = offset;
            return this;
        }

        /// <summary>
        /// 构建SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        public string Build()
        {
            StringBuilder sql = new StringBuilder();

            // SELECT
            sql.Append("SELECT ");
            if (_fields.Count > 0)
            {
                sql.Append(string.Join(", ", _fields));
            }
            else
            {
                sql.Append("*");
            }

            // FROM
            sql.Append(" FROM " + _tableName);

            // WHERE
            if (_where.Conditions.Count > 0)
            {
                sql.Append(" WHERE ");
                // 简化实现，实际项目中需要递归处理条件组
                sql.Append(_where.Conditions[0].ToString());
            }

            // ORDER BY
            if (_orders.Count > 0)
            {
                sql.Append(" ORDER BY ");
                sql.Append(string.Join(", ", _orders));
            }

            // LIMIT
            if (_limit.HasValue)
            {
                sql.Append(" LIMIT " + _limit.Value);
                if (_offset.HasValue)
                {
                    sql.Append(" OFFSET " + _offset.Value);
                }
            }

            return sql.ToString();
        }

        /// <summary>
        /// 静态工厂方法
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>查询构建器</returns>
        public static Query From(string tableName)
        {
            return new Query(tableName);
        }
    }
}