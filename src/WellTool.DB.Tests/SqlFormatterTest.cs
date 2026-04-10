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
        public void FormatTest()
        {
            // issue#I3XS44@Gitee
            // 测试简单SQL语句格式化
            var sql = "SELECT * FROM user WHERE id = 1";
            var formatted = SqlFormatter.Format(sql);
            
            // 验证格式化结果
            Assert.NotNull(formatted);
            Assert.NotEmpty(formatted);
            Assert.Contains("SELECT", formatted);
            Assert.Contains("FROM", formatted);
            Assert.Contains("WHERE", formatted);
        }

        [Fact]
        public void TestFormatSelectSql()
        {
            // 测试格式化 SELECT 语句
            var sql = "SELECT id, name, age FROM user WHERE age > 18 ORDER BY name";
            var formatted = SqlFormatter.Format(sql);
            
            // 验证格式化后的SQL包含换行和缩进
            Assert.Contains("\n", formatted);
            Assert.Contains("SELECT", formatted);
            Assert.Contains("FROM", formatted);
            Assert.Contains("WHERE", formatted);
            Assert.Contains("ORDER", formatted);
            Assert.Contains("BY", formatted);
        }

        [Fact]
        public void TestFormatInsertSql()
        {
            // 测试格式化 INSERT 语句
            var sql = "INSERT INTO user (id, name, age) VALUES (1, 'test', 25)";
            var formatted = SqlFormatter.Format(sql);
            
            // 验证格式化后的SQL包含INSERT和VALUES
            Assert.Contains("INSERT", formatted);
            Assert.Contains("VALUES", formatted);
        }

        [Fact]
        public void TestFormatUpdateSql()
        {
            // 测试格式化 UPDATE 语句
            var sql = "UPDATE user SET name = 'test', age = 25 WHERE id = 1";
            var formatted = SqlFormatter.Format(sql);
            
            // 验证格式化后的SQL包含UPDATE、SET和WHERE
            Assert.Contains("UPDATE", formatted);
            Assert.Contains("SET", formatted);
            Assert.Contains("WHERE", formatted);
        }

        [Fact]
        public void TestFormatDeleteSql()
        {
            // 测试格式化 DELETE 语句
            var sql = "DELETE FROM user WHERE id = 1";
            var formatted = SqlFormatter.Format(sql);
            
            // 验证格式化后的SQL包含DELETE和WHERE
            Assert.Contains("DELETE", formatted);
            Assert.Contains("WHERE", formatted);
        }

        [Fact]
        public void TestFormatJoinSql()
        {
            // 测试格式化 JOIN 语句
            var sql = "SELECT u.id, u.name, o.order_no FROM user u LEFT JOIN orders o ON u.id = o.user_id WHERE u.age > 18";
            var formatted = SqlFormatter.Format(sql);
            
            // 验证格式化后的SQL包含JOIN相关关键字
            Assert.Contains("SELECT", formatted);
            Assert.Contains("LEFT", formatted);
            Assert.Contains("JOIN", formatted);
            Assert.Contains("ON", formatted);
            Assert.Contains("WHERE", formatted);
        }

        [Fact]
        public void TestFormatComplexSelectSql()
        {
            // 测试格式化复杂 SELECT 语句
            var sql = "SELECT id, name, COUNT(*) as cnt FROM user WHERE age > 18 GROUP BY id, name HAVING COUNT(*) > 1 ORDER BY cnt DESC";
            var formatted = SqlFormatter.Format(sql);
            
            // 验证格式化后的SQL包含复杂查询的关键字
            Assert.Contains("SELECT", formatted);
            Assert.Contains("COUNT", formatted);
            Assert.Contains("GROUP", formatted);
            Assert.Contains("BY", formatted);
            Assert.Contains("HAVING", formatted);
            Assert.Contains("ORDER", formatted);
        }
    }
}
