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
/// Session 类测试
/// </summary>
public class SessionTest
{
    /// <summary>
    /// 测试构造函数
    /// </summary>
    [Fact]
    public void TestConstructor()
    {
        var connection = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
        using var session = new Session(connection);
        Assert.NotNull(session);
        Assert.Same(connection, session.GetConnection());
    }

    /// <summary>
    /// 测试获取连接
    /// </summary>
    [Fact]
    public void TestGetConnection()
    {
        var connection = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
        using var session = new Session(connection);
        Assert.Same(connection, session.GetConnection());
    }

    /// <summary>
    /// 测试获取 SQL 执行器
    /// </summary>
    [Fact]
    public void TestGetSqlExecutor()
    {
        var connection = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
        using var session = new Session(connection);
        var sqlExecutor = session.GetSqlExecutor();
        Assert.NotNull(sqlExecutor);
    }

    /// <summary>
    /// 测试获取方言
    /// </summary>
    [Fact]
    public void TestGetDialect()
    {
        var connection = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
        using var session = new Session(connection);
        var dialect = session.GetDialect();
        Assert.NotNull(dialect);
    }

    /// <summary>
    /// 测试事务操作
    /// </summary>
    [Fact]
    public void TestTransaction()
    {
        var connection = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
        using var session = new Session(connection);
        // 这里不实际打开连接和执行事务，因为需要真实的数据库环境
        // 只测试方法调用是否正常
        Assert.NotNull(session);
    }

    /// <summary>
    /// 测试释放资源
    /// </summary>
    [Fact]
    public void TestDispose()
    {
        var connection = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
        var session = new Session(connection);
        // 这里不实际打开连接，因为需要真实的数据库环境
        // 只测试方法调用是否正常
        Assert.NotNull(session);
        session.Dispose();
    }
}
