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
using Xunit;
using WellTool.DB;

namespace WellTool.DB.Tests;

/// <summary>
/// H2 数据库测试
/// </summary>
public class H2Test
{
    /// <summary>
    /// 测试 H2 数据库连接
    /// </summary>
    [Fact]
    public void TestH2Connection()
    {
        // 测试 H2 数据库连接
        var connectionString = "jdbc:h2:mem:testdb;DB_CLOSE_DELAY=-1";
        var dataSource = new TestDataSource(connectionString, "H2Driver");
        var db = new TestDb(dataSource);

        // 测试获取连接
        using var connection = db.GetConnection();
        Assert.NotNull(connection);
        Assert.Equal(connectionString, connection.ConnectionString);

        // 测试执行SQL语句
        var sql = "CREATE TABLE users (id INT PRIMARY KEY, name VARCHAR(50))";
        var result = db.Execute(sql);
        
        // 由于使用的是模拟连接，这里会返回0，但测试可以验证方法调用是否正常
        Assert.Equal(0, result);

        // 测试插入数据
        var insertSql = "INSERT INTO users (id, name) VALUES (?, ?)";
        var insertResult = db.Execute(insertSql, 1, "test");
        Assert.Equal(0, insertResult);

        // 测试查询数据
        var querySql = "SELECT * FROM users WHERE id = ?";
        var queryResult = db.Query(querySql, 1);
        Assert.NotNull(queryResult);
    }

    /// <summary>
    /// 测试 H2 数据库的事务操作
    /// </summary>
    [Fact]
    public void TestH2Transaction()
    {
        // 测试 H2 数据库的事务操作
        var connectionString = "jdbc:h2:mem:testdb;DB_CLOSE_DELAY=-1";
        var dataSource = new TestDataSource(connectionString, "H2Driver");
        var db = new TestDb(dataSource);

        // 测试事务操作（这里只是验证方法调用，实际执行需要真实数据库）
        using var connection = db.GetConnection();
        var transaction = connection.BeginTransaction();
        
        try
        {
            // 执行操作
            var sql = "INSERT INTO users (id, name) VALUES (?, ?)";
            var result = db.Execute(sql, 2, "test2");
            Assert.Equal(0, result);
            
            // 提交事务
            transaction?.Commit();
        }
        catch
        {
            // 回滚事务
            transaction?.Rollback();
            throw;
        }
    }

    /// <summary>
    /// 测试 H2 数据库的DDL操作
    /// </summary>
    [Fact]
    public void TestH2DDL()
    {
        // 测试 H2 数据库的DDL操作
        var connectionString = "jdbc:h2:mem:testdb;DB_CLOSE_DELAY=-1";
        var dataSource = new TestDataSource(connectionString, "H2Driver");
        var db = new TestDb(dataSource);

        // 测试创建表
        var createSql = "CREATE TABLE products (id INT PRIMARY KEY, name VARCHAR(100), price DECIMAL(10, 2))";
        var createResult = db.Execute(createSql);
        Assert.Equal(0, createResult);

        // 测试修改表
        var alterSql = "ALTER TABLE products ADD COLUMN description VARCHAR(255)";
        var alterResult = db.Execute(alterSql);
        Assert.Equal(0, alterResult);

        // 测试删除表
        var dropSql = "DROP TABLE products";
        var dropResult = db.Execute(dropSql);
        Assert.Equal(0, dropResult);
    }

    /// <summary>
    /// 测试 H2 数据库的特殊功能
    /// </summary>
    [Fact]
    public void TestH2SpecialFeatures()
    {
        // 测试 H2 数据库的特殊功能
        var connectionString = "jdbc:h2:mem:testdb;DB_CLOSE_DELAY=-1";
        var dataSource = new TestDataSource(connectionString, "H2Driver");
        var db = new TestDb(dataSource);

        // 测试 H2 特有的功能，如内存表
        var sql = "CREATE TABLE memory_table (id INT PRIMARY KEY, value VARCHAR(100)) ENGINE=MEMORY";
        var result = db.Execute(sql);
        Assert.Equal(0, result);

        // 测试插入和查询
        var insertSql = "INSERT INTO memory_table (id, value) VALUES (?, ?)";
        var insertResult = db.Execute(insertSql, 1, "test value");
        Assert.Equal(0, insertResult);

        var querySql = "SELECT * FROM memory_table";
        var queryResult = db.Query(querySql);
        Assert.NotNull(queryResult);
    }
}
