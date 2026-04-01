using System.Text;

namespace WellTool.DB.Sql
{
    /// <summary>
    /// SQL构建器
    /// </summary>
    public class SqlBuilder
    {
        private StringBuilder _sql;

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
        /// <param name="capacity">初始容量</param>
        public SqlBuilder(int capacity)
        {
            _sql = new StringBuilder(capacity);
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
        /// <param name="capacity">初始容量</param>
        /// <returns>SQL构建器</returns>
        public static SqlBuilder Create(int capacity)
        {
            return new SqlBuilder(capacity);
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
    }
}