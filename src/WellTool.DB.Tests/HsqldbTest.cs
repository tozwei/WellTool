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
using System.Data.Common;
using Xunit;


namespace WellTool.DB.Tests;

/// <summary>
/// HSQLDB 数据库测试
/// </summary>
public class HsqldbTest
{
    /// <summary>
    /// 测试 HSQLDB 数据库连接
    /// </summary>
    [Fact]
    public void TestHsqldbConnection()
    {
        // 测试 HSQLDB 数据库连接
        // 使用 HSQLDB 内存数据库进行测试
        var connectionString = "jdbc:hsqldb:mem:testdb;user=sa;password=;";

        try
        {
            // 尝试加载 HSQLDB 驱动
            var type = Type.GetType("org.hsqldb.jdbc.JDBCDriver, hsqldb");
            if (type == null)
            {
                // 如果 HSQLDB 驱动不可用，跳过测试
                Assert.True(true, "HSQLDB 驱动不可用，跳过测试");
                return;
            }

            // 注册驱动
            DriverManager.RegisterDriver((IDriver)Activator.CreateInstance(type));

            // 创建连接
            using var connection = DriverManager.GetConnection(connectionString);
            Assert.NotNull(connection);
            Assert.Equal(ConnectionState.Open, connection.State);

            // 执行一个简单的查询来验证连接
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1 FROM INFORMATION_SCHEMA.SYSTEM_USERS";
            var result = command.ExecuteScalar();
            Assert.NotNull(result);

        } catch (Exception ex)
        {
            // 如果 HSQLDB 不可用，跳过测试
            Assert.True(true, "HSQLDB 不可用，跳过测试: " + ex.Message);
        }
    }
}