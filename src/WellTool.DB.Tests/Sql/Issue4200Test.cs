// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.DB.Sql;
using Xunit;

namespace WellTool.DB.Tests.Sql;

/// <summary>
/// Issue 4200 测试 - IN子句检测
/// </summary>
public class Issue4200Test
{
    /// <summary>
    /// 测试IN子句检测
    /// </summary>
    [Fact]
    public void IsInClauseTest0()
    {
        // 简化的测试
        Assert.False(SqlUtil.IsInClause(""));
    }

    /// <summary>
    /// 测试IN子句检测
    /// </summary>
    [Fact]
    public void IsInClauseTest1()
    {
        Assert.False(SqlUtil.IsInClause(null));
    }

    /// <summary>
    /// 测试非IN子句
    /// </summary>
    [Fact]
    public void IsInClauseTest2()
    {
        Assert.False(SqlUtil.IsInClause("select * from table"));
    }

    /// <summary>
    /// 测试包含IN但非param对应的IN子句
    /// </summary>
    [Fact]
    public void IsInClauseTest3()
    {
        Assert.False(SqlUtil.IsInClause("in (1,2,3)"));
    }

    /// <summary>
    /// 测试包含IN但非param对应的IN子句
    /// </summary>
    [Fact]
    public void IsInClauseTest4()
    {
        Assert.False(SqlUtil.IsInClause("where id in"));
    }
}