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
/// CRUD 测试
/// </summary>
public class CRUDTest
{
    /// <summary>
    /// 测试 CRUD 操作
    /// </summary>
    [Fact]
    public void TestCRUD()
    {
        // 测试完整的 CRUD 操作
        // 使用 SQLite 内存数据库进行测试
        using var connection = new System.Data.SQLite.SQLiteConnection("Data Source=:memory:");
        connection.Open();

        // 创建测试表
        using var createTableCmd = connection.CreateCommand();
        createTableCmd.CommandText = @"CREATE TABLE TestCRUD (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT,
            Age INTEGER,
            Email TEXT
        )";
        createTableCmd.ExecuteNonQuery();

        // 1. 测试创建操作 (Create)
        using var insertCmd = connection.CreateCommand();
        insertCmd.CommandText = "INSERT INTO TestCRUD (Name, Age, Email) VALUES (@Name, @Age, @Email)";
        insertCmd.Parameters.AddWithValue("@Name", "John Doe");
        insertCmd.Parameters.AddWithValue("@Age", 30);
        insertCmd.Parameters.AddWithValue("@Email", "john@example.com");
        var rowsAffected = insertCmd.ExecuteNonQuery();
        Assert.Equal(1, rowsAffected);

        // 2. 测试读取操作 (Read)
        using var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM TestCRUD WHERE Id = @Id";
        selectCmd.Parameters.AddWithValue("@Id", 1);

        using var reader = selectCmd.ExecuteReader();
        Assert.True(reader.Read());
        Assert.Equal("John Doe", reader["Name"]);
        Assert.Equal(30, reader["Age"]);
        Assert.Equal("john@example.com", reader["Email"]);
        reader.Close();

        // 3. 测试更新操作 (Update)
        using var updateCmd = connection.CreateCommand();
        updateCmd.CommandText = "UPDATE TestCRUD SET Name = @Name, Age = @Age, Email = @Email WHERE Id = @Id";
        updateCmd.Parameters.AddWithValue("@Name", "Jane Smith");
        updateCmd.Parameters.AddWithValue("@Age", 25);
        updateCmd.Parameters.AddWithValue("@Email", "jane@example.com");
        updateCmd.Parameters.AddWithValue("@Id", 1);
        rowsAffected = updateCmd.ExecuteNonQuery();
        Assert.Equal(1, rowsAffected);

        // 验证更新结果
        using var selectUpdatedCmd = connection.CreateCommand();
        selectUpdatedCmd.CommandText = "SELECT * FROM TestCRUD WHERE Id = @Id";
        selectUpdatedCmd.Parameters.AddWithValue("@Id", 1);

        using var updatedReader = selectUpdatedCmd.ExecuteReader();
        Assert.True(updatedReader.Read());
        Assert.Equal("Jane Smith", updatedReader["Name"]);
        Assert.Equal(25, updatedReader["Age"]);
        Assert.Equal("jane@example.com", updatedReader["Email"]);
        updatedReader.Close();

        // 4. 测试删除操作 (Delete)
        using var deleteCmd = connection.CreateCommand();
        deleteCmd.CommandText = "DELETE FROM TestCRUD WHERE Id = @Id";
        deleteCmd.Parameters.AddWithValue("@Id", 1);
        rowsAffected = deleteCmd.ExecuteNonQuery();
        Assert.Equal(1, rowsAffected);

        // 验证删除结果
        using var selectDeletedCmd = connection.CreateCommand();
        selectDeletedCmd.CommandText = "SELECT COUNT(*) FROM TestCRUD";
        var count = (long)selectDeletedCmd.ExecuteScalar();
        Assert.Equal(0, count);

        // 关闭连接
        connection.Close();
    }
}