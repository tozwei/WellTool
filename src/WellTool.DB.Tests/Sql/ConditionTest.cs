// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.DB.Sql;
using Xunit;

namespace WellTool.DB.Tests.Sql;

/// <summary>
/// Condition 测试
/// </summary>
public class ConditionTest
{
    /// <summary>
    /// 测试条件ToString
    /// </summary>
    [Fact]
    public void ToStringTest()
    {
        // 测试 IS NULL
        var conditionNull = new Condition("user", null);
        Assert.Equal("user IS NULL", conditionNull.ToString());

        // 测试 IS NOT NULL
        var conditionNotNull = new Condition("user", "!= null");
        Assert.Equal("user IS NOT NULL", conditionNotNull.ToString());

        // 测试等于
        var condition2 = new Condition("user", "= zhangsan");
        Assert.Equal("user = ?", condition2.ToString());

        // 测试 LIKE
        var conditionLike = new Condition("user", "like %aaa");
        Assert.Equal("user LIKE ?", conditionLike.ToString());

        // 测试 IN
        var conditionIn = new Condition("user", "in 1,2,3");
        Assert.Equal("user IN (?,?,?)", conditionIn.ToString());

        // 测试 BETWEEN
        var conditionBetween = new Condition("user", "between 12 and 13");
        Assert.Equal("user BETWEEN ? AND ?", conditionBetween.ToString());
    }

    /// <summary>
    /// 测试不占位符模式
    /// </summary>
    [Fact]
    public void ToStringNoPlaceHolderTest()
    {
        // 测试 IS NULL
        var conditionNull = new Condition("user", null);
        conditionNull.IsPlaceHolder = false;
        Assert.Equal("user IS NULL", conditionNull.ToString());

        // 测试 IS NOT NULL
        var conditionNotNull = new Condition("user", "!= null");
        conditionNotNull.IsPlaceHolder = false;
        Assert.Equal("user IS NOT NULL", conditionNotNull.ToString());

        // 测试等于
        var conditionEquals = new Condition("user", "= zhangsan");
        conditionEquals.IsPlaceHolder = false;
        Assert.Equal("user = zhangsan", conditionEquals.ToString());
    }

    /// <summary>
    /// 测试静态工厂方法
    /// </summary>
    [Fact]
    public void StaticFactoryTest()
    {
        // 测试 Eq
        var eq = Condition.Eq("id", 1);
        Assert.Equal("id", eq.Field);
        Assert.Equal("=", eq.Operator);

        // 测试 Ne
        var ne = Condition.Ne("id", 1);
        Assert.Equal("!=", ne.Operator);

        // 测试 Like
        var like = Condition.Like("name", "%test%");
        Assert.Equal("LIKE", like.Operator);

        // 测试 In
        var @in = Condition.In("id", "1,2,3");
        Assert.Equal("IN", @in.Operator);

        // 测试 IsNull
        var isNull = Condition.IsNull("name");
        Assert.Equal("IS", isNull.Operator);

        // 测试 IsNotNull
        var isNotNull = Condition.IsNotNull("name");
        Assert.Equal("IS NOT", isNotNull.Operator);
    }
}