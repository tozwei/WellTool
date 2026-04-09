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
using WellTool.DB.NoSql.Mongo;

namespace WellTool.DB.Tests
{
    /// <summary>
    /// MongoDBTest
    /// </summary>
    public class MongoDBTest
    {
        [Fact]
        public void TestMongoDBCreation()
        {
            // 测试创建 MongoDB 数据源
            var connectionString = "mongodb://localhost:27017";
            var databaseName = "testdb";
            var mongoDS = new MongoDS(connectionString, databaseName);
            Assert.NotNull(mongoDS);
        }

        [Fact]
        public void TestMongoDBTestConnection()
        {
            // 测试 MongoDB 连接
            var connectionString = "mongodb://localhost:27017";
            var databaseName = "testdb";
            var mongoDS = new MongoDS(connectionString, databaseName);
            var result = mongoDS.TestConnection();
            Assert.True(result);
        }
    }
}

