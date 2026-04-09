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
/// Issue I73770 测试
/// </summary>
public class IssueI73770Test
{
    /// <summary>
    /// 测试 Issue I73770
    /// </summary>
    [Fact]
    public void TestIssueI73770()
    {
        // 测试 Issue I73770
        // 使用 SQLite 内存数据库进行测试
        using var connection = new System.Data.SQLite.SQLiteConnection("Data Source=:memory:");
        connection.Open();

        // 创建测试表
        using var createTableCmd = connection.CreateCommand();
        createTableCmd.CommandText = @"CREATE TABLE TestTable (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT,
            Age INTEGER,
            Address TEXT
        )";
        createTableCmd.ExecuteNonQuery();

        // 批量插入测试数据
        using var transaction = connection.BeginTransaction();
        try
        {
            for (int i = 1; i <= 5; i++)
            {
                using var insertCmd = connection.CreateCommand();
                insertCmd.Transaction = transaction;
                insertCmd.CommandText = "INSERT INTO TestTable (Name, Age, Address) VALUES (@Name, @Age, @Address)";
                insertCmd.Parameters.AddWithValue("@Name", "User" + i);
                insertCmd.Parameters.AddWithValue("@Age", 20 + i);
                insertCmd.Parameters.AddWithValue("@Address", "Address" + i);
                insertCmd.ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }

        // 测试批量查询
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

        // 测试事务回滚
        using var rollbackTransaction = connection.BeginTransaction();
        try
        {
            // 删除所有数据
            using var deleteCmd = connection.CreateCommand();
            deleteCmd.Transaction = rollbackTransaction;
            deleteCmd.CommandText = "DELETE FROM TestTable";
            deleteCmd.ExecuteNonQuery();

            // 验证数据已删除
            using var countCmd = connection.CreateCommand();
            countCmd.Transaction = rollbackTransaction;
            countCmd.CommandText = "SELECT COUNT(*) FROM TestTable";
            var deletedCount = (long)countCmd.ExecuteScalar();
            Assert.Equal(0, deletedCount);

            // 回滚事务
            rollbackTransaction.Rollback();
        }
        catch (Exception)
        {
            rollbackTransaction.Rollback();
            throw;
        }

        // 验证数据恢复
        using var finalCountCmd = connection.CreateCommand();
        finalCountCmd.CommandText = "SELECT COUNT(*) FROM TestTable";
        var finalCount = (long)finalCountCmd.ExecuteScalar();
        Assert.Equal(5, finalCount);
    }
}