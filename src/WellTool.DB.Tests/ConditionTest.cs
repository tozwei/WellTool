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
            var condition = Condition.Parse("age", "> @age");
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionWithEq()
        {
            // 测试等于条件
            var condition = Condition.Eq("age", 18);
            Assert.NotNull(condition);
            Assert.Equal("age", condition.Field);
            Assert.Equal("=", condition.Operator);
            Assert.Equal(18, condition.Value);
        }

        [Fact]
        public void TestConditionWithGt()
        {
            // 测试大于条件
            var condition = Condition.Gt("age", 18);
            Assert.NotNull(condition);
            Assert.Equal("age", condition.Field);
            Assert.Equal(">", condition.Operator);
            Assert.Equal(18, condition.Value);
        }

        [Fact]
        public void TestConditionWithLike()
        {
            // 测试 LIKE 条件
            var condition = Condition.Like("name", "%John%");
            Assert.NotNull(condition);
            Assert.Equal("name", condition.Field);
            Assert.Equal("LIKE", condition.Operator);
            Assert.Equal("%John%", condition.Value);
        }
    }
}

