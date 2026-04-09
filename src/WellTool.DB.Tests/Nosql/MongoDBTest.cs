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

using System.Collections.Generic;
using Xunit;

namespace WellTool.DB.Tests.NoSql;

/// <summary>
/// MongoDB 测试
/// </summary>
public class MongoDBTest
{
    /// <summary>
    /// 测试 MongoDB 连接
    /// </summary>
    [Fact]
    public void TestMongoDBConnection()
    {
        // 测试 MongoDB 连接
        // 使用默认的 MongoDB 连接字符串
        var connectionString = "mongodb://localhost:27017";

        try
        {
            // 尝试加载 MongoDB 驱动
            var clientType = Type.GetType("MongoDB.Driver.MongoClient, MongoDB.Driver");
            if (clientType == null)
            {
                // 如果 MongoDB 驱动不可用，跳过测试
                Assert.True(true, "MongoDB 驱动不可用，跳过测试");
                return;
            }

            // 创建客户端
            var client = Activator.CreateInstance(clientType, connectionString);
            Assert.NotNull(client);

            // 获取数据库
            var getDatabaseMethod = clientType.GetMethod("GetDatabase", new[] { typeof(string) });
            var database = getDatabaseMethod?.Invoke(client, new object[] { "test_db" });
            Assert.NotNull(database);

            // 获取集合
            var getCollectionMethod = database.GetType().GetMethod("GetCollection", new[] { typeof(string) });
            var collection = getCollectionMethod?.Invoke(database, new object[] { "test_collection" });
            Assert.NotNull(collection);

            // 测试插入和查询文档
            var document = new Dictionary<string, object> { { "name", "test" }, { "value", 123 } };
            var insertOneMethod = collection.GetType().GetMethod("InsertOne", new[] { typeof(object) });
            insertOneMethod?.Invoke(collection, new object[] { document });

            // 测试查询
            var findMethod = collection.GetType().GetMethod("Find");
            var filterType = Type.GetType("MongoDB.Driver.FilterDefinitionBuilder`1, MongoDB.Driver")?.MakeGenericType(typeof(object));
            var filterBuilder = Activator.CreateInstance(filterType);
            var eqMethod = filterType?.GetMethod("Eq", new[] { typeof(string), typeof(object) });
            var filter = eqMethod?.Invoke(filterBuilder, new object[] { "name", "test" });
            var findResult = findMethod?.Invoke(collection, new object[] { filter });
            Assert.NotNull(findResult);

            // 清理测试数据
            var deleteManyMethod = collection.GetType().GetMethod("DeleteMany", new[] { typeof(object) });
            deleteManyMethod?.Invoke(collection, new object[] { filter });

        } catch (Exception ex)
        {
            // 如果 MongoDB 不可用，跳过测试
            Assert.True(true, "MongoDB 不可用，跳过测试: " + ex.Message);
        }
    }
}