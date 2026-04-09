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

namespace WellTool.DB.Tests.Sql;

/// <summary>
/// ConditionGroup 测试
/// </summary>
public class ConditionGroupTest
{
    /// <summary>
    /// 测试条件组
    /// </summary>
    [Fact]
    public void TestConditionGroup()
    {
        // 测试条件组基本功能
        // 创建一个条件组，默认使用 AND 逻辑运算符
        var conditionGroup = new WellTool.DB.Sql.ConditionGroup();
        Assert.NotNull(conditionGroup);
        Assert.Equal(WellTool.DB.Sql.LogicalOperator.AND, conditionGroup.LogicalOperator);
        Assert.NotNull(conditionGroup.Conditions);
        Assert.Empty(conditionGroup.Conditions);

        // 添加等于条件
        conditionGroup.Eq("name", "test");
        Assert.Single(conditionGroup.Conditions);

        // 添加大于条件
        conditionGroup.Gt("age", 18);
        Assert.Equal(2, conditionGroup.Conditions.Count);

        // 创建一个使用 OR 逻辑运算符的条件组
        var orConditionGroup = new WellTool.DB.Sql.ConditionGroup(WellTool.DB.Sql.LogicalOperator.OR);
        Assert.Equal(WellTool.DB.Sql.LogicalOperator.OR, orConditionGroup.LogicalOperator);

        // 添加条件到 OR 条件组
        orConditionGroup.Eq("status", "active").Ne("type", "admin");
        Assert.Equal(2, orConditionGroup.Conditions.Count);

        // 测试嵌套条件组
        var nestedConditionGroup = new WellTool.DB.Sql.ConditionGroup();
        nestedConditionGroup.Eq("id", 1).Group().Eq("name", "test").Ne("age", 20);
        Assert.Equal(2, nestedConditionGroup.Conditions.Count);
    }
}