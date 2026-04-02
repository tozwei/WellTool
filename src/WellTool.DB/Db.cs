using System.Data;
using System.Data.Common;
using WellTool.DB.Dialect;
using WellTool.DB.Sql;

namespace WellTool.DB
{
    /// <summary>
    /// 数据库操作类，提供数据库增删改查等操作
    /// </summary>
    public class Db
    {
        private static Db _instance;
        private IDbConnection _connection;
        private IDialect _dialect;
        private SqlExecutor _sqlExecutor;

        /// <summary>
        /// 获取默认数据库实例
        /// </summary>
        /// <returns>数据库实例</returns>
        public static Db Use()
        {
            if (_instance == null)
            {
                _instance = new Db();
            }
            return _instance;
        }

        /// <summary>
        /// 构造
        /// </summary>
        public Db()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="connection">数据库连接</param>
        public Db(IDbConnection connection)
        {
            _connection = connection;
            _dialect = DialectFactory.GetDialect(connection);
            _sqlExecutor = new SqlExecutor(connection);
        }

        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>this</returns>
        public Db SetConnection(IDbConnection connection)
        {
            _connection = connection;
            _dialect = DialectFactory.GetDialect(connection);
            _sqlExecutor = new SqlExecutor(connection);
            return this;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public IDbConnection GetConnection()
        {
            return _connection;
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns>this</returns>
        public Db Open()
        {
            if (_connection != null && _connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return this;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <returns>this</returns>
        public Db Close()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
            return this;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响的行数</returns>
        public int Execute(string sql, params IDataParameter[] parameters)
        {
            Open();
            using (IDbCommand command = _connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandTimeout = GlobalDbConfig.CommandTimeout;
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 执行查询，返回结果集
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>结果集</returns>
        public IDataReader Query(string sql, params IDataParameter[] parameters)
        {
            Open();
            IDbCommand command = _connection.CreateCommand();
            command.CommandText = sql;
            command.CommandTimeout = GlobalDbConfig.CommandTimeout;
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command.ExecuteReader();
        }

        /// <summary>
        /// 执行查询，返回第一行第一列
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>第一行第一列</returns>
        public object Scalar(string sql, params IDataParameter[] parameters)
        {
            Open();
            using (IDbCommand command = _connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandTimeout = GlobalDbConfig.CommandTimeout;
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                return command.ExecuteScalar();
            }
        }

        /// <summary>
        /// 保存实体到数据库
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>影响的行数</returns>
        public int Save(Entity entity)
        {
            Open();
            var sqlBuilder = new SqlBuilder(_dialect);
            var sql = sqlBuilder.Insert(entity);
            return Execute(sql);
        }

        /// <summary>
        /// 从数据库中删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>影响的行数</returns>
        public int Delete(Entity entity)
        {
            Open();
            var sqlBuilder = new SqlBuilder(_dialect);
            var sql = sqlBuilder.Delete(entity);
            return Execute(sql);
        }

        /// <summary>
        /// 从数据库中更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>影响的行数</returns>
        public int Update(Entity entity)
        {
            Open();
            var sqlBuilder = new SqlBuilder(_dialect);
            var sql = sqlBuilder.Update(entity);
            return Execute(sql);
        }

        /// <summary>
        /// 从数据库中查找实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>找到的实体</returns>
        public Entity Find(Entity entity)
        {
            Open();
            var sqlBuilder = new SqlBuilder(_dialect);
            var sql = sqlBuilder.Select(entity);
            using var reader = Query(sql);
            if (reader.Read())
            {
                var result = new Entity(entity.TableName);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result[reader.GetName(i)] = reader.GetValue(i);
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns>事务</returns>
        public IDbTransaction BeginTransaction()
        {
            Open();
            return _connection.BeginTransaction(GlobalDbConfig.DefaultTransactionIsolationLevel);
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        /// <returns>事务</returns>
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            Open();
            return _connection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// 获取方言
        /// </summary>
        /// <returns>方言</returns>
        public IDialect GetDialect()
        {
            return _dialect;
        }

        /// <summary>
        /// 获取 SQL 执行器
        /// </summary>
        /// <returns>SQL 执行器</returns>
        public SqlExecutor GetSqlExecutor()
        {
            return _sqlExecutor;
        }
    }
}