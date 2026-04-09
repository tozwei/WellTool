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

namespace WellTool.DB.Tests.DS;

/// <summary>
/// Issue I70J95 测试
/// </summary>
public class IssueI70J95Test
{
    /// <summary>
    /// 测试 Issue I70J95
    /// </summary>
    [Fact]
    public void TestIssueI70J95()
    {
        // 测试 Issue I70J95
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
        for (int i = 1; i <= 5; i++)
        {
            using var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = "INSERT INTO TestTable (Name, Age) VALUES (@Name, @Age)";
            insertCmd.Parameters.AddWithValue("@Name", "User" + i);
            insertCmd.Parameters.AddWithValue("@Age", 20 + i);
            insertCmd.ExecuteNonQuery();
        }

        // 测试查询操作
        using var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM TestTable WHERE Age > @Age";
        selectCmd.Parameters.AddWithValue("@Age", 22);

        using var reader = selectCmd.ExecuteReader();
        var count = 0;
        while (reader.Read())
        {
            count++;
            var age = Convert.ToInt32(reader["Age"]);
            Assert.True(age > 22);
        }
        reader.Close();
        Assert.Equal(3, count); // 23, 24, 25

        // 测试事务操作
        using var transaction = connection.BeginTransaction();
        try
        {
            // 更新数据
            using var updateCmd = connection.CreateCommand();
            updateCmd.Transaction = transaction;
            updateCmd.CommandText = "UPDATE TestTable SET Age = @Age WHERE Id = @Id";
            updateCmd.Parameters.AddWithValue("@Age", 99);
            updateCmd.Parameters.AddWithValue("@Id", 1);
            var rowsAffected = updateCmd.ExecuteNonQuery();
            Assert.Equal(1, rowsAffected);

            // 提交事务
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }

        // 验证更新结果
        using var selectUpdatedCmd = connection.CreateCommand();
        selectUpdatedCmd.CommandText = "SELECT Age FROM TestTable WHERE Id = @Id";
        selectUpdatedCmd.Parameters.AddWithValue("@Id", 1);
        var updatedAge = (long)selectUpdatedCmd.ExecuteScalar();
        Assert.Equal(99, updatedAge);
    }
}