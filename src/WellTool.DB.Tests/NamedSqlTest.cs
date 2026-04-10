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
using WellTool.DB.Sql;
using System.Collections.Generic;

namespace WellTool.DB.Tests;

/// <summary>
/// 命名 SQL 测试
/// </summary>
public class NamedSqlTest
{
    /// <summary>
    /// 测试命名 SQL 基本功能
    /// </summary>
    [Fact]
    public void TestNamedSql()
    {
        // 测试基本的命名参数替换
        var sql = "SELECT * FROM user WHERE id = :id AND name = :name";
        var paramMap = new Dictionary<string, object>
        {
            { "id", 1 },
            { "name", "test" }
        };

        var namedSql = new NamedSql(sql, paramMap);
        var resultSql = namedSql.GetSql();
        var paramsArray = namedSql.GetParams();

        Assert.Equal("SELECT * FROM user WHERE id = ? AND name = ?", resultSql);
        Assert.Equal(2, paramsArray.Length);
        Assert.Equal(1, paramsArray[0]);
        Assert.Equal("test", paramsArray[1]);
    }

    /// <summary>
    /// 测试不同类型的占位符
    /// </summary>
    [Fact]
    public void TestDifferentPlaceholders()
    {
        // 测试不同类型的占位符格式
        var sql = "SELECT * FROM user WHERE id = :id AND name = @name AND age = ?age";
        var paramMap = new Dictionary<string, object>
        {
            { "id", 1 },
            { "name", "test" },
            { "age", 20 }
        };

        var namedSql = new NamedSql(sql, paramMap);
        var resultSql = namedSql.GetSql();
        var paramsArray = namedSql.GetParams();

        Assert.Equal("SELECT * FROM user WHERE id = ? AND name = ? AND age = ?", resultSql);
        Assert.Equal(3, paramsArray.Length);
        Assert.Equal(1, paramsArray[0]);
        Assert.Equal("test", paramsArray[1]);
        Assert.Equal(20, paramsArray[2]);
    }

    /// <summary>
    /// 测试数组参数（IN子句）
    /// </summary>
    [Fact]
    public void TestArrayParams()
    {
        // 测试数组参数的处理
        var sql = "SELECT * FROM user WHERE id IN (:ids)";
        var paramMap = new Dictionary<string, object>
        {
            { "ids", new int[] { 1, 2, 3 } }
        };

        var namedSql = new NamedSql(sql, paramMap);
        var resultSql = namedSql.GetSql();
        var paramsArray = namedSql.GetParams();

        Assert.Equal("SELECT * FROM user WHERE id IN (?,?,?)", resultSql);
        Assert.Equal(3, paramsArray.Length);
        Assert.Equal(1, paramsArray[0]);
        Assert.Equal(2, paramsArray[1]);
        Assert.Equal(3, paramsArray[2]);
    }

    /// <summary>
    /// 测试不存在的参数
    /// </summary>
    [Fact]
    public void TestNonExistentParams()
    {
        // 测试不存在的参数处理
        var sql = "SELECT * FROM user WHERE id = :id AND name = :name";
        var paramMap = new Dictionary<string, object>
        {
            { "id", 1 }
        };

        var namedSql = new NamedSql(sql, paramMap);
        var resultSql = namedSql.GetSql();
        var paramsArray = namedSql.GetParams();

        Assert.Equal("SELECT * FROM user WHERE id = ? AND name = :name", resultSql);
        Assert.Equal(1, paramsArray.Length);
        Assert.Equal(1, paramsArray[0]);
    }

    /// <summary>
    /// 测试空参数映射
    /// </summary>
    [Fact]
    public void TestEmptyParamMap()
    {
        // 测试空参数映射
        var sql = "SELECT * FROM user WHERE id = :id";
        var paramMap = new Dictionary<string, object>();

        var namedSql = new NamedSql(sql, paramMap);
        var resultSql = namedSql.GetSql();
        var paramsArray = namedSql.GetParams();

        Assert.Equal(sql, resultSql);
        Assert.Equal(0, paramsArray.Length);
    }
}