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
using WellTool.DB.Meta;

namespace WellTool.DB.Tests
{
    /// <summary>
    /// MetaUtilTest
    /// </summary>
    public class MetaUtilTest
    {
        [Fact]
        public void TestMetaUtilCreation()
        {
            // 测试创建 MetaUtil 实例
            var metaUtil = MetaUtil.Create();
            Assert.NotNull(metaUtil);
        }

        [Fact]
        public void TestMetaUtilWithDataSource()
        {
            // 测试使用数据源创建 MetaUtil 实例
            // 这里使用空实现，实际使用时需要替换为真实的数据源
            var dataSource = new System.Data.Common.DbConnectionStringBuilder();
            var metaUtil = MetaUtil.Create(dataSource.ToString());
            Assert.NotNull(metaUtil);
        }

        [Fact]
        public void TestMetaUtilGetTables()
        {
            // 测试获取表信息
            var metaUtil = MetaUtil.Create();
            var tables = metaUtil.GetTables();
            Assert.NotNull(tables);
        }

        [Fact]
        public void TestMetaUtilGetColumns()
        {
            // 测试获取列信息
            var metaUtil = MetaUtil.Create();
            var columns = metaUtil.GetColumns("users");
            Assert.NotNull(columns);
        }
    }
}

