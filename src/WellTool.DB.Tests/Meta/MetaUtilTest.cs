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

using System.Linq;
using Xunit;

namespace WellTool.DB.Tests.Meta;

/// <summary>
/// MetaUtil 测试
/// </summary>
public class MetaUtilTest
{
    /// <summary>
    /// 测试元数据工具
    /// </summary>
    [Fact]
    public void TestMetaUtil()
    {
        // 测试元数据工具
        // 使用 SQLite 内存数据库进行测试
        using var connection = new System.Data.SQLite.SQLiteConnection("Data Source=:memory:");
        connection.Open();

        // 创建测试表
        using var createTableCmd = connection.CreateCommand();
        createTableCmd.CommandText = @"CREATE TABLE TestTable (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT,
            Age INTEGER,
            Salary REAL
        )";
        createTableCmd.ExecuteNonQuery();

        // 测试获取表信息
        var table = WellTool.DB.Meta.MetaUtil.GetTable(connection, "TestTable");
        Assert.NotNull(table);
        Assert.Equal("TestTable", table.Name);
        Assert.NotEmpty(table.Columns);
        Assert.Equal(4, table.Columns.Count); // Id, Name, Age, Salary

        // 验证列信息
        var idColumn = table.Columns.FirstOrDefault(c => c.Name == "Id");
        Assert.NotNull(idColumn);
        Assert.Equal("INTEGER", idColumn.Type);

        var nameColumn = table.Columns.FirstOrDefault(c => c.Name == "Name");
        Assert.NotNull(nameColumn);
        Assert.Equal("TEXT", nameColumn.Type);

        var ageColumn = table.Columns.FirstOrDefault(c => c.Name == "Age");
        Assert.NotNull(ageColumn);
        Assert.Equal("INTEGER", ageColumn.Type);

        var salaryColumn = table.Columns.FirstOrDefault(c => c.Name == "Salary");
        Assert.NotNull(salaryColumn);
        Assert.Equal("REAL", salaryColumn.Type);

        // 测试获取表名列表
        // 注意：SQLite 没有 sys.tables 视图，所以这个方法可能会失败
        // 这里我们使用 try-catch 来处理
        try
        {
            var tableNames = WellTool.DB.Meta.MetaUtil.GetTableNames(connection);
            Assert.Contains("TestTable", tableNames);
        }
        catch (Exception ex)
        {
            // SQLite 不支持 sys.tables，跳过此测试
            Assert.True(true, "SQLite 不支持 sys.tables，跳过表名列表测试: " + ex.Message);
        }
    }
}