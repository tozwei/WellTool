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
    /// ConditionTest
    /// </summary>
    public class ConditionTest
    {
        [Fact]
        public void TestConditionCreation()
        {
            // 测试创建条件
            var condition = Condition.Create("age > @age");
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionWithAnd()
        {
            // 测试 AND 条件
            var condition = Condition.Create("age > @age").And("name LIKE @name");
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionWithOr()
        {
            // 测试 OR 条件
            var condition = Condition.Create("age > @age").Or("age < @minAge");
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionWithNot()
        {
            // 测试 NOT 条件
            var condition = Condition.Create("age > @age").Not();
            Assert.NotNull(condition);
        }
    }
}

