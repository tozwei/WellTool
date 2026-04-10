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
using WellTool.DB.Handler;
using System.Data;
using System.Collections.ObjectModel;

namespace WellTool.DB.Tests;

/// <summary>
/// 用户类
/// </summary>
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}

/// <summary>
/// Issue I73770 测试
/// </summary>
public class IssueI73770Test
{
    /// <summary>
    /// 测试 Issue I73770 - 分页查询功能
    /// </summary>
    [Fact]
    public void TestIssueI73770()
    {
        // 测试 Issue I73770 - 分页查询功能
        var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
        var db = new TestDb(dataSource);

        // 测试使用Entity和Page对象进行分页查询
        var where = new Entity("user");
        where.Set("id", 9);
        
        var page = new WellTool.DB.Sql.Page(1, 10);
        var fields = new Collection<string> { "id", "name" };
        
        // 测试分页查询方法
        var result = db.Page(fields, where, page);
        
        // 验证方法调用没有异常
        Assert.NotNull(result);
        
        // 由于使用的是模拟连接，结果集为空，但测试可以验证方法调用是否正常
    }

    /// <summary>
    /// 测试分页查询的参数绑定
    /// </summary>
    [Fact]
    public void TestPageQueryWithParameters()
    {
        // 测试带参数的分页查询
        var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
        var db = new TestDb(dataSource);

        // 测试使用Entity和Page对象进行分页查询
        var where = new Entity("user");
        where.Set("age", "> 18");
        where.Set("name", "LIKE %test%");
        
        var page = new WellTool.DB.Sql.Page(2, 20);
        var fields = new Collection<string> { "id", "name", "age" };
        
        // 测试分页查询方法
        var result = db.Page(fields, where, page);
        
        // 验证方法调用没有异常
        Assert.NotNull(result);
    }

    /// <summary>
    /// 测试不同分页大小的查询
    /// </summary>
    [Fact]
    public void TestPageQueryWithDifferentSizes()
    {
        // 测试不同分页大小的查询
        var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
        var db = new TestDb(dataSource);

        // 测试不同的分页大小
        var where = new Entity("user");
        var fields = new Collection<string> { "*" };
        
        // 测试每页10条
        var page1 = new WellTool.DB.Sql.Page(1, 10);
        var result1 = db.Page(fields, where, page1);
        Assert.NotNull(result1);
        
        // 测试每页20条
        var page2 = new WellTool.DB.Sql.Page(1, 20);
        var result2 = db.Page(fields, where, page2);
        Assert.NotNull(result2);
        
        // 测试第2页
        var page3 = new WellTool.DB.Sql.Page(2, 10);
        var result3 = db.Page(fields, where, page3);
        Assert.NotNull(result3);
    }
}
