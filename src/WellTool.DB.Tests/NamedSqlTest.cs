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

using Xunit;

namespace WellTool.DB.Tests;

/// <summary>
/// 命名 SQL 测试
/// </summary>
public class NamedSqlTest
{
    /// <summary>
    /// 测试命名 SQL
    /// </summary>
    [Fact]
    public void TestNamedSql()
    {
        // 测试命名参数的 SQL 查询
        // 使用 SQLite 内存数据库进行测试
        using var connection = new System.Data.SQLite.SQLiteConnection("Data Source=:memory:");
        connection.Open();

        // 创建测试表
        using var createTableCmd = connection.CreateCommand();
        createTableCmd.CommandText = @"CREATE TABLE TestTable (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT,
            Age INTEGER
        )";
        createTableCmd.ExecuteNonQuery();

        // 插入测试数据
        using var insertCmd = connection.CreateCommand();
        insertCmd.CommandText = "INSERT INTO TestTable (Name, Age) VALUES (@Name, @Age)";
        insertCmd.Parameters.AddWithValue("@Name", "John");
        insertCmd.Parameters.AddWithValue("@Age", 30);
        insertCmd.ExecuteNonQuery();

        // 测试命名参数查询
        using var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM TestTable WHERE Name = @Name AND Age = @Age";
        selectCmd.Parameters.AddWithValue("@Name", "John");
        selectCmd.Parameters.AddWithValue("@Age", 30);

        using var reader = selectCmd.ExecuteReader();
        Assert.True(reader.Read());
        Assert.Equal("John", reader["Name"]);
        Assert.Equal(30, reader["Age"]);
        reader.Close();

        // 测试多个命名参数的情况
        using var insertMultipleCmd = connection.CreateCommand();
        insertMultipleCmd.CommandText = "INSERT INTO TestTable (Name, Age) VALUES (@Name1, @Age1), (@Name2, @Age2)";
        insertMultipleCmd.Parameters.AddWithValue("@Name1", "Jane");
        insertMultipleCmd.Parameters.AddWithValue("@Age1", 25);
        insertMultipleCmd.Parameters.AddWithValue("@Name2", "Bob");
        insertMultipleCmd.Parameters.AddWithValue("@Age2", 35);
        insertMultipleCmd.ExecuteNonQuery();

        // 验证数据已插入
        using var selectAllCmd = connection.CreateCommand();
        selectAllCmd.CommandText = "SELECT COUNT(*) FROM TestTable";
        var count = (long)selectAllCmd.ExecuteScalar();
        Assert.Equal(3, count);
    }
}