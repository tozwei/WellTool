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
    /// SqlBuilderTest
    /// </summary>
    public class SqlBuilderTest
    {
        [Fact]
        public void TestSelectBuilder()
        {
            // 测试 SELECT 语句构建
            var builder = SqlBuilder.Create();
            builder.Append("SELECT id, name, age FROM users WHERE age > @age ORDER BY id DESC");
            var sql = builder.ToString();

            Assert.NotNull(sql);
            Assert.NotEmpty(sql);
            Assert.Contains("SELECT", sql);
            Assert.Contains("FROM", sql);
            Assert.Contains("WHERE", sql);
            Assert.Contains("ORDER BY", sql);
        }

        [Fact]
        public void TestInsertBuilder()
        {
            // 测试 INSERT 语句构建
            var builder = SqlBuilder.Create();
            builder.Append("INSERT INTO users (id, name, age) VALUES (@id, @name, @age)");
            var sql = builder.ToString();

            Assert.NotNull(sql);
            Assert.NotEmpty(sql);
            Assert.Contains("INSERT INTO", sql);
            Assert.Contains("VALUES", sql);
        }

        [Fact]
        public void TestUpdateBuilder()
        {
            // 测试 UPDATE 语句构建
            var builder = SqlBuilder.Create();
            builder.Append("UPDATE users SET name = @name, age = @age WHERE id = @id");
            var sql = builder.ToString();

            Assert.NotNull(sql);
            Assert.NotEmpty(sql);
            Assert.Contains("UPDATE", sql);
            Assert.Contains("SET", sql);
            Assert.Contains("WHERE", sql);
        }

        [Fact]
        public void TestDeleteBuilder()
        {
            // 测试 DELETE 语句构建
            var builder = SqlBuilder.Create();
            builder.Append("DELETE FROM users WHERE id = @id");
            var sql = builder.ToString();

            Assert.NotNull(sql);
            Assert.NotEmpty(sql);
            Assert.Contains("DELETE", sql);
            Assert.Contains("FROM", sql);
            Assert.Contains("WHERE", sql);
        }
    }
}

