using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WellTool.Core.Collection;
using WellTool.Core.Map;
using WellTool.DB.Dialect;
using WellTool.DB.Handler;
using WellTool.DB.Sql;

namespace WellTool.DB
{
    /// <summary>
    /// 抽象数据库操作类
    /// 通过给定的数据源执行给定SQL或者给定数据源和方言，执行相应的CRUD操作
    /// </summary>
    public abstract class AbstractDb
    {
        /// <summary>
        /// 数据源
        /// </summary>
        protected readonly IDbDataSource _ds;
        /// <summary>
        /// 是否支持事务
        /// </summary>
        protected bool? _isSupportTransaction;
        /// <summary>
        /// 是否大小写不敏感（默认大小写不敏感）
        /// </summary>
        protected bool _caseInsensitive = GlobalDbConfig.CaseInsensitive;
        /// <summary>
        /// SQL连接运行器
        /// </summary>
        protected SqlConnRunner _runner;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <param name="dialect">数据库方言</param>
        protected AbstractDb(IDbDataSource ds, Dialect dialect)
        {
            _ds = ds;
            _runner = new SqlConnRunner(dialect);
        }

        /// <summary>
        /// 获得数据源
        /// </summary>
        /// <returns>数据源</returns>
        public IDbDataSource GetDataSource() => _ds;

        /// <summary>
        /// 获得连接。根据实现不同，可以自定义获取连接的方式
        /// </summary>
        /// <returns>数据库连接</returns>
        public abstract IDbConnection GetConnection();

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="conn">连接</param>
        public abstract void CloseConnection(IDbConnection conn);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>结果对象列表</returns>
        public List<Entity> Query(string sql, params object[] parameters)
        {
            return Query<Entity>(sql, new EntityListHandler(_caseInsensitive), parameters);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="rsh">结果集处理器</param>
        /// <param name="parameters">参数</param>
        /// <returns>结果对象</returns>
        public T Query<T>(string sql, RsHandler<T> rsh, params object[] parameters)
        {
            var conn = GetConnection();
            try
            {
                return SqlExecutor.Query(conn, sql, rsh, parameters);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 查询单个记录
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>结果对象</returns>
        public Entity QueryOne(string sql, params object[] parameters)
        {
            return Query<Entity>(sql, new EntityHandler(_caseInsensitive), parameters);
        }

        /// <summary>
        /// 执行非查询语句（插入、更新、删除）
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响行数</returns>
        public int Execute(string sql, params object[] parameters)
        {
            var conn = GetConnection();
            try
            {
                return SqlExecutor.Execute(conn, sql, parameters);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="record">记录</param>
        /// <returns>插入行数</returns>
        public int Insert(Entity record)
        {
            var conn = GetConnection();
            try
            {
                return _runner.Insert(conn, record);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="record">记录</param>
        /// <returns>主键</returns>
        public long InsertForGeneratedKey(Entity record)
        {
            var conn = GetConnection();
            try
            {
                return _runner.InsertForGeneratedKey(conn, record);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 插入或更新数据
        /// </summary>
        /// <param name="record">记录</param>
        /// <param name="keys">需要检查唯一性的字段</param>
        /// <returns>插入行数</returns>
        public int InsertOrUpdate(Entity record, params string[] keys)
        {
            var conn = GetConnection();
            try
            {
                return _runner.InsertOrUpdate(conn, record, keys);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="records">记录列表</param>
        /// <returns>插入行数</returns>
        public int Insert(Collection<Entity> records)
        {
            var conn = GetConnection();
            try
            {
                return _runner.Insert(conn, records.ToArray());
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>删除行数</returns>
        public int Delete(Entity where)
        {
            var conn = GetConnection();
            try
            {
                return _runner.Delete(conn, where);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="record">记录</param>
        /// <param name="where">条件</param>
        /// <returns>影响行数</returns>
        public int Update(Entity record, Entity where)
        {
            var conn = GetConnection();
            try
            {
                return _runner.Update(conn, record, where);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 根据条件查询单个记录
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>记录</returns>
        public Entity Get(Entity where)
        {
            return Find(CollUtil.NewArrayList(where.GetFieldNames()), where, new EntityHandler(_caseInsensitive));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="fields">返回的字段列表</param>
        /// <param name="where">条件实体</param>
        /// <param name="rsh">结果集处理器</param>
        /// <returns>结果对象</returns>
        public T Find<T>(Collection<string> fields, Entity where, RsHandler<T> rsh)
        {
            var conn = GetConnection();
            try
            {
                return _runner.Find(conn, fields, where, rsh);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>记录列表</returns>
        public List<Entity> Find(Entity where)
        {
            return Find(CollUtil.NewArrayList(where.GetFieldNames()), where, new EntityListHandler(_caseInsensitive));
        }

        /// <summary>
        /// 结果的条目数
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>复合条件的结果数</returns>
        public long Count(Entity where)
        {
            var conn = GetConnection();
            try
            {
                return _runner.Count(conn, where);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="fields">返回的字段列表</param>
        /// <param name="where">条件</param>
        /// <param name="page">页码</param>
        /// <returns>分页结果</returns>
        public PageResult<Entity> Page(Collection<string> fields, Entity where, Page page)
        {
            var conn = GetConnection();
            try
            {
                return _runner.Page(conn, fields, where, page);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 设置是否在结果中忽略大小写
        /// </summary>
        /// <param name="caseInsensitive">是否忽略大小写</param>
        public void SetCaseInsensitive(bool caseInsensitive)
        {
            _caseInsensitive = caseInsensitive;
        }

        /// <summary>
        /// 获取SQL运行器
        /// </summary>
        /// <returns>SQL运行器</returns>
        public SqlConnRunner GetRunner() => _runner;

        /// <summary>
        /// 设置包装器
        /// </summary>
        /// <param name="wrapperChar">包装字符</param>
        /// <returns>this</returns>
        public AbstractDb SetWrapper(char? wrapperChar)
        {
            return SetWrapper(new Wrapper(wrapperChar));
        }

        /// <summary>
        /// 设置包装器
        /// </summary>
        /// <param name="wrapper">包装器</param>
        /// <returns>this</returns>
        public AbstractDb SetWrapper(Wrapper wrapper)
        {
            _runner.SetWrapper(wrapper);
            return this;
        }

        /// <summary>
        /// 取消包装器
        /// </summary>
        /// <returns>this</returns>
        public AbstractDb DisableWrapper()
        {
            return SetWrapper(null);
        }

        /// <summary>
        /// 检查数据库是否支持事务
        /// </summary>
        /// <param name="conn">连接</param>
        /// <exception cref="DbRuntimeException">不支持事务</exception>
        protected void CheckTransactionSupported(IDbConnection conn)
        {
            if (_isSupportTransaction == null)
            {
                _isSupportTransaction = conn.GetType().GetProperty("ConnectionString") != null;
            }
            if (_isSupportTransaction == false)
            {
                throw new DbRuntimeException("Transaction not supported for current database!");
            }
        }
    }
}
