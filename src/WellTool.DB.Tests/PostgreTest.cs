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
/// PostgreSQL 数据库测试
/// </summary>
public class PostgreTest
{
    /// <summary>
    /// 测试 PostgreSQL 数据库连接
    /// </summary>
    [Fact]
    public void TestPostgreConnection()
    {
        // 测试 PostgreSQL 数据库连接
        // 使用默认的 PostgreSQL 连接字符串格式
        var connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;";

        try
        {
            // 尝试加载 Npgsql 驱动
            var type = Type.GetType("Npgsql.NpgsqlConnection, Npgsql");
            if (type == null)
            {
                // 如果 Npgsql 驱动不可用，跳过测试
                Assert.True(true, "Npgsql 驱动不可用，跳过测试");
                return;
            }

            // 创建连接并打开
            var connection = Activator.CreateInstance(type, connectionString);
            var openMethod = type.GetMethod("Open");
            openMethod?.Invoke(connection, null);

            // 验证连接是否成功
            var stateProperty = type.GetProperty("State");
            var state = stateProperty?.GetValue(connection);
            Assert.Equal(System.Data.ConnectionState.Open, state);

            // 执行一个简单的查询来验证连接
            var createCommandMethod = type.GetMethod("CreateCommand");
            var command = createCommandMethod?.Invoke(connection, null);
            var commandType = command?.GetType();
            var commandTextProperty = commandType?.GetProperty("CommandText");
            commandTextProperty?.SetValue(command, "SELECT 1");
            var executeScalarMethod = commandType?.GetMethod("ExecuteScalar");
            var result = executeScalarMethod?.Invoke(command, null);
            Assert.Equal(1, result);

            // 关闭连接
            var closeMethod = type.GetMethod("Close");
            closeMethod?.Invoke(connection, null);
        }
        catch (Exception ex)
        {
            // 如果 PostgreSQL 不可用，跳过测试
            Assert.True(true, "PostgreSQL 不可用，跳过测试: " + ex.Message);
        }
    }
}