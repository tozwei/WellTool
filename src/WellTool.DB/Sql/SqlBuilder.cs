using System.Text;
using WellTool.DB;
using WellTool.DB.Dialect;

namespace WellTool.DB.Sql
{
    /// <summary>
    /// SQL构建器
    /// </summary>
    public class SqlBuilder
    {
        private StringBuilder _sql;
        private IDialect _dialect;

        /// <summary>
        /// 构造
        /// </summary>
        public SqlBuilder()
        {
            _sql = new StringBuilder();
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dialect">数据库方言</param>
        public SqlBuilder(IDialect dialect)
        {
            _sql = new StringBuilder();
            _dialect = dialect;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="capacity">初始容量</param>
        public SqlBuilder(int capacity)
        {
            _sql = new StringBuilder(capacity);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dialect">数据库方言</param>
        /// <param name="capacity">初始容量</param>
        public SqlBuilder(IDialect dialect, int capacity)
        {
            _sql = new StringBuilder(capacity);
            _dialect = dialect;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="sql">初始SQL</param>
        public SqlBuilder(string sql)
        {
            _sql = new StringBuilder(sql);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dialect">数据库方言</param>
        /// <param name="sql">初始SQL</param>
        public SqlBuilder(IDialect dialect, string sql)
        {
            _sql = new StringBuilder(sql);
            _dialect = dialect;
        }

        /// <summary>
        /// 添加SQL片段
        /// </summary>
        /// <param name="sql">SQL片段</param>
        /// <returns>this</returns>
        public SqlBuilder Append(string sql)
        {
            _sql.Append(sql);
            return this;
        }

        /// <summary>
        /// 添加SQL片段（带参数）
        /// </summary>
        /// <param name="format">SQL格式</param>
        /// <param name="args">参数</param>
        /// <returns>this</returns>
        public SqlBuilder Append(string format, params object[] args)
        {
            _sql.AppendFormat(format, args);
            return this;
        }

        /// <summary>
        /// 添加SQL片段并换行
        /// </summary>
        /// <param name="sql">SQL片段</param>
        /// <returns>this</returns>
        public SqlBuilder AppendLine(string sql)
        {
            _sql.AppendLine(sql);
            return this;
        }

        /// <summary>
        /// 清空SQL
        /// </summary>
        /// <returns>this</returns>
        public SqlBuilder Clear()
        {
            _sql.Clear();
            return this;
        }

        /// <summary>
        /// 获取SQL长度
        /// </summary>
        /// <returns>SQL长度</returns>
        public int Length()
        {
            return _sql.Length;
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>SQL语句</returns>
        public override string ToString()
        {
            return _sql.ToString();
        }

        /// <summary>
        /// 创建SQL构建器
        /// </summary>
        /// <returns>SQL构建器</returns>
        public static SqlBuilder Create()
        {
            return new SqlBuilder();
        }

        /// <summary>
        /// 创建SQL构建器
        /// </summary>
        /// <param name="dialect">数据库方言</param>
        /// <returns>SQL构建器</returns>
        public static SqlBuilder Create(IDialect dialect)
        {
            return new SqlBuilder(dialect);
        }

        /// <summary>
        /// 创建SQL构建器
        /// </summary>
        /// <param name="capacity">初始容量</param>
        /// <returns>SQL构建器</returns>
        public static SqlBuilder Create(int capacity)
        {
            return new SqlBuilder(capacity);
        }

        /// <summary>
        /// 创建SQL构建器
        /// </summary>
        /// <param name="dialect">数据库方言</param>
        /// <param name="capacity">初始容量</param>
        /// <returns>SQL构建器</returns>
        public static SqlBuilder Create(IDialect dialect, int capacity)
        {
            return new SqlBuilder(dialect, capacity);
        }

        /// <summary>
        /// 创建SQL构建器
        /// </summary>
        /// <param name="sql">初始SQL</param>
        /// <returns>SQL构建器</returns>
        public static SqlBuilder Create(string sql)
        {
            return new SqlBuilder(sql);
        }

        /// <summary>
        /// 创建SQL构建器
        /// </summary>
        /// <param name="dialect">数据库方言</param>
        /// <param name="sql">初始SQL</param>
        /// <returns>SQL构建器</returns>
        public static SqlBuilder Create(IDialect dialect, string sql)
        {
            return new SqlBuilder(dialect, sql);
        }

        /// <summary>
        /// 构建插入SQL
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>插入SQL</returns>
        public string Insert(Entity entity)
        {
            var tableName = _dialect.Quote(entity.TableName);
            var columns = new StringBuilder();
            var values = new StringBuilder();

            foreach (var (column, value) in entity)
            {
                if (columns.Length > 0)
                {
                    columns.Append(", ");
                    values.Append(", ");
                }
                columns.Append(_dialect.Quote(column));
                values.Append($"'{value}'");
            }

            return $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
        }

        /// <summary>
        /// 构建删除SQL
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>删除SQL</returns>
        public string Delete(Entity entity)
        {
            var tableName = _dialect.Quote(entity.TableName);
            var where = BuildWhere(entity);

            return $"DELETE FROM {tableName}{where}";
        }

        /// <summary>
        /// 构建更新SQL
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>更新SQL</returns>
        public string Update(Entity entity)
        {
            var tableName = _dialect.Quote(entity.TableName);
            var set = new StringBuilder();

            foreach (var (column, value) in entity)
            {
                if (set.Length > 0)
                {
                    set.Append(", ");
                }
                set.Append($"{_dialect.Quote(column)} = '{value}'");
            }

            var where = BuildWhere(entity);
            return $"UPDATE {tableName} SET {set}{where}";
        }

        /// <summary>
        /// 构建选择SQL
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>选择SQL</returns>
        public string Select(Entity entity)
        {
            var tableName = _dialect.Quote(entity.TableName);
            var where = BuildWhere(entity);

            return $"SELECT * FROM {tableName}{where}";
        }

        /// <summary>
        /// 构建WHERE子句
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>WHERE子句</returns>
        private string BuildWhere(Entity entity)
        {
            if (entity.Count == 0)
            {
                return string.Empty;
            }

            var where = new StringBuilder(" WHERE ");
            foreach (var (column, value) in entity)
            {
                if (where.Length > 7) // 7 is the length of " WHERE "
                {
                    where.Append(" AND ");
                }
                where.Append($"{_dialect.Quote(column)} = '{value}'");
            }

            return where.ToString();
        }
    }
}