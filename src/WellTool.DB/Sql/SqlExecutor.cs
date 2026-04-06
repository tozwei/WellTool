using System.Data;

namespace WellTool.DB.Sql
{
    /// <summary>
    /// SQL 执行器
    /// </summary>
    public class SqlExecutor
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connection">数据库连接</param>
        public SqlExecutor(IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// 执行查询并关闭 PreparedStatement
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="ps">PreparedStatement</param>
        /// <param name="rsh">结果集处理器</param>
        /// <returns>查询结果</returns>
        public T QueryAndClosePs<T>(IDbCommand ps, Handler.RsHandler<T> rsh)
        {
            using (ps)
            {
                using (var reader = ps.ExecuteReader())
                {
                    return rsh.Handle(reader);
                }
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="sql">SQL 语句</param>
        /// <param name="rsh">结果集处理器</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        public T Query<T>(string sql, Handler.RsHandler<T> rsh, params object[] parameters)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = sql;

                if (parameters != null && parameters.Length > 0)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = $"@{i}";
                        parameter.Value = parameters[i] ?? DBNull.Value;
                        command.Parameters.Add(parameter);
                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    return rsh.Handle(reader);
                }
            }
        }

        /// <summary>
        /// 执行非查询语句
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响的行数</returns>
        public int Execute(string sql, params object[] parameters)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = sql;

                if (parameters != null && parameters.Length > 0)
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = $"@{i}";
                        parameter.Value = parameters[i] ?? DBNull.Value;
                        command.Parameters.Add(parameter);
                    }
                }

                return command.ExecuteNonQuery();
            }
        }
    }
}