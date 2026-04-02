// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.DB.Sql;
using Xunit;

namespace WellTool.DB.Tests.Sql;

/// <summary>
/// ConditionBuilder 测试
/// </summary>
public class ConditionBuilderTest
{
    /// <summary>
    /// 测试条件构建器
    /// </summary>
    [Fact]
    public void BuildTest()
    {
        var c1 = new Condition("user", null);
        var c2 = new Condition("name", "!= null");
        c2.LinkOperator = LogicalOperator.OR;
        var c3 = new Condition("group", "like %aaa");

        var builder = ConditionBuilder.Of(c1, c2, c3);
        var sql = builder.Build();

        Assert.Equal("user IS NULL OR name IS NOT NULL AND group LIKE ?", sql);
        Assert.Single(builder.GetParamValues());
        Assert.Equal("%aaa", builder.GetParamValues()[0]);
    }

    /// <summary>
    /// 测试空条件
    /// </summary>
    [Fact]
    public void BuildEmptyTest()
    {
        var builder = ConditionBuilder.Of();
        var sql = builder.Build();
        Assert.Equal(string.Empty, sql);
    }

    /// <summary>
    /// 测试单个条件
    /// </summary>
    [Fact]
    public void BuildSingleConditionTest()
    {
        var c1 = new Condition("id", "=", 1);
        var builder = ConditionBuilder.Of(c1);
        var sql = builder.Build();

        Assert.Equal("id = ?", sql);
        Assert.Single(builder.GetParamValues());
    }
}