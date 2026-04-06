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

namespace WellTool.DB.Dialect;

using System.Data;

/// <summary>
/// 数据库方言接口
/// </summary>
public interface IDialect
{
    /// <summary>
    /// 获取数据库类型名称
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 构建分页 SQL
    /// </summary>
    /// <param name="sql">原始 SQL</param>
    /// <param name="offset">偏移量</param>
    /// <param name="limit">限制数</param>
    /// <returns>分页 SQL</returns>
    string BuildPaginationSql(string sql, int offset, int limit);

    /// <summary>
    /// 构建获取自增键的 SQL
    /// </summary>
    /// <returns>获取自增键的 SQL</returns>
    string BuildGetGeneratedKeysSql();

    /// <summary>
    /// 转义表名或列名
    /// </summary>
    /// <param name="name">表名或列名</param>
    /// <returns>转义后的表名或列名</returns>
    string Quote(string name);

    /// <summary>
    /// 创建查询的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="query">查询对象</param>
    /// <returns>PreparedStatement</returns>
    IDbCommand PreparedStatementForFind(IDbConnection conn, object query);

    /// <summary>
    /// 创建计数的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="query">查询对象</param>
    /// <returns>PreparedStatement</returns>
    IDbCommand PreparedStatementForCount(IDbConnection conn, object query);

    /// <summary>
    /// 创建分页的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="query">查询对象</param>
    /// <returns>PreparedStatement</returns>
    IDbCommand PreparedStatementForPage(IDbConnection conn, object query);

    /// <summary>
    /// 创建插入的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="entity">实体对象</param>
    /// <returns>PreparedStatement</returns>
    IDbCommand PreparedStatementForInsert(IDbConnection conn, object entity);

    /// <summary>
    /// 创建批量插入的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="entities">实体对象数组</param>
    /// <returns>批量 PreparedStatement</returns>
    object PreparedStatementForInsertBatch(IDbConnection conn, object[] entities);

    /// <summary>
    /// 创建删除的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="query">查询对象</param>
    /// <returns>PreparedStatement</returns>
    IDbCommand PreparedStatementForDelete(IDbConnection conn, object query);

    /// <summary>
    /// 创建更新的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="entity">实体对象</param>
    /// <param name="query">查询对象</param>
    /// <returns>PreparedStatement</returns>
    IDbCommand PreparedStatementForUpdate(IDbConnection conn, object entity, object query);

    /// <summary>
    /// 设置包装器
    /// </summary>
    /// <param name="wrapper">包装器</param>
    void SetWrapper(object wrapper);
}

/// <summary>
/// 数据库方言基类
/// </summary>
public abstract class Dialect : IDialect
{
    /// <summary>
    /// 获取数据库类型名称
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// 构建分页 SQL
    /// </summary>
    /// <param name="sql">原始 SQL</param>
    /// <param name="offset">偏移量</param>
    /// <param name="limit">限制数</param>
    /// <returns>分页 SQL</returns>
    public abstract string BuildPaginationSql(string sql, int offset, int limit);

    /// <summary>
    /// 构建获取自增键的 SQL
    /// </summary>
    /// <returns>获取自增键的 SQL</returns>
    public virtual string BuildGetGeneratedKeysSql()
    {
        return string.Empty;
    }

    /// <summary>
    /// 转义表名或列名
    /// </summary>
    /// <param name="name">表名或列名</param>
    /// <returns>转义后的表名或列名</returns>
    public virtual string Quote(string name)
    {
        return $"[{name}]";
    }

    /// <summary>
    /// 创建查询的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="query">查询对象</param>
    /// <returns>PreparedStatement</returns>
    public virtual IDbCommand PreparedStatementForFind(IDbConnection conn, object query)
    {
        // 默认实现，子类可重写
        return conn.CreateCommand();
    }

    /// <summary>
    /// 创建计数的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="query">查询对象</param>
    /// <returns>PreparedStatement</returns>
    public virtual IDbCommand PreparedStatementForCount(IDbConnection conn, object query)
    {
        // 默认实现，子类可重写
        return conn.CreateCommand();
    }

    /// <summary>
    /// 创建分页的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="query">查询对象</param>
    /// <returns>PreparedStatement</returns>
    public virtual IDbCommand PreparedStatementForPage(IDbConnection conn, object query)
    {
        // 默认实现，子类可重写
        return conn.CreateCommand();
    }

    /// <summary>
    /// 创建插入的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="entity">实体对象</param>
    /// <returns>PreparedStatement</returns>
    public virtual IDbCommand PreparedStatementForInsert(IDbConnection conn, object entity)
    {
        // 默认实现，子类可重写
        return conn.CreateCommand();
    }

    /// <summary>
    /// 创建批量插入的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="entities">实体对象数组</param>
    /// <returns>批量 PreparedStatement</returns>
    public virtual object PreparedStatementForInsertBatch(IDbConnection conn, object[] entities)
    {
        // 默认实现，子类可重写
        return conn.CreateCommand();
    }

    /// <summary>
    /// 创建删除的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="query">查询对象</param>
    /// <returns>PreparedStatement</returns>
    public virtual IDbCommand PreparedStatementForDelete(IDbConnection conn, object query)
    {
        // 默认实现，子类可重写
        return conn.CreateCommand();
    }

    /// <summary>
    /// 创建更新的 PreparedStatement
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="entity">实体对象</param>
    /// <param name="query">查询对象</param>
    /// <returns>PreparedStatement</returns>
    public virtual IDbCommand PreparedStatementForUpdate(IDbConnection conn, object entity, object query)
    {
        // 默认实现，子类可重写
        return conn.CreateCommand();
    }

    /// <summary>
    /// 设置包装器
    /// </summary>
    /// <param name="wrapper">包装器</param>
    public virtual void SetWrapper(object wrapper)
    {
        // 默认实现，子类可重写
    }
}