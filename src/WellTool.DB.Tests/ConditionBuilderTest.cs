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
    /// ConditionBuilderTest
    /// </summary>
    public class ConditionBuilderTest
    {
        [Fact]
        public void TestConditionBuilderCreation()
        {
            // 测试创建条件构建器
            var builder = new ConditionBuilder();
            Assert.NotNull(builder);
        }

        [Fact]
        public void TestConditionBuilderWithEqual()
        {
            // 测试等于条件
            var builder = new ConditionBuilder();
            var condition = builder.Equal("id", 1);
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionBuilderWithLike()
        {
            // 测试 LIKE 条件
            var builder = new ConditionBuilder();
            var condition = builder.Like("name", "%John%");
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionBuilderWithGreaterThan()
        {
            // 测试大于条件
            var builder = new ConditionBuilder();
            var condition = builder.GreaterThan("age", 18);
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionBuilderWithLessThan()
        {
            // 测试小于条件
            var builder = new ConditionBuilder();
            var condition = builder.LessThan("age", 65);
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionBuilderWithBetween()
        {
            // 测试 BETWEEN 条件
            var builder = new ConditionBuilder();
            var condition = builder.Between("age", 18, 65);
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionBuilderWithIn()
        {
            // 测试 IN 条件
            var builder = new ConditionBuilder();
            var condition = builder.In("id", 1, 2, 3);
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionBuilderWithIsNull()
        {
            // 测试 IS NULL 条件
            var builder = new ConditionBuilder();
            var condition = builder.IsNull("name");
            Assert.NotNull(condition);
        }

        [Fact]
        public void TestConditionBuilderWithIsNotNull()
        {
            // 测试 IS NOT NULL 条件
            var builder = new ConditionBuilder();
            var condition = builder.IsNotNull("name");
            Assert.NotNull(condition);
        }
    }
}

