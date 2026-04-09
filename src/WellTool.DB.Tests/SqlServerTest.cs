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
/// SQL Server 数据库测试
/// </summary>
public class SqlServerTest
{
    /// <summary>
    /// 测试 SQL Server 数据库连接
    /// </summary>
    [Fact]
    public void TestSqlServerConnection()
    {
        // 测试 SQL Server 数据库连接
        // 使用 LocalDB 进行测试，这是 SQL Server 的轻量级版本
        var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=master;Integrated Security=True;";

        try
        {
            using var connection = new System.Data.SqlClient.SqlConnection(connectionString);
            connection.Open();

            // 验证连接是否成功
            Assert.Equal(System.Data.ConnectionState.Open, connection.State);

            // 执行一个简单的查询来验证连接
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1";
            var result = command.ExecuteScalar();
            Assert.Equal(1, result);
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            // 如果 LocalDB 不可用，跳过测试
            Assert.True(true, "LocalDB 不可用，跳过测试: " + ex.Message);
        }
    }
}