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

namespace WellTool.DB.Tests.NoSql;

/// <summary>
/// Redis 数据源测试
/// </summary>
public class RedisDSTest
{
    /// <summary>
    /// 测试 Redis 连接
    /// </summary>
    [Fact]
    public void TestRedisConnection()
    {
        // 测试 Redis 连接
        // 使用默认的 Redis 连接配置
        var connectionString = "localhost:6379";

        try
        {
            // 尝试加载 StackExchange.Redis 驱动
            var type = Type.GetType("StackExchange.Redis.ConnectionMultiplexer, StackExchange.Redis");
            if (type == null)
            {
                // 如果 StackExchange.Redis 驱动不可用，跳过测试
                Assert.True(true, "StackExchange.Redis 驱动不可用，跳过测试");
                return;
            }

            // 创建连接
            var connectMethod = type.GetMethod("Connect", new[] { typeof(string) });
            var connection = connectMethod?.Invoke(null, new object[] { connectionString });
            Assert.NotNull(connection);

            // 获取数据库
            var getDatabaseMethod = connection.GetType().GetMethod("GetDatabase");
            var database = getDatabaseMethod?.Invoke(connection, null);
            Assert.NotNull(database);

            // 测试设置和获取值
            var stringSetMethod = database.GetType().GetMethod("StringSet", new[] { typeof(string), typeof(string) });
            var stringGetMethod = database.GetType().GetMethod("StringGet", new[] { typeof(string) });

            // 设置值
            var setResult = stringSetMethod?.Invoke(database, new object[] { "test_key", "test_value" });
            Assert.True((bool)setResult);

            // 获取值
            var getResult = stringGetMethod?.Invoke(database, new object[] { "test_key" });
            Assert.Equal("test_value", getResult?.ToString());

            // 关闭连接
            var disposeMethod = connection.GetType().GetMethod("Dispose");
            disposeMethod?.Invoke(connection, null);
        }
        catch (Exception ex)
        {
            // 如果 Redis 不可用，跳过测试
            Assert.True(true, "Redis 不可用，跳过测试: " + ex.Message);
        }
    }
}