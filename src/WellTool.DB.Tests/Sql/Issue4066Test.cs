// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.DB.Sql;
using Xunit;

namespace WellTool.DB.Tests.Sql;

/// <summary>
/// Issue 4066 测试 - 移除外层ORDER BY
/// </summary>
public class Issue4066Test
{
    /// <summary>
    /// 测试基本的ORDER BY移除
    /// </summary>
    [Fact]
    public void RemoveOuterOrderByTest1()
    {
        var sql = "SELECT * FROM users ORDER BY name";
        var result = SqlUtil.RemoveOuterOrderBy(sql);

        Assert.Equal("SELECT * FROM users", result);
    }

    /// <summary>
    /// 测试多字段ORDER BY移除
    /// </summary>
    [Fact]
    public void RemoveOuterOrderByTest2()
    {
        var sql = "SELECT id, name, age FROM users WHERE status = 'active' ORDER BY name ASC, age DESC, created_date";
        var result = SqlUtil.RemoveOuterOrderBy(sql);

        Assert.Equal("SELECT id, name, age FROM users WHERE status = 'active'", result);
    }

    /// <summary>
    /// 测试不含ORDER BY的语句
    /// </summary>
    [Fact]
    public void RemoveOuterOrderByTest3()
    {
        var sql = "SELECT * FROM users";
        var result = SqlUtil.RemoveOuterOrderBy(sql);

        Assert.Equal("SELECT * FROM users", result);
    }

    /// <summary>
    /// 测试ORDER BY DESC
    /// </summary>
    [Fact]
    public void RemoveOuterOrderByTest4()
    {
        var sql = "SELECT * FROM users ORDER BY id DESC";
        var result = SqlUtil.RemoveOuterOrderBy(sql);

        Assert.Equal("SELECT * FROM users", result);
    }
}