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
    /// SqlFormatterTest
    /// </summary>
    public class SqlFormatterTest
    {
        [Fact]
        public void TestFormatSelectSql()
        {
            // 测试格式化 SELECT 语句
            var sql = "SELECT id, name, age FROM users WHERE age > 18 ORDER BY id DESC";
            var formattedSql = SqlFormatter.Format(sql);
            Assert.NotNull(formattedSql);
            Assert.NotEmpty(formattedSql);
        }

        [Fact]
        public void TestFormatInsertSql()
        {
            // 测试格式化 INSERT 语句
            var sql = "INSERT INTO users (id, name, age) VALUES (1, 'John', 25)";
            var formattedSql = SqlFormatter.Format(sql);
            Assert.NotNull(formattedSql);
            Assert.NotEmpty(formattedSql);
        }

        [Fact]
        public void TestFormatUpdateSql()
        {
            // 测试格式化 UPDATE 语句
            var sql = "UPDATE users SET name = 'Jane', age = 30 WHERE id = 1";
            var formattedSql = SqlFormatter.Format(sql);
            Assert.NotNull(formattedSql);
            Assert.NotEmpty(formattedSql);
        }

        [Fact]
        public void TestFormatDeleteSql()
        {
            // 测试格式化 DELETE 语句
            var sql = "DELETE FROM users WHERE id = 1";
            var formattedSql = SqlFormatter.Format(sql);
            Assert.NotNull(formattedSql);
            Assert.NotEmpty(formattedSql);
        }
    }
}

