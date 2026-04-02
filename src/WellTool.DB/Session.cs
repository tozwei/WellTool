// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Data;
using WellTool.DB.Dialect;
using WellTool.DB.Sql;

namespace WellTool.DB;

/// <summary>
/// 数据库会话
/// </summary>
public class Session : IDisposable
{
    /// <summary>
    /// 连接
    /// </summary>
    private readonly IDbConnection _connection;

    /// <summary>
    /// 事务
    /// </summary>
    private IDbTransaction _transaction;

    /// <summary>
    /// SQL 执行器
    /// </summary>
    private SqlExecutor _sqlExecutor;

    /// <summary>
    /// 方言
    /// </summary>
    private IDialect _dialect;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="connection">连接</param>
    public Session(IDbConnection connection)
    {
        _connection = connection;
        _sqlExecutor = new SqlExecutor(connection);
        _dialect = DialectFactory.GetDialect(connection);
    }

    /// <summary>
    /// 获取连接
    /// </summary>
    /// <returns>连接</returns>
    public IDbConnection GetConnection()
    {
        return _connection;
    }

    /// <summary>
    /// 获取 SQL 执行器
    /// </summary>
    /// <returns>SQL 执行器</returns>
    public SqlExecutor GetSqlExecutor()
    {
        return _sqlExecutor;
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
    /// 开始事务
    /// </summary>
    public void BeginTransaction()
    {
        _transaction = _connection.BeginTransaction();
    }

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <param name="isolationLevel">隔离级别</param>
    public void BeginTransaction(IsolationLevel isolationLevel)
    {
        _transaction = _connection.BeginTransaction(isolationLevel);
    }

    /// <summary>
    /// 提交事务
    /// </summary>
    public void Commit()
    {
        _transaction?.Commit();
        _transaction = null;
    }

    /// <summary>
    /// 回滚事务
    /// </summary>
    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction = null;
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Rollback();
        _connection?.Close();
        _connection?.Dispose();
    }
}
