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
    /// Issue4200Test
    /// </summary>
    public class Issue4200Test
    {
        [Fact]
        public void TestSqlBuilderWithMultipleWhereConditions()
        {
            // 测试 SQL Builder 处理多个 WHERE 条件
            var builder = SqlBuilder.Create();
            builder.Append("SELECT id, name, age FROM users WHERE age > @age AND name LIKE @name");
            var sql = builder.ToString();

            Assert.NotNull(sql);
            Assert.NotEmpty(sql);
            Assert.Contains("SELECT", sql);
            Assert.Contains("FROM", sql);
            Assert.Contains("WHERE", sql);
            Assert.Contains("age > @age", sql);
            Assert.Contains("name LIKE @name", sql);
        }

        [Fact]
        public void TestSqlBuilderWithMultipleOrderBy()
        {
            // 测试 SQL Builder 处理多个 ORDER BY 条件
            var builder = SqlBuilder.Create();
            builder.Append("SELECT id, name, age FROM users ORDER BY age DESC, name ASC");
            var sql = builder.ToString();

            Assert.NotNull(sql);
            Assert.NotEmpty(sql);
            Assert.Contains("SELECT", sql);
            Assert.Contains("FROM", sql);
            Assert.Contains("ORDER BY", sql);
            Assert.Contains("age DESC", sql);
            Assert.Contains("name ASC", sql);
        }
    }
}

