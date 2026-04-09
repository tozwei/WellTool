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

namespace WellTool.DB.Tests;

/// <summary>
/// 数据源测试
/// </summary>
public class DsTest
{
    /// <summary>
    /// 测试数据源操作
    /// </summary>
    [Fact]
    public void TestDataSource()
    {
        // 测试数据源操作
        // 使用 SQLite 内存数据库进行测试
        var connectionString = "Data Source=:memory:";

        // 创建 SQLite 连接
        using var connection = new System.Data.SQLite.SQLiteConnection(connectionString);
        connection.Open();

        // 验证连接状态
        Assert.Equal(System.Data.ConnectionState.Open, connection.State);

        // 创建测试表
        using var createTableCmd = connection.CreateCommand();
        createTableCmd.CommandText = @"CREATE TABLE TestTable (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT,
            Value INTEGER
        )";
        createTableCmd.ExecuteNonQuery();

        // 插入测试数据
        using var insertCmd = connection.CreateCommand();
        insertCmd.CommandText = "INSERT INTO TestTable (Name, Value) VALUES (@Name, @Value)";
        insertCmd.Parameters.AddWithValue("@Name", "Test");
        insertCmd.Parameters.AddWithValue("@Value", 123);
        var rowsAffected = insertCmd.ExecuteNonQuery();
        Assert.Equal(1, rowsAffected);

        // 测试查询
        using var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM TestTable WHERE Id = @Id";
        selectCmd.Parameters.AddWithValue("@Id", 1);

        using var reader = selectCmd.ExecuteReader();
        Assert.True(reader.Read());
        Assert.Equal("Test", reader["Name"]);
        Assert.Equal(123, reader["Value"]);
        reader.Close();

        // 关闭连接
        connection.Close();
        Assert.Equal(System.Data.ConnectionState.Closed, connection.State);
    }
}