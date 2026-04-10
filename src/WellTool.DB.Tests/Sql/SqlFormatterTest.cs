// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using Xunit;
using WellTool.DB.Sql;

namespace WellTool.DB.Tests.Sql;

/// <summary>
/// SqlFormatter 测试
/// </summary>
public class SqlFormatterTest
{
    /// <summary>
    /// 测试简单SQL格式化
    /// </summary>
    [Fact]
    public void FormatTest()
    {
        // 测试简单SQL语句
        var sql = "SELECT * FROM user WHERE id = 1";
        var formatted = SqlFormatter.Format(sql);
        Assert.NotNull(formatted);
        Assert.NotEmpty(formatted);
    }

    /// <summary>
    /// 测试简单SELECT语句
    /// </summary>
    [Fact]
    public void FormatSelectTest()
    {
        // 测试复杂SELECT语句
        var sql = "SELECT id, name, age FROM user WHERE id = 1 AND age > 18 ORDER BY id DESC";
        var formatted = SqlFormatter.Format(sql);
        Assert.NotNull(formatted);
        Assert.NotEmpty(formatted);
    }

    /// <summary>
    /// 测试INSERT语句
    /// </summary>
    [Fact]
    public void FormatInsertTest()
    {
        var sql = "INSERT INTO user (id, name, age) VALUES (1, '张三', 20)";
        var formatted = SqlFormatter.Format(sql);
        Assert.NotNull(formatted);
        Assert.NotEmpty(formatted);
    }

    /// <summary>
    /// 测试UPDATE语句
    /// </summary>
    [Fact]
    public void FormatUpdateTest()
    {
        var sql = "UPDATE user SET name = '李四', age = 21 WHERE id = 1";
        var formatted = SqlFormatter.Format(sql);
        Assert.NotNull(formatted);
        Assert.NotEmpty(formatted);
    }

    /// <summary>
    /// 测试DELETE语句
    /// </summary>
    [Fact]
    public void FormatDeleteTest()
    {
        var sql = "DELETE FROM user WHERE id = 1";
        var formatted = SqlFormatter.Format(sql);
        Assert.NotNull(formatted);
        Assert.NotEmpty(formatted);
    }

    /// <summary>
    /// 测试带有JOIN的SQL语句
    /// </summary>
    [Fact]
    public void FormatJoinTest()
    {
        var sql = "SELECT u.id, u.name, o.order_no FROM user u INNER JOIN order o ON u.id = o.user_id WHERE u.age > 18";
        var formatted = SqlFormatter.Format(sql);
        Assert.NotNull(formatted);
        Assert.NotEmpty(formatted);
    }

    /// <summary>
    /// 测试带有GROUP BY和HAVING的SQL语句
    /// </summary>
    [Fact]
    public void FormatGroupByTest()
    {
        var sql = "SELECT age, COUNT(*) FROM user GROUP BY age HAVING COUNT(*) > 1";
        var formatted = SqlFormatter.Format(sql);
        Assert.NotNull(formatted);
        Assert.NotEmpty(formatted);
    }
}
