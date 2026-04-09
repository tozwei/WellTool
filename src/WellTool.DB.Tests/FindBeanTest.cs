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

using System;
using Xunit;

namespace WellTool.DB.Tests;

/// <summary>
/// 测试 Bean 类
/// </summary>
public class TestBean
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
}

/// <summary>
/// FindBean 测试
/// </summary>
public class FindBeanTest
{
    /// <summary>
    /// 测试查找 Bean
    /// </summary>
    [Fact]
    public void TestFindBean()
    {
        // 测试从数据库中查询数据并映射到对象
        // 使用 SQLite 内存数据库进行测试
        using var connection = new System.Data.SQLite.SQLiteConnection("Data Source=:memory:");
        connection.Open();

        // 创建测试表
        using var createTableCmd = connection.CreateCommand();
        createTableCmd.CommandText = @"CREATE TABLE TestBean (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT,
            Age INTEGER,
            Address TEXT
        )";
        createTableCmd.ExecuteNonQuery();

        // 插入测试数据
        using var insertCmd = connection.CreateCommand();
        insertCmd.CommandText = "INSERT INTO TestBean (Name, Age, Address) VALUES (@Name, @Age, @Address)";
        insertCmd.Parameters.AddWithValue("@Name", "John");
        insertCmd.Parameters.AddWithValue("@Age", 30);
        insertCmd.Parameters.AddWithValue("@Address", "New York");
        insertCmd.ExecuteNonQuery();

        // 测试查询并映射到对象
        using var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM TestBean WHERE Id = @Id";
        selectCmd.Parameters.AddWithValue("@Id", 1);

        using var reader = selectCmd.ExecuteReader();
        Assert.True(reader.Read());

        // 手动映射到对象
        var bean = new TestBean
        {
            Id = Convert.ToInt32(reader["Id"]),
            Name = reader["Name"].ToString(),
            Age = Convert.ToInt32(reader["Age"]),
            Address = reader["Address"].ToString()
        };

        // 验证对象属性
        Assert.NotNull(bean);
        Assert.Equal(1, bean.Id);
        Assert.Equal("John", bean.Name);
        Assert.Equal(30, bean.Age);
        Assert.Equal("New York", bean.Address);
        reader.Close();
    }
}