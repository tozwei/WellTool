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
using WellTool.DB.Nosql;

namespace WellTool.DB.Tests
{
    /// <summary>
    /// RedisDSTest
    /// </summary>
    public class RedisDSTest
    {
        [Fact]
        public void TestRedisDSCreation()
        {
            // 测试创建 Redis 数据源
            var redisDS = RedisDS.Create("localhost", 6379);
            Assert.NotNull(redisDS);
        }

        [Fact]
        public void TestRedisDSWithPassword()
        {
            // 测试使用密码创建 Redis 数据源
            var redisDS = RedisDS.Create("localhost", 6379, "password");
            Assert.NotNull(redisDS);
        }

        [Fact]
        public void TestRedisDSWithDatabase()
        {
            // 测试指定数据库创建 Redis 数据源
            var redisDS = RedisDS.Create("localhost", 6379, "password", 0);
            Assert.NotNull(redisDS);
        }

        [Fact]
        public void TestRedisDSWithConnectionString()
        {
            // 测试使用连接字符串创建 Redis 数据源
            var connectionString = "redis://localhost:6379";
            var redisDS = RedisDS.Create(connectionString);
            Assert.NotNull(redisDS);
        }
    }
}

