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
/// Issue I9BANE 测试
/// </summary>
public class IssueI9BANETest
{
    /// <summary>
    /// 测试 Issue I9BANE
    /// </summary>
    [Fact]
    public void TestIssueI9BANE()
    {
        // 测试 Issue I9BANE
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

        // 测试查询操作
        using var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM TestTable WHERE Id = @Id";
        selectCmd.Parameters.AddWithValue("@Id", 1);

        using var reader = selectCmd.ExecuteReader();
        Assert.True(reader.Read());
        Assert.Equal("John", reader["Name"]);
        Assert.Equal(30, reader["Age"]);
        reader.Close();

        // 测试更新操作
        using var updateCmd = connection.CreateCommand();
        updateCmd.CommandText = "UPDATE TestTable SET Name = @Name, Age = @Age WHERE Id = @Id";
        updateCmd.Parameters.AddWithValue("@Name", "Jane");
        updateCmd.Parameters.AddWithValue("@Age", 25);
        updateCmd.Parameters.AddWithValue("@Id", 1);
        var rowsAffected = updateCmd.ExecuteNonQuery();
        Assert.Equal(1, rowsAffected);

        // 验证更新结果
        using var selectUpdatedCmd = connection.CreateCommand();
        selectUpdatedCmd.CommandText = "SELECT * FROM TestTable WHERE Id = @Id";
        selectUpdatedCmd.Parameters.AddWithValue("@Id", 1);

        using var updatedReader = selectUpdatedCmd.ExecuteReader();
        Assert.True(updatedReader.Read());
        Assert.Equal("Jane", updatedReader["Name"]);
        Assert.Equal(25, updatedReader["Age"]);
        updatedReader.Close();

        // 测试删除操作
        using var deleteCmd = connection.CreateCommand();
        deleteCmd.CommandText = "DELETE FROM TestTable WHERE Id = @Id";
        deleteCmd.Parameters.AddWithValue("@Id", 1);
        rowsAffected = deleteCmd.ExecuteNonQuery();
        Assert.Equal(1, rowsAffected);

        // 验证删除结果
        using var selectDeletedCmd = connection.CreateCommand();
        selectDeletedCmd.CommandText = "SELECT COUNT(*) FROM TestTable";
        var count = (long)selectDeletedCmd.ExecuteScalar();
        Assert.Equal(0, count);
    }
}