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
using WellTool.DB.Ds;

namespace WellTool.DB.Tests
{
    /// <summary>
    /// IssueI70J95Test
    /// </summary>
    public class IssueI70J95Test
    {
        [Fact]
        public void TestDataSourceWrapperCreation()
        {
            // 测试创建数据源包装器
            var connectionString = "Server=localhost;Database=test;User Id=sa;Password=password;";
            var wrapper = DataSourceWrapper.Create(connectionString);
            Assert.NotNull(wrapper);
        }

        [Fact]
        public void TestDataSourceWrapperGetConnection()
        {
            // 测试获取数据库连接
            var connectionString = "Server=localhost;Database=test;User Id=sa;Password=password;";
            var wrapper = DataSourceWrapper.Create(connectionString);
            var connection = wrapper.GetConnection();
            Assert.NotNull(connection);
        }

        [Fact]
        public void TestDataSourceWrapperWithOptions()
        {
            // 测试使用选项创建数据源包装器
            var connectionString = "Server=localhost;Database=test;User Id=sa;Password=password;";
            var options = new DataSourceOptions();
            var wrapper = DataSourceWrapper.Create(connectionString, options);
            Assert.NotNull(wrapper);
        }
    }
}

