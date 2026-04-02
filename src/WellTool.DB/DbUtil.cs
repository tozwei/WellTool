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

namespace WellTool.DB;

/// <summary>
    /// 数据库工具类
    /// </summary>
    public static class DbUtil
    {


        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="providerName">提供程序名称</param>
        /// <returns>数据库连接</returns>
        public static DbConnection CreateConnection(string connectionString, string providerName = "System.Data.SqlClient")
        {
            var factory = DbProviderFactories.GetFactory(providerName);
            var connection = factory.CreateConnection();
            if (connection == null)
            {
                throw new DbException("Failed to create database connection");
            }
            connection.ConnectionString = connectionString;
            return connection;
        }

        /// <summary>
        /// 执行 SQL 查询
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>数据读取器</returns>
        public static DbDataReader ExecuteReader(DbConnection connection, string sql, params DbParameter[] parameters)
        {
            var command = CreateCommand(connection, sql, parameters);
            return command.ExecuteReader();
        }

        /// <summary>
        /// 执行 SQL 语句并返回受影响的行数
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(DbConnection connection, string sql, params DbParameter[] parameters)
        {
            var command = CreateCommand(connection, sql, parameters);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行 SQL 查询并返回第一行第一列的值
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        public static object? ExecuteScalar(DbConnection connection, string sql, params DbParameter[] parameters)
        {
            var command = CreateCommand(connection, sql, parameters);
            return command.ExecuteScalar();
        }

        /// <summary>
        /// 创建数据库命令
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>数据库命令</returns>
        private static DbCommand CreateCommand(DbConnection connection, string sql, params DbParameter[] parameters)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;
            if (parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }
            return command;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="connection">数据库连接</param>
        public static void CloseConnection(DbConnection? connection)
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>事务</returns>
        public static DbTransaction BeginTransaction(DbConnection connection)
        {
            return connection.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction">事务</param>
        public static void CommitTransaction(DbTransaction? transaction)
        {
            transaction?.Commit();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="transaction">事务</param>
        public static void RollbackTransaction(DbTransaction? transaction)
        {
            transaction?.Rollback();
        }

        /// <summary>
        /// 给字符串添加引号
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>带引号的字符串</returns>
        public static string Quote(string value)
        {
            return $"'{value}'";
        }

        /// <summary>
        /// 如果字符串没有被引号包裹，则添加引号
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>带引号的字符串</returns>
        public static string QuoteIfNotQuoted(string value)
        {
            if (IsQuoted(value))
            {
                return value;
            }
            return Quote(value);
        }

        /// <summary>
        /// 去除字符串的引号
        /// </summary>
        /// <param name="value">带引号的字符串</param>
        /// <returns>不带引号的字符串</returns>
        public static string Unquote(string value)
        {
            if (IsQuoted(value))
            {
                return value.Substring(1, value.Length - 2);
            }
            return value;
        }

        /// <summary>
        /// 检查字符串是否被引号包裹
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns>是否被引号包裹</returns>
        public static bool IsQuoted(string value)
        {
            return !string.IsNullOrEmpty(value) && value.StartsWith("'") && value.EndsWith("'");
        }
}
