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
    /// Issue4066Test
    /// </summary>
    public class Issue4066Test
    {
        [Fact]
        public void TestSqlBuilderWithJoin()
        {
            // 测试 SQL Builder 处理 JOIN 语句
            var sql = SqlBuilder
                .Select("u.id", "u.name", "o.order_date")
                .From("users u")
                .Join("orders o", "u.id = o.user_id")
                .Where("u.age > @age")
                .Build();

            Assert.NotNull(sql);
            Assert.NotEmpty(sql);
            Assert.Contains("SELECT", sql);
            Assert.Contains("FROM", sql);
            Assert.Contains("JOIN", sql);
            Assert.Contains("WHERE", sql);
        }

        [Fact]
        public void TestSqlBuilderWithLeftJoin()
        {
            // 测试 SQL Builder 处理 LEFT JOIN 语句
            var sql = SqlBuilder
                .Select("u.id", "u.name", "o.order_date")
                .From("users u")
                .LeftJoin("orders o", "u.id = o.user_id")
                .Where("u.age > @age")
                .Build();

            Assert.NotNull(sql);
            Assert.NotEmpty(sql);
            Assert.Contains("SELECT", sql);
            Assert.Contains("FROM", sql);
            Assert.Contains("LEFT JOIN", sql);
            Assert.Contains("WHERE", sql);
        }

        [Fact]
        public void TestSqlBuilderWithRightJoin()
        {
            // 测试 SQL Builder 处理 RIGHT JOIN 语句
            var sql = SqlBuilder
                .Select("u.id", "u.name", "o.order_date")
                .From("users u")
                .RightJoin("orders o", "u.id = o.user_id")
                .Where("o.order_date > @date")
                .Build();

            Assert.NotNull(sql);
            Assert.NotEmpty(sql);
            Assert.Contains("SELECT", sql);
            Assert.Contains("FROM", sql);
            Assert.Contains("RIGHT JOIN", sql);
            Assert.Contains("WHERE", sql);
        }
    }
}

