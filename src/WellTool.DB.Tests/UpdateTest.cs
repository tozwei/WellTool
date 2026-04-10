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
using WellTool.DB;

namespace WellTool.DB.Tests;

/// <summary>
/// 更新测试
/// </summary>
public class UpdateTest
{
    /// <summary>
    /// 测试更新操作
    /// </summary>
    [Fact]
    public void TestUpdate()
    {
        // 测试数据库更新操作
        var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
        var db = new TestDb(dataSource);

        // 创建测试实体
        var entity = new Entity("user");
        entity.Set("id", 1);
        entity.Set("name", "updated_name");
        entity.Set("age", 30);

        // 创建条件实体
        var where = new Entity("user");
        where.Set("id", 1);

        // 测试更新方法
        var result = db.Update(entity, where);

        // 由于使用的是模拟连接，这里会返回0，但测试可以验证方法调用是否正常
        Assert.Equal(0, result);
    }

    /// <summary>
    /// 测试批量更新操作
    /// </summary>
    [Fact]
    public void TestBatchUpdate()
    {
        // 测试批量更新操作
        var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
        var db = new TestDb(dataSource);

        // 创建多个测试实体
        var entity1 = new Entity("user");
        entity1.Set("id", 1);
        entity1.Set("status", "active");

        var entity2 = new Entity("user");
        entity2.Set("id", 2);
        entity2.Set("status", "active");

        // 测试批量更新（这里只是验证方法调用，实际执行需要真实数据库）
        // 使用Execute方法执行批量更新
        var sql = "UPDATE user SET status = 'active' WHERE id IN (1, 2)";
        var result = db.Execute(sql);

        // 由于使用的是模拟连接，这里会返回0
        Assert.Equal(0, result);
    }

    /// <summary>
    /// 测试更新操作的参数绑定
    /// </summary>
    [Fact]
    public void TestUpdateWithParameters()
    {
        // 测试带参数的更新操作
        var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
        var db = new TestDb(dataSource);

        // 使用参数化查询
        var sql = "UPDATE user SET name = ?, age = ? WHERE id = ?";
        var result = db.Execute(sql, "test", 25, 1);

        // 由于使用的是模拟连接，这里会返回0
        Assert.Equal(0, result);
    }
}
