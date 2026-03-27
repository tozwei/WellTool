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
using System.Data.Common;
using System.Collections.Generic;

namespace WellTool.DB;

/// <summary>
/// 数据库操作类
/// </summary>
public class Db : IDisposable
{
    /// <summary>
    /// 数据库连接
    /// </summary>
    private DbConnection _connection;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="connection">数据库连接</param>
    public Db(DbConnection connection)
    {
        _connection = connection;
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="providerName">提供程序名称</param>
    public Db(string connectionString, string providerName = "System.Data.SqlClient")
        : this(DbUtil.CreateConnection(connectionString, providerName))
    {
    }

    /// <summary>
    /// 执行 SQL 查询并返回实体列表
    /// </summary>
    /// <param name="sql">SQL 语句</param>
    /// <param name="parameters">参数</param>
    /// <returns>实体列表</returns>
    public List<Entity> Query(string sql, params DbParameter[] parameters)
    {
        var result = new List<Entity>();
        using var reader = DbUtil.ExecuteReader(_connection, sql, parameters);
        while (reader.Read())
        {
            var entity = new Entity();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                entity[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
            }
            result.Add(entity);
        }
        return result;
    }

    /// <summary>
    /// 执行 SQL 语句并返回受影响的行数
    /// </summary>
    /// <param name="sql">SQL 语句</param>
    /// <param name="parameters">参数</param>
    /// <returns>受影响的行数</returns>
    public int Execute(string sql, params DbParameter[] parameters)
    {
        return DbUtil.ExecuteNonQuery(_connection, sql, parameters);
    }

    /// <summary>
    /// 执行 SQL 查询并返回第一行第一列的值
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="sql">SQL 语句</param>
    /// <param name="parameters">参数</param>
    /// <returns>查询结果</returns>
    public T? Scalar<T>(string sql, params DbParameter[] parameters)
    {
        var result = DbUtil.ExecuteScalar(_connection, sql, parameters);
        if (result == null || result == DBNull.Value)
        {
            return default;
        }
        return (T)result;
    }

    /// <summary>
    /// 插入实体
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="entity">实体</param>
    /// <returns>受影响的行数</returns>
    public int Insert(string tableName, Entity entity)
    {
        var fields = entity.GetFieldNames();
        var fieldNames = string.Join(", ", fields);
        var placeholders = string.Join(", ", fields.Select(f => $"@{f}"));
        var sql = $"INSERT INTO {tableName} ({fieldNames}) VALUES ({placeholders})";
        var parameters = fields.Select(f => CreateParameter(f, entity[f])).ToArray();
        return Execute(sql, parameters);
    }

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="entity">实体</param>
    /// <param name="whereClause">WHERE 子句</param>
    /// <param name="whereParameters">WHERE 子句参数</param>
    /// <returns>受影响的行数</returns>
    public int Update(string tableName, Entity entity, string whereClause, params DbParameter[] whereParameters)
    {
        var fields = entity.GetFieldNames();
        var setClause = string.Join(", ", fields.Select(f => $"{f} = @{f}"));
        var sql = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";
        var parameters = fields.Select(f => CreateParameter(f, entity[f])).Concat(whereParameters).ToArray();
        return Execute(sql, parameters);
    }

    /// <summary>
    /// 删除记录
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="whereClause">WHERE 子句</param>
    /// <param name="parameters">参数</param>
    /// <returns>受影响的行数</returns>
    public int Delete(string tableName, string whereClause, params DbParameter[] parameters)
    {
        var sql = $"DELETE FROM {tableName} WHERE {whereClause}";
        return Execute(sql, parameters);
    }

    /// <summary>
    /// 创建数据库参数
    /// </summary>
    /// <param name="name">参数名</param>
    /// <param name="value">参数值</param>
    /// <returns>数据库参数</returns>
    private DbParameter CreateParameter(string name, object? value)
    {
        var parameter = _connection.CreateCommand().CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value ?? DBNull.Value;
        return parameter;
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        DbUtil.CloseConnection(_connection);
        _connection.Dispose();
    }
}
