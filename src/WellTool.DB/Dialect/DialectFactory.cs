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

using System.Data.Common;

namespace WellTool.DB.Dialect;

/// <summary>
/// 数据库方言工厂
/// </summary>
public static class DialectFactory
{
    /// <summary>
    /// 方言映射
    /// </summary>
    private static readonly Dictionary<string, IDialect> _dialects = new()
    {
        { "System.Data.SqlClient", new Impl.SqlServerDialect() },
        { "Microsoft.Data.SqlClient", new Impl.SqlServerDialect() },
        { "MySql.Data.MySqlClient", new Impl.MySqlDialect() },
        { "Npgsql", new Impl.PostgreSqlDialect() }
    };

    /// <summary>
    /// 根据提供程序名称获取方言
    /// </summary>
    /// <param name="providerName">提供程序名称</param>
    /// <returns>方言</returns>
    public static IDialect GetDialect(string providerName)
    {
        if (_dialects.TryGetValue(providerName, out var dialect))
        {
            return dialect;
        }
        return new Impl.SqlServerDialect(); // 默认使用 SQL Server 方言
    }

    /// <summary>
    /// 根据连接获取方言
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <returns>方言</returns>
    public static IDialect GetDialect(DbConnection connection)
    {
        return GetDialect(connection.GetType().Assembly.GetName().Name);
    }

    /// <summary>
    /// 注册方言
    /// </summary>
    /// <param name="providerName">提供程序名称</param>
    /// <param name="dialect">方言</param>
    public static void RegisterDialect(string providerName, IDialect dialect)
    {
        _dialects[providerName] = dialect;
    }
}