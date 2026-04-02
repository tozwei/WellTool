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
using WellTool.DB.Handler;

namespace WellTool.DB.Sql;

/// <summary>
/// SQL 执行器
/// </summary>
public class SqlExecutor
{
    /// <summary>
    /// 连接
    /// </summary>
    private readonly IDbConnection _connection;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="connection">连接</param>
    public SqlExecutor(IDbConnection connection)
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
}
