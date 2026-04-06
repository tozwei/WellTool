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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using WellTool.DB.Handler;
using WellTool.DB.Sql;

namespace WellTool.DB;

/// <summary>
/// SQL 连接运行器
/// </summary>
public class SqlConnRunner
{
    /// <summary>
    /// 连接
    /// </summary>
    private readonly IDbConnection _connection;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="connection">连接</param>
    public SqlConnRunner(IDbConnection connection)
    {
        _connection = connection;
    }

    /// <summary>
    /// 执行 SQL 查询
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    /// <param name="sql">SQL 语句</param>
    /// <param name="handler">结果处理器</param>
    /// <param name="parameters">参数</param>
    /// <returns>查询结果</returns>
    public T Query<T>(string sql, RsHandler<T> handler, params object[] parameters)
    {
        using var command = CreateCommand(sql, parameters);
        using var reader = command.ExecuteReader();
        return handler.Handle(reader);
    }

    /// <summary>
    /// 执行 SQL 查询
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    /// <param name="sql">SQL 语句</param>
    /// <param name="handler">结果处理器</param>
    /// <param name="parameters">参数</param>
    /// <returns>查询结果</returns>
    public T Query<T>(string sql, RsHandler<T> handler, IDictionary<string, object> parameters)
    {
        using var command = CreateCommand(sql, parameters);
        using var reader = command.ExecuteReader();
        return handler.Handle(reader);
    }

    /// <summary>
    /// 执行 SQL 更新
    /// </summary>
    /// <param name="sql">SQL 语句</param>
    /// <param name="parameters">参数</param>
    /// <returns>影响的行数</returns>
    public int Execute(string sql, params object[] parameters)
    {
        using var command = CreateCommand(sql, parameters);
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// 执行 SQL 更新
    /// </summary>
    /// <param name="sql">SQL 语句</param>
    /// <param name="parameters">参数</param>
    /// <returns>影响的行数</returns>
    public int Execute(string sql, IDictionary<string, object> parameters)
    {
        using var command = CreateCommand(sql, parameters);
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// 执行 SQL 查询并返回单个值
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    /// <param name="sql">SQL 语句</param>
    /// <param name="parameters">参数</param>
    /// <returns>查询结果</returns>
    public T ExecuteScalar<T>(string sql, params object[] parameters)
    {
        using var command = CreateCommand(sql, parameters);
        var result = command.ExecuteScalar();
        return result == DBNull.Value ? default : (T)result;
    }

    /// <summary>
    /// 执行 SQL 查询并返回单个值
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    /// <param name="sql">SQL 语句</param>
    /// <param name="parameters">参数</param>
    /// <returns>查询结果</returns>
    public T ExecuteScalar<T>(string sql, IDictionary<string, object> parameters)
    {
        using var command = CreateCommand(sql, parameters);
        var result = command.ExecuteScalar();
        return result == DBNull.Value ? default : (T)result;
    }

    /// <summary>
    /// 创建命令
    /// </summary>
    /// <param name="sql">SQL 语句</param>
    /// <param name="parameters">参数</param>
    /// <returns>命令</returns>
    private IDbCommand CreateCommand(string sql, params object[] parameters)
    {
        var command = _connection.CreateCommand();
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

        return command;
    }

    /// <summary>
    /// 创建命令
    /// </summary>
    /// <param name="sql">SQL 语句</param>
    /// <param name="parameters">参数</param>
    /// <returns>命令</returns>
    private IDbCommand CreateCommand(string sql, IDictionary<string, object> parameters)
    {
        var command = _connection.CreateCommand();
        command.CommandText = sql;

        if (parameters != null && parameters.Count > 0)
        {
            foreach (var (name, value) in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = name.StartsWith("@") ? name : $"@{name}";
                parameter.Value = value ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }
        }

        return command;
    }

    /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="fields">字段列表</param>
        /// <param name="where">条件</param>
        /// <param name="page">分页信息</param>
        /// <returns>分页结果</returns>
        public PageResult<T> Page<T>(IDbConnection conn, Collection<string> fields, object where, WellTool.DB.Sql.Page page)
        {
            // 默认实现，子类可重写
            return new PageResult<T>(new List<T>(), 0, page.PageNumber, page.PageSize);
        }

    /// <summary>
    /// 设置包装器
    /// </summary>
    /// <param name="wrapper">包装器</param>
    public void SetWrapper(object wrapper)
    {
        // 默认实现，子类可重写
    }

    /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="record">记录</param>
    /// <returns>插入行数</returns>
    public int Insert(IDbConnection conn, Entity record)
    {
        // 默认实现，子类可重写
        return 0;
    }

    /// <summary>
    /// 插入数据并返回生成的主键
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="record">记录</param>
    /// <returns>生成的主键</returns>
    public long InsertForGeneratedKey(IDbConnection conn, Entity record)
    {
        // 默认实现，子类可重写
        return 0;
    }

    /// <summary>
    /// 插入或更新数据
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="record">记录</param>
    /// <param name="keys">需要检查唯一性的字段</param>
    /// <returns>插入或更新的行数</returns>
    public int InsertOrUpdate(IDbConnection conn, Entity record, params string[] keys)
    {
        // 默认实现，子类可重写
        return 0;
    }

    /// <summary>
    /// 批量插入数据
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="records">记录列表</param>
    /// <returns>插入行数</returns>
    public int Insert(IDbConnection conn, params Entity[] records)
    {
        // 默认实现，子类可重写
        return 0;
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="where">条件</param>
    /// <returns>删除行数</returns>
    public int Delete(IDbConnection conn, Entity where)
    {
        // 默认实现，子类可重写
        return 0;
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="record">记录</param>
    /// <param name="where">条件</param>
    /// <returns>更新行数</returns>
    public int Update(IDbConnection conn, Entity record, Entity where)
    {
        // 默认实现，子类可重写
        return 0;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    /// <param name="conn">数据库连接</param>
    /// <param name="fields">字段列表</param>
    /// <param name="where">条件</param>
    /// <param name="rsh">结果集处理器</param>
    /// <returns>查询结果</returns>
    public T Find<T>(IDbConnection conn, Collection<string> fields, Entity where, RsHandler<T> rsh)
    {
        // 默认实现，子类可重写
        return default(T);
    }

    /// <summary>
    /// 统计记录数
    /// </summary>
    /// <param name="conn">数据库连接</param>
    /// <param name="where">条件</param>
    /// <returns>记录数</returns>
    public long Count(IDbConnection conn, Entity where)
    {
        // 默认实现，子类可重写
        return 0;
    }
}
