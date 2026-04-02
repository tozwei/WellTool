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

using WellTool.DB;
using WellTool.DB.Dialect.Impl;
using WellTool.DB.Sql;
using Xunit;

namespace WellTool.DB.Tests.Sql;

/// <summary>
/// SqlBuilder 类测试
/// </summary>
public class SqlBuilderTest
{
    /// <summary>
    /// 测试构造函数
    /// </summary>
    [Fact]
    public void TestConstructor()
    {
        var dialect = new AnsiSqlDialect();
        var sqlBuilder = new SqlBuilder(dialect);
        Assert.NotNull(sqlBuilder);
    }

    /// <summary>
    /// 测试 Append 方法
    /// </summary>
    [Fact]
    public void TestAppend()
    {
        var dialect = new AnsiSqlDialect();
        var sqlBuilder = new SqlBuilder(dialect);
        var result = sqlBuilder.Append("SELECT * FROM ").Append("user");
        Assert.Same(sqlBuilder, result);
        Assert.Equal("SELECT * FROM user", sqlBuilder.ToString());
    }

    /// <summary>
    /// 测试 AppendFormat 方法
    /// </summary>
    [Fact]
    public void TestAppendFormat()
    {
        var dialect = new AnsiSqlDialect();
        var sqlBuilder = new SqlBuilder(dialect);
        var result = sqlBuilder.Append("SELECT * FROM {0}", "user");
        Assert.Same(sqlBuilder, result);
        Assert.Equal("SELECT * FROM user", sqlBuilder.ToString());
    }

    /// <summary>
    /// 测试 AppendLine 方法
    /// </summary>
    [Fact]
    public void TestAppendLine()
    {
        var dialect = new AnsiSqlDialect();
        var sqlBuilder = new SqlBuilder(dialect);
        var result = sqlBuilder.AppendLine("SELECT * FROM user");
        Assert.Same(sqlBuilder, result);
        Assert.Equal("SELECT * FROM user\r\n", sqlBuilder.ToString());
    }

    /// <summary>
    /// 测试 Clear 方法
    /// </summary>
    [Fact]
    public void TestClear()
    {
        var dialect = new AnsiSqlDialect();
        var sqlBuilder = new SqlBuilder(dialect);
        sqlBuilder.Append("SELECT * FROM user");
        var result = sqlBuilder.Clear();
        Assert.Same(sqlBuilder, result);
        Assert.Equal(0, sqlBuilder.Length());
    }

    /// <summary>
    /// 测试 Length 方法
    /// </summary>
    [Fact]
    public void TestLength()
    {
        var dialect = new AnsiSqlDialect();
        var sqlBuilder = new SqlBuilder(dialect);
        sqlBuilder.Append("SELECT * FROM user");
        Assert.Equal(18, sqlBuilder.Length());
    }

    /// <summary>
    /// 测试 Insert 方法
    /// </summary>
    [Fact]
    public void TestInsert()
    {
        var dialect = new AnsiSqlDialect();
        var sqlBuilder = new SqlBuilder(dialect);
        var entity = new Entity("user");
        entity.Set("id", 1).Set("name", "test");
        var sql = sqlBuilder.Insert(entity);
        Assert.Equal("INSERT INTO \"user\" (\"id\", \"name\") VALUES ('1', 'test')", sql);
    }

    /// <summary>
    /// 测试 Delete 方法
    /// </summary>
    [Fact]
    public void TestDelete()
    {
        var dialect = new AnsiSqlDialect();
        var sqlBuilder = new SqlBuilder(dialect);
        var entity = new Entity("user");
        entity.Set("id", 1);
        var sql = sqlBuilder.Delete(entity);
        Assert.Equal("DELETE FROM \"user\" WHERE \"id\" = '1'", sql);
    }

    /// <summary>
    /// 测试 Update 方法
    /// </summary>
    [Fact]
    public void TestUpdate()
    {
        var dialect = new AnsiSqlDialect();
        var sqlBuilder = new SqlBuilder(dialect);
        var entity = new Entity("user");
        entity.Set("id", 1).Set("name", "test");
        var sql = sqlBuilder.Update(entity);
        Assert.Equal("UPDATE \"user\" SET \"id\" = '1', \"name\" = 'test' WHERE \"id\" = '1' AND \"name\" = 'test'", sql);
    }

    /// <summary>
    /// 测试 Select 方法
    /// </summary>
    [Fact]
    public void TestSelect()
    {
        var dialect = new AnsiSqlDialect();
        var sqlBuilder = new SqlBuilder(dialect);
        var entity = new Entity("user");
        entity.Set("id", 1);
        var sql = sqlBuilder.Select(entity);
        Assert.Equal("SELECT * FROM \"user\" WHERE \"id\" = '1'", sql);
    }
}
