using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using WellTool.Core.Map;
using WellTool.DB.Dialect;
using WellTool.DB.Handler;
using WellTool.DB.Sql;

namespace WellTool.DB
{
    /// <summary>
    /// 提供基于方言的原始增删改查执行封装
    /// </summary>
    public class DialectRunner
    {
        private IDialect _dialect;
        private SqlExecutor _sqlExecutor;
        /// <summary>
        /// 是否大小写不敏感（默认大小写不敏感）
        /// </summary>
        protected bool _caseInsensitive = GlobalDbConfig.CaseInsensitive;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dialect">方言</param>
        /// <param name="connection">数据库连接</param>
        public DialectRunner(IDialect dialect, IDbConnection connection)
        {
            _dialect = dialect;
            _sqlExecutor = new SqlExecutor(connection);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="driverClassName">驱动类名，用于识别方言</param>
        /// <param name="connection">数据库连接</param>
        public DialectRunner(string driverClassName, IDbConnection connection)
        {
            _dialect = DialectFactory.GetDialect(driverClassName);
            _sqlExecutor = new SqlExecutor(connection);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="records">记录列表</param>
        /// <returns>插入行数</returns>
        public int[] Insert(IDbConnection conn, params Entity[] records)
        {
            if (records == null || records.Length == 0)
            {
                return new int[] { 0 };
            }

            if (records.Length == 1)
            {
                var ps = _dialect.PreparedStatementForInsert(conn, records[0]);
                return new int[] { ps.ExecuteNonQuery() };
            }

            var batchPs = _dialect.PreparedStatementForInsertBatch(conn, records) as IDbCommand;
            if (batchPs != null)
            {
                return new int[] { batchPs.ExecuteNonQuery() };
            }
            return new int[] { 0 };
        }

        /// <summary>
        /// 插入或更新数据
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="record">记录</param>
        /// <param name="keys">需要检查唯一性的字段</param>
        /// <returns>插入行数</returns>
        public int InsertOrUpdate(IDbConnection conn, Entity record, params string[] keys)
        {
            var where = record.Filter(keys);
            if (MapUtil.IsNotEmpty(where) && Count(conn, where) > 0)
            {
                return Update(conn, record.RemoveNew(keys), where);
            }
            else
            {
                return Insert(conn, record)[0];
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="where">条件</param>
        /// <returns>影响行数</returns>
        public int Delete(IDbConnection conn, Entity where)
        {
            if (MapUtil.IsEmpty(where))
            {
                throw new Exception("Empty entity provided!");
            }

            var ps = _dialect.PreparedStatementForDelete(conn, Query.Of(where));
            return ps.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="record">记录</param>
        /// <param name="where">条件</param>
        /// <returns>影响行数</returns>
        public int Update(IDbConnection conn, Entity record, Entity where)
        {
            if (MapUtil.IsEmpty(record))
            {
                throw new Exception("Empty entity provided!");
            }
            if (MapUtil.IsEmpty(where))
            {
                throw new Exception("Empty where provided!");
            }

            string tableName = record.GetTableName();
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = where.GetTableName();
                record.SetTableName(tableName);
            }

            var query = new Query(SqlUtil.BuildConditions(where), tableName);
            var ps = _dialect.PreparedStatementForUpdate(conn, record, query);
            return ps.ExecuteNonQuery();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="query">查询对象</param>
        /// <param name="rsh">结果集处理器</param>
        /// <returns>结果对象</returns>


        /// <summary>
        /// 获取结果总数
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="where">查询条件</param>
        /// <returns>复合条件的结果数</returns>
        public long Count(IDbConnection conn, Entity where)
        {
            var result = _sqlExecutor.QueryAndClosePs(_dialect.PreparedStatementForCount(conn, Query.Of(where)), new NumberHandler());
            return Convert.ToInt64(result);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="query">查询条件</param>
        /// <param name="rsh">结果集处理器</param>
        /// <returns>结果对象</returns>
        public T Find<T>(IDbConnection conn, Query query, RsHandler<T> rsh)
        {
            return _sqlExecutor.QueryAndClosePs(_dialect.PreparedStatementForFind(conn, query), rsh);
        }

        public T Page<T>(IDbConnection conn, Query query, RsHandler<T> rsh)
        {
            if (query.GetPage() == null)
            {
                return Find(conn, query, rsh);
            }

            return _sqlExecutor.QueryAndClosePs(_dialect.PreparedStatementForPage(conn, query), rsh);
        }

        public PageResult<Entity> Page(IDbConnection conn, Collection<string> fields, Entity where, WellTool.DB.Sql.Page page)
        {
            var query = WellTool.DB.Sql.Query.Of(where).SelectFields(fields).SetPage(page);
            var entities = Find(conn, query, new EntityListHandler());
            var count = Count(conn, where);
            return new PageResult<Entity>(entities, count, page.PageNumber, page.PageSize);
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
        /// 获取方言
        /// </summary>
        public IDialect GetDialect() => _dialect;

        /// <summary>
        /// 设置方言
        /// </summary>
        /// <param name="dialect">方言</param>
        public void SetDialect(IDialect dialect)
        {
            _dialect = dialect;
        }

        /// <summary>
        /// 设置包装器
        /// </summary>
        /// <param name="wrapperChar">包装字符</param>
        public void SetWrapper(char? wrapperChar)
        {
            SetWrapper(new Wrapper(wrapperChar));
        }

        /// <summary>
        /// 设置包装器
        /// </summary>
        /// <param name="wrapper">包装器</param>
        public void SetWrapper(Wrapper wrapper)
        {
            _dialect.SetWrapper(wrapper);
        }
    }
}
