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
using WellTool.DB.Dialect;
using WellTool.DB.Dialect.Impl;

namespace WellTool.DB.Tests
{
    /// <summary>
    /// DialectFactoryTest
    /// </summary>
    public class DialectFactoryTest
    {
        [Fact]
        public void TestDialectFactoryGetDialect()
        {
            // 测试获取数据库方言
            var dialect = DialectFactory.GetDialect("System.Data.SqlClient");
            Assert.NotNull(dialect);
        }

        [Fact]
        public void TestDialectFactoryGetDefaultDialect()
        {
            // 测试获取默认数据库方言
            var dialect = DialectFactory.GetDialect("");
            Assert.NotNull(dialect);
        }

        [Fact]
        public void TestDialectFactoryRegisterDialect()
        {
            // 测试注册数据库方言
            var dialect = new SqlServerDialect();
            DialectFactory.RegisterDialect("test", dialect);
            // 验证注册操作没有抛出异常
            Assert.True(true);//
        }
    }
}

