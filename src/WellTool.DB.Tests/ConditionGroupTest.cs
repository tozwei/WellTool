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

namespace WellTool.DB.Tests
{
    /// <summary>
    /// ConditionGroupTest
    /// </summary>
    public class ConditionGroupTest
    {
        [Fact]
        public void TestConditionGroupCreation()
        {
            // 测试创建条件组
            var group = new ConditionGroup();
            Assert.NotNull(group);
        }

        [Fact]
        public void TestConditionGroupWithConditions()
        {
            // 测试添加条件到条件组
            var group = new ConditionGroup();
            group.Add(Condition.Create("age > @age"));
            group.Add(Condition.Create("name LIKE @name"));
            Assert.NotNull(group);
        }

        [Fact]
        public void TestConditionGroupWithAnd()
        {
            // 测试 AND 条件组
            var group = new ConditionGroup();
            group.Add(Condition.Create("age > @age"));
            group.Add(Condition.Create("name LIKE @name"));
            var condition = group.And();
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionGroupWithOr()
        {
            // 测试 OR 条件组
            var group = new ConditionGroup();
            group.Add(Condition.Create("age > @age"));
            group.Add(Condition.Create("age < @minAge"));
            var condition = group.Or();
            Assert.NotNull(condition);
        }
    }
}

