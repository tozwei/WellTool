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
using System.Data.SqlClient;
using Xunit;

namespace WellTool.DB.Tests;

/// <summary>
/// Db 类测试
/// </summary>
public class DbTest
{
    /// <summary>
    /// 测试获取默认数据库实例
    /// </summary>
    [Fact]
    public void TestUse()
    {
        var db1 = Db.Use();
        var db2 = Db.Use();
        Assert.Same(db1, db2);
    }

    /// <summary>
    /// 测试设置和获取数据库连接
    /// </summary>
    [Fact]
    public void TestSetAndGetConnection()
    {
        var connection = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
        var db = Db.Use().SetConnection(connection);
        Assert.Same(connection, db.GetConnection());
    }

    /// <summary>
    /// 测试打开和关闭数据库连接
    /// </summary>
    [Fact]
    public void TestOpenAndClose()
    {
        var connection = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
        var db = Db.Use().SetConnection(connection);
        db.Open();
        Assert.Equal(ConnectionState.Open, connection.State);
        db.Close();
        Assert.Equal(ConnectionState.Closed, connection.State);
    }

    /// <summary>
    /// 测试获取方言
    /// </summary>
    [Fact]
    public void TestGetDialect()
    {
        var connection = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
        var db = Db.Use().SetConnection(connection);
        var dialect = db.GetDialect();
        Assert.NotNull(dialect);
    }

    /// <summary>
    /// 测试获取 SQL 执行器
    /// </summary>
    [Fact]
    public void TestGetSqlExecutor()
    {
        var connection = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
        var db = Db.Use().SetConnection(connection);
        var sqlExecutor = db.GetSqlExecutor();
        Assert.NotNull(sqlExecutor);
    }
}
